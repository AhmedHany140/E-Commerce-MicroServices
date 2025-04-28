
using ecommerce.shared.Logs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using OrderApi.Application.Services;
using Polly;
using Polly.Retry;

namespace OrderApi.Application.DepenanceInjection
{
	public static class MyServicContainer
	{
		public static IServiceCollection AddAplicationService(this IServiceCollection services,IConfiguration configuration)
		{
			//Register HttpClient

			services.AddHttpClient<IOrderService,OrderService>(options =>
			{
				options.BaseAddress = new Uri(configuration["ApiGateway:BaseAddress"]!);
				options.Timeout = TimeSpan.FromSeconds(1); 
			});


	

			//Retry Strategy options
			var retrystartegy = new RetryStrategyOptions()
			{
				ShouldHandle = new PredicateBuilder().Handle<TaskCanceledException>(),
				BackoffType = DelayBackoffType.Constant,
				UseJitter = true,
				MaxRetryAttempts = 10,
				Delay = TimeSpan.FromSeconds(10),
				OnRetry = args =>
				{
					string message = $"OnRetry, Attemps = {args.AttemptNumber}  OutCome = {args.Outcome}";
					LogExeption.LogToConsole(message);
					LogExeption.LogToDebugger(message);

					return ValueTask.CompletedTask;
				}
			};

			//use Retry Strategy
			services.AddResiliencePipeline("my-retry-pipline", builder =>
			{
				builder.AddRetry(retrystartegy);
			});

			
			return services;
		}
	}
}

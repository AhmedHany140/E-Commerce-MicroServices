

using ecommerce.shared.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using System.Runtime.CompilerServices;

namespace ecommerce.shared.DependanceInjection
{
	public static class SharedServiceContainer
	{
		public static IServiceCollection AddSharedServices<TContext>(this IServiceCollection services
			, IConfiguration configuration, string filename) where TContext : DbContext
		{
			services.AddDbContext<TContext>(options =>

				options.UseSqlServer(configuration.GetConnectionString("eCommerceConnection"), sqlserveroptions =>
				sqlserveroptions.EnableRetryOnFailure())
			);

	
			//cofiguration log

			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Information()
				.WriteTo.Debug()
				.WriteTo.Console()
				.WriteTo.File(path: $"{filename}-.txt",//example filename-2025-04-07.txt
				restrictedToMinimumLevel:LogEventLevel.Information,
				outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exeption}"
				, rollingInterval: RollingInterval.Day//every day log file new log file created
				).CreateLogger();

			//add JWT Authintecation Schema
			JWTAuthenticationSchema.AddJWTAurhenticationSchema(services,configuration);
			return services;
		}

		public static IApplicationBuilder UseSharedPolices(this IApplicationBuilder app )
		{
			//use Global Middelware
			app.UseMiddleware<GlobalExeptionMiddleware>();

			//register middleware to block outside APIs calls 
			app.UseMiddleware<listenToApiMiddleWare>();

			return app;
		}
	}
}

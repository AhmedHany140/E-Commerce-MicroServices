using ecommerce.shared.DependanceInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderApi.Application.DepenanceInjection;
using OrderApi.Application.Interface;
using OrderApi.Application.Services;
using OrderAPI.Infrastructure.Data;
using OrderAPI.Infrastructure.Reosatory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderAPI.Infrastructure.DependanceInjection
{
	public static class ServiceContainer
	{
		public  static  IServiceCollection AddInfrastructureServie(this IServiceCollection services,IConfiguration configuration)
		{
			SharedServiceContainer.AddSharedServices<OrderDbContext>(services, configuration, configuration["MySerilog:FileName"]);

			services.AddScoped<IOrder, OrderReposatory>();

			MyServicContainer.AddAplicationService(services,configuration);


			return services;
		}

		public  static IApplicationBuilder UseInfrastructurePolice(this IApplicationBuilder builder)
		{
			SharedServiceContainer.UseSharedPolices(builder);

			return builder;
		}
	}
}

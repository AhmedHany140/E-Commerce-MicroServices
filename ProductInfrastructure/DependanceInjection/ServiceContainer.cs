using ecommerce.shared.DependanceInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductApi.Application.Interface;
using ProductApi.Infrastructure.Data;
using ProductApi.Infrastructure.Reposatories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Infrastructure.DependanceInjection
{
	public static class ServiceContainer
	{
		public static  IServiceCollection AddInfrastructureService(this IServiceCollection services,IConfiguration configuration)
		{
			//add database connection & authentication Schema

			 SharedServiceContainer.AddSharedServices<ProductDbContext>
				(services, configuration, configuration["MySerilog:FileName"]!);

			services.AddScoped<IProduct, ProductReposatory>();



			return services;


		}

		public static   IApplicationBuilder UseInfrastructurePolice(this IApplicationBuilder app)
		{
			//use global Exeption Middleware 
			//use middleware to block outside APIs calls 

			SharedServiceContainer.UseSharedPolices(app);

			return app;
		}
	}
}

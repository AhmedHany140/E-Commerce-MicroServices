using Application.Domain.Entities;
using ApplicationApi.Infrastructure.Data;
using ApplicationApi.Infrastructure.Reposatory;
using AuthenticationApi.Application.Interfaces;
using ecommerce.shared.DependanceInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationApi.Infrastructure.DependanceInjection
{
	public static class ServiceContainer
	{
		public static IServiceCollection AddInfrastructureService(this IServiceCollection services,IConfiguration configuration)
		{
			//add Dbcontext & authintecationschema

			SharedServiceContainer.AddSharedServices<AuthenticationDbContext>(services, configuration, "MySerilog:FileName");


			services.AddIdentity<AppUser, IdentityRole>()
	      .AddEntityFrameworkStores<AuthenticationDbContext>();

			services.AddScoped<IUser, UserReposatory>();

			return services;
		}


		public static IApplicationBuilder userInfrastructurePolicy(this IApplicationBuilder app)
		{
			SharedServiceContainer.UseSharedPolices(app);

			return app;
		}
	}
}

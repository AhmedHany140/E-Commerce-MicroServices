using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.shared.DependanceInjection
{
	public static class JWTAuthenticationSchema
	{
		public static IServiceCollection AddJWTAurhenticationSchema(this IServiceCollection services,IConfiguration configuration)
		{
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
		 
		      {
				var key = Encoding.UTF8.GetBytes(configuration.GetSection("Authentication:Key").Value!);
				string issuer = configuration.GetSection("Authentication:issuer").Value!;
				string audience = configuration.GetSection("Authentication:audience").Value!;

				options.SaveToken = true;
				options.RequireHttpsMetadata = false;
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,

					ValidIssuer = issuer,
					ValidAudience = audience,
					IssuerSigningKey = new SymmetricSecurityKey(key),

				};

				});

			return services;
		}
	}
}

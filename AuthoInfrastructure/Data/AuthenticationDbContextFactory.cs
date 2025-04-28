using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ApplicationApi.Infrastructure.Data
{
	public class AuthenticationDbContextFactory : IDesignTimeDbContextFactory<AuthenticationDbContext>
	{
		public AuthenticationDbContext CreateDbContext(string[] args)
		{
			IConfigurationRoot configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory()) // important!
				.AddJsonFile("appsettings.json")
				.Build();

			var builder = new DbContextOptionsBuilder<AuthenticationDbContext>();
			var connectionString = configuration.GetConnectionString("eCommerceConnection");

			builder.UseSqlServer(connectionString);

			return new AuthenticationDbContext(builder.Options);
		}
	}
}

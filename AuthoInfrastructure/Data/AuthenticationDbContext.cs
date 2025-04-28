using Application.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationApi.Infrastructure.Data
{
	public class AuthenticationDbContext : IdentityDbContext<AppUser>
	{
		public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options):base(options)
		{
			
		}
	}
}

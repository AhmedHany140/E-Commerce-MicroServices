using Microsoft.EntityFrameworkCore;
using OrderAPiDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderAPI.Infrastructure.Data
{
	public class OrderDbContext(DbContextOptions<OrderDbContext> options):DbContext(options)
	{
		public DbSet<Order> Orders { get; set; }
	}
}

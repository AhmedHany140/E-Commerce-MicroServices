using ecommerce.shared.Logs;
using ecommerce.shared.Responses;
using Microsoft.EntityFrameworkCore;
using OrderApi.Application.Interface;
using OrderAPI.Infrastructure.Data;
using OrderAPiDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrderAPI.Infrastructure.Reosatory
{
	public class OrderReposatory(OrderDbContext context) : IOrder
	{
		public async Task<Response> CreateAsync(Order entity)
		{
			try
			{
				var order = await context.Orders.AddAsync(entity);

				await context.SaveChangesAsync();

				return order.Entity is not null ? new Response(true, "order created successfully")
					: new Response(message: "Error Happen when create this order  ");

			}
			catch(Exception ex)
			{
				LogExeption.LogExeptions(ex);

				return new Response(message: "Error Haapen when create ");
			}
		}

		public async Task<Response> DeleteeAsync(Order entity)
		{
			try
			{
				var order = await FindByIdeAsync(entity.Id);
				if (order is null)
					return new Response(message: "Order Not found");

				context.Orders.Remove(order);
				await context.SaveChangesAsync();

				return new Response(true, "Order Deleted Succssfully");
			}
			catch (Exception ex)
			{
				LogExeption.LogExeptions(ex);

				return new Response(message: "Error Haapen when Delete order ");
			}
		}

		public async Task<Order> FindByIdeAsync(int id)
		{
			try
			{
				var order = await context.Orders.FindAsync(id);

				return order is not null ? order : null!;
			}
			catch (Exception ex)
			{
				LogExeption.LogExeptions(ex);

				throw new Exception("Error Happen when get Order");
			}
		}

		public async Task<IEnumerable<Order>> GetAllAsync()
		{
			try
			{
				var orders =await context.Orders.ToListAsync();

				return orders is not null ? orders : null!;
			}
			catch (Exception ex)
			{
				LogExeption.LogExeptions(ex);

				throw new Exception(message: "Error Haapen when Get all Orders ");
			}
		}

		public async Task<Order> GetByAsync(Expression<Func<Order, bool>> expression)
		{
			try
			{
				var order = await context.Orders.Where(expression).FirstOrDefaultAsync();

				return order is not null ? order : null!;
			}
			catch (Exception ex)
			{
				LogExeption.LogExeptions(ex);

				throw new Exception(message: "Error Haapen when create ");
			}
		}

		public async Task<IEnumerable<Order>> GetordersAsync(Expression<Func<Order, bool>> expression)
		{
			try
			{
				var orders = await context.Orders.Where(expression).ToListAsync();

				return orders is not null ? orders : null!;
			}
			catch (Exception ex)
			{
				LogExeption.LogExeptions(ex);

				throw new Exception(message: "Error Haapen when create ");
			}
		}

		public async Task<Response> UpdateAsync(Order entity)
		{
			try
			{
				var order = await FindByIdeAsync(entity.Id);

				if (order is null)
				{
					new Response(message: "Order Not Found");
				
				}

			    context.Entry(order).State = EntityState.Detached;

				context.Orders.Update(entity);
				await context.SaveChangesAsync();

				return new Response(true, "Update Order Seuccess");
			}
			catch (Exception ex)
			{
				LogExeption.LogExeptions(ex);

				return new Response(message: "Error Haapen when create ");
			}
		}
	}
}

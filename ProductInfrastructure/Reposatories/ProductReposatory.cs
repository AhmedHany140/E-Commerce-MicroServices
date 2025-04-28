using ecommerce.shared.Logs;

using ecommerce.shared.Responses;
using Microsoft.EntityFrameworkCore;
using ProductApi.Application.Interface;
using ProductApi.Domain.Entities;
using ProductApi.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Infrastructure.Reposatories
{

	public class ProductReposatory(ProductDbContext context) : IProduct
	{
		public async Task<Response> CreateAsync(Product entity)
		{
			try
			{
				//if product already exists 
				var getproduct = await GetByAsync(_ => _.Name.Equals(entity.Name));

				if(getproduct is not null && string.IsNullOrEmpty(getproduct.Name))
				{
					return new Response(message: $"{entity.Name} Already Exists ");
				}

				var currententity = await context.Products.AddAsync(entity);
				await context.SaveChangesAsync();

				if(currententity is not null && currententity.Entity.Id>0)
				{
					return new Response(true, $"{entity.Name} Added Sucessfully");
				}
				else
				{
					return new Response(message: $"Error Occur While Added {entity.Name}");
				}
					
			}
			catch(Exception ex)
			{
				//Log to original Exeption
				LogExeption.LogExeptions(ex);

				//Display Message To client
				return  new Response(false, "Error Occur ,When adding new Product");
			}
		}

		public async Task<Response> DeleteeAsync(Product entity)
		{
			try
			{
				var product = await FindByIdeAsync(entity.Id);

				if(product is  null )
				{
					return new Response(message: $"Error Occur when deleting Product");
					
				}

				context.Products.Remove(product);
				await context.SaveChangesAsync();
				return new Response(true, $"{entity.Name} Deleted Successfully");
			}
			catch (Exception ex)
			{
				//Log to original Exeption
				LogExeption.LogExeptions(ex);

				//Display Message To client
				return new Response(false, "Error Occur ,When Deleting  Product");
			}
		}

		public async Task<Product> FindByIdeAsync(int id)
		{
			try
			{
				var product = await context.Products.FindAsync(id);

				return product is not null ? product : null!;
			}
			catch (Exception ex)
			{
				//Log to original Exeption
				LogExeption.LogExeptions(ex);

				//Display Message To client
				throw new Exception("Error Occur ,When Retriving  Product");
			}
		}

		public async Task<IEnumerable<Product>> GetAllAsync()
		{
			try
			{
				var products = await context.Products.AsNoTracking().ToListAsync();

				return products is not null ? products : null!;
			}
			catch (Exception ex)
			{
				//Log to original Exeption
				LogExeption.LogExeptions(ex);

				//Display Message To client
				throw new Exception("Error Occur ,When Retriving  Products");
			}
		}

		public async Task<Product> GetByAsync(Expression<Func<Product,bool>> expression)
		{
			
			try
			{
				var product = await context.Products.Where(expression).FirstOrDefaultAsync();

				return product is not null ? product : null!;
			}
			catch (Exception ex)
			{
				//Log to original Exeption
				LogExeption.LogExeptions(ex);

				//Display Message To client
				throw new Exception("Error Occur ,When Retriving  Products");
			}
		}

		public async Task<Response> UpdateAsync(Product entity)
		{
			try
			{
				var product = await FindByIdeAsync(entity.Id);
				if (product is not null)
				{
					
					context.Entry(product).State = EntityState.Detached;
					context.Products.Update(entity);
					await context.SaveChangesAsync();
					return new Response(true, "Updated Successfully");
				}
				else
				{
					return new Response(message: $"Error happen {entity.Name} Not Exists to Uppdate it ");
				}
			}
			catch (Exception ex)
			{
				//Log to original Exeption
				LogExeption.LogExeptions(ex);

				//Display Message To client
				return new Response(false, "Error Occur ,When Updating new Product");
			}
		}
	}
}

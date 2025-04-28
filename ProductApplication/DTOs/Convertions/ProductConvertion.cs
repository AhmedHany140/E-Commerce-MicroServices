using ProductApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Application.DTOs.Convertions
{
	public static class ProductConvertion
	{
		public static Product ToEntity(ProductDto productDto) => new()
		{
	        Id=productDto.Id,
			 Name=productDto.Name,
			 Price=productDto.Price,
		     Quentity=productDto.Quentity
			 
		};


		public static (ProductDto? ,IEnumerable<ProductDto?>) FromEntity(Product? product,IEnumerable<Product>? products)
		{
            //return single
		    if(product is not null && products is null)
			{
				var singlepropuct = new ProductDto(product.Id,
					product!.Name!, product!.Quentity, product!.Price);

				return (singlepropuct!, null!);
			}

			//return list
			if(products is not null && product is null)
			{

				var productlist = products.Select(pr => 

					new ProductDto(pr.Id, pr.Name!, pr.Quentity, pr.Price)
				
				).ToList();

				return (null, productlist);
				
			}

			return (null!, null!);
		}
	}
}

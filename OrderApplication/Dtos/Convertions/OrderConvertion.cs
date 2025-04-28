using OrderAPiDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Application.Dtos.Convertions
{
	public static class OrderConvertion
	{
		public static Order ToEntity(OrderDto OrderDto) => new()
		{
	       Id=OrderDto.Id,
			ProductId = OrderDto.Productid,
			ClientId = OrderDto.Clientid,
			PurchaseQuentity=OrderDto.Quentity,
			OrderDate = OrderDto.orderdate
		};

		public static (OrderDto? ,IEnumerable<OrderDto>?) FromEntity(Order? order,IEnumerable<Order>? orders)
		{
			if(order is not null && orders is null)
			{
				var singalorder = new OrderDto(order.Id,
					 order.ProductId, 
					order.ClientId,order.PurchaseQuentity, order.OrderDate
					);

				return (singalorder, null!);

			}

			if (orders is not null && order is null)
			{
				var orderlist = orders.Select(o =>

					new OrderDto(o.Id, o.ProductId, o.ClientId, o.PurchaseQuentity, o.OrderDate)

				).ToList();

				return (null!, orderlist);

			}


			return (null!, null!);
		}
	}
}

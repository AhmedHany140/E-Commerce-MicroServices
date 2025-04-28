using OrderApi.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Application.Services
{
	public interface IOrderService
	{
		Task<OrderDetails> GetOrderDetails(int id);
		Task<IEnumerable<OrderDto>> GetOrdersByClientId(int id);
	}
}

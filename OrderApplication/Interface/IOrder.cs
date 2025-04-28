using ecommerce.shared.Interface;
using OrderAPiDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Application.Interface
{
	public interface IOrder : IGenaricInterface<Order>
	{
		Task<IEnumerable<Order>> GetordersAsync(Expression<Func<Order,bool>> expression);
	}
	
}

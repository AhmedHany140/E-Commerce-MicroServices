using ecommerce.shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Application.Dtos;
using OrderApi.Application.Dtos.Convertions;
using OrderApi.Application.Interface;
using OrderApi.Application.Services;

namespace OrderAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class OrderController (IOrder orderrepo,IOrderService orderService): ControllerBase
	{

		[HttpGet]
		public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
		{
			var orders =await orderrepo.GetAllAsync();

			if (!orders.Any())
				return NotFound("No Orders Founded");

			var (_,list) = OrderConvertion.FromEntity(null!, orders);

			return list!.Any() ? Ok(list) : NotFound("No Orders Founded");
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<OrderDto>> GetOrderbyId(int id)
		{
			var order = await orderrepo.FindByIdeAsync(id);

			if (order is null)
				return NotFound("Order Not Found");

			var (o, _) = OrderConvertion.FromEntity(order, null!);

			return o is not null ? Ok(o) : NotFound("Order Not Found");
		}

		[HttpPost]
		public async Task<ActionResult<Response>> CreateOrder(OrderDto orderDto)
		{
			if (!ModelState.IsValid)
				return BadRequest("incomplement data ");

			var order = OrderConvertion.ToEntity(orderDto);

			var response=await orderrepo.CreateAsync(order);

			return response.flag is true ? Ok(response) : BadRequest(response);
		}


		[HttpPut]
		public async Task<ActionResult<Response>> UpdateOrder(OrderDto orderDto)
		{
			if (!ModelState.IsValid)
				return BadRequest("incomplement data ");

			var order = OrderConvertion.ToEntity(orderDto);


			var response = await orderrepo.UpdateAsync(order);

			return response.flag is true ? Ok(response) : BadRequest("Can't Update Order");
		}

		[HttpDelete]
		public async Task<ActionResult<Response>> DeleteOrder(OrderDto orderDto)
		{
			if (!ModelState.IsValid)
				return BadRequest("incomplement data ");

			var order = OrderConvertion.ToEntity(orderDto);


			var response = await orderrepo.DeleteeAsync(order);

			return response.flag  ? Ok(response) : BadRequest("Can't delete Order");
		}

		[HttpGet("client/{ClientId:int}")]
		public async Task<ActionResult<IEnumerable<OrderDto>>> GetClientOrders(int ClientId)

		{
			if (ClientId <= 0)
				return BadRequest("client not found");

			var orders = await orderService.GetOrdersByClientId( ClientId);

			return orders is null ? BadRequest("orders not found") : Ok(orders); 
		}

		[HttpGet("Details/{orderid:int}")]
		public async Task<ActionResult<OrderDetails>> GetOrderDetails(int orderid)
		{
			if(orderid<=0)
				return BadRequest("order not found");

			var orderdetails = await orderService.GetOrderDetails(orderid);

			return orderdetails is not null ? Ok(orderdetails) : NotFound("order not found");
		}

	}
}

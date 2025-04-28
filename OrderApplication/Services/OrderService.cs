using ecommerce.shared.Logs;
using OrderApi.Application.Dtos;
using OrderApi.Application.Dtos.Convertions;
using OrderApi.Application.Interface;
using Polly;
using Polly.Registry;
using ProductApi.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Application.Services
{
	public class OrderService(HttpClient httpClient,
		ResiliencePipelineProvider<string> resiliencePipeline,IOrder OrderRepo) : IOrderService
	{
		public async Task<ProductDto> GetProduct(int Productid) 
		{
			var getproduct = await httpClient.GetAsync($"/api/Product/{Productid}");

			if (!getproduct.IsSuccessStatusCode)
				return null!;

			var product =await getproduct.Content.ReadFromJsonAsync<ProductDto>();

			return product!;
		}

		public async Task<AppUserDto> GetUser(int userid)
		{
			var getuser = await httpClient.GetAsync($"/api/Authentication/{userid}");

			if (!getuser.IsSuccessStatusCode)
				return null!;

			var user = await getuser.Content.ReadFromJsonAsync<AppUserDto>();

			return user!;
		}
		public async Task<OrderDetails> GetOrderDetails(int id)
		{
			//get order
			var order = await OrderRepo.FindByIdeAsync(id);

			if (order is null || order.Id < 0)
				return null!;

			//get Retry Pipleline 
			var retrypipline = resiliencePipeline.GetPipeline("my-retry-pipline");

			//Prepare Product
			var product =await retrypipline.ExecuteAsync(async token => await GetProduct(order.ProductId));

			//prepare Client
			var Client =await retrypipline.ExecuteAsync(async token => await GetUser(order.ClientId));

			return new OrderDetails(order.Id,product.Id,Client.Id,Client.Name,
				Client.Email,Client.Address,product.Name,
				order.PurchaseQuentity,Client.PhoneNumber,product.Price,
				(product.Price * order.PurchaseQuentity),
				order.OrderDate
			);
		}

		public async Task<IEnumerable<OrderDto>> GetOrdersByClientId(int id)
		{
			var orders = await OrderRepo.GetordersAsync(o=>o.ClientId==id);

			if (!orders.Any()) return null!;

			var (_, list) =  OrderConvertion.FromEntity(null, orders);

			return list is not null ? list : null!;
		}
	}
}

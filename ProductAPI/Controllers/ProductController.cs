
using ecommerce.shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Application.DTOs;
using ProductApi.Application.DTOs.Convertions;
using ProductApi.Application.Interface;
using ProductApi.Domain.Entities;

namespace ProductAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController(IProduct productrepo) : ControllerBase
	{
		[HttpGet]
		[AllowAnonymous]
		public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
		{
			var products = await productrepo.GetAllAsync();

			if (!products.Any())
				return NotFound("No Products Detected from Database");

			var (_, list) = ProductConvertion.FromEntity(null, products);

			return list.Any() ? Ok(list) : NotFound("No Products Founded");

		}

		[HttpGet("{id:int}")]
		[AllowAnonymous]
		public async Task<ActionResult<ProductDto>> GetProduct(int id)
		{
			var product = await productrepo.FindByIdeAsync(id);

			if (product is  null)
				return NotFound("No Product Detected from Database");

			var (p, _) = ProductConvertion.FromEntity(product, null);

			return p is not null ? Ok(p) : NotFound("No Product Founded");
		}

		[HttpPost]
		[Authorize(Roles ="Admin")]
		public async Task<ActionResult<Response>> AddProduct(ProductDto productDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var product = ProductConvertion.ToEntity(productDto);
			var response = await productrepo.CreateAsync(product);

			return response.flag ? Ok(response) : BadRequest(response);


		}

		[HttpPut]
		[Authorize(Roles = "Admin")]
		public async Task<ActionResult<Response>> UpdateProduct(ProductDto productDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var product = ProductConvertion.ToEntity(productDto);
			var response = await productrepo.UpdateAsync(product);

			return response.flag  ? Ok(response) : BadRequest(response);


		}

		[HttpDelete]
		[Authorize(Roles = "Admin")]
		public async Task<ActionResult<Response>> DeleteProduct(ProductDto productDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var product = ProductConvertion.ToEntity(productDto);
			var response = await productrepo.DeleteeAsync(product);

			return response.flag  ? Ok(response) : BadRequest(response);


		}
	}
}

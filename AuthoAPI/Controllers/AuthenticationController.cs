using AuthenticationApi.Application.Dtos;
using AuthenticationApi.Application.Interfaces;
using ecommerce.shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[AllowAnonymous]
	public class AuthenticationController(IUser userrepo) : ControllerBase
	{


		[HttpPost("Register")]
		public async Task<ActionResult<Response>> Register(AppUseerDto useerDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await userrepo.Register(useerDto);

			return result.flag is true ? Ok(result) : NotFound(result);
		}


		[HttpPost("Login")]
		public async Task<ActionResult<Response>> Login(LoginDto loginDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await userrepo.Login(loginDto);

			return result.flag is true ? Ok(result) : NotFound(result);
		}


		[HttpGet("{Email}")]
		[Authorize]
		public async Task<ActionResult<Response>> GetUserbyEmail(string Email)
		{

			var result = await userrepo.GetUser(Email);

			return result is not null ? Ok(result) : NotFound("User Not NotFound");

		}
	}
}

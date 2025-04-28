using AuthenticationApi.Application.Dtos;
using AuthenticationApi.Application.Interfaces;
using AuthoAPI.OTPService.Interface;
using AuthoInfrastructure.OTPService;
using ecommerce.shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[AllowAnonymous]
	public class AuthenticationController(IUser userrepo, OtpService otpService, IEmailService emailService) : ControllerBase
	{

		private readonly OtpService _otpService=otpService;
		private readonly IEmailService _emailService=emailService;

	
		[HttpPost("Register")]
		public async Task<ActionResult<Response>> Register(AppUseerDto useerDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await userrepo.Register(useerDto);

			return result.flag? Ok(result) : NotFound(result);
		}


		[HttpPost("Login")]
		public async Task<ActionResult<Response>> Login(LoginDto loginDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await userrepo.Login(loginDto);

			return result.flag  ? Ok(result) : NotFound(result);
		}


		[HttpGet("{Email}")]
		[Authorize]
		public async Task<ActionResult<Response>> GetUserbyEmail(string Email)
		{

			var result = await userrepo.GetUser(Email);

			return result is not null ? Ok(result) : NotFound("User Not NotFound");

		}




		[HttpPost("generate")]
		public async Task<IActionResult> GenerateOtp([FromBody] string request)
		{
			try
			{
				var otp = _otpService.GenerateOtp(request);

				await _emailService.SendEmailAsync(
					request,
					"Your OTP Code",
					$"Your OTP is: {otp}"
				);

				return Ok(new { Message = "OTP sent successfully" });
			}
			catch (Exception ex)
			{
			
				return StatusCode(500, new
				{
					Message = "Failed to send OTP",
					Error = ex.Message
				});
			}
		}

		[HttpPost("verify")]
	      public IActionResult VerifyOtp([FromBody] OtpRequest request)
	      {
	      	var isValid = _otpService.ValidateOtp(request.Otp, request.EmailOrPhone);
	      
	      	if (!isValid)
	      	{
	      		return BadRequest(new { Message = "Invalid OTP" });
	      	}
	      
	      	return Ok(new { Message = "OTP verified successfully" });
	      }
		
	}
}

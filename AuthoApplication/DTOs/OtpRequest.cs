using System.ComponentModel.DataAnnotations;

namespace AuthenticationApi.Application.Dtos
{
	public record OtpRequest
	(
		[Required] string EmailOrPhone ,
		[Required] string Otp 
	);
}

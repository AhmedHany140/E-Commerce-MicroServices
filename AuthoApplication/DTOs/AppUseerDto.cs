using System.ComponentModel.DataAnnotations;
namespace AuthenticationApi.Application.Dtos
{
	public record AppUseerDto
	(
		int Id,
		[Required]string Name,
		[Required] string PhoneNumber,
		[Required ,EmailAddress] string Email,
		[Required] string Password,
		[Required] string Role
	);
}

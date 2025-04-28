using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationApi.Application.Dtos
{
	public record GetUserDto(
		string Id,
		[Required]string Name,
		[Required] string PhoneNumber,
		[Required, EmailAddress] string Email,
		[Required] List<string> Roles
		);
}

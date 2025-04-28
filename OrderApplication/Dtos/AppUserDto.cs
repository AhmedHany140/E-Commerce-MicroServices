using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Application.Dtos
{
	public record  AppUserDto(
		int Id,
		[Required] string Name,
		[Required] string PhoneNumber,
		[Required] string Address,
		[Required,EmailAddress] string Email,
		[Required] string Password,
		[Required] string Role
		);
	
}

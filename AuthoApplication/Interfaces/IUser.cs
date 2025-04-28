using Application.Domain.Entities;
using AuthenticationApi.Application.Dtos;
using ecommerce.shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationApi.Application.Interfaces
{
	public interface IUser
	{
		Task<Response> Register(AppUseerDto user);
		Task<Response> Login(LoginDto login);

		Task<GetUserDto> GetUser(string Email);
	}
}

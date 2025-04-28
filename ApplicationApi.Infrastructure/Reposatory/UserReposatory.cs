

using Application.Domain.Entities;
using AuthenticationApi.Application.Dtos;
using AuthenticationApi.Application.Interfaces;
using ecommerce.shared.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApplicationApi.Infrastructure.Reposatory
{
	public class UserReposatory(UserManager<AppUser> usermanger, IConfiguration configuration, RoleManager<IdentityRole> roleManager) : IUser
	{
		public async Task<GetUserDto> GetUser(string Email)
		{
			var user = await usermanger.FindByEmailAsync(Email);

			var roles =await usermanger.GetRolesAsync(user);

			List<string> userroles = new List<string>();

			if(roles is not null)
			userroles.AddRange(roles);
			
			

			return user is null ? null! : new GetUserDto(user.Id, user.UserName!, user.PhoneNumber!, user.Email!,userroles);
		}

		public async Task<Response> Login(LoginDto login)
		{
			if (login is null)
				return new Response(message: "invalaid Data send");

			var userfromDb = await usermanger.FindByEmailAsync(login.Email);

			if (userfromDb is null)
				return new Response(message: "User Not Found");

			var correctpassword = await usermanger.CheckPasswordAsync(userfromDb, login.Password);

			if (!correctpassword)
				return new Response(message: " Uncorrect Password");

			SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:Key"]!));


			SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			List<Claim> claims = new List<Claim>();
			claims.Add(new Claim(ClaimTypes.NameIdentifier, userfromDb.Id));
			claims.Add(new Claim(ClaimTypes.Name, userfromDb.UserName!));


			IList<string> roles = await usermanger.GetRolesAsync(userfromDb);

			claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
			

			JwtSecurityToken securityToken = new JwtSecurityToken(
				issuer: configuration["Authentication:issuer"],
				audience: configuration["Authentication:audience"],
				expires: DateTime.UtcNow.AddDays(30),
				claims: claims,
				signingCredentials: signingCredentials
				);


			var token = new JwtSecurityTokenHandler().WriteToken(securityToken);



			return new Response(true, token);
		}

		public async Task<Response> Register(AppUseerDto user)
		{
			if (user is null)
				return new Response(message:"invalide Data");

			var User = new AppUser
			{
				Email = user.Email,
				UserName = user.Name,
				PhoneNumber = user.PhoneNumber,

			};


			var result = await usermanger.CreateAsync(User, user.Password);

			if (!result.Succeeded)
				return new Response(message: string.Join(" | ", result.Errors.Select(e => e.Description)));

		
			if (!await roleManager.RoleExistsAsync(user.Role))
			{
				await roleManager.CreateAsync(new IdentityRole(user.Role));
			}

	
			await usermanger.AddToRoleAsync(User, user.Role);



			return new Response(true, message: "Acount Created Success");

		}
	}
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.shared.Middleware
{
	public class listenToApiMiddleWare(RequestDelegate next)
	{
		public async Task InvokeAsync(HttpContext context)
		{
			//Extract Specific header from request
			var SignedHeader = context.Request.Headers["Api-Geteway"];

			//check if request comes from Geteway
			if(SignedHeader.FirstOrDefault()is null)
			{
				context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
				await context.Response.WriteAsync("Sorry,Service Unavailable");
				return;
			}
			else
			{
				await next(context);
			}


		}
	}
}

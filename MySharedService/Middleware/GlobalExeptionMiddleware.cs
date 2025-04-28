using ecommerce.shared.Logs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ecommerce.shared.Middleware
{
	public class GlobalExeptionMiddleware(RequestDelegate next)
	{
        public async Task InvokeAsync(HttpContext context)
		{
			string message = "Sorry,Internal Server error occur ,kindly try again.. ";
			int statuscode =(int) HttpStatusCode.InternalServerError;//500
			string title = "Error";

			try
			{
				await next(context);

				//check if too many request 429
				if(context.Response.StatusCode==StatusCodes.Status429TooManyRequests)
				{
					title = "Warning";
					message = "Too Many Requests acts";
					statuscode = StatusCodes.Status429TooManyRequests;

					 await ModifyHeader(context,title, message, statuscode);
				}

				//check if unauthorize 401
				if(context.Response.StatusCode==StatusCodes.Status401Unauthorized)
				{
					title = "Alert";
					message = "You a re unauthorize";
					statuscode = StatusCodes.Status401Unauthorized;
					await ModifyHeader(context, title, message, statuscode);

				}

				//check about forbidden response 403
				if(context.Response.StatusCode==StatusCodes.Status403Forbidden)
				{
					title = "Out of access";
					message = "not allowed to access ";
					statuscode = StatusCodes.Status403Forbidden;
					await ModifyHeader(context, title, message, statuscode);
				}
			}
			catch(Exception ex)
			{
				//Log Exeption to Console / Debbuger / File
				LogExeption.LogExeptions(ex);

				//Handle Exeption Time Out 408

				if(ex is TaskCanceledException || ex is TimeoutException)
				{
					title = "Out of Time";
					message = "Request Timeout ,Try Again";
					statuscode = StatusCodes.Status408RequestTimeout;

				}
				//if exeption acts do this
				//if exeption un acts do the default
				await ModifyHeader(context, title, message, statuscode);

			}

		}

		private async Task ModifyHeader(HttpContext context, string title, string message, int statuscode)
		{
			//scary message to client
			context.Response.ContentType = "application/json";
			await context.Response.WriteAsync(JsonSerializer.Serialize(new ProblemDetails
			{
              Detail=message,
			  Title=title,
			  Status=statuscode,
			  
			  
			}),CancellationToken.None);
			return;
		}
	}
	
}

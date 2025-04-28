namespace ApiGateway.Midlleware
{
	public class AttachSignatureTorequesteMiddleware(RequestDelegate next)
	{
        public async Task InvokeAsync(HttpContext context)
		{
			context.Request.Headers["Api-Geteway"] = "Signed";

		  await	next(context);
		}
	}
}


using ApiGateway.Midlleware;
using ecommerce.shared.DependanceInjection;
using Microsoft.AspNetCore.Http.HttpResults;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

    var builder = WebApplication.CreateBuilder(args);


    builder.Configuration.AddJsonFile("Ocelot.json", optional: false, reloadOnChange: true);

	builder.Services.AddOcelot().AddCacheManager(x => x.WithDictionaryHandle());

	JWTAuthenticationSchema.AddJWTAurhenticationSchema(builder.Services, builder.Configuration);

    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(builder =>
        {
            builder.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
        });
    });


	var app = builder.Build();
	app.UseCors();
    app.UseMiddleware<AttachSignatureTorequesteMiddleware>();
    app.UseOcelot().Wait();

    app.UseHttpsRedirection();
    app.Run();
	Console.WriteLine("app run");
		


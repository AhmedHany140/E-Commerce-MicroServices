
using OrderAPI.Infrastructure.DependanceInjection;
using System.Threading.Tasks;


	var builder = WebApplication.CreateBuilder(args);

	// Add services to the container.

	builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
	{
		options.SuppressModelStateInvalidFilter = true;
	});

	builder.Services.AddEndpointsApiExplorer();
	builder.Services.AddSwaggerGen();


	 builder.Services.AddInfrastructureServie(builder.Configuration);

	var app = builder.Build();

	app.UseInfrastructurePolice(); //here i use this middileware 

	// Configure the HTTP request pipeline.
	if (app.Environment.IsDevelopment())
	{
		app.UseSwagger();
		app.UseSwaggerUI();
	}

	app.UseHttpsRedirection();

	app.UseAuthorization();


	app.MapControllers();

	app.Run();
		
	


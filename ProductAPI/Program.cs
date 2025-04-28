
using ProductApi.Infrastructure.DependanceInjection;
using System.Threading.Tasks;

	
	var builder = WebApplication.CreateBuilder(args);

	// stop default filter

	builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
	{
		options.SuppressModelStateInvalidFilter = true;
	});

	builder.Services.AddEndpointsApiExplorer();
	builder.Services.AddSwaggerGen();

	builder.Services.AddInfrastructureService(builder.Configuration);

	var app = builder.Build();

	app.UseInfrastructurePolice();
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
		
	


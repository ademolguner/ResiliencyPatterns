using ResiliencyPatterns.SpinningWheels.Api;
using ResiliencyPatterns.SpinningWheels.Api.Services;
using ResiliencyPatterns.SpinningWheels.Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .DependencyRegistration(builder.Configuration);

var app = builder.Build();
app.UseSwagger()
    .UseSwaggerUI()
    .UseHttpsRedirection()
    .UseRouting()
    .UseEndpoints(endpoints => { endpoints.MapControllers(); });
app.Run();
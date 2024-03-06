using Polly;
using ResiliencyPatterns.Api.HttpClientServices;
using ResiliencyPatterns.Api.HttpClientServices.Interfaces;
using ResiliencyPatterns.Api.StrategyOptions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.AddControllers();


builder.Services.AddHttpClient<IBasketService, BasketService>()
    .SetHandlerLifetime(TimeSpan.FromMinutes(5)) //Set lifetime to five minutes
    .AddPolicyHandler(ResiliencyStrategies.GetRetryPolicy());


var app = builder.Build();
app.UseSwagger()
    .UseSwaggerUI()
    .UseHttpsRedirection()
    .UseRouting()
    .UseEndpoints(endpoints => { endpoints.MapControllers(); });
app.Run();
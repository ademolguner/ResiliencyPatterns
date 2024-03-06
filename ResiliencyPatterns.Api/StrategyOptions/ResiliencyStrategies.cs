using System.Net;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;

namespace ResiliencyPatterns.Api.StrategyOptions;

public static class ResiliencyStrategies
{
    private static readonly HttpStatusCode[] HttpStatusCodesWorthRetrying =
    [
        HttpStatusCode.RequestTimeout, // 408
        HttpStatusCode.InternalServerError, // 500
        HttpStatusCode.BadGateway, // 502
        HttpStatusCode.ServiceUnavailable, // 503
        HttpStatusCode.GatewayTimeout // 504
    ];

    
    
    public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
            .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        //.ExecuteAsync(async () => await new HttpClient().GetAsync(new Uri("api/status")));
    }
    
    // public void GetRetryStrategyOptions()
    // {
    //     var optionsOnRetry = new RetryStrategyOptions
    //     {
    //         MaxRetryAttempts = 2, 
    //         Delay = TimeSpan.FromMinutes(10), 
    //         MaxDelay = TimeSpan.FromMinutes(10), 
    //         BackoffType = DelayBackoffType.Linear
    //         //,OnRetry = arguments=>{}
    //         ,Name = ""
    //         //, ShouldHandle = arguments => 
    //         ,UseJitter = true
    //         //, DelayGenerator = arguments => 
    //         ,OnRetry = static args =>
    //         {
    //             Console.WriteLine("OnRetry, Attempt: {0}", args.AttemptNumber);
    //
    //             // Event handlers can be asynchronous; here, we return an empty ValueTask.
    //             return default;
    //         }
    //     };
    //
    //     var pipelineTry = new ResiliencePipelineBuilder()
    //         .AddRetry(optionsOnRetry)
    //         .Build();
    //
    //     //pipelineTry.Execute(YourClassOrAnyThink.DoOnTry);
    // }
    //
    // private RetryPolicy<HttpResponseMessage> SetRetryPolicy()
    // {
    //     var retryPolicy =  Policy
    //         .Handle<HttpRequestException>() // Belirli bir hatayı ele alır
    //         //.OrResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode) // Başarısız yanıtları ele alır
    //         .OrResult<HttpResponseMessage>(r => HttpStatusCodesWorthRetrying.Contains(r.StatusCode))
    //         .Retry(3, onRetry: (exception, retryCount) => // En fazla 3 tekrar deneme, ve her tekrarda bir olayı ele alır
    //         {
    //             Console.WriteLine($"Retry #{retryCount}");
    //         });
    //
    //     return retryPolicy;
    //     
    //
    // }
    //
    // public void ConfigureServices(IServiceCollection services)
    // {
    //     //var retryClient = new HttpClient(new RetryPolicyDelegatingHandler(SetRetryPolicy()));
    //
    //     var policy = Policy
    //         .Handle<HttpRequestException>() // Belirli bir hatayı ele alır
    //         //.OrResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode) // Başarısız yanıtları ele alır
    //         .OrResult<HttpResponseMessage>(r => HttpStatusCodesWorthRetrying.Contains(r.StatusCode))
    //         .RetryAsync(3, onRetry: (exception, retryCount) => // En fazla 3 tekrar deneme, ve her tekrarda bir olayı ele alır
    //         {
    //             Console.WriteLine($"Retry #{retryCount}");
    //         });
    //     
    //     services.AddControllers();        
    //     services.AddHttpClient("nameClient")
    //         .AddPolicyHandler(policy)
    //         //.AddTransientHttpErrorPolicy(p => p.RetryAsync(3))
    //         .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new[]
    //         {
    //             TimeSpan.FromSeconds(1),
    //             TimeSpan.FromSeconds(5),
    //             TimeSpan.FromSeconds(10)
    //         }));
    //      
    // }
}
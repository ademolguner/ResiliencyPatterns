using System.Net;
using Polly;
using Polly.Extensions.Http;

namespace ResiliencyPatterns.SpinningWheels.Api.StrategyOptions;

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
            .OrResult(r => HttpStatusCodesWorthRetrying.Contains(r.StatusCode))
            .WaitAndRetryAsync(
                retryCount : 6, 
                sleepDurationProvider: retryAttempt => 
                                       TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), //TimeSpan.FromSeconds(10)
                onRetryAsync: (exception, timespan, retryAttempt, context) =>
                {
                    Console.WriteLine($"Retry #{retryAttempt} due to {exception.Exception.Message}. " +
                                      $"Waiting for {timespan.TotalSeconds} seconds.");
                    return Task.CompletedTask;
                });
    }
}
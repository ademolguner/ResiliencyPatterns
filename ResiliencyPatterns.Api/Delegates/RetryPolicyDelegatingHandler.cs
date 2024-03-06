using Polly.Retry;

namespace ResiliencyPatterns.Api.Delegates;

public class RetryPolicyDelegatingHandler : DelegatingHandler
{
    private readonly RetryPolicy<HttpResponseMessage> _retryPolicy;

    public RetryPolicyDelegatingHandler(RetryPolicy<HttpResponseMessage> retryPolicy)
    {
        _retryPolicy = retryPolicy ?? throw new ArgumentNullException(nameof(retryPolicy));
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_retryPolicy.Execute(() => base.Send(request, cancellationToken)));
    }
}
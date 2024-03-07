using System.Net.Mime;
using ResiliencyPatterns.SpinningWheels.Api.Clients.Wallet.Implementations;
using ResiliencyPatterns.SpinningWheels.Api.Clients.Wallet.Interfaces;

namespace ResiliencyPatterns.SpinningWheels.Api.Clients.Wallet;

public  static class WalletClientExtensions
{
    private const string Accept = "Accept";
    private const string ClientBaseAddressKey = "ExternalServiceSettings:WalletClientOptions:BaseAddress";
    private const string ClientTimeoutKey = "ExternalServiceSettings:WalletClientOptions:Timeout";

    public static IHttpClientBuilder AddWalletClient(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddHttpClient<IWalletClient, WalletClient>(client =>
        {
            client.BaseAddress = new Uri(configuration.GetValue<string>(ClientBaseAddressKey) ?? string.Empty);
            //client.Timeout = TimeSpan.FromMilliseconds(configuration.GetValue<int>(ClientTimeoutKey));
            client.DefaultRequestHeaders.Add(Accept, MediaTypeNames.Application.Json);
        });
    }
}
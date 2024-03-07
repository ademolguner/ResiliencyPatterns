using ResiliencyPatterns.SpinningWheels.Api.Clients.Wallet.Interfaces;

namespace ResiliencyPatterns.SpinningWheels.Api.Clients.Wallet.Implementations;

public class WalletClient(HttpClient httpClient) : IWalletClient
{
    private readonly HttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

    public async Task<decimal> GetUserBalanceAsync(string userId, CancellationToken cancellationToken)
    {
        var path = $"api/wallet/balance?userId={userId}";
        var httpResponseMessage = await _httpClient.GetAsync($"{path}", HttpCompletionOption.ResponseContentRead, cancellationToken);
        var content = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);
        var response = Convert.ToDecimal(content);
        return response;
    }
}
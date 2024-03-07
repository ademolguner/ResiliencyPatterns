using ResiliencyPatterns.SpinningWheels.Api.Clients.Wallet.Interfaces;
using ResiliencyPatterns.SpinningWheels.Api.Services.Interfaces;

namespace ResiliencyPatterns.SpinningWheels.Api.Services;

public class UserService(IWalletClient walletClient) : IUserService
{
    private readonly IWalletClient _walletClient = walletClient ?? throw new ArgumentNullException(nameof(walletClient));

    public async Task<decimal> GetBalance(string userId, CancellationToken cancellationToken)
    {
        return await _walletClient.GetUserBalanceAsync(userId, cancellationToken);
    }
}
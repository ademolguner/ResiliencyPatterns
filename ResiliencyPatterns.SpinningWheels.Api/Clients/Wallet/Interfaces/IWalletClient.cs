namespace ResiliencyPatterns.SpinningWheels.Api.Clients.Wallet.Interfaces;

public interface IWalletClient
{
    Task<decimal> GetUserBalanceAsync(string userId, CancellationToken cancellationToken);
}
namespace ResiliencyPatterns.SpinningWheels.Api.Services.Interfaces;

public interface IUserService
{
    Task<decimal> GetBalance(string userId,CancellationToken cancellationToken);
}
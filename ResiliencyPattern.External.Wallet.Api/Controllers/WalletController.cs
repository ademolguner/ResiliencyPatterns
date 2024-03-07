using Microsoft.AspNetCore.Mvc;

namespace ResiliencyPattern.External.Wallet.Api.Controllers;

/// <inheritdoc />
[ApiController]
[Produces("application/json")]
[Route("api/wallet")]
public class WalletController : ControllerBase
{
    [HttpGet("balance")]
    public Task<IActionResult> GetBalanceAsync([FromQuery] string userId)
    {
        return Task.FromResult<IActionResult>(Ok(25));
    }
}
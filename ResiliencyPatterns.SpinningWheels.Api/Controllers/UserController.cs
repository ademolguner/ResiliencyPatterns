using Microsoft.AspNetCore.Mvc;
using ResiliencyPatterns.SpinningWheels.Api.Services.Interfaces;

namespace ResiliencyPatterns.SpinningWheels.Api.Controllers;

/// <inheritdoc />
[ApiController]
[Produces("application/json")]
[Route("api/user")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet("balance")]
    public async Task<IActionResult> GetBalanceAsync([FromQuery] string userId, CancellationToken cancellationToken)
    {
        var response = await userService.GetBalance(userId,cancellationToken);
        return Ok(response);
    }

}
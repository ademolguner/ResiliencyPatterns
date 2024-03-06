using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace ResiliencyPatterns.Api.Controllers;

/// <inheritdoc />
[ApiController]
[Produces("application/json")]
public class CommonController : ControllerBase
{
    
    [HttpGet]
    [Route("status")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public Task<IActionResult> StatusAsync()
    {
        return Task.FromResult<IActionResult>(Ok(true));
    }
}

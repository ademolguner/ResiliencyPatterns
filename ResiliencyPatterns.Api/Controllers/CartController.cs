using Microsoft.AspNetCore.Mvc;
using Polly.CircuitBreaker;
using ResiliencyPatterns.Api.HttpClientServices.Interfaces;

namespace ResiliencyPatterns.Api.Controllers;

/// <inheritdoc />
[ApiController]
[Produces("application/json")]
[Route("api/card")]
public class CartController(IBasketService basketService) : ControllerBase
{
    public async Task<IActionResult> Index()
    {
        try
        {
            var user =Guid.NewGuid();
            //Http requests using the Typed Client (Service Agent)
            var userBasketDto = await basketService.GetBasket(user);
            return Ok(userBasketDto);
        }
        catch (BrokenCircuitException)
        {
            // Catches error when Basket.api is in circuit-opened mode
            return Ok(HandleBrokenCircuitException());
        }
    }

    protected virtual string HandleBrokenCircuitException() => "Basket Service is inoperative, please try later on. (Business message due to Circuit-Breaker)";
}
using ResiliencyPatterns.Api.HttpClientServices.Interfaces;
using ResiliencyPatterns.Api.Models.Baskets;

namespace ResiliencyPatterns.Api.HttpClientServices;

public class BasketService:IBasketService
{
    public Task<UserBasketDto> GetBasket(Guid user)
    {
        throw new NotImplementedException();
    }
}
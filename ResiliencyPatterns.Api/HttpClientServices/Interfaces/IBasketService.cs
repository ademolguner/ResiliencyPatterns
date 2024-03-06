using ResiliencyPatterns.Api.Models.Baskets;

namespace ResiliencyPatterns.Api.HttpClientServices.Interfaces;

public interface IBasketService
{
    Task<UserBasketDto> GetBasket(Guid user);
}
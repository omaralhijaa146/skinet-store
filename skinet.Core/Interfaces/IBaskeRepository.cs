using skinet.Core.Entities;

namespace skinet.Core.Interfaces;

public interface IBaskeRepository
{
    
    public Task<CustomerBasket> GetBasketAsync(string basketId);
    public Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
    public Task<bool> DeleteBasketAsync(string basketId);
    
}
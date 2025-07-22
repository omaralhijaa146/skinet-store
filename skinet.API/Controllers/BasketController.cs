using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using skinet.API.Dtos;
using skinet.Core.Entities;
using skinet.Core.Interfaces;

namespace skinet.API.Controllers;

public class BasketController:BaseApiController
{
    private readonly IBaskeRepository _baskeRepository;
    private readonly IMapper _mapper;

    public BasketController(IBaskeRepository baskeRepository,IMapper mapper)
    {
        _baskeRepository = baskeRepository;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<ActionResult<CustomerBasket>> GetBasket([FromQuery] string id)
    {
        var basket = await _baskeRepository.GetBasketAsync(id);
        return Ok(basket??new CustomerBasket(id));
    }
    
    [HttpPost]
    public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
    {
        var customerBasket=_mapper.Map<CustomerBasketDto,CustomerBasket>(basket);
        var updatedBasket = await _baskeRepository.UpdateBasketAsync(customerBasket);
        return Ok(updatedBasket);
    }

    [HttpDelete]
    public async Task DeleteBasket([FromQuery] string id)
    {
        await _baskeRepository.DeleteBasketAsync(id);
    }
}
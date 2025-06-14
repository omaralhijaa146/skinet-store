using AutoMapper;
using skinet.API.Dtos;
using skinet.Core.Entities;

namespace skinet.API.Helpers;

public class MappingProfiles:Profile
{
    public MappingProfiles()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType.Name)).ForMember(dest=>dest.ProductBrand,opt=>opt.MapFrom(src=>src.ProductBrand.Name)).ForMember(dest=>dest.PictureUrl,opt=>opt.MapFrom<ProductUrlResolver>());
    }
}
﻿using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using skinet.Core.Entities;

namespace skinet.Core.Specifications;

public class ProductsWithTypesAndBrandsSpecification:BaseSpecification<Product>
{
    public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams):base(x=>
        (
            (string.IsNullOrEmpty(productParams.Search)|| x.Name.ToLower().Contains(productParams.Search))&&
            (!productParams.BrandId.HasValue|| x.ProductBrandId==productParams.BrandId)
            && (!productParams.TypeId.HasValue|| x.ProductTypeId==productParams.TypeId)
            )
    )
    {
        AddInclude(x => x.ProductType);
        AddInclude(x => x.ProductBrand);
        AddOrderBy(x => x.Name);
        ApplyPaging(productParams.PageSize,productParams.PageSize*(productParams.PageIndex-1));
        
        if (!string.IsNullOrEmpty(productParams.Sort))
        {
            
            switch (productParams.Sort)
            {
                case "priceAsc":
                    AddOrderBy(x => x.Price);
                    break;
                case "priceDesc":
                    AddOrderByDescending(x => x.Price);
                    break;
               default:
                   AddOrderBy(x => x.Name);
                   break;
            }    
        }
        
    }
    
    public ProductsWithTypesAndBrandsSpecification(int id) : base(x=>x.Id==id)
    {
        AddInclude(x => x.ProductType);
        AddInclude(x => x.ProductBrand);
    }
}
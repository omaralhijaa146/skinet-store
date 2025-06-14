﻿using System.Linq.Expressions;
using skinet.Core.Entities;

namespace skinet.Core.Specifications;

public class ProductsWithTypesAndBrandsSpecification:BaseSpecification<Product>
{
    public ProductsWithTypesAndBrandsSpecification()
    {
        AddInclude(x => x.ProductType);
        AddInclude(x => x.ProductBrand);
    }
    
    public ProductsWithTypesAndBrandsSpecification(int id) : base(x=>x.Id==id)
    {
        AddInclude(x => x.ProductType);
        AddInclude(x => x.ProductBrand);
    }
}
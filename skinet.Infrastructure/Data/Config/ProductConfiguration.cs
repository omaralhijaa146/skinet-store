﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using skinet.Core.Entities;

namespace skinet.Infrastructure.Data.Config;

public class ProductConfiguration:IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
        builder.Property(x => x.PictureUrl).IsRequired();
        
        builder.HasOne(x=>x.ProductBrand).WithMany().HasForeignKey(x=>x.ProductBrandId);
 
        builder.HasOne(x => x.ProductType).WithMany().HasForeignKey(x=>x.ProductTypeId);
    }
}
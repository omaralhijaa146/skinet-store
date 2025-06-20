﻿
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using skinet.Core.Entities;

namespace skinet.Infrastructure.Data;

public class StoreContext:DbContext
{
    public StoreContext(DbContextOptions options):base(options)
    { }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductBrand> ProductBrands { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
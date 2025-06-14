using Microsoft.EntityFrameworkCore;
using skinet.Core.Entities;
using skinet.Core.Interfaces;

namespace skinet.Infrastructure.Data;

public class ProductRepository:IProductRepository
{
    private readonly StoreContext _context;

    public ProductRepository(StoreContext context)
    {
        _context = context;
    }
    
    public async Task<Product> GetProductByIdAsync(int id)
    {
       return  await _context.Products.Include(x=>x.ProductType).Include(x=>x.ProductBrand).FirstOrDefaultAsync(x=>x.Id == id);
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        var result = await _context.Products.Include(x=>x.ProductType).Include(x=>x.ProductBrand).ToListAsync();
        return result;
    }

    public async Task<IReadOnlyList<ProductBrand>> GetProductBrandAsync()
    {
        var result= await _context.ProductBrands.ToListAsync();
        return result;
    }

    public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
    {
        var result= await _context.ProductTypes.ToListAsync();
        return result;
    }
}
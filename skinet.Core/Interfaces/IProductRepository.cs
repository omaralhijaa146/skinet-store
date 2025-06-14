using skinet.Core.Entities;

namespace skinet.Core.Interfaces;

public interface IProductRepository
{
    public Task<Product> GetProductByIdAsync(int id);
    public Task<IReadOnlyList<Product>> GetProductsAsync();
    
    public Task<IReadOnlyList<ProductBrand>> GetProductBrandAsync();
    public Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
    
}
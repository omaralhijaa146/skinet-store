using skinet.Core.Entities;
using skinet.Core.Specifications;

namespace skinet.Core.Interfaces;

public interface IGenericRepository<T> where T:BaseEntity
{
    public Task<T> GetByIdAsync(int id);
    public Task<IReadOnlyList<T>> ListAllAsync();
    
    public Task<T> GetEntityWithSpec(ISpecification<T> spec);
    
    public Task<IReadOnlyList<T>> ListAllAsyncSpec(ISpecification<T> spec);
}
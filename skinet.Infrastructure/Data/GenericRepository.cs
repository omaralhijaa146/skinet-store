using Microsoft.EntityFrameworkCore;
using skinet.Core.Entities;
using skinet.Core.Interfaces;
using skinet.Core.Specifications;

namespace skinet.Infrastructure.Data;

public class GenericRepository<T> : IGenericRepository<T> where T:BaseEntity
{
    private readonly StoreContext _context;

    public GenericRepository(StoreContext context)
    {
        _context = context;
    }
    
    public async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(x=>x.Id==id);
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<T>> ListAllAsyncSpec(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).ToListAsync(); 
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(),spec);
    }
}
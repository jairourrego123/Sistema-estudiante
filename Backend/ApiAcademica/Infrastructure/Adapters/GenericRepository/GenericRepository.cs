

using Domain.Entities;
using Infrastructure.Ports;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Infrastructure.DataSource;

namespace Infrastructure.Adapters.GenericRepository;

public class GenericRepository<T> : IGenericRepository<T> where T : DomainBase
{
    private readonly DataContext _context;
    private readonly DbSet<T> _dataset;

    public GenericRepository(DataContext context)
    {
        _context = context;
        _dataset = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetManyAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        bool isTracking = false)
    {
        IQueryable<T> q = _dataset;
        if (filter != null) q = q.Where(filter);
        if (orderBy != null) q = orderBy(q);
        return isTracking
            ? await q.ToListAsync()
            : await q.AsNoTracking().ToListAsync();
    }

    public Task<T?> GetOneAsync(Guid id)
        => _dataset.FindAsync(id).AsTask();

    public Task<T> AddAsync(T entity)
    {
        _dataset.Add(entity);
        return Task.FromResult(entity);
    }

    public Task UpdateAsync(T entity)
    {
        _dataset.Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(T entity)
    {
        _dataset.Remove(entity);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync()
    {
        _context.SaveChangesAsync();
        return Task.CompletedTask;
    }
}

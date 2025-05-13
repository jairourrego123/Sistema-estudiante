
using Domain.Entities;
using System.Linq.Expressions;

namespace Infrastructure.Ports;

public interface IGenericRepository<T> where T : DomainBase
{
    Task<T?> GetOneAsync(Guid id);
    Task<IEnumerable<T>> GetManyAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        bool isTracking = false
    );

    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task SaveChangesAsync();

}

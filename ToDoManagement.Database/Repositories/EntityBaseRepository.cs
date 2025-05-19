using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ToDoManagement.Database.Interfaces;

namespace ToDoManagement.Database.Repositories;

public class EntityBaseRepository<T1, T2>(T1 context) : IEntityBaseRepository<T1, T2>
    where T2 : class
    where T1 : DbContext
{
    private readonly T1 _context = context;

    public async Task AddAsync(T2 entityToInsert, CancellationToken cancellationToken)
    {
        await _context.Set<T2>().AddAsync(entityToInsert, cancellationToken);
    }

    public void Delete(T2 entityToUpdate)
    {
        _context.Set<T2>().Remove(entityToUpdate);
    }

    public async Task<T2?> FindAsync(Expression<Func<T2, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _context.Set<T2>().AsNoTracking().FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<T2?> GetAsync(object key, CancellationToken cancellationToken)
    {
        return await _context.Set<T2>().FindAsync([key], cancellationToken);
    }

    public T1 GetContext() => _context;

    public async Task UpdateAsync(T2 entityToUpdate, object key, CancellationToken cancellationToken)
    {
        var existing = await GetAsync(key, cancellationToken);

        if (existing != null)
        {
            _context.Entry(existing).CurrentValues.SetValues(entityToUpdate);
        }
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}

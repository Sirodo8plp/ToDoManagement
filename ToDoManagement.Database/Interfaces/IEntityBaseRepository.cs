using System.Linq.Expressions;

namespace ToDoManagement.Database.Interfaces;

public interface IEntityBaseRepository<T1, T2>
{
    public T1 GetContext();

    public Task AddAsync(T2 entityToInsert, CancellationToken cancellationToken);
    public Task UpdateAsync(T2 entityToUpdate, object key, CancellationToken cancellationToken);
    public void Delete(T2 entityToUpdate);
    public Task<T2?> GetAsync(object key, CancellationToken cancellationToken);
    public Task<T2?> FindAsync(Expression<Func<T2, bool>> predicate, CancellationToken cancellationToken);
    public Task SaveChangesAsync(CancellationToken cancellationToken);
}
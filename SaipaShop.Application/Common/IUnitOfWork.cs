using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SaipaShop.Application.Common;

public interface IUnitOfWork
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;

    void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
    void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class;

    public DatabaseFacade GetDatabase { get; }

    int SaveAllChanges();
    Task<int> SaveAllChangesAsync(CancellationToken ct);
}
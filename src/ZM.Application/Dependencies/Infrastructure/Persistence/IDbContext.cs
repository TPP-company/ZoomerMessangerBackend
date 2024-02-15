using Microsoft.EntityFrameworkCore;
using ZM.Domain.Common;

namespace ZM.Application.Dependencies.Infrastructure.Persistence;

/// <summary>
/// Абстракция для доступа к данным.
/// </summary>
public interface IDbContext
{
    DbSet<TEntity> Set<TEntity>() where TEntity : Entity;

    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

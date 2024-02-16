using Microsoft.EntityFrameworkCore;
using ZM.Domain.Common;

namespace ZM.Application.Dependencies.Infrastructure.Persistence;

/// <summary>
/// Абстракция для доступа к данным.
/// </summary>
public interface IDbContext
{
    /// <summary>
    /// Указать сущность.
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    DbSet<TEntity> Set<TEntity>() where TEntity : Entity;

    /// <summary>
    /// Завершить транзакцию.
    /// </summary>
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Откатить транзакцию.
    /// </summary>
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Открыть транзакцию.
    /// </summary>
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Сохранить изменения.
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

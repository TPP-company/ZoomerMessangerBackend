using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using ZM.Application.Dependencies.Infrastructure.Persistence;
using ZM.Domain.ChatGroups;
using ZM.Domain.Chats;
using ZM.Domain.Users;

namespace ZM.Infrastructure.Persistence.App;
internal class AppDbContext : DbContext, IDbContext
{
	private IDbContextTransaction? _currentTransaction;

	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}

	public DbSet<User> Users { get; set; } = null!;
	public DbSet<ChatGroup> ChatGroups { get; set; } = null!;
	public DbSet<ChatGroupMessage> ChatGroupMessages { get; set; } = null!;
	public DbSet<P2PChat> P2PChats { get; set; } = null!;
	public DbSet<P2PChatMessage> P2PChatMessages { get; set; } = null!;

	public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
	{
		if (_currentTransaction is not null)
		{
			throw new InvalidOperationException("A transaction is already in progress.");
		}

		_currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.RepeatableRead, cancellationToken);
	}

	public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
	{
		try
		{
			await SaveChangesAsync(cancellationToken);

			_currentTransaction?.Commit();
		}
		catch
		{
			await RollbackTransactionAsync(cancellationToken);
			throw;
		}
		finally
		{
			if (_currentTransaction is not null)
			{
				await _currentTransaction.DisposeAsync();
				_currentTransaction = null;
			}
		}
	}

	public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
	{
		if (_currentTransaction is null)
		{
			throw new InvalidOperationException("A transaction must be in progress to execute rollback.");
		}

		try
		{
			await _currentTransaction.RollbackAsync(cancellationToken);
		}
		finally
		{
			await _currentTransaction.DisposeAsync();
			_currentTransaction = null;
		}
	}

	DbSet<TEntity> IDbContext.Set<TEntity>()
	{
		return Set<TEntity>();
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<ChatGroup>()
			.HasOne(cg => cg.Creator);

        modelBuilder.Entity<ChatGroup>()
            .HasMany(cg => cg.Members)
            .WithMany(c => c.ChatGroups);
    }
}

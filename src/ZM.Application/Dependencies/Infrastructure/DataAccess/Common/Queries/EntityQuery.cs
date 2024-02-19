using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ZM.Application.Common.Exceptions;
using ZM.Application.Common.Mappings;
using ZM.Application.Common.Models.Pagination;
using ZM.Domain.Common;
using System.Linq.Dynamic.Core;

namespace ZM.Application.Dependencies.Infrastructure.DataAccess.Common.Queries;

internal static class EntityQuery
{
    /// <summary>
    /// Получить сущность по идентификатору.
    /// </summary>
    public static Task<TEntity?> GetByIdAsync<TEntity>(
        this IQueryable<TEntity> entities,
        Guid id,
        CancellationToken cancellationToken = default)
        where TEntity : Entity
    {
        return entities.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    /// <summary>
    /// Получить ProjectTo сущность по идентификатору.
    /// </summary>
    public static Task<TDto?> GetProjectedByIdAsync<TEntity, TDto>(
        this IQueryable<TEntity> entities,
        Guid id,
        IMapper mapper,
        CancellationToken cancellationToken = default)
        where TEntity : Entity
        where TDto : IMapFrom<TEntity>, IHasId
    {
        return entities
            .ProjectTo<TDto>(mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    /// <summary>
    /// Получить ProjectTo сущности.
    /// </summary>
    public static Task<List<TDto>> GetProjectedListAsync<TEntity, TDto>(
        this IQueryable<TEntity> entities,
        IMapper mapper,
        CancellationToken cancellationToken = default)
        where TEntity : Entity
        where TDto : IMapFrom<TEntity>
    {
        return entities
            .ProjectTo<TDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Получить <see cref="PaginatedResponse{TDto}"/>.
    /// </summary>
    /// <exception cref="InvalidPaginationException">Неверные параметры для постраничного получения.</exception>
    public static async Task<PaginatedResponse<TDto>> GetPaginatedResponseAsync<TEntity, TDto>(
        this IQueryable<TEntity> source,
        PagedAndSorted options,
        Expression<Func<TEntity, bool>>? searchFilter,
        IMapper mapper,
        CancellationToken cancellationToken = default)
        where TEntity : Entity
        where TDto : IMapFrom<TEntity>
    {
        var query = ApplyPaginated(source, options, searchFilter, out var totalCount, out var totalPages);

        var list = await query.GetProjectedListAsync<TEntity, TDto>(mapper, cancellationToken: cancellationToken);

        return new PaginatedResponse<TDto>(options.PageNumber, options.PageSize, totalCount, totalPages, list);
    }

    /// <summary>
    /// Получить <see cref="PaginatedResponse{TDto}"/>.
    /// </summary>
    /// <exception cref="InvalidPaginationException">Неверные параметры для постраничного получения.</exception>
    public static async Task<PaginatedResponse<TDto>> GetPaginatedResponseAsync<TEntity, TDto>(
        this IQueryable<TEntity> source,
        PagedAndSorted options,
        Expression<Func<TEntity, bool>>? searchFilter,
        Expression<Func<TEntity, TDto>> selector,
        CancellationToken cancellationToken = default)
        where TEntity : Entity
        where TDto : IMapFrom<TEntity>
    {
        var query = ApplyPaginated(source, options, searchFilter, out var totalCount, out var totalPages);

        var list = await query.Select(selector).ToListAsync(cancellationToken);

        return new PaginatedResponse<TDto>(options.PageNumber, options.PageSize, totalCount, totalPages, list);
    }

    private static IQueryable<TEntity> ApplyPaginated<TEntity>(
        IQueryable<TEntity> source,
        PagedAndSorted options,
        Expression<Func<TEntity, bool>>? searchFilter,
        out int totalCount,
        out int totalPages) where TEntity : Entity
    {
        totalCount = source.Count();

        totalPages = totalCount == 0 ?
            0 :
            (int)Math.Ceiling(totalCount / (double)options.PageSize);

        var query = source
            .AsNoTracking()
            .Skip((options.PageNumber - 1) * options.PageSize)
            .Take(options.PageSize);

        if (!string.IsNullOrWhiteSpace(options.Sorting))
        {
            try
            {
                query = query.OrderBy(options.Sorting);
            }
            catch (Exception ex)
            {
                throw new InvalidPaginationException("Неверные параметры для постраничного получения", ex);
            }
        }

        if (searchFilter is not null)
        {
            query = query.Where(searchFilter);
        }

        return query;
    }
}

using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Extensions;
using Ozds.Data.Queries.Abstractions;

namespace Ozds.Data.Queries;

public class AuditableQueries(
  IDbContextFactory<DataDbContext> factory
) : IQueries
{
  public async Task<T?> ReadById<T>(
    string id,
    CancellationToken cancellationToken
  )
    where T : class, IAuditableEntity
  {
    var entity = await ReadById(typeof(T), id, cancellationToken);
    return entity is null ? default : (T)entity;
  }

  public async Task<object?> ReadById(
    Type entityType,
    string id,
    CancellationToken cancellationToken
  )
  {
    if (!entityType.IsAssignableTo(typeof(IAuditableEntity)))
    {
      throw new InvalidOperationException(
        $"Type {entityType} is not assignable to {typeof(IAuditableEntity)}");
    }

    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);
    var queryable = context.GetQueryable(entityType);
    var item = await queryable
      .Where(context.PrimaryKeyEquals(entityType, id))
      .FirstOrDefaultAsync(cancellationToken);
    return item;
  }

  public async Task<List<T>> ReadByIds<T>(
    IEnumerable<string> ids,
    CancellationToken cancellationToken,
    bool deleted = false
  )
    where T : class, IAuditableEntity
  {
    var entities = await ReadByIds(typeof(T), ids, cancellationToken, deleted);
    return entities.OfType<T>().ToList();
  }

  public async Task<List<object>> ReadByIds(
    Type entityType,
    IEnumerable<string> ids,
    CancellationToken cancellationToken,
    bool deleted = false
  )
  {
    if (!entityType.IsAssignableTo(typeof(IAuditableEntity)))
    {
      throw new InvalidOperationException(
        $"Type {entityType} is not assignable to {typeof(IAuditableEntity)}");
    }

    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);
    var queryable = context
      .GetQueryable<IAuditableEntity>(entityType)
      .Where(context.PrimaryKeyIn<IAuditableEntity>(ids));

    var filtered = deleted
      ? queryable.Where(x => x.IsDeleted)
      : queryable.Where(x => !x.IsDeleted);

    var items = await filtered
      .ToListAsync(cancellationToken);

    return items.OfType<object>().ToList();
  }

  public async Task<List<T?>> ReadByIdsOrdered<T>(
    IEnumerable<string> ids,
    CancellationToken cancellationToken,
    bool deleted = false
  )
    where T : class, IAuditableEntity
  {
    var entities = await ReadByIdsOrdered(
      typeof(T),
      ids,
      cancellationToken,
      deleted
    );
    return entities.Cast<T?>().ToList();
  }

  public async Task<List<object?>> ReadByIdsOrdered(
    Type entityType,
    IEnumerable<string> ids,
    CancellationToken cancellationToken,
    bool deleted = false
  )
  {
    if (!entityType.IsAssignableTo(typeof(IAuditableEntity)))
    {
      throw new InvalidOperationException(
        $"Type {entityType} is not assignable to {typeof(IAuditableEntity)}");
    }

    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);
    var queryable = context
      .GetQueryable<IAuditableEntity>(entityType)
      .Where(context.PrimaryKeyIn(entityType, ids))
      .OfType<IAuditableEntity>();

    var filtered = deleted
      ? queryable.Where(x => x.IsDeleted)
      : queryable.Where(x => !x.IsDeleted);

    var items = await filtered
      .ToDictionaryAsync(
        x => x.Id,
        x => x,
        cancellationToken);

    return ids
      .Select(
        id =>
        {
          if (items.TryGetValue(id, out var item))
          {
            return item;
          }

          return default;
        })
      .Cast<object?>()
      .ToList();
  }

  public async Task<PaginatedList<T>> Read<T>(
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount,
    bool deleted = false
  )
  {
    var entities = await Read(
      typeof(T),
      pageNumber,
      cancellationToken,
      pageCount,
      deleted
    );

    return entities.Items.OfType<T>().ToPaginatedList(entities.TotalCount);
  }

  public async Task<PaginatedList<object>> Read(
    Type entityType,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount,
    bool deleted = false
  )
  {
    if (!entityType.IsAssignableTo(typeof(IAuditableEntity)))
    {
      throw new InvalidOperationException(
        $"Type {entityType} is not assignable to {typeof(IAuditableEntity)}");
    }

    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);
    var queryable = context.GetQueryable<IAuditableEntity>(entityType);

    var filtered = deleted
      ? queryable.Where(x => x.IsDeleted)
      : queryable.Where(x => !x.IsDeleted);

    var ordered = filtered
      .OrderByDescending(x => x.DeletedOn)
      .OrderByDescending(x => x.LastUpdatedOn)
      .OrderByDescending(x => x.CreatedOn);

    var count = await filtered.CountAsync(cancellationToken);
    var items = await ordered
      .Skip(pageNumber * pageCount)
      .Take(pageCount)
      .ToListAsync(cancellationToken);

    return items.OfType<object>().ToPaginatedList(count);
  }
}

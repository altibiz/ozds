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
    where T : class, IAuditableIdentifiableEntity
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
    if (!entityType.IsAssignableTo(typeof(IAuditableIdentifiableEntity)))
    {
      throw new InvalidOperationException(
        $"Type {entityType} is not assignable to {typeof(IAuditableIdentifiableEntity)}");
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
    CancellationToken cancellationToken
  )
    where T : class, IAuditableIdentifiableEntity
  {
    var entities = await ReadByIds(typeof(T), ids, cancellationToken);
    return entities.OfType<T>().ToList();
  }

  public async Task<List<object>> ReadByIds(
    Type entityType,
    IEnumerable<string> ids,
    CancellationToken cancellationToken
  )
  {
    if (!entityType.IsAssignableTo(typeof(IAuditableIdentifiableEntity)))
    {
      throw new InvalidOperationException(
        $"Type {entityType} is not assignable to {typeof(IAuditableIdentifiableEntity)}");
    }

    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);
    var queryable = context
      .GetQueryable<IAuditableIdentifiableEntity>(entityType)
      .Where(context.PrimaryKeyIn(entityType, ids));

    var filtered = queryable;

    var items = await filtered
      .ToListAsync(cancellationToken);

    return items.OfType<object>().ToList();
  }

  public async Task<List<T?>> ReadByIdsOrdered<T>(
    IEnumerable<string> ids,
    CancellationToken cancellationToken
  )
    where T : class, IAuditableIdentifiableEntity
  {
    var entities = await ReadByIdsOrdered(
      typeof(T),
      ids,
      cancellationToken
    );
    return entities.Cast<T?>().ToList();
  }

  public async Task<List<object?>> ReadByIdsOrdered(
    Type entityType,
    IEnumerable<string> ids,
    CancellationToken cancellationToken
  )
  {
    if (!entityType.IsAssignableTo(typeof(IAuditableIdentifiableEntity)))
    {
      throw new InvalidOperationException(
        $"Type {entityType} is not assignable to {typeof(IAuditableIdentifiableEntity)}");
    }

    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);
    var queryable = context
      .GetQueryable<IAuditableIdentifiableEntity>(entityType)
      .Where(context.PrimaryKeyIn(entityType, ids))
      .OfType<IAuditableIdentifiableEntity>();

    var filtered = queryable;

    var items = await filtered
      .ToDictionaryAsync(
        x => x.Id,
        x => x,
        cancellationToken);

    return ids
      .Select(id =>
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

  public async Task<PaginatedList<T>> ReadByTitle<T>(
    string title,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount
  )
    where T : class, IAuditableIdentifiableEntity
  {
    var entities = await ReadByTitle(
      typeof(T),
      title,
      pageNumber,
      cancellationToken,
      pageCount
    );

    return entities.Items.OfType<T>().ToPaginatedList(entities.TotalCount);
  }

  public async Task<PaginatedList<object>> ReadByTitle(
    Type modelType,
    string title,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount
  )
  {
    if (!modelType.IsAssignableTo(typeof(IAuditableIdentifiableEntity)))
    {
      throw new InvalidOperationException(
        $"Type {modelType} is not assignable to {typeof(IAuditableIdentifiableEntity)}"
      );
    }

    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    var queryable = context
      .GetQueryable<IAuditableIdentifiableEntity>(modelType);

    var filtered = queryable.Where(x => x.Title.Contains(title));

    var ordered = filtered
      .OrderByDescending(x => x.CreatedOn);

    var total = await filtered.CountAsync(cancellationToken);
    var items = await ordered
      .Skip(pageNumber * pageCount)
      .Take(pageCount)
      .ToListAsync(cancellationToken);

    return items
      .OfType<object>()
      .ToPaginatedList(total);
  }

  public async Task<PaginatedList<T>> Read<T>(
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount
  )
    where T : class, IAuditableEntity
  {
    var entities = await Read(
      typeof(T),
      pageNumber,
      cancellationToken,
      pageCount
    );

    return entities.Items.OfType<T>().ToPaginatedList(entities.TotalCount);
  }

  public async Task<PaginatedList<object>> Read(
    Type entityType,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount
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

    var filtered = queryable;

    var ordered = filtered
      .OrderByDescending(x => x.CreatedOn);

    var count = await filtered.CountAsync(cancellationToken);
    var items = await ordered
      .Skip(pageNumber * pageCount)
      .Take(pageCount)
      .ToListAsync(cancellationToken);

    return items.OfType<object>().ToPaginatedList(count);
  }
}

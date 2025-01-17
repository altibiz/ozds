using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Extensions;
using Ozds.Data.Queries.Abstractions;

namespace Ozds.Data.Queries;

public class ReadonlyQueries(
  IDbContextFactory<DataDbContext> factory
) : IQueries
{
  public async Task<T?> ReadSingle<T>(
    string id,
    CancellationToken cancellationToken
  )
    where T : class, IReadonlyEntity
  {
    var entity = await ReadSingleDynamic(typeof(T), id, cancellationToken);
    return entity is null ? default : (T)entity;
  }

  public async Task<object?> ReadSingleDynamic(
    Type entityType,
    string id,
    CancellationToken cancellationToken
  )
  {
    if (!entityType.IsAssignableTo(typeof(IReadonlyEntity)))
    {
      throw new InvalidOperationException(
        $"Type {entityType} is not assignable to {typeof(IReadonlyEntity)}");
    }

    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);
    var queryable = context.GetQueryable(entityType);
    var item = await queryable
      .Where(context.PrimaryKeyEquals(entityType, id))
      .FirstOrDefaultAsync(cancellationToken);
    return item;
  }

  public async Task<PaginatedList<T>> Read<T>(
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount,
    Expression<Func<T, bool>>? where = null,
    Expression<Func<T, object>>? orderByDesc = null,
    Expression<Func<T, object>>? orderByAsc = null
  )
  {
    var entities = await ReadDynamic(
      typeof(T),
      pageNumber,
      cancellationToken,
      pageCount,
      where is { }
        ? Expression.Lambda<Func<object, bool>>(where.Body, where.Parameters)
        : default,
      orderByDesc is { }
        ? Expression.Lambda<Func<object, object>>(orderByDesc.Body, orderByDesc.Parameters)
        : default,
      orderByAsc is { }
      ? Expression.Lambda<Func<object, object>>(orderByAsc.Body, orderByAsc.Parameters)
      : default
    );

    return entities.Items.OfType<T>().ToPaginatedList(entities.TotalCount);
  }

  public async Task<PaginatedList<object>> ReadDynamic(
    Type entityType,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount,
    Expression<Func<object, bool>>? where = null,
    Expression<Func<object, object>>? orderByDesc = null,
    Expression<Func<object, object>>? orderByAsc = null
  )
  {
    if (!entityType.IsAssignableTo(typeof(IReadonlyEntity)))
    {
      throw new InvalidOperationException(
        $"Type {entityType} is not assignable to {typeof(IReadonlyEntity)}");
    }

    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);
    var queryable = context.GetQueryable<IReadonlyEntity>(entityType);

    var filtered = where is { }
      ? queryable.Where(
        Expression.Lambda<Func<IReadonlyEntity, bool>>(
          where.Body,
          where.Parameters))
      : queryable;

    var ordered = filtered;
    ordered = orderByAsc is { }
      ? ordered.OrderBy(
        Expression.Lambda<Func<IReadonlyEntity, object>>(
          orderByAsc.Body,
          orderByAsc.Parameters))
      : ordered;
    ordered = orderByDesc is { }
      ? ordered.OrderByDescending(
        Expression.Lambda<Func<IReadonlyEntity, object>>(
          orderByDesc.Body,
          orderByDesc.Parameters))
      : ordered;
    if (orderByAsc is null && orderByDesc is null)
    {
      var primaryKey = context.PrimaryKeyOf(entityType);
      ordered = ordered.OrderByDescending(
        Expression.Lambda<Func<IReadonlyEntity, object>>(
          primaryKey.Body,
          primaryKey.Parameters));
    }

    var count = await filtered.CountAsync(cancellationToken);
    var items = await ordered
      .Skip((pageNumber - 1) * pageCount)
      .Take(pageCount)
      .ToListAsync(cancellationToken);

    return items.OfType<object>().ToPaginatedList(count);
  }
}

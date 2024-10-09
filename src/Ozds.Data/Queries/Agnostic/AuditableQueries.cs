using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;
using Ozds.Data.Queries.Abstractions;

namespace Ozds.Business.Queries.Agnostic;

public class AuditableQueries(
  IDbContextFactory<DataDbContext> factory
) : IQueries
{
  public async Task<T?> ReadSingle<T>(
    string id,
    CancellationToken cancellationToken
  )
    where T : class, IAuditableEntity
  {
    return await ReadSingleDynamic<T>(id, cancellationToken);
  }

  public async Task<T?> ReadSingleDynamic<T>(
    string id,
    CancellationToken cancellationToken
  )
  {
    var entityType = typeof(T);
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
    return item is null ? default : (T)item;
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
    return await ReadDynamic(
      pageNumber,
      cancellationToken,
      pageCount,
      where,
      orderByDesc,
      orderByAsc
    );
  }

  public async Task<PaginatedList<T>> ReadDynamic<T>(
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount,
    Expression<Func<T, bool>>? where = null,
    Expression<Func<T, object>>? orderByDesc = null,
    Expression<Func<T, object>>? orderByAsc = null
  )
  {
    var entityType = typeof(T);
    if (!entityType.IsAssignableTo(typeof(IAuditableEntity)))
    {
      throw new InvalidOperationException(
        $"Type {entityType} is not assignable to {typeof(IAuditableEntity)}");
    }

    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);
    var queryable = context.GetQueryable<IAuditableEntity>(entityType);

    var filtered = where is { }
      ? queryable.Where(
        Expression.Lambda<Func<IAuditableEntity, bool>>(
          where.Body,
          where.Parameters))
      : queryable;

    var ordered = filtered;
    ordered = orderByAsc is { }
      ? ordered.OrderBy(
        Expression.Lambda<Func<IAuditableEntity, object>>(
          orderByAsc.Body,
          orderByAsc.Parameters))
      : ordered;
    ordered = orderByDesc is { }
      ? ordered.OrderByDescending(
        Expression.Lambda<Func<IAuditableEntity, object>>(
          orderByDesc.Body,
          orderByDesc.Parameters))
      : ordered;
    if (orderByAsc is null && orderByDesc is null)
    {
      ordered = ordered
        .OrderByDescending(x => x.DeletedOn)
        .OrderByDescending(x => x.LastUpdatedOn)
        .OrderByDescending(x => x.CreatedOn);
    }

    var count = await filtered.CountAsync(cancellationToken);
    var items = await ordered
      .Skip((pageNumber - 1) * pageCount)
      .Take(pageCount)
      .ToListAsync(cancellationToken);

    return items.OfType<T>().ToPaginatedList(count);
  }
}

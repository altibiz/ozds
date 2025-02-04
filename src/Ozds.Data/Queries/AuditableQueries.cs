using System.Linq.Expressions;
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
  public async Task<T?> ReadSingle<T>(
    string id,
    CancellationToken cancellationToken
  )
    where T : class, IAuditableEntity
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
      where is not null
        ? Expression.Lambda<Func<object, bool>>(where.Body, where.Parameters)
        : default,
      orderByDesc is not null
        ? Expression.Lambda<Func<object, object>>(
          orderByDesc.Body, orderByDesc.Parameters)
        : default,
      orderByAsc is not null
        ? Expression.Lambda<Func<object, object>>(
          orderByAsc.Body, orderByAsc.Parameters)
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
    if (!entityType.IsAssignableTo(typeof(IAuditableEntity)))
    {
      throw new InvalidOperationException(
        $"Type {entityType} is not assignable to {typeof(IAuditableEntity)}");
    }

    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);
    var queryable = context.GetQueryable<IAuditableEntity>(entityType);

    var filtered = where is not null
      ? queryable.Where(
        Expression.Lambda<Func<IAuditableEntity, bool>>(
          where.Body,
          where.Parameters))
      : queryable;

    var ordered = filtered;
    ordered = orderByAsc is not null
      ? ordered.OrderBy(
        Expression.Lambda<Func<IAuditableEntity, object>>(
          orderByAsc.Body,
          orderByAsc.Parameters))
      : ordered;
    ordered = orderByDesc is not null
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
      .Skip(pageNumber * pageCount)
      .Take(pageCount)
      .ToListAsync(cancellationToken);

    return items.OfType<object>().ToPaginatedList(count);
  }

  public async Task<string> ReadEntityTypeName(
    Type entityType,
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

    return context.GetEntityTypeNameFromEntityType(entityType)
      ?? throw new InvalidOperationException(
        $"Type {entityType} doesn't have a type name");
  }

  public async Task<string> ReadEntityTableName(
    Type entityType,
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

    return context.GetTableNameFromEntityType(entityType)
      ?? throw new InvalidOperationException(
        $"Type {entityType} doesn't have a table");
  }

  public async Task<Type> ReadEntityType(
    string entityTypeName,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    return context.GetEntityTypeFromEntityTypeName(entityTypeName)
      ?? throw new InvalidOperationException(
        $"Type {entityTypeName} doesn't have a type");
  }
}

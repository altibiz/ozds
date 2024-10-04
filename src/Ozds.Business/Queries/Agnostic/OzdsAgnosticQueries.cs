using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion.Abstractions;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Extensions;

namespace Ozds.Business.Queries.Agnostic;

// FIXME: Operation is not valid due to the current state of the object
// when ordering by primary key

public class OzdsAgnosticQueries(
  IDbContextFactory<DataDbContext> factory,
  AgnosticModelEntityConverter modelEntityConverter
) : IQueries
{
  public async Task<T?> ReadSingle<T>(string id)
    where T : IIdentifiable
  {
    using var context = await factory.CreateDbContextAsync();

    var queryable = context.GetQueryable(modelEntityConverter
      .EntityType(typeof(T)))
        as IQueryable<IIdentifiableEntity>
      ?? throw new InvalidOperationException();

    var item = await queryable
      .Where(
        context.PrimaryKeyEquals(modelEntityConverter.EntityType(typeof(T)), id))
      .FirstOrDefaultAsync();

    return item is null
      ? default
      : (T)modelEntityConverter.ToModel((item as IIdentifiableEntity)!);
  }

  public async Task<T?> ReadSingleDynamic<T>(string id)
  {
    if (!typeof(T).IsAssignableTo(typeof(IIdentifiable)))
    {
      throw new InvalidOperationException(
        $"{typeof(T)} is not identifiable");
    }

    using var context = await factory.CreateDbContextAsync();

    var queryable = context.GetQueryable(modelEntityConverter
      .EntityType(typeof(T)))
        as IQueryable<IIdentifiableEntity>
      ?? throw new InvalidOperationException();

    var item = await queryable
      .Where(
        context.PrimaryKeyEquals(modelEntityConverter.EntityType(typeof(T)), id))
      .FirstOrDefaultAsync();

    return item is null
      ? default
      : (T)modelEntityConverter.ToModel((item as IIdentifiableEntity)!);
  }

  public async Task<PaginatedList<T>> Read<T>(
    string? whereClause = default,
    string? orderByDescClause = default,
    string? orderByAscClause = default,
    int pageNumber = QueryConstants.StartingPage,
    int pageCount = QueryConstants.DefaultPageCount
  )
    where T : IModel
  {
    using var context = await factory.CreateDbContextAsync();

    var queryable = context.GetQueryable(modelEntityConverter
      .EntityType(typeof(T)))
        as IQueryable<IEntity>
      ?? throw new InvalidOperationException();

    var filtered = whereClause is not null
      ? queryable.WhereDynamic(whereClause)
      : queryable;

    var ordered = filtered;
    if (orderByDescClause is null && orderByAscClause is null)
    {
      var ordering = context
        .PrimaryKeyOf(modelEntityConverter
          .EntityType(typeof(T)));
      var parameter = Expression.Parameter(typeof(IEntity), "e");
      var entityOrdering = Expression.Lambda<Func<IEntity, object>>(
        Expression.Invoke(
          ordering,
          Expression.Convert(parameter, typeof(object))),
        parameter);
      ordered = filtered.OrderBy(entityOrdering);
    }
    else
    {
      var orderByDesc = orderByDescClause is not null
        ? filtered.OrderByDescendingDynamic(orderByDescClause)
        : filtered;
      ordered = orderByAscClause is not null
        ? orderByDesc.OrderByDescendingDynamic(orderByAscClause)
        : orderByDesc;
    }

    var count = await filtered.CountAsync();

    var items = await ordered
      .Skip((pageNumber - 1) * pageCount)
      .Take(pageCount)
      .ToListAsync();

    return items
      .Select(modelEntityConverter.ToModel)
      .OfType<T>()
      .ToPaginatedList(count);
  }

  public async Task<PaginatedList<T>> ReadDynamic<T>(
    string? whereClause = default,
    string? orderByDescClause = default,
    string? orderByAscClause = default,
    int pageNumber = QueryConstants.StartingPage,
    int pageCount = QueryConstants.DefaultPageCount
  )
  {
    if (!typeof(T).IsAssignableTo(typeof(IModel)))
    {
      throw new InvalidOperationException(
        $"{typeof(T)} is not a model");
    }

    using var context = await factory.CreateDbContextAsync();

    var queryable = context.GetQueryable(modelEntityConverter
      .EntityType(typeof(T)))
        as IQueryable<IEntity>
      ?? throw new InvalidOperationException();

    var filtered = whereClause is not null
      ? queryable.WhereDynamic(whereClause)
      : queryable;

    var ordered = filtered;
    if (orderByDescClause is null && orderByAscClause is null)
    {
      var ordering = context
        .PrimaryKeyOf(modelEntityConverter.EntityType(typeof(T)));
      var parameter = Expression.Parameter(typeof(IEntity), "e");
      var entityOrdering = Expression.Lambda<Func<IEntity, object>>(
        Expression.Invoke(
          ordering,
          Expression.Convert(parameter, typeof(object))),
        parameter);
      ordered = filtered.OrderBy(entityOrdering);
    }
    else
    {
      var orderByDesc = orderByDescClause is not null
        ? filtered.OrderByDescendingDynamic(orderByDescClause)
        : filtered;
      ordered = orderByAscClause is not null
        ? orderByDesc.OrderByDescendingDynamic(orderByAscClause)
        : orderByDesc;
    }

    var count = await filtered.CountAsync();

    var items = await ordered
      .Skip((pageNumber - 1) * pageCount)
      .Take(pageCount)
      .ToListAsync();

    return items
      .Select(modelEntityConverter.ToModel)
      .OfType<T>()
      .ToPaginatedList(count);
  }
}

using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Extensions;
using Ozds.Data.Queries.Abstractions;

namespace Ozds.Data.Queries;

public class EntityQueries(
  IDbContextFactory<DataDbContext> factory
) : IQueries
{
  public async Task<PaginatedList<T>> Read<T>(
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount
  )
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
    if (!entityType.IsAssignableTo(typeof(IEntity)))
    {
      throw new InvalidOperationException(
        $"Type {entityType} is not assignable to {typeof(IEntity)}");
    }

    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);
    var queryable = context.GetQueryable<IEntity>(entityType);

    var filtered = queryable;

    var ordered = queryable.OrderByDescending(context.PrimaryKeyOf(entityType));

    var count = await filtered.CountAsync(cancellationToken);
    var items = await ordered
      .Skip(pageNumber * pageCount)
      .Take(pageCount)
      .ToListAsync(cancellationToken);

    return items.OfType<object>().ToPaginatedList(count);
  }
}

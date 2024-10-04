using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models.Enums;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data.Context;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;
using IEvent = Ozds.Business.Models.Abstractions.IEvent;

namespace Ozds.Business.Queries.Agnostic;

public class OzdsEventQueries(
  IDbContextFactory<DataDbContext> contextFactory,
  AgnosticModelEntityConverter modelEntityConverter
) : IQueries
{
  private readonly AgnosticModelEntityConverter _modelEntityConverter =
    modelEntityConverter;

  public async Task<T?> ReadSingle<T>(string id)
    where T : class, IEvent
  {
    await using var context = await contextFactory.CreateDbContextAsync();
    var entityType = _modelEntityConverter.EntityType(typeof(T));
    var queryable = context.GetQueryable(entityType);
    var item = await queryable
      .Where(context.PrimaryKeyEquals(entityType, id))
      .FirstOrDefaultAsync();
    return item is null ? null : _modelEntityConverter.ToModel(item) as T;
  }

  public async Task<PaginatedList<T>> Read<T>(
    LevelModel minLevel,
    int pageNumber = QueryConstants.StartingPage,
    int pageCount = QueryConstants.DefaultPageCount
  )
    where T : class, IEvent
  {
    await using var context = await contextFactory.CreateDbContextAsync();
    var dbSetType = _modelEntityConverter.EntityType(typeof(T));
    var queryable = context.GetQueryable(dbSetType)
        as IQueryable<EventEntity>
      ?? throw new InvalidOperationException(
        $"No DbSet found for {dbSetType}");
    var minLevelEntity = minLevel.ToEntity();
    var filtered = queryable.Where(e => e.Level >= minLevelEntity);
    var ordered = filtered
      .OrderByDescending(aggregate => aggregate.Timestamp);

    var count = await filtered.CountAsync();
    var items = await ordered
      .Skip((pageNumber - 1) * pageCount)
      .Take(pageCount)
      .ToListAsync();
    return items
      .Select(_modelEntityConverter.ToModel)
      .OfType<T>()
      .ToPaginatedList(count);
  }
}

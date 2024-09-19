using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;
using Ozds.Business.Models.Joins;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data.Context;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Joins;
using Ozds.Data.Extensions;
using INotification = Ozds.Business.Models.Abstractions.INotification;

namespace Ozds.Business.Queries.Agnostic;

public class OzdsNotificationQueries(
  IDbContextFactory<DataDbContext> contextFactory,
  AgnosticModelEntityConverter modelEntityConverter
) : IQueries
{
  private readonly AgnosticModelEntityConverter _modelEntityConverter =
    modelEntityConverter;

  public async Task<T?> ReadSingle<T>(string id)
    where T : class, INotification
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
    IEnumerable<string> whereClauses,
    DateTimeOffset fromDate,
    DateTimeOffset toDate,
    int pageNumber = QueryConstants.StartingPage,
    int pageCount = QueryConstants.DefaultPageCount
  )
    where T : class, INotification
  {
    await using var context = await contextFactory.CreateDbContextAsync();
    var dbSetType = _modelEntityConverter.EntityType(typeof(T));
    var queryable = context.GetQueryable(dbSetType)
        as IQueryable<NotificationEntity>
      ?? throw new InvalidOperationException(
        $"No DbSet found for {dbSetType}");
    var filtered = whereClauses.Aggregate(
      queryable,
      (current, clause) => current.WhereDynamic(clause));
    var timeFiltered = filtered
      .Where(aggregate => aggregate.Timestamp >= fromDate)
      .Where(aggregate => aggregate.Timestamp < toDate);
    var count = await timeFiltered.CountAsync();
    var ordered = timeFiltered
      .OrderByDescending(aggregate => aggregate.Timestamp);
    var items = await ordered
      .Skip((pageNumber - 1) * pageCount)
      .Take(pageCount)
      .ToListAsync();
    return items
      .Select(_modelEntityConverter.ToModel)
      .OfType<T>()
      .ToPaginatedList(count);
  }

  public async Task<PaginatedList<T>> ReadForRecipient<T>(
    RepresentativeModel representative,
    bool seen = false,
    int pageNumber = QueryConstants.StartingPage,
    int pageCount = QueryConstants.DefaultPageCount
  )
    where T : class, INotification
  {
    await using var context = await contextFactory.CreateDbContextAsync();
    var dbSetType = _modelEntityConverter.EntityType(typeof(T));
    var queryable = context.GetQueryable(dbSetType)
        as IQueryable<NotificationEntity>
      ?? throw new InvalidOperationException(
        $"No DbSet found for {dbSetType}");

    var filtered = context.NotificationRecipients
      .Where(nr => nr.RepresentativeId == representative.Id)
      .Where(seen
        ? nr => nr.SeenOn != null
        : nr => nr.SeenOn == null)
      .Join(
        queryable,
        context.ForeignKeyOf<NotificationRecipientEntity>(nameof(NotificationRecipientEntity.Notification)),
        context.PrimaryKeyOf<NotificationEntity>(),
        (_, n) => n
      );
    var count = await filtered.CountAsync();
    var ordered = filtered
      .OrderByDescending(aggregate => aggregate.Timestamp);
    var items = await ordered
      .Skip((pageNumber - 1) * pageCount)
      .Take(pageCount)
      .ToListAsync();
    return items
      .Select(_modelEntityConverter.ToModel)
      .OfType<T>()
      .ToPaginatedList(count);
  }

  public async Task<List<NotificationRecipientModel>> Recipients(
    INotification notification)
  {
    await using var context = await contextFactory.CreateDbContextAsync();
    var topics = notification.Topics.Select(x => x.ToEntity());
    var representatives = await context.Representatives
      .Where(r => r.Topics.Any(t => topics.Contains(t)))
      .ToListAsync();

    return representatives
      .Select(representative => new NotificationRecipientModel
      {
        NotificationId = notification.Id,
        RepresentativeId = representative.Id
      })
      .ToList();
  }
}

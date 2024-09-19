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
  DataDbContext context,
  AgnosticModelEntityConverter modelEntityConverter
) : IQueries
{
  private readonly DataDbContext _context = context;

  private readonly AgnosticModelEntityConverter _modelEntityConverter =
    modelEntityConverter;

  public async Task<T?> ReadSingle<T>(string id)
    where T : class, INotification
  {
    var entityType = _modelEntityConverter.EntityType(typeof(T));
    var queryable = _context.GetQueryable(entityType);
    var item = await queryable
      .Where(_context.PrimaryKeyEquals(entityType, id))
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
    var dbSetType = _modelEntityConverter.EntityType(typeof(T));
    var queryable = _context.GetQueryable(dbSetType)
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
    var dbSetType = _modelEntityConverter.EntityType(typeof(T));
    var queryable = _context.GetQueryable(dbSetType)
        as IQueryable<NotificationEntity>
      ?? throw new InvalidOperationException(
        $"No DbSet found for {dbSetType}");

    var filtered = _context.NotificationRecipients
      .Where(nr => nr.RepresentativeId == representative.Id)
      .Where(nr => seen || nr.SeenOn == null)
      .Join(
        queryable,
        _context.ForeignKeyOf<NotificationRecipientEntity>(nameof(NotificationRecipientEntity.Notification)),
        _context.PrimaryKeyOf<NotificationEntity>(),
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
    var topics = notification.Topics.Select(x => x.ToEntity());
    var representatives = await _context.Representatives
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

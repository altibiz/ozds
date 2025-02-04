using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Joins;
using Ozds.Data.Extensions;
using Ozds.Data.Queries.Abstractions;

namespace Ozds.Data.Queries;

public class NotificationQueries(
  IDbContextFactory<DataDbContext> factory
) : IQueries
{
  public async Task<T?> ReadSingle<T>(
    Type type,
    string id,
    CancellationToken cancellationToken
  )
    where T : INotificationEntity
  {
    var entity = await ReadSingleDynamic(type, id, cancellationToken);
    return (T?)entity;
  }

  public async Task<INotificationEntity?> ReadSingleDynamic(
    Type type,
    string id,
    CancellationToken cancellationToken
  )
  {
    if (!type.IsAssignableTo(typeof(INotificationEntity)))
    {
      throw new ArgumentException(
        "Type must be a concrete type of NotificationEntity",
        nameof(type)
      );
    }

    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    return (INotificationEntity?)await context.Notifications
      .Where(context.PrimaryKeyEquals(type, id))
      .FirstOrDefaultAsync(cancellationToken);
  }

  public async Task<PaginatedList<T>> ReadForRecipient<T>(
    string representativeId,
    bool seen,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount
  )
    where T : INotificationEntity
  {
    var entities = await ReadForRecipientDynamic(
      typeof(T),
      representativeId,
      seen,
      pageNumber,
      cancellationToken,
      pageCount
    );

    return entities.Items.OfType<T>().ToPaginatedList(entities.TotalCount);
  }

  public async Task<List<T>> ReadForRecipient<T>(
    string representativeId,
    bool seen,
    CancellationToken cancellationToken
  )
    where T : INotificationEntity
  {
    var entities = await ReadForRecipientDynamic(
      typeof(T),
      representativeId,
      seen,
      cancellationToken
    );

    return entities.OfType<T>().ToList();
  }

  public async Task<PaginatedList<INotificationEntity>> ReadForRecipientDynamic(
    Type entityType,
    string representativeId,
    bool seen,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount
  )
  {
    if (!entityType.IsAssignableTo(typeof(INotificationEntity)))
    {
      throw new InvalidOperationException(
        $"{entityType} is not a ${typeof(INotificationEntity)}"
      );
    }

    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    var queryable = context.Notifications;

    var initial = context.NotificationRecipients
      .Where(recipient => recipient.RepresentativeId == representativeId);
    initial = seen ? initial.Where(nr => nr.SeenOn != null) : initial;
    var filtered = initial
      .Join(
        queryable,
        context.ForeignKeyOf<NotificationRecipientEntity>(
          nameof(NotificationRecipientEntity.Notification)),
        context.PrimaryKeyOf<NotificationEntity>(),
        (_, notification) => notification
      );
    var ordered = filtered
      .OrderByDescending(aggregate => aggregate.Timestamp);

    var count = await filtered.CountAsync(cancellationToken);
    var items = await ordered
      .Skip(pageNumber * pageCount)
      .Take(pageCount)
      .ToListAsync(cancellationToken);

    return items.OfType<INotificationEntity>().ToPaginatedList(count);
  }

  public async Task<List<INotificationEntity>> ReadForRecipientDynamic(
    Type entityType,
    string representativeId,
    bool seen,
    CancellationToken cancellationToken
  )
  {
    if (!entityType.IsAssignableTo(typeof(INotificationEntity)))
    {
      throw new InvalidOperationException(
        $"{entityType} is not a ${typeof(INotificationEntity)}"
      );
    }

    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    var queryable = context.Notifications;

    var initial = context.NotificationRecipients
      .Where(recipient => recipient.RepresentativeId == representativeId);
    initial = seen ? initial.Where(nr => nr.SeenOn != null) : initial;
    var filtered = initial
      .Join(
        queryable,
        context.ForeignKeyOf<NotificationRecipientEntity>(
          nameof(NotificationRecipientEntity.Notification)),
        context.PrimaryKeyOf<NotificationEntity>(),
        (_, notification) => notification
      );
    var ordered = filtered
      .OrderByDescending(aggregate => aggregate.Timestamp);

    var items = await ordered.ToListAsync(cancellationToken);

    return items.OfType<INotificationEntity>().ToList();
  }

  public async Task<List<NotificationRecipientEntity>> Recipients(
    INotificationEntity notification)
  {
    await using var context = await factory.CreateDbContextAsync();

    var topics = notification.Topics;
    var representatives = await context.Representatives
      .Where(r => r.Topics.Any(t => topics.Contains(t)))
      .ToListAsync();

    return representatives
      .Select(
        representative => new NotificationRecipientEntity
        {
          NotificationId = notification.Id,
          RepresentativeId = representative.Id
        })
      .ToList();
  }
}

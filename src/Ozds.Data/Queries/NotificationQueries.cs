using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Joins;
using Ozds.Data.Queries.Abstractions;

namespace Ozds.Data.Queries;

public class NotificationQueries(IDbContextFactory<DataDbContext> factory)
  : IQueries
{
  public async Task<PaginatedList<T>> ReadForRecipient<T>(
    string representativeId,
    int pageNumber,
    CancellationToken cancellationToken,
    bool seen = false,
    string? title = null,
    int pageCount = QueryConstants.DefaultPageCount
  )
    where T : INotificationEntity
  {
    var entities = await ReadForRecipient(
      typeof(T),
      representativeId,
      pageNumber,
      cancellationToken,
      seen,
      title,
      pageCount
    );

    return entities.Items.OfType<T>().ToPaginatedList(entities.TotalCount);
  }

  public async Task<List<T>> ReadForRecipient<T>(
    string representativeId,
    CancellationToken cancellationToken,
    bool seen = false,
    string? title = null
  )
    where T : INotificationEntity
  {
    var entities = await ReadForRecipient(
      typeof(T),
      representativeId,
      cancellationToken,
      seen,
      title
    );

    return entities.OfType<T>().ToList();
  }

  public async Task<PaginatedList<INotificationEntity>> ReadForRecipient(
    Type entityType,
    string representativeId,
    int pageNumber,
    CancellationToken cancellationToken,
    bool seen = false,
    string? title = null,
    int pageCount = QueryConstants.DefaultPageCount
  )
  {
    if (!entityType.IsAssignableTo(typeof(INotificationEntity)))
    {
      throw new InvalidOperationException(
        $"{entityType} is not a ${typeof(INotificationEntity)}"
      );
    }

    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );

    var recipients = context.NotificationRecipients.Where(recipient =>
      recipient.RepresentativeId == representativeId
    );

    recipients = seen
      ? recipients.Where(x => x.SeenOn != null)
      : recipients.Where(x => x.SeenOn == null);

    var filtered = recipients
      .Include(x => x.Notification)
      .Select(x => x.Notification);

    if (!string.IsNullOrEmpty(title))
    {
      filtered = filtered.Where(x => x.Title.Contains(title));
    }

    var ordered = filtered.OrderByDescending(aggregate => aggregate.Timestamp);

    var count = await filtered.CountAsync(cancellationToken);
    var items = await ordered
      .Skip(pageNumber * pageCount)
      .Take(pageCount)
      .ToListAsync(cancellationToken);

    return items.OfType<INotificationEntity>().ToPaginatedList(count);
  }

  public async Task<List<INotificationEntity>> ReadForRecipient(
    Type entityType,
    string representativeId,
    CancellationToken cancellationToken,
    bool seen = false,
    string? title = null
  )
  {
    if (!entityType.IsAssignableTo(typeof(INotificationEntity)))
    {
      throw new InvalidOperationException(
        $"{entityType} is not a ${typeof(INotificationEntity)}"
      );
    }

    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );

    var recipients = context.NotificationRecipients.Where(recipient =>
      recipient.RepresentativeId == representativeId
    );

    recipients = seen
      ? recipients.Where(x => x.SeenOn != null)
      : recipients.Where(x => x.SeenOn == null);

    var filtered = recipients
      .Include(x => x.Notification)
      .Select(x => x.Notification);

    if (!string.IsNullOrEmpty(title))
    {
      filtered = filtered.Where(x => x.Title.Contains(title));
    }

    var ordered = filtered.OrderByDescending(aggregate => aggregate.Timestamp);

    var items = await ordered.ToListAsync(cancellationToken);

    return items.OfType<INotificationEntity>().ToList();
  }

  public async Task<List<NotificationRecipientEntity>> Recipients(
    INotificationEntity notification
  )
  {
    await using var context = await factory.CreateDbContextAsync();

    var topics = notification.Topics;
    var representatives = await context
      .Representatives.Where(r => r.Topics.Any(t => topics.Contains(t)))
      .ToListAsync();

    return representatives
      .Select(representative => new NotificationRecipientEntity
      {
        NotificationId = notification.Id,
        RepresentativeId = representative.Id,
      })
      .ToList();
  }

  public async Task<List<NotificationRecipientEntity>> Recipients(
    IEnumerable<INotificationEntity> notifications
  )
  {
    await using var context = await factory.CreateDbContextAsync();

    var topics = notifications.SelectMany(x => x.Topics);
    var representatives = await context
      .Representatives.Where(r => r.Topics.Any(t => topics.Contains(t)))
      .ToListAsync();

    return notifications
      .SelectMany(notification =>
        representatives
          .Where(representative =>
            representative.Topics.Exists(t => notification.Topics.Contains(t))
          )
          .Select(representative => new NotificationRecipientEntity
          {
            NotificationId = notification.Id,
            RepresentativeId = representative.Id,
          })
      )
      .ToList();
  }
}

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
  public async Task<PaginatedList<T>> ReadForRecipients<T>(
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

    var filtered = context.NotificationRecipients
      .Where(recipient => recipient.RepresentativeId == representativeId)
      .Where(
        seen
          ? nr => nr.SeenOn != null
          : nr => nr.SeenOn == null)
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
      .Skip((pageNumber - 1) * pageCount)
      .Take(pageCount)
      .ToListAsync(cancellationToken);

    return items.OfType<INotificationEntity>().ToPaginatedList(count);
  }
}

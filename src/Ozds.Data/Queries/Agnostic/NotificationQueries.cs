using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Joins;
using Ozds.Data.Extensions;
using Ozds.Data.Queries.Abstractions;

namespace Ozds.Data.Queries.Agnostic;

public class NotificationQueries(
  IDbContextFactory<DataDbContext> factory
) : IQueries
{
  public async Task<PaginatedList<T>> ReadForRecipient<T>(
    string representativeId,
    bool seen,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount
  )
    where T : class, INotificationEntity
  {
    var entityType = typeof(T);

    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    var queryable = context.GetQueryable<NotificationEntity>(entityType);

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

    return items.OfType<T>().ToPaginatedList(count);
  }
}

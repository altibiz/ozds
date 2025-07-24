using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Joins;
using Ozds.Data.Extensions;
using Ozds.Data.Mutations.Abstractions;
using Ozds.Time.Queries.Abstractions;

namespace Ozds.Data.Mutations;

public class NotificationMutations(
  IDbContextFactory<DataDbContext> factory,
  IClockQueries clock
) : IMutations
{
  public async Task<NotificationRecipientEntity?> MarkNotificationAsSeen(
    string notificationId,
    string representativeId,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    // NOTE: like this instead of direct update because of events
    var recipient = await context.NotificationRecipients
      .Where(
        context.ForeignKeyEquals<NotificationRecipientEntity>(
          nameof(NotificationRecipientEntity.Notification),
          notificationId))
      .Where(
        context.ForeignKeyEquals<NotificationRecipientEntity>(
          nameof(NotificationRecipientEntity.Representative),
          representativeId))
      .FirstOrDefaultAsync(cancellationToken);

    if (recipient is null)
    {
      return null;
    }

    recipient.SeenOn = clock.Timestamp();
    await context.SaveChangesAsync(cancellationToken);

    return recipient;
  }

  public async Task<IResolvableNotificationEntity> MarkNotificationAsResolved(
    IResolvableNotificationEntity notification,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    context.Update(notification);

    await context.SaveChangesAsync(cancellationToken);

    return notification;
  }
}

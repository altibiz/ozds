using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Joins;
using Ozds.Data.Extensions;
using Ozds.Data.Mutations.Abstractions;

namespace Ozds.Data.Mutations;

public class NotificationMutations(
  IDbContextFactory<DataDbContext> factory
) : IMutations
{
  public async Task Create(
    INotificationEntity entity,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    context.Add(entity);
    await context.SaveChangesAsync(cancellationToken);
  }

  public async Task AddRecipients(
    IReadOnlyCollection<NotificationRecipientEntity> recipients,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);
    context.NotificationRecipients.AddRange(recipients);
    await context.SaveChangesAsync(cancellationToken);
  }

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

    recipient.SeenOn = DateTimeOffset.UtcNow;
    await context.SaveChangesAsync(cancellationToken);

    return recipient;
  }
}

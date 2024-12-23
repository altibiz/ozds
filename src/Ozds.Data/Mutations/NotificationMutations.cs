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

  public async Task MarkNotificationAsSeen(
    string notificationId,
    string representativeId,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);
    await context.NotificationRecipients
      .Where(
        context.ForeignKeyEquals<NotificationRecipientEntity>(
          nameof(NotificationRecipientEntity.Notification),
          notificationId))
      .Where(
        context.ForeignKeyEquals<NotificationRecipientEntity>(
          nameof(NotificationRecipientEntity.Representative),
          representativeId))
      .ExecuteUpdateAsync(
        s => s.SetProperty(x => x.SeenOn, DateTimeOffset.UtcNow),
        cancellationToken);
  }
}

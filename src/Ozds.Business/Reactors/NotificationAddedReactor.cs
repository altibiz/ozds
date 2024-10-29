using System.Threading.Channels;
using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Conversion.Joins;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
using Ozds.Business.Reactors.Abstractions;
using Ozds.Data.Context;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Joins;
using Ozds.Data.Extensions;
using Ozds.Data.Observers.Abstractions;
using Ozds.Data.Observers.EventArgs;

namespace Ozds.Business.Reactors;

public class NotificationAddedReactor(
  INotificationCreatedPublisher publisher,
  IDbContextFactory<DataDbContext> factory,
  IEntityChangesSubscriber subscriber
) : BackgroundService, IReactor
{
  private readonly Channel<EntitiesChangedEventArgs> channel =
    Channel.CreateUnbounded<EntitiesChangedEventArgs>();

  public override async Task StartAsync(CancellationToken cancellationToken)
  {
    subscriber.SubscribeEntitiesChanged(OnChanged);

    await base.StartAsync(cancellationToken);
  }

  public override Task StopAsync(CancellationToken cancellationToken)
  {
    subscriber.SubscribeEntitiesChanged(OnChanged);

    return base.StopAsync(cancellationToken);
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    await foreach (var eventArgs in channel.Reader.ReadAllAsync(stoppingToken))
    {
      await using var context = await factory
        .CreateDbContextAsync(stoppingToken);
      await Handle(context, publisher, eventArgs);
    }
  }

  private void OnChanged(object? sender, EntitiesChangedEventArgs eventArgs)
  {
    channel.Writer.TryWrite(eventArgs);
  }

  private static async Task Handle(
    DataDbContext context,
    INotificationCreatedPublisher publisher,
    EntitiesChangedEventArgs eventArgs)
  {
    foreach (var entry in eventArgs.Entities)
    {
      if (
        entry.State is not EntityChangedState.Added ||
        entry.Entity is not NotificationRecipientEntity recipient)
      {
        continue;
      }

      var notification = await context.Notifications
        .Where(
          context.PrimaryKeyEquals<NotificationEntity>(
            recipient.NotificationId))
        .FirstOrDefaultAsync();

      if (notification is null)
      {
        continue;
      }

      if (entry.State is EntityChangedState.Added)
      {
        publisher.PublishCreated(
          new NotificationCreatedEventArgs
          {
            Notification = notification.ToModel(),
            Recipient = recipient.ToModel()
          });
      }
    }
  }
}

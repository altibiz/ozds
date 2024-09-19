using System.Threading.Channels;
using Ozds.Business.Conversion;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
using Ozds.Business.Reactors.Abstractions;
using Ozds.Data.Entities.Base;
using Ozds.Data.Observers.Abstractions;
using Ozds.Data.Observers.EventArgs;

namespace Ozds.Business.Reactors;

public class NotificationAddedReactor(
  IServiceScopeFactory serviceScopeFactory,
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
      await using var scope = serviceScopeFactory.CreateAsyncScope();
      await Handle(scope.ServiceProvider, eventArgs);
    }
  }

  private void OnChanged(object? sender, EntitiesChangedEventArgs eventArgs)
  {
    channel.Writer.TryWrite(eventArgs);
  }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
  private static async Task Handle(
    IServiceProvider serviceProvider,
    EntitiesChangedEventArgs eventArgs)
  {
    var publisher = serviceProvider.GetRequiredService<INotificationCreatedPublisher>();

    foreach (var entry in eventArgs.Entities)
    {
      if (
        entry.State is not EntityChangedState.Added ||
        entry.Entity is not NotificationEntity notification)
      {
        continue;
      }

      if (entry.State is EntityChangedState.Added)
      {
        publisher.PublishCreated(
          new NotificationCreatedEventArgs
          {
            Notification = notification.ToModel()
          });
      }
    }
  }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
}

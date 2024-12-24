using System.Threading.Channels;
using Ozds.Business.Caching;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Reactors.Abstractions;
using Ozds.Data.Observers.Abstractions;
using Ozds.Data.Observers.EventArgs;

namespace Ozds.Business.Reactors;

public class ValidationCacheUpdateReactor(
  IEntityChangesSubscriber subscriber,
  ValidationCache cache,
  ILogger<ValidationCacheUpdateReactor> logger
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
      try
      {
        await Handle(eventArgs);
      }
      catch (Exception ex)
      {
        logger.LogError(
          ex,
          "Validation cache update failed for {Count} entities",
          eventArgs.Entities.Count);
      }
    }
  }

  private void OnChanged(object? sender, EntitiesChangedEventArgs eventArgs)
  {
    channel.Writer.TryWrite(eventArgs);
  }

  private async Task Handle(
    EntitiesChangedEventArgs eventArgs)
  {
    foreach (var entry in eventArgs.Entities)
    {
      if (entry.Entity is not IMeasurementValidator validator)
      {
        continue;
      }

      if (entry.State is not EntityChangedState.Modified)
      {
        await cache.TryUpdateAsync(validator, CancellationToken.None);
      }
      else if (entry.State is EntityChangedState.Removed)
      {
        await cache.TryRemoveAsync(validator, CancellationToken.None);
      }
    }
  }
}

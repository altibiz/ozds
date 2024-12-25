using System.Threading.Channels;
using Ozds.Business.Caching;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Reactors.Abstractions;
using Ozds.Data.Observers.Abstractions;
using Ozds.Data.Observers.EventArgs;

namespace Ozds.Business.Reactors;

public class MeasurementLocationCacheUpdateReactor(
  IEntityChangesSubscriber subscriber,
  MeasurementLocationCache cache,
  ILogger<MeasurementLocationCacheUpdateReactor> logger
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
    subscriber.UnsubscribeEntitiesChanged(OnChanged);
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
          "Measurement location cache update failed for {Count} entities",
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
      if (entry.Entity is not IMeasurementLocation measurementLocation)
      {
        continue;
      }

      if (entry.State is not EntityChangedState.Modified)
      {
        await cache.TryUpdateAsync(measurementLocation, CancellationToken.None);
      }
      else if (entry.State is EntityChangedState.Removed)
      {
        await cache.TryRemoveAsync(measurementLocation, CancellationToken.None);
      }
    }
  }
}

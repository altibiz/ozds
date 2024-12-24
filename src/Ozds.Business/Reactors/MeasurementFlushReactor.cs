using System.Threading.Channels;
using Ozds.Business.Buffers;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Mutations;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
using Ozds.Business.Reactors.Abstractions;

namespace Ozds.Business.Reactors;

public class MeasurementFlushReactor(
  IMeasurementFlushSubscriber subscriber,
  IServiceScopeFactory serviceScopeFactory,
  IMeasurementFinalizePublisher publisher,
  ILogger<MeasurementFlushReactor> logger
) : BackgroundService, IReactor
{
  private readonly Channel<MeasurementFlushEventArgs> channel =
    Channel.CreateUnbounded<MeasurementFlushEventArgs>();

  public override async Task StartAsync(CancellationToken cancellationToken)
  {
    subscriber.SubscribeFlush(OnFlush);
    await base.StartAsync(cancellationToken);
  }

  public override async Task StopAsync(CancellationToken cancellationToken)
  {
    try
    {
      await using var scope = serviceScopeFactory.CreateAsyncScope();
      var buffer = scope.ServiceProvider
        .GetRequiredService<MeasurementBuffer>();
      var flushed = buffer.Flush();
      logger.LogInformation(
        "Flushed {Count} measurements from buffer",
        flushed.Count);
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "Failed to flush measurements from cache");
    }
    subscriber.UnsubscribeFlush(OnFlush);
    await base.StopAsync(cancellationToken);
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
          "Measurement flush failed for {Count} aggregates",
          eventArgs.Measurements.Count);
      }
    }
  }

  private void OnFlush(object? sender, MeasurementFlushEventArgs eventArgs)
  {
    channel.Writer.TryWrite(eventArgs);
  }

  private async Task Handle(
    MeasurementFlushEventArgs eventArgs)
  {
    await using var scope = serviceScopeFactory.CreateAsyncScope();
    var mutations = scope.ServiceProvider
      .GetRequiredService<MeasurementUpsertMutations>();
    var measurements = eventArgs.Measurements;
    var result = await mutations
      .UpsertMeasurements(measurements, CancellationToken.None);

    publisher.PublishFinalize(
      new MeasurementFinalizeEventArgs
      {
        Measurements = result.Where(x => x is not IAggregate).ToList(),
        Aggregates = result.OfType<IAggregate>().ToList()
      });
  }
}

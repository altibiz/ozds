using Ozds.Business.Buffers;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Mutations;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
using Ozds.Business.Reactors.Abstractions;
using Ozds.Business.Reactors.Base;

namespace Ozds.Business.Reactors;

public class MeasurementFlushReactor(
  IServiceScopeFactory factory,
  IMeasurementFlushSubscriber subscriber
) : Reactor<MeasurementFlushEventArgs, MeasurementFlushHandler>(
  subscriber,
  factory
)
{
  private readonly IServiceScopeFactory factory = factory;

  public override async Task StopAsync(CancellationToken cancellationToken)
  {
    {
      await using var scope = factory.CreateAsyncScope();
      try
      {
        var buffer = scope.ServiceProvider
          .GetRequiredService<MeasurementBuffer>();
        var mutations = scope.ServiceProvider
          .GetRequiredService<MeasurementUpsertMutations>();
        var logger = scope.ServiceProvider
          .GetRequiredService<ILogger<MeasurementFlushHandler>>();
        var flushed = buffer.Flush(immediate: true);
        await mutations.UpsertMeasurements(flushed, CancellationToken.None);
        logger.LogInformation(
          "Flushed {Count} measurements from buffer",
          flushed.Count);
      }
      catch (Exception ex)
      {
        var logger = scope.ServiceProvider
          .GetRequiredService<ILogger<MeasurementFlushHandler>>();
        logger.LogError(ex, "Failed to flush measurements from cache");
      }
    }

    await base.StopAsync(cancellationToken);
  }
}

public class MeasurementFlushHandler(
  IMeasurementFinalizePublisher publisher,
  MeasurementUpsertMutations mutations
) : IHandler<MeasurementFlushEventArgs>
{
  public async Task Handle(
    MeasurementFlushEventArgs eventArgs,
    CancellationToken cancellationToken)
  {
    var measurements = eventArgs.Measurements;
    var result = await mutations
      .UpsertMeasurements(measurements, cancellationToken);

    publisher.Publish(
      new MeasurementFinalizeEventArgs
      {
        Measurements = result.Where(x => x is not IAggregate).ToList(),
        Aggregates = result.OfType<IAggregate>().ToList()
      });
  }
}

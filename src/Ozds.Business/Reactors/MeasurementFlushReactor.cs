using Ozds.Business.Buffers;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Mutations;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
using Ozds.Business.Reactors.Base;

namespace Ozds.Business.Reactors;

public class MeasurementFlushReactor(
  IServiceProvider serviceProvider
) : Reactor<
  MeasurementFlushEventArgs,
  IMeasurementFlushSubscriber,
  MeasurementFlushHandler>(serviceProvider)
{
}

public class MeasurementFlushHandler(
  IMeasurementFinalizePublisher publisher,
  MeasurementUpsertMutations mutations,
  MeasurementBuffer buffer
) : Handler<MeasurementFlushEventArgs>
{

  public override async Task Handle(
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

  public override async Task BeforeStopAsync(
    CancellationToken cancellationToken
  )
  {
    var flushed = buffer.Flush(immediate: true);
    await mutations.UpsertMeasurements(flushed, cancellationToken);
  }
}

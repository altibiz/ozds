using Ozds.Business.Buffers;
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
  MeasurementMutations mutations,
  MeasurementBuffer buffer
) : Handler<MeasurementFlushEventArgs>
{
  public override async Task Handle(
    MeasurementFlushEventArgs eventArgs,
    CancellationToken cancellationToken)
  {
    var measurements = eventArgs.Measurements;
    await mutations.CreateMeasurements(measurements, cancellationToken);
  }

  public override async Task BeforeStopAsync(
    CancellationToken cancellationToken
  )
  {
    var flushed = buffer.Flush(immediate: true);
    await mutations.CreateMeasurements(flushed, cancellationToken);
  }
}

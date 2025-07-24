using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.Base;
using Ozds.Business.Observers.EventArgs;
using Ozds.Jobs.Observers.Abstractions;
using Ozds.Jobs.Observers.EventArgs;

namespace Ozds.Business.Observers.Implementations;

public class JobsMeterJobRelay(
  IServiceProvider serviceProvider,
  IMeterJobSubscriber subscriber
) : Relay<
  MeterJobEventArgs,
  JobsMeterJobEventArgs,
  JobsMeterJobPipe>(
  serviceProvider
), IJobsMeterJobSubscriber
{
  protected override void SubscribeIn(
    EventHandler<MeterJobEventArgs> eventHandler)
  {
    subscriber.Subscribe(eventHandler);
  }

  protected override void UnsubscribeIn(
    EventHandler<MeterJobEventArgs> eventHandler)
  {
    subscriber.Unsubscribe(eventHandler);
  }
}

public class JobsMeterJobPipe
  : IPipe<MeterJobEventArgs, JobsMeterJobEventArgs>
{
  public Task<JobsMeterJobEventArgs> Transform(
    MeterJobEventArgs eventArgs,
    CancellationToken cancellationToken)
  {
    var modelEventArgs = new JobsMeterJobEventArgs
    {
      Id = eventArgs.Id,
      ScheduledAt = eventArgs.ScheduledAt,
      StartedAt = eventArgs.StartedAt,
      ScheduledFireAt = eventArgs.ScheduledFireAt,
      FiredAt = eventArgs.FiredAt,
      RefireCount = eventArgs.RefireCount
    };
    return Task.FromResult(modelEventArgs);
  }
}

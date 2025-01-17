using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.Base;
using Ozds.Business.Observers.EventArgs;
using Ozds.Jobs.Observers.Abstractions;
using Ozds.Jobs.Observers.EventArgs;

namespace Ozds.Business.Observers.Implementations;

public class JobsMessengerJobRelay(
  IServiceProvider serviceProvider,
  IMessengerJobSubscriber subscriber
) : Relay<
  MessengerJobEventArgs,
  JobsMessengerJobEventArgs,
  JobsMessengerJobPipe>(
  serviceProvider
), IJobsMessengerJobSubscriber
{
  protected override void SubscribeIn(
    EventHandler<MessengerJobEventArgs> eventHandler)
  {
    subscriber.Subscribe(eventHandler);
  }

  protected override void UnsubscribeIn(
    EventHandler<MessengerJobEventArgs> eventHandler)
  {
    subscriber.Unsubscribe(eventHandler);
  }
}

public class JobsMessengerJobPipe
  : IPipe<MessengerJobEventArgs, JobsMessengerJobEventArgs>
{
  public Task<JobsMessengerJobEventArgs> Transform(
    MessengerJobEventArgs eventArgs,
    CancellationToken cancellationToken)
  {
    var modelEventArgs = new JobsMessengerJobEventArgs
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

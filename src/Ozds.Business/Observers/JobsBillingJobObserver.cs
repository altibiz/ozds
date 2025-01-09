using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.Base;
using Ozds.Business.Observers.EventArgs;
using Ozds.Jobs.Observers.Abstractions;
using Ozds.Jobs.Observers.EventArgs;

namespace Ozds.Business.Observers;

public class JobsBillingJobRelay(
  IServiceProvider serviceProvider,
  IBillingJobSubscriber subscriber
) : Relay<
  BillingJobEventArgs,
  JobsBillingJobEventArgs,
  JobsBillingJobPipe>(serviceProvider),
  IJobsBillingJobSubscriber
{
  protected override void SubscribeIn(
    EventHandler<BillingJobEventArgs> eventHandler)
  {
    subscriber.Subscribe(eventHandler);
  }

  protected override void UnsubscribeIn(
    EventHandler<BillingJobEventArgs> eventHandler)
  {
    subscriber.Unsubscribe(eventHandler);
  }
}

public class JobsBillingJobPipe
  : IPipe<BillingJobEventArgs, JobsBillingJobEventArgs>
{
  public Task<JobsBillingJobEventArgs> Transform(
    BillingJobEventArgs eventArgs,
    CancellationToken cancellationToken)
  {
    var modelEventArgs = new JobsBillingJobEventArgs
    {
      NetworkUserId = eventArgs.NetworkUserId
    };
    return Task.FromResult(modelEventArgs);
  }
}

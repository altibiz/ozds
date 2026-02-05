using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.Base;
using Ozds.Business.Observers.EventArgs;
using Ozds.Jobs.Observers.Abstractions;
using Ozds.Jobs.Observers.EventArgs;

namespace Ozds.Business.Observers.Implementations;

public class JobsArchivalJobRelay(
  IServiceProvider serviceProvider,
  IArchivalJobSubscriber subscriber
)
  : Relay<ArchivalJobEventArgs, JobsArchivalJobEventArgs, JobsArchivalJobPipe>(
    serviceProvider
  ),
    IJobsArchivalJobSubscriber
{
  protected override void SubscribeIn(
    EventHandler<ArchivalJobEventArgs> eventHandler
  )
  {
    subscriber.Subscribe(eventHandler);
  }

  protected override void UnsubscribeIn(
    EventHandler<ArchivalJobEventArgs> eventHandler
  )
  {
    subscriber.Unsubscribe(eventHandler);
  }
}

public class JobsArchivalJobPipe
  : IPipe<ArchivalJobEventArgs, JobsArchivalJobEventArgs>
{
  public Task<JobsArchivalJobEventArgs> Transform(
    ArchivalJobEventArgs eventArgs,
    CancellationToken cancellationToken
  )
  {
    var modelEventArgs = new JobsArchivalJobEventArgs();
    return Task.FromResult(modelEventArgs);
  }
}

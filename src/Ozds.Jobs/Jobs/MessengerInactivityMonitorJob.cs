using Ozds.Jobs.Observers.Abstractions;
using Quartz;

namespace Ozds.Jobs;

public class MessengerInactivityMonitorJob(
  IMessengerJobPublisher messengerJobPublisher
) : IJob
{
  public string Id { get; set; } = default!;

  public Task Execute(IJobExecutionContext context)
  {
    messengerJobPublisher.PublishInactivity(Id);

    return Task.CompletedTask;
  }
}

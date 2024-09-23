using Ozds.Jobs.Observers.Abstractions;
using Ozds.Jobs.Observers.EventArgs;
using Quartz;

namespace Ozds.Jobs;

public class MessengerInactivityMonitorJob(
  IMessengerJobPublisher messengerJobPublisher
) : IJob
{
  public string Id { get; set; } = default!;

  public DateTimeOffset ScheduledAt { get; set; } = default!;

  public Task Execute(IJobExecutionContext context)
  {
    messengerJobPublisher.PublishInactivity(
      new MessengerInactivityEventArgs
      {
        Id = Id,
        ScheduledAt = ScheduledAt,
        StartedAt = context.Trigger.StartTimeUtc,
        ScheduledFireAt = context.ScheduledFireTimeUtc ?? default,
        FiredAt = context.FireTimeUtc,
      });

    return Task.CompletedTask;
  }
}

using Ozds.Jobs.Observers.Abstractions;
using Ozds.Jobs.Observers.EventArgs;
using Quartz;

namespace Ozds.Jobs;

public class MessengerInactivityMonitorJob(
  IMessengerJobPublisher messengerJobPublisher,
  ILogger<MessengerInactivityMonitorJob> logger
) : IJob
{
  public string Id { get; set; } = default!;

  public DateTimeOffset ScheduledAt { get; set; } = default!;

  public Task Execute(IJobExecutionContext context)
  {

    logger.LogDebug(
      "Executing {Group} job for {Id}",
      nameof(MessengerInactivityMonitorJob),
      Id
    );

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

using System.Text.Json;
using Ozds.Jobs.Observers.Abstractions;
using Ozds.Jobs.Observers.EventArgs;
using Quartz;

namespace Ozds.Jobs;

public class MeterInactivityMonitorJob(
  IMeterJobPublisher messengerJobPublisher,
  ILogger<MeterInactivityMonitorJob> logger
) : IJob
{
  private static readonly JsonSerializerOptions JsonSerializerOptions = new()
  {
    WriteIndented = true,
  };

  public string Id { get; set; } = default!;

  public DateTimeOffset ScheduledAt { get; set; } = default!;

  public Task Execute(IJobExecutionContext context)
  {
    var eventArgs = new MeterJobEventArgs
    {
      Id = Id,
      ScheduledAt = ScheduledAt,
      StartedAt = context.Trigger.StartTimeUtc,
      ScheduledFireAt = context.ScheduledFireTimeUtc ?? default,
      FiredAt = context.FireTimeUtc,
      RefireCount = context.RefireCount,
    };

    logger.LogDebug(
      "Executing job for {Id} with {EventArgs}",
      Id,
      JsonSerializer.Serialize(eventArgs, JsonSerializerOptions)
    );

    messengerJobPublisher.Publish(eventArgs);

    return Task.CompletedTask;
  }
}

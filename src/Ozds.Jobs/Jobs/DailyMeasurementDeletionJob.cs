using System.Text.Json;
using Ozds.Jobs.Observers.Abstractions;
using Ozds.Jobs.Observers.EventArgs;
using Quartz;

namespace Ozds.Jobs;

public class DailyMeasurementDeletionJob(
  IArchivalJobPublisher publisher,
  ILogger<DailyMeasurementDeletionJob> logger
) : IJob
{
  private static readonly JsonSerializerOptions JsonSerializerOptions = new()
  {
    WriteIndented = true
  };

  public DateTimeOffset ScheduledAt { get; set; } = default!;

  public Task Execute(IJobExecutionContext context)
  {
    var eventArgs = new ArchivalJobEventArgs
    {
      ScheduledAt = ScheduledAt,
      StartedAt = context.Trigger.StartTimeUtc,
      ScheduledFireAt = context.ScheduledFireTimeUtc ?? default,
      FiredAt = context.FireTimeUtc,
      RefireCount = context.RefireCount
    };

    logger.LogDebug(
      "Executing job for with {EventArgs}",
      JsonSerializer.Serialize(eventArgs, JsonSerializerOptions));

    publisher.Publish(eventArgs);

    return Task.CompletedTask;
  }
}

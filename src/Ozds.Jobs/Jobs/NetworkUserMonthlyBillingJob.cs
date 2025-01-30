using System.Text.Json;
using Ozds.Jobs.Observers.Abstractions;
using Ozds.Jobs.Observers.EventArgs;
using Quartz;

namespace Ozds.Jobs;

public class NetworkUserMonthlyBillingJob(
  IBillingJobPublisher messengerJobPublisher,
  ILogger<NetworkUserMonthlyBillingJob> logger
) : IJob
{
  private static readonly JsonSerializerOptions JsonSerializerOptions = new()
  {
    WriteIndented = true
  };

  public string NetworkUserId { get; set; } = default!;

  public DateTimeOffset ScheduledAt { get; set; } = default!;

  public Task Execute(IJobExecutionContext context)
  {
    var eventArgs = new BillingJobEventArgs
    {
      Id = NetworkUserId,
      ScheduledAt = ScheduledAt,
      StartedAt = context.Trigger.StartTimeUtc,
      ScheduledFireAt = context.ScheduledFireTimeUtc ?? default,
      FiredAt = context.FireTimeUtc,
      RefireCount = context.RefireCount
    };

    logger.LogDebug(
      "Executing job for {Id} with {EventArgs}",
      NetworkUserId,
      JsonSerializer.Serialize(eventArgs, JsonSerializerOptions));

    messengerJobPublisher.Publish(eventArgs);

    return Task.CompletedTask;
  }
}

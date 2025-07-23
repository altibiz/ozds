using System.Globalization;
using Ozds.Jobs.Manager.Abstractions;
using Ozds.Jobs.Managers.Base;
using Ozds.Time.Queries.Abstractions;
using Quartz;

namespace Ozds.Jobs.Managers.Implementations;

public sealed record MessengerJobContext(
  string MessengerId,
  TimeSpan InactivityDuration
);

public class MessengerJobManager(
  IServiceProvider serviceProvider,
  IClockQueries clock
) : JobManagerBase<MessengerJobContext>(serviceProvider), IMessengerJobManager
{
  public Task EnsureInactivityMonitorJob(
    MessengerInactivityMonitorDetails details,
    CancellationToken cancellationToken)
  {
    return Ensure(
      new MessengerJobContext(details.MessengerId, details.InactivityDuration),
      cancellationToken);
  }

  public Task EnsureInactivityMonitorJobs(
    IEnumerable<MessengerInactivityMonitorDetails> details,
    CancellationToken cancellationToken)
  {
    return Ensure(
      details.Select(
        details =>
          new MessengerJobContext(
            details.MessengerId,
            details.InactivityDuration)),
      cancellationToken);
  }

  public Task RescheduleInactivityMonitorJob(
    MessengerInactivityMonitorDetails details,
    CancellationToken cancellationToken)
  {
    return Reschedule(
      new MessengerJobContext(details.MessengerId, details.InactivityDuration),
      cancellationToken);
  }

  public Task RescheduleInactivityMonitorJobs(
    IEnumerable<MessengerInactivityMonitorDetails> details,
    CancellationToken cancellationToken)
  {
    return Reschedule(
      details.Select(
        details =>
          new MessengerJobContext(
            details.MessengerId,
            details.InactivityDuration)),
      cancellationToken);
  }

  public Task UnscheduleInactivityMonitorJob(
    string id,
    CancellationToken cancellationToken)
  {
    return Unschedule(
      new MessengerJobContext(id, TimeSpan.Zero),
      cancellationToken);
  }

  public Task UnscheduleInactivityMonitorJobs(
    IEnumerable<string> ids,
    CancellationToken cancellationToken)
  {
    return Unschedule(
      ids.Select(id => new MessengerJobContext(id, TimeSpan.Zero)),
      cancellationToken);
  }

  protected override IJobDetail CreateJob(MessengerJobContext context)
  {
    var now = clock.Now();

    return JobBuilder.Create<MessengerInactivityMonitorJob>()
      .UsingJobData(
        nameof(MessengerInactivityMonitorJob.Id),
        context.MessengerId)
      .UsingJobData(
        nameof(MessengerInactivityMonitorJob.ScheduledAt),
        now.ToString("o", CultureInfo.InvariantCulture))
      .Build();
  }

  protected override ITrigger CreateTrigger(
    TriggerBuilder builder,
    MessengerJobContext context)
  {
    var now = clock.Now();
    var startAt = now.Add(context.InactivityDuration);

    return builder
      .StartAt(startAt)
      .WithSimpleSchedule(
        x => x
          .WithMisfireHandlingInstructionNextWithExistingCount())
      .Build();
  }

  protected override IReadOnlyCollection<TriggerKey> CreateTriggerKeys(
    MessengerJobContext context)
  {
    return
    [
      new TriggerKey(
        context.MessengerId,
        nameof(MessengerInactivityMonitorJob)
      )
    ];
  }
}

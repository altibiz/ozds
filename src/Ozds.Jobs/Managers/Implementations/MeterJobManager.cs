using System.Globalization;
using Ozds.Jobs.Manager.Abstractions;
using Ozds.Jobs.Managers.Base;
using Ozds.Time.Queries.Abstractions;
using Quartz;

namespace Ozds.Jobs.Managers.Implementations;

public sealed record MeterJobContext(
  string MeterId,
  TimeSpan InactivityDuration
);

public class MeterJobManager(
  IServiceProvider serviceProvider,
  IClockQueries clock
) : JobManagerBase<MeterJobContext>(serviceProvider), IMeterJobManager
{
  public Task EnsureInactivityMonitorJob(
    MeterInactivityMonitorDetails details,
    CancellationToken cancellationToken)
  {
    return Ensure(
      new MeterJobContext(details.MeterId, details.InactivityDuration),
      cancellationToken);
  }

  public Task EnsureInactivityMonitorJobs(
    IEnumerable<MeterInactivityMonitorDetails> details,
    CancellationToken cancellationToken)
  {
    return Ensure(
      details.Select(details =>
        new MeterJobContext(
          details.MeterId,
          details.InactivityDuration)),
      cancellationToken);
  }

  public Task RescheduleInactivityMonitorJob(
    MeterInactivityMonitorDetails details,
    CancellationToken cancellationToken)
  {
    return Reschedule(
      new MeterJobContext(details.MeterId, details.InactivityDuration),
      cancellationToken);
  }

  public Task RescheduleInactivityMonitorJobs(
    IEnumerable<MeterInactivityMonitorDetails> details,
    CancellationToken cancellationToken)
  {
    return Reschedule(
      details.Select(details =>
        new MeterJobContext(
          details.MeterId,
          details.InactivityDuration)),
      cancellationToken);
  }

  public Task UnscheduleInactivityMonitorJob(
    string id,
    CancellationToken cancellationToken)
  {
    return Unschedule(
      new MeterJobContext(id, TimeSpan.Zero),
      cancellationToken);
  }

  public Task UnscheduleInactivityMonitorJobs(
    IEnumerable<string> ids,
    CancellationToken cancellationToken)
  {
    return Unschedule(
      ids.Select(id => new MeterJobContext(id, TimeSpan.Zero)),
      cancellationToken);
  }

  protected override IJobDetail CreateJob(MeterJobContext context)
  {
    var now = clock.Now();

    return JobBuilder.Create<MeterInactivityMonitorJob>()
      .UsingJobData(
        nameof(MeterInactivityMonitorJob.Id),
        context.MeterId)
      .UsingJobData(
        nameof(MeterInactivityMonitorJob.ScheduledAt),
        now.ToString("o", CultureInfo.InvariantCulture))
      .Build();
  }

  protected override ITrigger CreateTrigger(
    TriggerBuilder builder,
    MeterJobContext context)
  {
    var now = clock.Now();
    var startAt = now.Add(context.InactivityDuration);

    return builder
      .StartAt(startAt)
      .WithSimpleSchedule(x => x
        .WithMisfireHandlingInstructionNextWithExistingCount())
      .Build();
  }

  protected override IReadOnlyCollection<TriggerKey> CreateTriggerKeys(
    MeterJobContext context
  )
  {
    return
    [
      new TriggerKey(
        context.MeterId,
        nameof(MeterInactivityMonitorJob)
      )
    ];
  }
}

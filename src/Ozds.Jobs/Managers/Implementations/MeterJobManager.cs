using System.Globalization;
using Ozds.Jobs.Manager.Abstractions;
using Ozds.Jobs.Scheduler;
using Ozds.Time.Queries.Abstractions;
using Quartz;

namespace Ozds.Jobs.Managers.Implementations;

public class MeterJobManager(
  OzdsSchedulerFactory schedulerFactory,
  ILogger<MeterJobManager> logger,
  IClockQueries clock
) : IMeterJobManager
{
  public async Task EnsureInactivityMonitorJob(
    string id,
    TimeSpan inactivityDuration,
    CancellationToken cancellationToken)
  {
    logger.LogDebug(
      "Ensuring {Group} job for {Id} with inactivity duration {Duration}",
      nameof(MeterInactivityMonitorJob),
      id,
      inactivityDuration
    );

    var scheduler = await schedulerFactory.GetScheduler(cancellationToken);

    var triggerKey = new TriggerKey(id, nameof(MeterInactivityMonitorJob));
    if (!await scheduler.CheckExists(triggerKey, cancellationToken))
    {
      var job = CreateJob(id);
      var trigger = CreateTrigger(id, inactivityDuration);
      await scheduler.ScheduleJob(job, trigger, cancellationToken);
    }
  }

  public async Task RescheduleInactivityMonitorJob(
    string id,
    TimeSpan inactivityDuration,
    CancellationToken cancellationToken)
  {
    logger.LogDebug(
      "Rescheduling {Group} job for {Id} with inactivity duration {Duration}",
      nameof(MeterInactivityMonitorJob),
      id,
      inactivityDuration
    );

    var scheduler = await schedulerFactory.GetScheduler(cancellationToken);

    var triggerKey = new TriggerKey(id, nameof(MeterInactivityMonitorJob));
    var trigger = CreateTrigger(id, inactivityDuration);

    if (await scheduler.CheckExists(triggerKey, cancellationToken))
    {
      await scheduler.UnscheduleJob(triggerKey, cancellationToken);
      var job = CreateJob(id);
      await scheduler.ScheduleJob(job, trigger, cancellationToken);
    }
    else
    {
      var job = CreateJob(id);
      await scheduler.ScheduleJob(job, trigger, cancellationToken);
    }
  }

  public async Task UnscheduleInactivityMonitorJob(
    string id,
    CancellationToken cancellationToken)
  {
    logger.LogDebug(
      "Unscheduling {Group} job for {Id}",
      nameof(MeterInactivityMonitorJob),
      id
    );

    var scheduler = await schedulerFactory.GetScheduler(cancellationToken);

    var triggerKey = new TriggerKey(id, nameof(MeterInactivityMonitorJob));

    if (await scheduler.CheckExists(triggerKey, cancellationToken))
    {
      await scheduler.UnscheduleJob(triggerKey, cancellationToken);
    }
  }

  private IJobDetail CreateJob(string id)
  {
    var now = clock.Now();
    return JobBuilder.Create<MeterInactivityMonitorJob>()
      .WithIdentity(id, nameof(MeterInactivityMonitorJob))
      .UsingJobData(nameof(MeterInactivityMonitorJob.Id), id)
      .UsingJobData(
        nameof(MeterInactivityMonitorJob.ScheduledAt),
        now.ToString("o", CultureInfo.InvariantCulture))
      .Build();
  }

  private ITrigger CreateTrigger(string id, TimeSpan inactivityDuration)
  {
    var now = clock.Now();
    var startAt = now.Add(inactivityDuration);

    logger.LogDebug(
      "{Now} Creating trigger for {Group} job"
      + " for {Id} with inactivity duration {Duration}"
      + " starting at {StartAt}",
      now,
      nameof(MeterInactivityMonitorJob),
      id,
      inactivityDuration,
      startAt
    );

    return TriggerBuilder.Create()
      .WithIdentity(id, nameof(MeterInactivityMonitorJob))
      .ForJob(id, nameof(MeterInactivityMonitorJob))
      .StartAt(startAt)
      .WithSimpleSchedule(
        x => x
          .WithMisfireHandlingInstructionNextWithExistingCount())
      .Build();
  }
}

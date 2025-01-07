using System.Globalization;
using Ozds.Jobs.Manager.Abstractions;
using Quartz;

namespace Ozds.Jobs.Managers;

public class MessengerJobManager(
  ISchedulerFactory schedulerFactory,
  ILogger<MessengerJobManager> logger
) : IMessengerJobManager
{
  public async Task EnsureInactivityMonitorJob(
    string id,
    TimeSpan inactivityDuration,
    CancellationToken cancellationToken)
  {
    logger.LogDebug(
      "Ensuring {Group} job for {Id} with inactivity duration {Duration}",
      nameof(MessengerInactivityMonitorJob),
      id,
      inactivityDuration
    );

    var scheduler = await schedulerFactory.GetScheduler(cancellationToken);

    var triggerKey = new TriggerKey(id, nameof(MessengerInactivityMonitorJob));
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
      nameof(MessengerInactivityMonitorJob),
      id,
      inactivityDuration
    );

    var scheduler = await schedulerFactory.GetScheduler(cancellationToken);

    var triggerKey = new TriggerKey(id, nameof(MessengerInactivityMonitorJob));
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
      nameof(MessengerInactivityMonitorJob),
      id
    );

    var scheduler = await schedulerFactory.GetScheduler(cancellationToken);

    var triggerKey = new TriggerKey(id, nameof(MessengerInactivityMonitorJob));

    if (await scheduler.CheckExists(triggerKey, cancellationToken))
    {
      await scheduler.UnscheduleJob(triggerKey, cancellationToken);
    }
  }

  private IJobDetail CreateJob(string id)
  {
    return JobBuilder.Create<MessengerInactivityMonitorJob>()
      .WithIdentity(id, nameof(MessengerInactivityMonitorJob))
      .UsingJobData(nameof(MessengerInactivityMonitorJob.Id), id)
      .UsingJobData(
        nameof(MessengerInactivityMonitorJob.ScheduledAt),
        DateTimeOffset.UtcNow.ToString("o", CultureInfo.InvariantCulture))
      .Build();
  }

  private ITrigger CreateTrigger(string id, TimeSpan inactivityDuration)
  {
    var now = DateTimeOffset.UtcNow;
    var startAt = now.Add(inactivityDuration);

    logger.LogDebug(
      "{Now} Creating trigger for {Group} job"
      + " for {Id} with inactivity duration {Duration}"
      + " starting at {StartAt}",
      now,
      nameof(MessengerInactivityMonitorJob),
      id,
      inactivityDuration,
      startAt
    );

    return TriggerBuilder.Create()
      .WithIdentity(id, nameof(MessengerInactivityMonitorJob))
      .ForJob(id, nameof(MessengerInactivityMonitorJob))
      .StartAt(startAt)
      .WithSimpleSchedule(
        x => x
          .WithMisfireHandlingInstructionNextWithExistingCount())
      .Build();
  }
}

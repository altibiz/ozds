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
    TimeSpan inactivityDuration)
  {
    logger.LogDebug(
      "Ensuring {Group} job for {Id} with inactivity duration {Duration}",
      nameof(MessengerInactivityMonitorJob),
      id,
      inactivityDuration
    );

    var scheduler = await schedulerFactory.GetScheduler();

    var triggerKey = new TriggerKey(id, nameof(MessengerInactivityMonitorJob));
    if (!await scheduler.CheckExists(triggerKey))
    {
      var job = CreateJob(id);
      var trigger = CreateTrigger(id, inactivityDuration);
      await scheduler.ScheduleJob(job, trigger);
    }
  }

  public async Task RescheduleInactivityMonitorJob(
    string id,
    TimeSpan inactivityDuration)
  {
    logger.LogDebug(
      "Rescheduling {Group} job for {Id} with inactivity duration {Duration}",
      nameof(MessengerInactivityMonitorJob),
      id,
      inactivityDuration
    );

    var scheduler = await schedulerFactory.GetScheduler();

    var triggerKey = new TriggerKey(id, nameof(MessengerInactivityMonitorJob));
    var trigger = CreateTrigger(id, inactivityDuration);

    if (await scheduler.CheckExists(triggerKey))
    {
      await scheduler.UnscheduleJob(triggerKey);
      var job = CreateJob(id);
      await scheduler.ScheduleJob(job, trigger);
    }
    else
    {
      var job = CreateJob(id);
      await scheduler.ScheduleJob(job, trigger);
    }
  }

  public async Task UnscheduleInactivityMonitorJob(string id)
  {
    logger.LogDebug(
      "Unscheduling {Group} job for {Id}",
      nameof(MessengerInactivityMonitorJob),
      id
    );

    var scheduler = await schedulerFactory.GetScheduler();

    var triggerKey = new TriggerKey(id, nameof(MessengerInactivityMonitorJob));

    if (await scheduler.CheckExists(triggerKey))
    {
      await scheduler.UnscheduleJob(triggerKey);
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

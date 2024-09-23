using System.Globalization;
using Ozds.Jobs.Manager.Abstractions;
using Quartz;

namespace Ozds.Jobs.Managers;

public class MessengerJobManager(ISchedulerFactory schedulerFactory)
  : IMessengerJobManager
{
  public async Task EnsureInactivityMonitorJob(
    string id,
    TimeSpan inactivityDuration)
  {
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
    var scheduler = await schedulerFactory.GetScheduler();

    var triggerKey = new TriggerKey(id, nameof(MessengerInactivityMonitorJob));
    var trigger = CreateTrigger(id, inactivityDuration);

    if (await scheduler.CheckExists(triggerKey))
    {
      await scheduler.RescheduleJob(triggerKey, trigger);
    }
    else
    {
      var job = CreateJob(id);
      await scheduler.ScheduleJob(job, trigger);
    }
  }

  public async Task UnscheduleInactivityMonitorJob(string id)
  {
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
    var interval = (int)inactivityDuration.TotalSeconds * 6;
    return TriggerBuilder.Create()
      .WithIdentity(id, nameof(MessengerInactivityMonitorJob))
      .ForJob(id, nameof(MessengerInactivityMonitorJob))
      .StartAt(DateTimeOffset.UtcNow.Add(inactivityDuration))
      .WithSimpleSchedule(x => x
        .WithIntervalInSeconds(interval)
        .WithRepeatCount(3)
        .WithMisfireHandlingInstructionFireNow())
      .Build();
  }
}

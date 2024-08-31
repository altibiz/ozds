using Ozds.Jobs.Manager.Abstractions;
using Quartz;

namespace Ozds.Jobs.Managers
{
  public class MessengerJobManager(ISchedulerFactory schedulerFactory) : IMessengerJobManager
  {
    public async Task EnsureInactivityMonitorJob(string id, TimeSpan inactivityDuration)
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

    public async Task RescheduleInactivityMonitorJob(string id, TimeSpan inactivityDuration)
    {
      var scheduler = await schedulerFactory.GetScheduler();

      var triggerKey = new TriggerKey(id, nameof(MessengerInactivityMonitorJob));

      if (await scheduler.CheckExists(triggerKey))
      {
        var trigger = CreateTrigger(id, inactivityDuration);
        await scheduler.RescheduleJob(triggerKey, trigger);
      }
      else
      {
        await EnsureInactivityMonitorJob(id, inactivityDuration);
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
        .Build();
    }

    private ITrigger CreateTrigger(string id, TimeSpan inactivityDuration)
    {
      return TriggerBuilder.Create()
        .WithIdentity(id, nameof(MessengerInactivityMonitorJob))
        .ForJob(id, nameof(MessengerInactivityMonitorJob))
        .StartAt(DateBuilder.FutureDate((int)inactivityDuration.TotalMilliseconds, IntervalUnit.Millisecond))
        .WithSimpleSchedule(x => x.WithMisfireHandlingInstructionFireNow())
        .Build();
    }
  }
}

using Ozds.Jobs.Manager.Abstractions;
using Quartz;

namespace Ozds.Jobs.Managers
{
  public class MessengerJobManager(ISchedulerFactory schedulerFactory) : IMessengerJobManager
  {
    public async Task EnsureInactivityMonitorJob(string id, TimeSpan inactivityDuration)
    {
      var scheduler = await schedulerFactory.GetScheduler();

      var jobKey = new JobKey(id, nameof(MessengerInactivityMonitorJob));

      if (!await scheduler.CheckExists(jobKey))
      {
        var jobDetail = CreateJobDetail(id);
        var trigger = CreateTrigger(id, inactivityDuration);

        await scheduler.ScheduleJob(jobDetail, trigger);
      }
    }

    public async Task RefreshInactivityMonitorJob(string id, TimeSpan inactivityDuration)
    {
      var scheduler = await schedulerFactory.GetScheduler();

      var jobKey = new JobKey(id, nameof(MessengerInactivityMonitorJob));

      if (await scheduler.CheckExists(jobKey))
      {
        var trigger = CreateTrigger(id, inactivityDuration);
        await scheduler.RescheduleJob(new TriggerKey(id, nameof(MessengerInactivityMonitorJob)), trigger);
      }
      else
      {
        await EnsureInactivityMonitorJob(id, inactivityDuration);
      }
    }

    private IJobDetail CreateJobDetail(string id)
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
        .StartAt(DateBuilder.FutureDate((int)inactivityDuration.TotalMilliseconds, IntervalUnit.Millisecond))
        .WithSimpleSchedule(x => x.WithMisfireHandlingInstructionFireNow())
        .Build();
    }
  }
}

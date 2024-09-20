using Ozds.Jobs.Manager.Abstractions;
using Quartz;

namespace Ozds.Jobs.Managers;

public class BillingJobManager(ISchedulerFactory schedulerFactory)
  : INetworkUserBillingJobManager
{
  public async Task EnsureMonthlyBillingJob(
    string networkUserId)
  {
    var scheduler = await schedulerFactory.GetScheduler();

    var triggerKey = new TriggerKey(networkUserId, nameof(NetworkUserMonthlyBillingJob));
    if (!await scheduler.CheckExists(triggerKey))
    {
      var job = CreateJob(networkUserId);
      var trigger = CreateTrigger(networkUserId);

      await scheduler.ScheduleJob(job, trigger);
    }
  }

  public async Task RescheduleMonthlyBillingJob(string networkUserId)
  {
    var scheduler = await schedulerFactory.GetScheduler();

    var triggerKey = new TriggerKey(networkUserId, nameof(NetworkUserMonthlyBillingJob));

    if (await scheduler.CheckExists(triggerKey))
    {
      var trigger = CreateTrigger(networkUserId);
      await scheduler.RescheduleJob(triggerKey, trigger);
    }
    else
    {
      await EnsureMonthlyBillingJob(networkUserId);
    }
  }

  public async Task UnscheduleMonthlyBillingJob(string networkUserId)
  {
    var scheduler = await schedulerFactory.GetScheduler();

    var triggerKey = new TriggerKey(networkUserId, nameof(NetworkUserMonthlyBillingJob));

    if (await scheduler.CheckExists(triggerKey))
    {
      await scheduler.UnscheduleJob(triggerKey);
    }
  }

  private IJobDetail CreateJob(string id)
  {
    return JobBuilder.Create<NetworkUserMonthlyBillingJob>()
      .WithIdentity(id, nameof(NetworkUserMonthlyBillingJob))
      .UsingJobData(nameof(NetworkUserMonthlyBillingJob.NetworkUserId), id)
      .Build();
  }

  private ITrigger CreateTrigger(string id)
  {
    return TriggerBuilder.Create()
      .WithIdentity(id, nameof(NetworkUserMonthlyBillingJob))
      .ForJob(id, nameof(NetworkUserMonthlyBillingJob))
      // NOTE: at start of every month
      .WithCronSchedule("0 * * */1 * ?")
      .WithSimpleSchedule(x => x.WithMisfireHandlingInstructionFireNow())
      .Build();
  }
}

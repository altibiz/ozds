using Ozds.Jobs.Manager.Abstractions;
using Quartz;

namespace Ozds.Jobs.Managers;

public class BillingJobManager(ISchedulerFactory schedulerFactory)
  : IBillingJobManager
{
  public async Task EnsureMonthlyBillingJob(
    string networkUserId,
    CancellationToken cancellationToken)
  {
    var scheduler = await schedulerFactory.GetScheduler(cancellationToken);

    var triggerKey = new TriggerKey(
      networkUserId, nameof(NetworkUserMonthlyBillingJob));
    if (!await scheduler.CheckExists(triggerKey, cancellationToken))
    {
      var job = CreateJob(networkUserId);
      var trigger = CreateTrigger(networkUserId);

      await scheduler.ScheduleJob(job, trigger, cancellationToken);
    }
  }

  public async Task RescheduleMonthlyBillingJob(
    string networkUserId,
    CancellationToken cancellationToken
  )
  {
    var scheduler = await schedulerFactory.GetScheduler(cancellationToken);

    var triggerKey = new TriggerKey(
      networkUserId, nameof(NetworkUserMonthlyBillingJob));

    if (await scheduler.CheckExists(triggerKey, cancellationToken))
    {
      var trigger = CreateTrigger(networkUserId);
      await scheduler.RescheduleJob(triggerKey, trigger, cancellationToken);
    }
    else
    {
      await EnsureMonthlyBillingJob(networkUserId, cancellationToken);
    }
  }

  public async Task UnscheduleMonthlyBillingJob(
    string networkUserId,
    CancellationToken cancellationToken
  )
  {
    var scheduler = await schedulerFactory.GetScheduler(cancellationToken);

    var triggerKey = new TriggerKey(
      networkUserId, nameof(NetworkUserMonthlyBillingJob));

    if (await scheduler.CheckExists(triggerKey, cancellationToken))
    {
      await scheduler.UnscheduleJob(triggerKey, cancellationToken);
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
      .WithCronSchedule(
        "0 * * */1 * ?",
        x => x.WithMisfireHandlingInstructionFireAndProceed())
      .Build();
  }
}

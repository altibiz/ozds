using System.Globalization;
using Ozds.Jobs.Manager.Abstractions;
using Quartz;

namespace Ozds.Jobs.Managers.Implementations;

public class BillingJobManager(
  ISchedulerFactory schedulerFactory,
  ILogger<BillingJobManager> logger
)
  : IBillingJobManager
{
  public async Task EnsureMonthlyBillingJob(
    string networkUserId,
    CancellationToken cancellationToken)
  {
    logger.LogDebug(
      "Ensuring {Group} monthly billing job"
      + " for network user {NetworkUserId}",
      nameof(NetworkUserMonthlyBillingJob),
      networkUserId
    );

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
    logger.LogDebug(
      "Rescheduling {Group} monthly billing job"
      + " for network user {NetworkUserId}",
      nameof(NetworkUserMonthlyBillingJob),
      networkUserId
    );

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
    logger.LogDebug(
      "Unscheduling {Group} monthly billing job"
      + " for network user {NetworkUserId}",
      nameof(NetworkUserMonthlyBillingJob),
      networkUserId
    );

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
      .UsingJobData(
        nameof(NetworkUserMonthlyBillingJob.ScheduledAt),
        DateTimeOffset.UtcNow.ToString("o", CultureInfo.InvariantCulture))
      .Build();
  }

  private ITrigger CreateTrigger(string id)
  {
    var now = DateTimeOffset.UtcNow;

    logger.LogDebug(
      "{Now} Creating trigger for {Group} monthly billing job"
      + " for network user {NetworkUserId}",
      now,
      nameof(NetworkUserMonthlyBillingJob),
      id
    );

    return TriggerBuilder.Create()
      .WithIdentity(id, nameof(NetworkUserMonthlyBillingJob))
      .ForJob(id, nameof(NetworkUserMonthlyBillingJob))
      .WithCronSchedule(
        "0 0 0 1 * ?",
        x => x.WithMisfireHandlingInstructionFireAndProceed())
      .Build();
  }
}

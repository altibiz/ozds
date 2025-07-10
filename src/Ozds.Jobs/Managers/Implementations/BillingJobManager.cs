using System.Globalization;
using Microsoft.Extensions.Options;
using Ozds.Jobs.Manager.Abstractions;
using Ozds.Jobs.Options;
using Ozds.Jobs.Scheduler;
using Ozds.Time.Queries.Abstractions;
using Quartz;

namespace Ozds.Jobs.Managers.Implementations;

public class BillingJobManager(
  OzdsSchedulerFactory schedulerFactory,
  ILogger<BillingJobManager> logger,
  IClockQueries clock,
  ITimeQueries time,
  IOptions<OzdsJobsOptions> options
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
      nameof(MonthlyNetworkUserBillingJob),
      networkUserId
    );

    var scheduler = await schedulerFactory.GetScheduler(cancellationToken);

    var triggerKey = new TriggerKey(
      networkUserId, nameof(MonthlyNetworkUserBillingJob));
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
      nameof(MonthlyNetworkUserBillingJob),
      networkUserId
    );

    var scheduler = await schedulerFactory.GetScheduler(cancellationToken);

    var triggerKey = new TriggerKey(
      networkUserId, nameof(MonthlyNetworkUserBillingJob));

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
      nameof(MonthlyNetworkUserBillingJob),
      networkUserId
    );

    var scheduler = await schedulerFactory.GetScheduler(cancellationToken);

    var triggerKey = new TriggerKey(
      networkUserId, nameof(MonthlyNetworkUserBillingJob));

    if (await scheduler.CheckExists(triggerKey, cancellationToken))
    {
      await scheduler.UnscheduleJob(triggerKey, cancellationToken);
    }
  }

  private IJobDetail CreateJob(string id)
  {
    var now = clock.Now();

    return JobBuilder.Create<MonthlyNetworkUserBillingJob>()
      .WithIdentity(id, nameof(MonthlyNetworkUserBillingJob))
      .UsingJobData(nameof(MonthlyNetworkUserBillingJob.NetworkUserId), id)
      .UsingJobData(
        nameof(MonthlyNetworkUserBillingJob.ScheduledAt),
        now.ToString("o", CultureInfo.InvariantCulture))
      .Build();
  }

  private ITrigger CreateTrigger(string id)
  {
    var now = clock.Now();

    logger.LogDebug(
      "{Now} Creating trigger for {Group} monthly billing job"
      + " for network user {NetworkUserId}",
      now,
      nameof(MonthlyNetworkUserBillingJob),
      id
    );

    return TriggerBuilder.Create()
      .WithIdentity(id, nameof(MonthlyNetworkUserBillingJob))
      .ForJob(id, nameof(MonthlyNetworkUserBillingJob))
      .WithCronSchedule(
        options.Value.Billing.MonthlyBillingCron,
        x => x
          .WithMisfireHandlingInstructionFireAndProceed()
          .InTimeZone(time.CroatianTimeZone))
      .Build();
  }
}

using System.Globalization;
using Microsoft.Extensions.Options;
using Ozds.Jobs.Manager.Abstractions;
using Ozds.Jobs.Managers.Base;
using Ozds.Jobs.Options;
using Ozds.Time.Queries.Abstractions;
using Quartz;

namespace Ozds.Jobs.Managers.Implementations;

public sealed record BillingJobContext(string NetworkUserId);

public class BillingJobManager(
  IServiceProvider serviceProvider,
  IClockQueries clock,
  ITimeQueries time,
  IOptions<OzdsJobsOptions> options
) : JobManagerBase<BillingJobContext>(serviceProvider),
    IBillingJobManager
{
  public Task EnsureMonthlyBillingJob(
    string networkUserId,
    CancellationToken cancellationToken)
  {
    return Ensure(
      new BillingJobContext(networkUserId),
      cancellationToken);
  }

  public Task EnsureMonthlyBillingJobs(
    IEnumerable<string> networkUserIds,
    CancellationToken cancellationToken
  )
  {
    return Ensure(
      networkUserIds.Select(x => new BillingJobContext(x)),
      cancellationToken);
  }

  public Task RescheduleMonthlyBillingJob(
    string networkUserId,
    CancellationToken cancellationToken
  )
  {
    return Reschedule(
      new BillingJobContext(networkUserId),
      cancellationToken);
  }

  public Task RescheduleMonthlyBillingJobs(
    IEnumerable<string> networkUserIds,
    CancellationToken cancellationToken)
  {
    return Reschedule(
      networkUserIds.Select(x => new BillingJobContext(x)),
      cancellationToken);
  }

  public Task UnscheduleMonthlyBillingJob(
    string networkUserId,
    CancellationToken cancellationToken
  )
  {
    return Unschedule(
      new BillingJobContext(networkUserId),
      cancellationToken);
  }

  public Task UnscheduleMonthlyBillingJobs(
    IEnumerable<string> networkUserIds,
    CancellationToken cancellationToken
  )
  {
    return Unschedule(
      networkUserIds.Select(x => new BillingJobContext(x)),
      cancellationToken);
  }

  protected override IJobDetail CreateJob(BillingJobContext context)
  {
    var now = clock.Now();

    return JobBuilder.Create<MonthlyNetworkUserBillingJob>()
      .WithIdentity(
        context.NetworkUserId,
        nameof(MonthlyNetworkUserBillingJob))
      .UsingJobData(
        nameof(MonthlyNetworkUserBillingJob.NetworkUserId),
        context.NetworkUserId)
      .UsingJobData(
        nameof(MonthlyNetworkUserBillingJob.ScheduledAt),
        now.ToString("o", CultureInfo.InvariantCulture))
      .Build();
  }

  protected override IReadOnlyCollection<TriggerKey> CreateTriggerKeys(
    BillingJobContext context
  )
  {
    return [new TriggerKey(
      context.NetworkUserId,
      nameof(MonthlyNetworkUserBillingJob)
    )];
  }

  protected override ITrigger CreateTrigger(
    TriggerBuilder builder,
    BillingJobContext context
  )
  {
    return builder
     .WithCronSchedule(
       options.Value.Billing.MonthlyBillingCron,
       x => x
         .WithMisfireHandlingInstructionFireAndProceed()
         .InTimeZone(time.CroatianTimeZone))
     .Build();
  }
}

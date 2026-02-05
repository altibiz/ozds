using System.Globalization;
using Microsoft.Extensions.Options;
using Ozds.Jobs.Manager.Abstractions;
using Ozds.Jobs.Managers.Base;
using Ozds.Jobs.Options;
using Ozds.Time.Queries.Abstractions;
using Quartz;

namespace Ozds.Jobs.Managers.Implementations;

#pragma warning disable S2094 // Classes should not be empty
public sealed record ArchivalJobContext;
#pragma warning restore S2094 // Classes should not be empty

public class ArchivalJobManager(
  IServiceProvider serviceProvider,
  IClockQueries clock,
  ITimeQueries time,
  IOptions<OzdsJobsOptions> options
) : JobManagerBase<ArchivalJobContext>(serviceProvider), IArchivalJobManager
{
  public Task EnsureDailyMeasurementDeletionJob(
    CancellationToken cancellationToken
  )
  {
    return Ensure(new ArchivalJobContext(), cancellationToken);
  }

  public Task RescheduleDailyMeasurementDeletionJob(
    CancellationToken cancellationToken
  )
  {
    return Reschedule(new ArchivalJobContext(), cancellationToken);
  }

  public Task UnscheduleDailyMeasurementDeletionJob(
    CancellationToken cancellationToken
  )
  {
    return Unschedule(new ArchivalJobContext(), cancellationToken);
  }

  protected override IReadOnlyCollection<TriggerKey> CreateTriggerKeys(
    ArchivalJobContext context
  )
  {
    return
    [
      new TriggerKey(
        nameof(DailyMeasurementDeletionJob),
        nameof(DailyMeasurementDeletionJob)
      ),
    ];
  }

  protected override IJobDetail CreateJob(ArchivalJobContext context)
  {
    return JobBuilder
      .Create<DailyMeasurementDeletionJob>()
      .WithIdentity(
        nameof(DailyMeasurementDeletionJob),
        nameof(DailyMeasurementDeletionJob)
      )
      .UsingJobData(
        nameof(DailyMeasurementDeletionJob.ScheduledAt),
        clock.Now().ToString("o", CultureInfo.InvariantCulture)
      )
      .Build();
  }

  protected override ITrigger CreateTrigger(
    TriggerBuilder builder,
    ArchivalJobContext context
  )
  {
    return builder
      .WithCronSchedule(
        options.Value.Archival.DailyMeasurementDeletionCron,
        x =>
          x.WithMisfireHandlingInstructionFireAndProceed()
            .InTimeZone(time.CroatianTimeZone)
      )
      .Build();
  }
}

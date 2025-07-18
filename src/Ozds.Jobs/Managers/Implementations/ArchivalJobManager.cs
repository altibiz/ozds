using System.Globalization;
using Microsoft.Extensions.Options;
using Ozds.Jobs.Manager.Abstractions;
using Ozds.Jobs.Options;
using Ozds.Jobs.Scheduler;
using Ozds.Time.Queries.Abstractions;
using Quartz;

namespace Ozds.Jobs.Managers.Implementations;

public class ArchivalJobManager(
  OzdsSchedulerFactory schedulerFactory,
  ILogger<ArchivalJobManager> logger,
  IClockQueries clock,
  ITimeQueries time,
  IOptions<OzdsJobsOptions> options
)
  : IArchivalJobManager
{
  public async Task EnsureDailyMeasurementDeletionJob(
    CancellationToken cancellationToken)
  {
    var scheduler = await schedulerFactory.GetScheduler(cancellationToken);

    var triggerKey = CreateTriggerKey();

    logger.LogDebug(
      "Ensuring {Group} daily deletion job",
      triggerKey.Group
    );

    if (!await scheduler.CheckExists(triggerKey, cancellationToken))
    {
      var job = CreateJob();
      var trigger = CreateTrigger();

      await scheduler.ScheduleJob(job, trigger, cancellationToken);

      logger.LogDebug(
        "Created {Cron} trigger for {Group} firing at {Fire}",
        options.Value.Archival.DailyMeasurementDeletionCron,
        triggerKey.Group,
        trigger.GetNextFireTimeUtc()
      );
    }
  }

  public async Task RescheduleDailyMeasurementDeletionJob(
    CancellationToken cancellationToken
  )
  {
    var scheduler = await schedulerFactory.GetScheduler(cancellationToken);

    var triggerKey = CreateTriggerKey();

    logger.LogDebug(
      "Rescheduling {Group} daily deletion job",
      triggerKey.Group
    );

    if (await scheduler.CheckExists(triggerKey, cancellationToken))
    {
      var trigger = CreateTrigger();

      await scheduler.RescheduleJob(triggerKey, trigger, cancellationToken);

      logger.LogDebug(
        "Rescheduled {Cron} trigger for {Group} firing at {Fire}",
        options.Value.Archival.DailyMeasurementDeletionCron,
        triggerKey.Group,
        trigger.GetNextFireTimeUtc()
      );
    }
    else
    {
      await EnsureDailyMeasurementDeletionJob(cancellationToken);
    }
  }

  public async Task UnscheduleDailyMeasurementDeletionJob(
    CancellationToken cancellationToken
  )
  {
    var scheduler = await schedulerFactory.GetScheduler(cancellationToken);

    var triggerKey = CreateTriggerKey();

    logger.LogDebug(
      "Unscheduling {Group} daily deletion job",
      triggerKey.Group
    );

    if (await scheduler.CheckExists(triggerKey, cancellationToken))
    {
      await scheduler.UnscheduleJob(triggerKey, cancellationToken);

      logger.LogDebug(
        "Unscheduled {Cron} trigger for {Group}",
        options.Value.Archival.DailyMeasurementDeletionCron,
        triggerKey.Group
      );
    }
  }

  private IJobDetail CreateJob()
  {
    return JobBuilder.Create<DailyMeasurementDeletionJob>()
      .WithIdentity(CreateJobKey())
      .UsingJobData(
        nameof(DailyMeasurementDeletionJob.ScheduledAt),
        clock.Now().ToString("o", CultureInfo.InvariantCulture))
      .Build();
  }

  private ITrigger CreateTrigger()
  {
    return TriggerBuilder.Create()
      .WithIdentity(CreateTriggerKey())
      .ForJob(CreateJobKey())
      .StartNow()
      .WithCronSchedule(
        options.Value.Archival.DailyMeasurementDeletionCron,
        x => x
          .WithMisfireHandlingInstructionFireAndProceed()
          .InTimeZone(time.CroatianTimeZone))
      .Build();
  }

  private TriggerKey CreateTriggerKey()
  {
    return new TriggerKey(
      nameof(DailyMeasurementDeletionJob),
      nameof(DailyMeasurementDeletionJob));
  }

  private JobKey CreateJobKey()
  {
    return new JobKey(
      nameof(DailyMeasurementDeletionJob),
      nameof(DailyMeasurementDeletionJob));
  }
}

using System.Text.Json;
using Ozds.Jobs.Manager.Abstractions;
using Ozds.Jobs.Scheduler;
using Quartz;

namespace Ozds.Jobs.Managers.Base;

public abstract class JobManagerBase<TContext> : IJobManager
{
  protected JobManagerBase(IServiceProvider serviceProvider)
  {
    SchedulerFactory =
      serviceProvider.GetRequiredService<OzdsSchedulerFactory>();

    Logger = (
      serviceProvider.GetRequiredService(
        typeof(ILogger<>).MakeGenericType(GetType())
      ) as ILogger
    )!;
  }

  protected OzdsSchedulerFactory SchedulerFactory { get; init; }

  protected ILogger Logger { get; init; }

  protected async Task Ensure(
    TContext context,
    CancellationToken cancellationToken
  )
  {
    var triggerKeys = CreateTriggerKeys(context);

    var job = CreateJob(context);
    var triggers = CreateTriggers(context);

    try
    {
      var scheduler = await SchedulerFactory.GetScheduler(cancellationToken);

      await scheduler.ScheduleJob(job, triggers, false, cancellationToken);

      if (Logger.IsEnabled(LogLevel.Debug))
      {
        var json = ToJson(triggerKeys, triggers);

        Logger.LogDebug("Ensured job for {TriggerKeys}", json);
      }
    }
    catch (ObjectAlreadyExistsException ex)
    {
      if (Logger.IsEnabled(LogLevel.Debug))
      {
        var json = ToJson(triggerKeys, triggers);
        Logger.LogDebug(ex, "Job already exists for {TriggerKeys}", json);
      }
    }
    catch (JobPersistenceException ex)
    {
      if (Logger.IsEnabled(LogLevel.Debug))
      {
        var json = ToJson(triggerKeys, triggers);
        Logger.LogDebug(ex, "Job already exists for {TriggerKeys}", json);
      }
    }
  }

  protected async Task Ensure(
    IEnumerable<TContext> contexts,
    CancellationToken cancellationToken
  )
  {
    var triggerKeys = contexts.Select(CreateTriggerKeys).ToList();

    var jobsWithTriggers = contexts
      .Zip(triggerKeys)
      .Select(x =>
      {
        var (context, triggerKeys) = x;
        return new
        {
          Job = CreateJob(context),
          Triggers = CreateTriggers(context, triggerKeys),
        };
      })
      .ToDictionary(x => x.Job, x => x.Triggers)
      .AsReadOnly();

    try
    {
      var scheduler = await SchedulerFactory.GetScheduler(cancellationToken);

      await scheduler.ScheduleJobs(jobsWithTriggers, false, cancellationToken);

      if (Logger.IsEnabled(LogLevel.Debug))
      {
        var json = ToJson(triggerKeys, jobsWithTriggers);
        Logger.LogDebug("Ensured jobs for {TriggerKeys}", json);
      }
    }
    catch (ObjectAlreadyExistsException ex)
    {
      if (Logger.IsEnabled(LogLevel.Debug))
      {
        var json = ToJson(triggerKeys, jobsWithTriggers);
        Logger.LogDebug(ex, "Jobs already exist for {TriggerKeys}", json);
      }
    }
    catch (JobPersistenceException ex)
    {
      if (Logger.IsEnabled(LogLevel.Debug))
      {
        var json = ToJson(triggerKeys, jobsWithTriggers);
        Logger.LogDebug(ex, "Jobs already exists for {TriggerKeys}", json);
      }
    }
  }

  public async Task Reschedule(
    TContext context,
    CancellationToken cancellationToken
  )
  {
    var triggerKeys = CreateTriggerKeys(context);

    var job = CreateJob(context);
    var triggers = CreateTriggers(context);

    var scheduler = await SchedulerFactory.GetScheduler(cancellationToken);

    await scheduler.ScheduleJob(job, triggers, true, cancellationToken);

    if (Logger.IsEnabled(LogLevel.Debug))
    {
      var json = ToJson(triggerKeys, triggers);
      Logger.LogDebug("Rescheduled job for {TriggerKeys}", json);
    }
  }

  protected async Task Reschedule(
    IEnumerable<TContext> contexts,
    CancellationToken cancellationToken
  )
  {
    var triggerKeys = contexts.Select(CreateTriggerKeys).ToList();

    var jobsWithTriggers = contexts
      .Zip(triggerKeys)
      .Select(x =>
      {
        var (context, triggerKeys) = x;
        return new
        {
          Job = CreateJob(context),
          Triggers = CreateTriggers(context, triggerKeys),
        };
      })
      .ToDictionary(x => x.Job, x => x.Triggers)
      .AsReadOnly();

    var scheduler = await SchedulerFactory.GetScheduler(cancellationToken);

    await scheduler.ScheduleJobs(jobsWithTriggers, true, cancellationToken);

    if (Logger.IsEnabled(LogLevel.Debug))
    {
      var json = ToJson(triggerKeys, jobsWithTriggers);
      Logger.LogDebug("Rescheduled jobs for {TriggerKeys}", json);
    }
  }

  public async Task Unschedule(
    TContext context,
    CancellationToken cancellationToken
  )
  {
    var triggerKeys = CreateTriggerKeys(context);

    var job = CreateJob(context);
    var triggers = CreateTriggers(context);

    var scheduler = await SchedulerFactory.GetScheduler(cancellationToken);

    await scheduler.ScheduleJob(job, triggers, true, cancellationToken);

    if (Logger.IsEnabled(LogLevel.Debug))
    {
      var json = ToJson(triggerKeys, triggers);
      Logger.LogDebug("Unscheduled job for {TriggerKeys}", json);
    }
  }

  protected async Task Unschedule(
    IEnumerable<TContext> contexts,
    CancellationToken cancellationToken
  )
  {
    var triggerKeys = contexts.SelectMany(CreateTriggerKeys).ToList();

    var scheduler = await SchedulerFactory.GetScheduler(cancellationToken);

    await scheduler.UnscheduleJobs(triggerKeys, cancellationToken);

    if (Logger.IsEnabled(LogLevel.Debug))
    {
      var json = JsonSerializer.Serialize(
        triggerKeys,
        JobManagerBaseExtensions.JsonOptions
      );

      Logger.LogDebug("Unscheduled jobs for {TriggerKeys}", json);
    }
  }

  protected IReadOnlyCollection<ITrigger> CreateTriggers(
    TContext context,
    IReadOnlyCollection<TriggerKey>? triggerKeys = null
  )
  {
    triggerKeys ??= CreateTriggerKeys(context);
    return triggerKeys
      .Select(key =>
        CreateTrigger(TriggerBuilder.Create().WithIdentity(key), context)
      )
      .ToList();
  }

  protected abstract IReadOnlyCollection<TriggerKey> CreateTriggerKeys(
    TContext context
  );

  protected abstract IJobDetail CreateJob(TContext context);

  protected abstract ITrigger CreateTrigger(
    TriggerBuilder builder,
    TContext context
  );

  private static string ToJson(
    List<IReadOnlyCollection<TriggerKey>> triggerKeys,
    IReadOnlyCollection<
      KeyValuePair<IJobDetail, IReadOnlyCollection<ITrigger>>
    > jobsWithTriggers
  )
  {
    var json = JsonSerializer.Serialize(
      triggerKeys
        .Zip(jobsWithTriggers)
        .Select(x =>
        {
          var (triggerKeys, jobWithTriggers) = x;

          var firstTriggerKey = triggerKeys.First();
          var firstTrigger = jobWithTriggers.Value.First();

          return new
          {
            firstTriggerKey.Group,
            firstTriggerKey.Name,
            Fire = firstTrigger.GetNextFireTimeUtc(),
          };
        }),
      JobManagerBaseExtensions.JsonOptions
    );

    return json;
  }

  private static string ToJson(
    IReadOnlyCollection<TriggerKey> triggerKeys,
    IReadOnlyCollection<ITrigger> triggers
  )
  {
    var firstTriggerKey = triggerKeys.First();
    var firstTrigger = triggers.First();
    var json = JsonSerializer.Serialize(
      new
      {
        firstTriggerKey.Group,
        firstTriggerKey.Name,
        Fire = firstTrigger.GetNextFireTimeUtc(),
      },
      JobManagerBaseExtensions.JsonOptions
    );

    return json;
  }
}

public static class JobManagerBaseExtensions
{
  public static readonly JsonSerializerOptions JsonOptions = new()
  {
    WriteIndented = true,
  };
}

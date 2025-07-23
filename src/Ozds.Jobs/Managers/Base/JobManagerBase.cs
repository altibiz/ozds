using System.Text.Json;
using Ozds.Jobs.Manager.Abstractions;
using Ozds.Jobs.Scheduler;
using Quartz;

namespace Ozds.Jobs.Managers.Base;

public abstract class JobManagerBase<TContext>
  : IJobManager
{
  protected OzdsSchedulerFactory SchedulerFactory { get; init; }

  protected ILogger Logger { get; init; }

  protected JobManagerBase(
    IServiceProvider serviceProvider
  )
  {
    SchedulerFactory = serviceProvider
      .GetRequiredService<OzdsSchedulerFactory>();

    Logger = (serviceProvider
    .GetRequiredService(typeof(ILogger<>).MakeGenericType(GetType()))
      as ILogger)!;
  }

  protected async Task Ensure(
    TContext context,
    CancellationToken cancellationToken)
  {
    var triggerKeys = CreateTriggerKeys(context);
    var triggerKeysJson = JsonSerializer.Serialize(
      triggerKeys,
      JobManagerBaseExtensions.JsonOptions
    );

    Logger.LogDebug(
      "Ensuring job for {TriggerKeys}",
      triggerKeysJson
    );

    var job = CreateJob(context);
    var triggers = CreateTriggers(context);

    try
    {
      var scheduler = await SchedulerFactory.GetScheduler(cancellationToken);

      await scheduler.ScheduleJob(
        job,
        triggers,
        replace: false,
        cancellationToken
      );
    }
    catch (ObjectAlreadyExistsException ex)
    {
      Logger.LogDebug(
        ex,
        "Job already exists for {TriggerKeys}",
        triggerKeysJson
      );
    }
  }

  protected async Task Ensure(
    IEnumerable<TContext> contexts,
    CancellationToken cancellationToken)
  {
    var triggerKeys = contexts.Select(CreateTriggerKeys).ToList();
    var triggerKeysJson = JsonSerializer.Serialize(
      triggerKeys,
      JobManagerBaseExtensions.JsonOptions
    );

    Logger.LogDebug(
      "Ensuring jobs for {TriggerKeys}",
      triggerKeysJson
    );

    var jobsWithTriggers = contexts
      .Zip(triggerKeys)
      .Select(x => {
        var (context, triggerKeys) = x;
        return new
        {
          Job = CreateJob(context),
          Triggers = CreateTriggers(context, triggerKeys)
        };
      })
      .ToDictionary(x => x.Job, x => x.Triggers)
      .AsReadOnly();

    try
    {
      var scheduler = await SchedulerFactory.GetScheduler(cancellationToken);

      await scheduler.ScheduleJobs(
        jobsWithTriggers,
        replace: false,
        cancellationToken
      );
    }
    catch (ObjectAlreadyExistsException ex)
    {
      Logger.LogDebug(
        ex,
        "Jobs already exist for {TriggerKeys}",
        triggerKeysJson
      );
    }
  }

  public async Task Reschedule(
    TContext context,
    CancellationToken cancellationToken)
  {
    var triggerKeys = CreateTriggerKeys(context);
    var triggerKeysJson = JsonSerializer.Serialize(
      triggerKeys,
      JobManagerBaseExtensions.JsonOptions
    );

    Logger.LogDebug(
      "Ensuring job for {TriggerKeys}",
      triggerKeysJson
    );

    var job = CreateJob(context);
    var triggers = CreateTriggers(context);

    var scheduler = await SchedulerFactory.GetScheduler(cancellationToken);

    await scheduler.ScheduleJob(
      job,
      triggers,
      replace: true,
      cancellationToken
    );
  }

  protected async Task Reschedule(
    IEnumerable<TContext> contexts,
    CancellationToken cancellationToken)
  {
    var triggerKeys = contexts.Select(CreateTriggerKeys).ToList();
    var triggerKeysJson = JsonSerializer.Serialize(
      triggerKeys,
      JobManagerBaseExtensions.JsonOptions
    );

    Logger.LogDebug(
      "Ensuring jobs for {TriggerKeys}",
      triggerKeysJson
    );

    var jobsWithTriggers = contexts
      .Zip(triggerKeys)
      .Select(x => {
        var (context, triggerKeys) = x;
        return new
        {
          Job = CreateJob(context),
          Triggers = CreateTriggers(context, triggerKeys)
        };
      })
      .ToDictionary(x => x.Job, x => x.Triggers)
      .AsReadOnly();

    var scheduler = await SchedulerFactory.GetScheduler(cancellationToken);

    await scheduler.ScheduleJobs(
      jobsWithTriggers,
      replace: true,
      cancellationToken
    );
  }

  public async Task Unschedule(
    TContext context,
    CancellationToken cancellationToken)
  {
    var triggerKeys = CreateTriggerKeys(context);
    var triggerKeysJson = JsonSerializer.Serialize(
      triggerKeys,
      JobManagerBaseExtensions.JsonOptions
    );

    Logger.LogDebug(
      "Ensuring job for {TriggerKeys}",
      triggerKeysJson
    );

    var job = CreateJob(context);
    var triggers = CreateTriggers(context);

    var scheduler = await SchedulerFactory.GetScheduler(cancellationToken);

    await scheduler.ScheduleJob(
      job,
      triggers,
      replace: true,
      cancellationToken
    );
  }

  protected async Task Unschedule(
    IEnumerable<TContext> contexts,
    CancellationToken cancellationToken)
  {
    var triggerKeys = contexts.SelectMany(CreateTriggerKeys).ToList();
    var triggerKeysJson = JsonSerializer.Serialize(
      triggerKeys,
      JobManagerBaseExtensions.JsonOptions
    );

    Logger.LogDebug(
      "Ensuring jobs for {TriggerKeys}",
      triggerKeysJson
    );

    var scheduler = await SchedulerFactory.GetScheduler(cancellationToken);

    await scheduler.UnscheduleJobs(
      triggerKeys,
      cancellationToken
    );
  }

  protected IReadOnlyCollection<ITrigger> CreateTriggers(
    TContext context,
    IReadOnlyCollection<TriggerKey>? triggerKeys = null
  )
  {
    triggerKeys ??= CreateTriggerKeys(context);
    return triggerKeys
      .Select(key => CreateTrigger(
        TriggerBuilder.Create().WithIdentity(key),
        context))
      .ToList();
  }

  protected abstract IReadOnlyCollection<TriggerKey> CreateTriggerKeys(
    TContext context);

  protected abstract IJobDetail CreateJob(TContext context);

  protected abstract ITrigger CreateTrigger(
    TriggerBuilder builder,
    TContext context
  );
}

public static class JobManagerBaseExtensions
{
  public static readonly JsonSerializerOptions JsonOptions = new()
  {
    WriteIndented = true
  };
}

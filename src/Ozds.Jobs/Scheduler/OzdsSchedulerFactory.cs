using Quartz;
using Quartz.Logging;

namespace Ozds.Jobs.Scheduler;

public class OzdsSchedulerFactory(
  ISchedulerFactory schedulerFactory,
  ILoggerFactory loggerFactory
) : IHostedService
{
  private readonly SemaphoreSlim @lock = new(1, 1);

  private IScheduler? inner;

  public Task StartAsync(CancellationToken cancellationToken)
  {
    LogProvider.SetCurrentLogProvider(
      new QuartzAspNetCoreLogProvider(loggerFactory));

    return Task.CompletedTask;
  }

  public async Task StopAsync(CancellationToken cancellationToken)
  {
    var schedulers = await schedulerFactory.GetAllSchedulers(cancellationToken);

    foreach (var scheduler in schedulers)
    {
      await scheduler.Shutdown(cancellationToken);
    }
  }

  public async Task<IScheduler> GetScheduler(
    CancellationToken cancellationToken
  )
  {
    await @lock.WaitAsync(cancellationToken);

    try
    {
      if (inner == null)
      {
        inner = await schedulerFactory.GetScheduler(cancellationToken);

        if (inner.InStandbyMode)
        {
          await inner.Start(cancellationToken);
        }
      }

      return inner;
    }
    finally
    {
      @lock.Release();
    }
  }

  private sealed class QuartzAspNetCoreLogProvider(
    ILoggerFactory loggerFactory
  ) : ILogProvider
  {
    public Logger GetLogger(string name)
    {
      ILogger logger;
      try
      {
        logger = loggerFactory.CreateLogger(name);
      }
      catch (ObjectDisposedException)
      {
        return (_, _, _, _) => { return false; };
      }

      return (level, func, exception, parameters) =>
      {
        try
        {
          logger.Log(
            level switch
            {
              Quartz.Logging.LogLevel.Fatal =>
                Microsoft.Extensions.Logging.LogLevel.Critical,
              Quartz.Logging.LogLevel.Error =>
                Microsoft.Extensions.Logging.LogLevel.Error,
              Quartz.Logging.LogLevel.Warn =>
                Microsoft.Extensions.Logging.LogLevel.Warning,
              Quartz.Logging.LogLevel.Info =>
                Microsoft.Extensions.Logging.LogLevel.Information,
              Quartz.Logging.LogLevel.Debug =>
                Microsoft.Extensions.Logging.LogLevel.Debug,
              Quartz.Logging.LogLevel.Trace =>
                Microsoft.Extensions.Logging.LogLevel.Trace,
              _ => Microsoft.Extensions.Logging.LogLevel.Information
            },
            exception,
#pragma warning disable CA2254 // Template should be a static expression
            func is { } f ? f() : null,
#pragma warning restore CA2254 // Template should be a static expression
            parameters);
        }
        catch (ObjectDisposedException)
        {
          return false;
        }

        return true;
      };
    }

    public IDisposable OpenNestedContext(string message)
    {
      throw new NotImplementedException();
    }

    public IDisposable OpenMappedContext(
      string key,
      object value,
      bool destructure = false)
    {
      throw new NotImplementedException();
    }
  }
}

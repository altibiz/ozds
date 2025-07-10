using Quartz;

namespace Ozds.Jobs.Scheduler;

public class OzdsSchedulerFactory(
  ISchedulerFactory schedulerFactory
) : IHostedService
{
  private readonly SemaphoreSlim @lock = new(1, 1);

  private IScheduler? inner;

  public Task StartAsync(CancellationToken cancellationToken)
  {
    // NOTE: not needed
    return Task.CompletedTask;
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

  public async Task StopAsync(CancellationToken cancellationToken)
  {
    var schedulers = await schedulerFactory.GetAllSchedulers(cancellationToken);

    foreach (var scheduler in schedulers)
    {
      await scheduler.Shutdown(cancellationToken);
    }
  }
}

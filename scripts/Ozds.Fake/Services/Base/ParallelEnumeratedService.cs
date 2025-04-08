using Ozds.Fake.Workers.Abstractions;

namespace Ozds.Fake.Services.Base;

public abstract class ParallelEnumeratedService<TItem, TWorker>(
  IServiceProvider services
) : BackgroundService
  where TWorker : IEnumeratedBackgroundServiceWorker<TItem>
{
  protected abstract IEnumerable<TItem> GetEnumerable();

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    var enumerable = GetEnumerable();
    var tasks = new List<Task>();
    foreach (var item in enumerable)
    {
      tasks.Add(
        Task.Run(
          async () =>
          {
            await using var scope = services.CreateAsyncScope();
            var worker = scope.ServiceProvider.GetRequiredService<TWorker>();
            await worker.ExecuteAsync(item, stoppingToken);
          }, stoppingToken));
    }

    await Task.WhenAll(tasks);
    services
      .GetRequiredService<IHostApplicationLifetime>()
      .StopApplication();
  }
}

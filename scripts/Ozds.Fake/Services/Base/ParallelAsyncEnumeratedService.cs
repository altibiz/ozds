using Ozds.Fake.Workers.Abstractions;

namespace Ozds.Fake.Services.Base;

public abstract class ParallelAsyncEnumeratedService<TItem, TWorker>(
  IServiceProvider services
) : BackgroundService
  where TWorker : IEnumeratedBackgroundServiceWorker<TItem>
{
  protected abstract IAsyncEnumerable<TItem> GetEnumerable(
    CancellationToken cancellationToken
  );

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    var enumerable = GetEnumerable(stoppingToken);
    var tasks = new List<Task>();
    await foreach (var item in enumerable.WithCancellation(stoppingToken))
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

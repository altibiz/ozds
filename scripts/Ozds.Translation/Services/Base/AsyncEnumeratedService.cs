using Ozds.Translation.Workers.Abstractions;

namespace Ozds.Translation.Services.Base;

public abstract class AsyncEnumeratedService<TItem, TWorker>(
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
    await foreach (var item in enumerable.WithCancellation(stoppingToken))
    {
      await using var scope = services.CreateAsyncScope();
      var worker = scope.ServiceProvider.GetRequiredService<TWorker>();
      await worker.ExecuteAsync(item, stoppingToken);
    }

    services.GetRequiredService<IHostApplicationLifetime>().StopApplication();
  }
}

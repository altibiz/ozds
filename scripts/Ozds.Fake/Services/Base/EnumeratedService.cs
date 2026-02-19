using Ozds.Fake.Workers.Abstractions;

namespace Ozds.Fake.Services.Base;

public abstract class EnumeratedService<TItem, TWorker>(
  IServiceProvider services
) : BackgroundService
  where TWorker : IEnumeratedBackgroundServiceWorker<TItem>
{
  protected abstract IEnumerable<TItem> GetEnumerable();

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    var enumerable = GetEnumerable();
    foreach (var item in enumerable)
    {
      await using var scope = services.CreateAsyncScope();
      var worker = scope.ServiceProvider.GetRequiredService<TWorker>();
      await worker.ExecuteAsync(item, stoppingToken);
    }

    services.GetRequiredService<IHostApplicationLifetime>().StopApplication();
  }
}

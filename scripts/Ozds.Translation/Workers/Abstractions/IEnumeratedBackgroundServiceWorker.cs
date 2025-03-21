namespace Ozds.Translation.Workers.Abstractions;

public interface IEnumeratedBackgroundServiceWorker<T> : IWorker
{
  public Task ExecuteAsync(T item, CancellationToken stoppingToken);
}

using Ozds.Business.Queries;

namespace Ozds.Business.Services;

public class MigrationService(
  IServiceProvider serviceProvider
) : IHostedService
{
  public async Task StartAsync(CancellationToken cancellationToken)
  {
    await using var scope = serviceProvider.CreateAsyncScope();

    var queries = scope.ServiceProvider
      .GetRequiredService<MigrationQueries>();

    var pendingMigrations = await queries.ReadPendingMigrations(
      cancellationToken);
    if (pendingMigrations.Count == 0)
    {
      return;
    }

    throw new InvalidOperationException(
      $"Please run migrations:\n{string.Join(",\n", pendingMigrations)}");
  }

  public Task StopAsync(CancellationToken cancellationToken)
  {
    return Task.CompletedTask;
  }
}

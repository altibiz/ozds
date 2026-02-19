using Microsoft.Extensions.Options;
using Ozds.Data.Mutations;
using Ozds.Data.Options;
using Ozds.Data.Queries;

namespace Ozds.Data.Services;

public class MigrationService(
  IServiceProvider serviceProvider,
  IOptions<OzdsDataOptions> options
) : IHostedService
{
  public async Task StartAsync(CancellationToken cancellationToken)
  {
    await using var scope = serviceProvider.CreateAsyncScope();

    if (options.Value.MigrateOnStartup)
    {
      var mutations =
        scope.ServiceProvider.GetRequiredService<MigrationMutations>();
      await mutations.MigrateAsync(cancellationToken);
      return;
    }

    var queries = scope.ServiceProvider.GetRequiredService<MigrationQueries>();

    var pendingMigrations = await queries.ReadPendingMigrations(
      cancellationToken
    );
    if (pendingMigrations.Count == 0)
    {
      return;
    }

    var pendingMigrationsString = string.Join(
      "," + Environment.NewLine,
      pendingMigrations
    );

    throw new InvalidOperationException(
      $"Please run migrations:{Environment.NewLine}{pendingMigrationsString}"
    );
  }

  public Task StopAsync(CancellationToken cancellationToken)
  {
    return Task.CompletedTask;
  }
}

using Ozds.Business.Mutations;
using Ozds.Business.Queries;

namespace Ozds.Migration.Services;

public class MigrateHostedService(
  IServiceProvider serviceProvider,
  ILogger<MigrateHostedService> logger
) : BackgroundService
{
  protected override async Task ExecuteAsync(
    CancellationToken stoppingToken
  )
  {
    await using var scope = serviceProvider
      .CreateAsyncScope();

    var migrationQueries = scope.ServiceProvider
      .GetRequiredService<MigrationQueries>();
    var pendingMigrations = await migrationQueries
      .ReadPendingMigrations(stoppingToken);
    logger.LogInformation(
      "Found {Count} pending migrations:\n{Migrations}",
      pendingMigrations.Count,
      string.Join("\n", pendingMigrations)
    );

    var migrationMutations = scope.ServiceProvider
      .GetRequiredService<MigrationMutations>();
    await migrationMutations.MigrateAsync(stoppingToken);

    var applicationLifetime = scope.ServiceProvider
      .GetRequiredService<IHostApplicationLifetime>();
    applicationLifetime.StopApplication();
  }
}

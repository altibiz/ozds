using DataMigrationMutations = Ozds.Data.Mutations.MigrationMutations;
using DataMigrationQueries = Ozds.Data.Queries.MigrationQueries;
using JobsMigrationMutations = Ozds.Jobs.Mutations.MigrationMutations;
using JobsMigrationQueries = Ozds.Jobs.Queries.MigrationQueries;
using MessagingMigrationMutations = Ozds.Messaging.Mutations.MigrationMutations;
using MessagingMigrationQueries = Ozds.Messaging.Queries.MigrationQueries;

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
    await MigrateDataAsync(stoppingToken);
    await MigrateMessagingAsync(stoppingToken);
    await MigrateJobsAsync(stoppingToken);

    var applicationLifetime = serviceProvider
      .GetRequiredService<IHostApplicationLifetime>();
    applicationLifetime.StopApplication();
  }

  private async Task MigrateDataAsync(
    CancellationToken stoppingToken
  )
  {
    await using var scope = serviceProvider
      .CreateAsyncScope();

    var migrationQueries = scope.ServiceProvider
      .GetRequiredService<DataMigrationQueries>();
    var pendingMigrations = await migrationQueries
      .ReadPendingMigrations(stoppingToken);
    logger.LogInformation(
      "Found {Count} pending data migrations:\n{Migrations}",
      pendingMigrations.Count,
      string.Join(Environment.NewLine, pendingMigrations)
    );

    var migrationMutations = scope.ServiceProvider
      .GetRequiredService<DataMigrationMutations>();
    await migrationMutations.MigrateAsync(stoppingToken);
  }

  private async Task MigrateMessagingAsync(
    CancellationToken stoppingToken
  )
  {
    await using var scope = serviceProvider
      .CreateAsyncScope();

    var migrationQueries = scope.ServiceProvider
      .GetRequiredService<MessagingMigrationQueries>();
    var pendingMigrations = await migrationQueries
      .ReadPendingMigrations(stoppingToken);
    logger.LogInformation(
      "Found {Count} pending messaging migrations:\n{Migrations}",
      pendingMigrations.Count,
      string.Join(Environment.NewLine, pendingMigrations)
    );

    var migrationMutations = scope.ServiceProvider
      .GetRequiredService<MessagingMigrationMutations>();
    await migrationMutations.MigrateAsync(stoppingToken);
  }

  private async Task MigrateJobsAsync(
    CancellationToken stoppingToken
  )
  {
    await using var scope = serviceProvider
      .CreateAsyncScope();

    var migrationQueries = scope.ServiceProvider
      .GetRequiredService<JobsMigrationQueries>();
    var pendingMigrations = await migrationQueries
      .ReadPendingMigrations(stoppingToken);
    logger.LogInformation(
      "Found {Count} pending jobs migrations:\n{Migrations}",
      pendingMigrations.Count,
      string.Join(Environment.NewLine, pendingMigrations)
    );

    var migrationMutations = scope.ServiceProvider
      .GetRequiredService<JobsMigrationMutations>();
    await migrationMutations.MigrateAsync(stoppingToken);
  }
}

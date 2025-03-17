using Ozds.Business.Queries.Abstractions;
using DataMigrationQueries = Ozds.Data.Queries.MigrationQueries;
using JobsMigrationQueries = Ozds.Jobs.Queries.MigrationQueries;
using MessagingMigrationQueries = Ozds.Messaging.Queries.MigrationQueries;

namespace Ozds.Business.Queries;

public class MigrationQueries(
  DataMigrationQueries dataQueries,
  JobsMigrationQueries jobsQueries,
  MessagingMigrationQueries messagingQueries
) : IQueries
{
  public async Task<List<string>> ReadPendingMigrations(
    CancellationToken cancellationToken
  )
  {
    var pendingMigrations = new List<string>();

    var pendingDataMigrations = await dataQueries
      .ReadPendingMigrations(cancellationToken);
    pendingMigrations.AddRange(pendingDataMigrations);

    var pendingJobsMigrations = await jobsQueries
      .ReadPendingMigrations(cancellationToken);
    pendingMigrations.AddRange(pendingJobsMigrations);

    var pendingMessagingMigrations = await messagingQueries
      .ReadPendingMigrations(cancellationToken);
    pendingMigrations.AddRange(pendingMessagingMigrations);

    return pendingMigrations;
  }
}

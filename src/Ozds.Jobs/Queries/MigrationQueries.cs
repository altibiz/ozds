using Microsoft.EntityFrameworkCore;
using Ozds.Jobs.Context;
using Ozds.Jobs.Queries.Abstractions;

namespace Ozds.Jobs.Queries;

public class MigrationQueries(
  IDbContextFactory<JobsDbContext> factory
) : IQueries
{
  public async Task<List<string>> ReadPendingMigrations(
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    var assemblyName = typeof(JobsDbContext).Assembly.GetName().Name;

    var pendingMigrations = await context.Database
      .GetPendingMigrationsAsync(cancellationToken);

    return pendingMigrations
      .Select(migration => $"{assemblyName}:{migration}")
      .ToList();
  }
}

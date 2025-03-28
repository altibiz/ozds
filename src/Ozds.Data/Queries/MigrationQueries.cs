using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Queries.Abstractions;

namespace Ozds.Data.Queries;

public class MigrationQueries(
  IDbContextFactory<DataDbContext> factory
) : IQueries
{
  public async Task<List<string>> ReadPendingMigrations(
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    var assemblyName = typeof(DataDbContext).Assembly.GetName().Name;

    var pendingMigrations = await context.Database
      .GetPendingMigrationsAsync(cancellationToken);

    return pendingMigrations
      .Select(migration => $"{assemblyName}:{migration}")
      .ToList();
  }
}

using Microsoft.EntityFrameworkCore;
using Ozds.Messaging.Context;
using Ozds.Messaging.Queries.Abstractions;

namespace Ozds.Messaging.Queries;

public class MigrationQueries(IDbContextFactory<MessagingDbContext> factory)
  : IQueries
{
  public async Task<List<string>> ReadPendingMigrations(
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );

    var assemblyName = typeof(MessagingDbContext).Assembly.GetName().Name;

    var pendingMigrations = await context.Database.GetPendingMigrationsAsync(
      cancellationToken
    );

    return pendingMigrations
      .Select(migration => $"{assemblyName}:{migration}")
      .ToList();
  }
}

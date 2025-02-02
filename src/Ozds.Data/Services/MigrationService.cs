using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;

namespace Ozds.Data.Services;

public class MigrationService(
  IDbContextFactory<DataDbContext> factory
) : IHostedService
{
  public async Task StartAsync(CancellationToken cancellationToken)
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);
    await context.Database.MigrateAsync(cancellationToken);
  }

  public Task StopAsync(CancellationToken cancellationToken)
  {
    return Task.CompletedTask;
  }
}

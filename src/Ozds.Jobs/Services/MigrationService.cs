using Microsoft.EntityFrameworkCore;
using Ozds.Jobs.Context;

namespace Ozds.Jobs.Services;

public class MigrationService(
  IDbContextFactory<JobsDbContext> factory
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

using Microsoft.EntityFrameworkCore;
using Ozds.Messaging.Context;

namespace Ozds.Messaging.Services;

public class MigrationService(
  IDbContextFactory<MessagingDbContext> factory
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

using Microsoft.EntityFrameworkCore;
using Ozds.Messaging.Context;
using Ozds.Messaging.Mutations.Abstractions;

namespace Ozds.Messaging.Mutations;

public class MigrationMutations(IDbContextFactory<MessagingDbContext> factory)
  : IMutations
{
  public async Task MigrateAsync(CancellationToken cancellationToken)
  {
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );
    context.Database.SetCommandTimeout(TimeSpan.FromHours(6));
    await context.Database.MigrateAsync(cancellationToken);
  }
}

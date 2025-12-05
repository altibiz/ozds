using Microsoft.EntityFrameworkCore;
using Ozds.Jobs.Context;
using Ozds.Jobs.Mutations.Abstractions;

namespace Ozds.Jobs.Mutations;

public class MigrationMutations(
  IDbContextFactory<JobsDbContext> factory
) : IMutations
{
  public async Task MigrateAsync(CancellationToken cancellationToken)
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);
    context.Database.SetCommandTimeout(TimeSpan.FromHours(6));
    await context.Database.MigrateAsync(cancellationToken);
  }
}

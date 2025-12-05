using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Mutations.Abstractions;

namespace Ozds.Data.Mutations;

public class MigrationMutations(
  IDbContextFactory<DataDbContext> factory
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

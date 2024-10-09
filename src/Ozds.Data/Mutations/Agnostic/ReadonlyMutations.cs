using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Mutations.Abstractions;

namespace Ozds.Data.Mutations.Agnostic;

public class ReadonlyMutations(
  IDbContextFactory<DataDbContext> factory
) : IMutations
{
  public async Task Create(
    IReadonlyEntity entity,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);
    context.Add(entity);
    await context.SaveChangesAsync(cancellationToken);
  }
}

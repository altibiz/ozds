using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Mutations.Abstractions;

namespace Ozds.Data.Mutations.Agnostic;

public class JoinMutations(
  IDbContextFactory<DataDbContext> factory
) : IMutations
{
  public async Task Create(
    IJoinEntity entity,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);
    context.Add(entity);
    await context.SaveChangesAsync(cancellationToken);
  }

  public async Task Update(
    IJoinEntity entity,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);
    context.Update(entity);
    await context.SaveChangesAsync(cancellationToken);
  }

  public async Task Delete(
    IJoinEntity entity,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);
    context.Remove(entity);
    await context.SaveChangesAsync(cancellationToken);
  }
}

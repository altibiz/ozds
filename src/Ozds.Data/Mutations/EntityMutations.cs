using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Mutations.Abstractions;

namespace Ozds.Data.Mutations;

public class EntityMutations(
  IDbContextFactory<DataDbContext> factory
) : IMutations
{
  public async Task Create(
    IEntity entity,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);
    context.Add(entity);
    await context.SaveChangesAsync(cancellationToken);
  }

  public async Task Create(
    IEnumerable<IEntity> entities,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);
    context.AddRange(entities);
    await context.SaveChangesAsync(cancellationToken);
  }

  public async Task Update(
    IEntity entity,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);
    context.Update(entity);
    await context.SaveChangesAsync(cancellationToken);
  }

  public async Task Delete(
    IEntity entity,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);
    context.Remove(entity);
    await context.SaveChangesAsync(cancellationToken);
  }
}

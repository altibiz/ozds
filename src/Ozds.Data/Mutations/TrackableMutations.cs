using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Mutations.Abstractions;

namespace Ozds.Data.Mutations;

public class TrackableMutations(IDbContextFactory<DataDbContext> factory)
  : IMutations
{
  public async Task Create(
    ITrackableEntity entity,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );
    context.Add(entity);
    await context.SaveChangesAsync(cancellationToken);
  }

  public async Task Create(
    IEnumerable<ITrackableEntity> entity,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );
    context.AddRange(entity);
    await context.SaveChangesAsync(cancellationToken);
  }

  public async Task Update(
    ITrackableEntity entity,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );
    context.Update(entity);
    await context.SaveChangesAsync(cancellationToken);
  }

  public async Task Delete(
    ITrackableEntity entity,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );
    context.Remove(entity);
    await context.SaveChangesAsync(cancellationToken);
  }
}

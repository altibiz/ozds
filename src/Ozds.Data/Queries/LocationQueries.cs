using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Joins;
using Ozds.Data.Extensions;
using Ozds.Data.Queries.Abstractions;

namespace Ozds.Data.Queries;

public class LocationQueries(
  IDbContextFactory<DataDbContext> factory
) : IQueries
{
  public async Task<List<LocationEntity>> LocationsByRepresentativeId(
    string representativeId,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    var locations = await context.LocationRepresentatives
      .Where(context.ForeignKeyEquals<LocationRepresentativeEntity>(
        nameof(LocationRepresentativeEntity.RepresentativeId),
        representativeId))
      .Include(x => x.Location)
      .Select(x => x.Location)
      .ToListAsync(cancellationToken);

    return locations;
  }
}

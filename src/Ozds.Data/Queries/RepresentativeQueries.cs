using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Extensions;
using Ozds.Data.Queries.Abstractions;

namespace Ozds.Data.Queries;

public class RepresentativeQueries(
  IDbContextFactory<DataDbContext> factory
) : IQueries
{
  public async Task<List<RepresentativeEntity>> ReadByUserIds(
    IEnumerable<string> userIds,
    CancellationToken cancellationToken,
    bool deleted = false
  )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    var filtered = context.Representatives
      .Where(context.PrimaryKeyIn<RepresentativeEntity>(userIds));

    if (!deleted)
    {
      filtered = filtered.Where(representative => !representative.IsDeleted);
    }

    var entities = await filtered.ToListAsync(cancellationToken);

    return entities;
  }
}

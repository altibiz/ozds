using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;
using Ozds.Data.Queries.Abstractions;

namespace Ozds.Data.Queries;

public class NetworkUserQueries(
  IDbContextFactory<DataDbContext> factory) : IQueries
{
  public async Task<PaginatedList<NetworkUserEntity>>
    ReadNetworkUsersByRepresentative(
      string representativeId,
      RoleEntity role,
      int pageNumber,
      CancellationToken cancellationToken,
      int pageCount = QueryConstants.DefaultPageCount
    )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    var filtered = role switch
    {
      RoleEntity.OperatorRepresentative =>
        context.Representatives
          .Where(context.PrimaryKeyEquals<RepresentativeEntity>(representativeId))
          .Include(x => x.NetworkUsers)
          .SelectMany(x => x.NetworkUsers),
      RoleEntity.LocationRepresentative =>
        context.Representatives
          .Where(context.PrimaryKeyEquals<RepresentativeEntity>(representativeId))
          .Include(x => x.Locations)
          .ThenInclude(x => x.NetworkUsers)
          .SelectMany(x => x.Locations.SelectMany(x => x.NetworkUsers)),
      RoleEntity.NetworkUserRepresentative => context.NetworkUsers,
      _ => throw new ArgumentOutOfRangeException(nameof(role))
    };

    var ordered = filtered
      .OrderByDescending(x => x.DeletedOn)
      .OrderByDescending(x => x.LastUpdatedOn)
      .OrderByDescending(x => x.CreatedOn);

    var count = await ordered.CountAsync(cancellationToken);
    var items = await ordered
      .Skip((pageNumber - 1) * pageCount)
      .Take(pageCount)
      .ToListAsync(cancellationToken);

    return items.OfType<NetworkUserEntity>().ToPaginatedList(count);
  }
}

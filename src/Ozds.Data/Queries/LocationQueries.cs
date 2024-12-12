using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Entities.Joins;
using Ozds.Data.Extensions;
using Ozds.Data.Queries.Abstractions;

namespace Ozds.Data.Queries;

public class LocationQueries(
  IDbContextFactory<DataDbContext> factory
) : IQueries
{
  public async Task<LocationEntity?> ReadLocationByRepresentativeId(
    string representativeId,
    RoleEntity role,
    string locationId,
    CancellationToken cancellationToken,
    bool deleted = false
  )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    var filtered = role switch
    {
      RoleEntity.OperatorRepresentative => context.Locations,
      RoleEntity.LocationRepresentative => context.LocationRepresentatives
        .Where(context.ForeignKeyEquals<LocationRepresentativeEntity>(
          nameof(LocationRepresentativeEntity.Representative),
          representativeId))
        .Include(x => x.Location)
        .Select(x => x.Location)
        .Concat(context.Representatives
          .Where(context.PrimaryKeyEquals<RepresentativeEntity>(
            representativeId))
          .Include(x => x.NetworkUsers)
          .ThenInclude(x => x.Location)
          .SelectMany(x => x.NetworkUsers.Select(x => x.Location))),
      RoleEntity.NetworkUserRepresentative => context.Representatives
        .Where(context.PrimaryKeyEquals<RepresentativeEntity>(representativeId))
        .Include(x => x.NetworkUsers)
        .ThenInclude(x => x.Location)
        .SelectMany(x => x.NetworkUsers.Select(x => x.Location)),
      _ => throw new ArgumentOutOfRangeException(nameof(role))
    };

    filtered = filtered.Where(context
      .PrimaryKeyEquals<LocationEntity>(locationId));

    if (!deleted)
    {
      filtered = filtered.Where(x => x.DeletedOn == null);
    }

    var item = await filtered
      .FirstOrDefaultAsync(cancellationToken);

    return item;
  }

  public async Task<PaginatedList<LocationEntity>>
    ReadLocationsByRepresentativeId(
      string representativeId,
      RoleEntity role,
      int pageNumber,
      CancellationToken cancellationToken,
      int pageSize = QueryConstants.DefaultPageCount,
      bool deleted = false
    )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    var filtered = role switch
    {
      RoleEntity.OperatorRepresentative => context.Locations,
      RoleEntity.LocationRepresentative => context.LocationRepresentatives
        .Where(context.ForeignKeyEquals<LocationRepresentativeEntity>(
          nameof(LocationRepresentativeEntity.Representative),
          representativeId))
        .Include(x => x.Location)
        .Select(x => x.Location)
        .Concat(context.Representatives
          .Where(context.PrimaryKeyEquals<RepresentativeEntity>(
            representativeId))
          .Include(x => x.NetworkUsers)
          .ThenInclude(x => x.Location)
          .SelectMany(x => x.NetworkUsers.Select(x => x.Location))),
      RoleEntity.NetworkUserRepresentative => context.Representatives
        .Where(context.PrimaryKeyEquals<RepresentativeEntity>(representativeId))
        .Include(x => x.NetworkUsers)
        .ThenInclude(x => x.Location)
        .SelectMany(x => x.NetworkUsers.Select(x => x.Location)),
      _ => throw new ArgumentOutOfRangeException(nameof(role))
    };

    if (!deleted)
    {
      filtered = filtered.Where(x => x.DeletedOn == null);
    }

    var ordered = filtered
      .OrderByDescending(x => x.LastUpdatedOn)
      .OrderByDescending(x => x.CreatedOn)
      .OrderByDescending(x => x.DeletedOn);

    var count = await filtered.CountAsync(cancellationToken);

    var items = await ordered
      .Skip((pageNumber - 1) * pageSize)
      .Take(pageSize)
      .ToListAsync(cancellationToken);

    return items.ToPaginatedList(count);
  }

  public async Task<List<LocationEntity>>
    ReadAllLocationsByRepresentativeId(
      string representativeId,
      RoleEntity role,
      CancellationToken cancellationToken
    )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    var filtered = role switch
    {
      RoleEntity.OperatorRepresentative => context.Locations,
      RoleEntity.LocationRepresentative => context.LocationRepresentatives
        .Where(context.ForeignKeyEquals<LocationRepresentativeEntity>(
          nameof(LocationRepresentativeEntity.Representative),
          representativeId))
        .Include(x => x.Location)
        .Select(x => x.Location)
        .Concat(context.Representatives
          .Where(context.PrimaryKeyEquals<RepresentativeEntity>(
            representativeId))
          .Include(x => x.NetworkUsers)
          .ThenInclude(x => x.Location)
          .SelectMany(x => x.NetworkUsers.Select(x => x.Location))),
      RoleEntity.NetworkUserRepresentative => context.Representatives
        .Where(context.PrimaryKeyEquals<RepresentativeEntity>(representativeId))
        .Include(x => x.NetworkUsers)
        .ThenInclude(x => x.Location)
        .SelectMany(x => x.NetworkUsers.Select(x => x.Location)),
      _ => throw new ArgumentOutOfRangeException(nameof(role))
    };

    filtered = filtered.Where(x => x.DeletedOn == null);

    var ordered = filtered
      .OrderByDescending(x => x.LastUpdatedOn)
      .OrderByDescending(x => x.CreatedOn)
      .OrderByDescending(x => x.DeletedOn);

    var items = await ordered.ToListAsync(cancellationToken);

    return items;
  }
}

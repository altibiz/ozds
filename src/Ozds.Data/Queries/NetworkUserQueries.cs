using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Entities.Joins;
using Ozds.Data.Extensions;
using Ozds.Data.Queries.Abstractions;

namespace Ozds.Data.Queries;

public class NetworkUserQueries(IDbContextFactory<DataDbContext> factory)
  : IQueries
{
  public async Task<PaginatedList<NetworkUserEntity>> ReadByRepresentativeId(
    string representativeId,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageSize = QueryConstants.DefaultPageCount,
    bool deleted = false,
    string? title = null
  )
  {
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );

    var filtered = context
      .NetworkUserRepresentatives.Where(
        context.ForeignKeyEquals<NetworkUserRepresentativeEntity>(
          nameof(NetworkUserRepresentativeEntity.Representative),
          representativeId
        )
      )
      .Include(x => x.NetworkUser)
      .Select(x => x.NetworkUser);

    filtered = deleted
      ? filtered.Where(x => x.IsDeleted)
      : filtered.Where(x => !x.IsDeleted);

    if (!string.IsNullOrWhiteSpace(title))
    {
      filtered = filtered.Where(x => x.Title.Contains(title));
    }

    var ordered = filtered
      .OrderByDescending(x => x.LastUpdatedOn)
      .OrderByDescending(x => x.CreatedOn)
      .OrderByDescending(x => x.DeletedOn);

    var count = await filtered.CountAsync(cancellationToken);
    var items = await ordered
      .Skip(pageNumber * pageSize)
      .Take(pageSize)
      .ToListAsync(cancellationToken);

    return items.ToPaginatedList(count);
  }

  public async Task<NetworkUserEntity?> ReadIndirectByRepresentativeIdAndId(
    string representativeId,
    RoleEntity role,
    string networkUserId,
    CancellationToken cancellationToken,
    bool deleted = false
  )
  {
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );

    var filtered = role switch
    {
      RoleEntity.NetworkUserRepresentative => context
        .NetworkUserRepresentatives.Where(
          context.ForeignKeyEquals<NetworkUserRepresentativeEntity>(
            nameof(NetworkUserRepresentativeEntity.Representative),
            representativeId
          )
        )
        .Include(x => x.NetworkUser)
        .Select(x => x.NetworkUser),
      RoleEntity.LocationRepresentative => context
        .Representatives.Where(
          context.PrimaryKeyEquals<RepresentativeEntity>(representativeId)
        )
        .Include(x => x.Locations)
          .ThenInclude(x => x.NetworkUsers)
        .SelectMany(x => x.Locations.SelectMany(x => x.NetworkUsers))
        .Concat(
          context
            .NetworkUserRepresentatives.Where(
              context.ForeignKeyEquals<NetworkUserRepresentativeEntity>(
                nameof(NetworkUserRepresentativeEntity.Representative),
                representativeId
              )
            )
            .Include(x => x.NetworkUser)
            .Select(x => x.NetworkUser)
        ),
      RoleEntity.OperatorRepresentative => context.NetworkUsers,
      _ => throw new ArgumentOutOfRangeException(nameof(role)),
    };

    filtered = filtered.Where(
      context.PrimaryKeyEquals<NetworkUserEntity>(networkUserId)
    );

    if (!deleted)
    {
      filtered = filtered.Where(x => x.DeletedOn == null);
    }
    else
    {
      filtered = filtered.Where(x => x.DeletedOn != null);
    }

    var item = await filtered.FirstOrDefaultAsync(cancellationToken);

    return item;
  }

  public async Task<
    PaginatedList<NetworkUserEntity>
  > ReadIndirectByRepresentativeId(
    string representativeId,
    RoleEntity role,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount,
    bool deleted = false,
    string? title = null
  )
  {
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );

    var filtered = role switch
    {
      RoleEntity.NetworkUserRepresentative => context
        .NetworkUserRepresentatives.Where(
          context.ForeignKeyEquals<NetworkUserRepresentativeEntity>(
            nameof(NetworkUserRepresentativeEntity.Representative),
            representativeId
          )
        )
        .Include(x => x.NetworkUser)
        .Select(x => x.NetworkUser),
      RoleEntity.LocationRepresentative => context
        .Representatives.Where(
          context.PrimaryKeyEquals<RepresentativeEntity>(representativeId)
        )
        .Include(x => x.Locations)
          .ThenInclude(x => x.NetworkUsers)
        .SelectMany(x => x.Locations.SelectMany(x => x.NetworkUsers))
        .Concat(
          context
            .NetworkUserRepresentatives.Where(
              context.ForeignKeyEquals<NetworkUserRepresentativeEntity>(
                nameof(NetworkUserRepresentativeEntity.Representative),
                representativeId
              )
            )
            .Include(x => x.NetworkUser)
            .Select(x => x.NetworkUser)
        ),
      RoleEntity.OperatorRepresentative => context.NetworkUsers,
      _ => throw new ArgumentOutOfRangeException(nameof(role)),
    };

    if (!deleted)
    {
      filtered = filtered.Where(x => x.DeletedOn == null);
    }
    else
    {
      filtered = filtered.Where(x => x.DeletedOn != null);
    }

    if (!string.IsNullOrWhiteSpace(title))
    {
      filtered = filtered.Where(x => x.Title.Contains(title));
    }

    var ordered = filtered
      .OrderByDescending(x => x.DeletedOn)
      .OrderByDescending(x => x.LastUpdatedOn)
      .OrderByDescending(x => x.CreatedOn);

    var count = await filtered.CountAsync(cancellationToken);

    var items = await ordered
      .Skip(pageNumber * pageCount)
      .Take(pageCount)
      .ToListAsync(cancellationToken);

    return items.ToPaginatedList(count);
  }
}

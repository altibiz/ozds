using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Models.Composite;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Enums;

namespace Ozds.Business.Queries;

public class OzdsNetworkUserQueries(OzdsDataDbContext context) : IOzdsQueries
{
  private readonly OzdsDataDbContext context = context;

  public async Task<NetworkUserModel?>
    NetworkUserById(string id)
  {
    var networkUser =
      await context.NetworkUsers
        .Where(context.PrimaryKeyEquals<NetworkUserEntity>(id))
        .FirstOrDefaultAsync();
    if (networkUser is not null)
    {
      return networkUser.ToModel();
    }

    return null;
  }

  public async Task<PaginatedList<LocationModel>> GetLocations(
    string title,
    int pageNumber = QueryConstants.StartingPage,
    int pageCount = QueryConstants.DefaultPageCount
  )
  {
    var filtered = context.Locations
      .Where(
        catalogue => catalogue.Title
          .StartsWith(title));
    var count = await filtered.CountAsync();
    var items = await filtered
      .OrderBy(context.PrimaryKeyOf<LocationEntity>())
      .Skip((pageNumber - 1) * pageCount)
      .Take(pageCount)
      .ToListAsync();
    return items
      .Select(item => item.ToModel())
      .ToPaginatedList(count);
  }

  public async Task<PaginatedList<RepresentativeModel>>
    GetEligibleRepresentatives(
      string title,
      int pageNumber = QueryConstants.StartingPage,
      int pageCount = QueryConstants.DefaultPageCount
    )
  {
    var filtered = context.Representatives
      .Where(
        rep => rep.Title
          .StartsWith(title))
      .Where(rep => rep.Role == RoleEntity.NetworkUserRepresentative);
    var count = await filtered.CountAsync();
    var items = await filtered
      .OrderBy(context.PrimaryKeyOf<RepresentativeEntity>())
      .Skip((pageNumber - 1) * pageCount)
      .Take(pageCount)
      .ToListAsync();
    return items
      .Select(item => item.ToModel())
      .ToPaginatedList(count);
  }

  public async Task<NetworkUserWithRepresentativesModel?>
    NetworkUserWithRepresentativesById(string id)
  {
    var networkUser =
      await context.NetworkUsers
        .Where(context.PrimaryKeyEquals<NetworkUserEntity>(id))
        .Include(x => x.Representatives)
        .FirstOrDefaultAsync();
    if (networkUser is not null)
    {
      return new NetworkUserWithRepresentativesModel(
        networkUser.ToModel(),
        networkUser.Representatives.Select(x => x.ToModel()).ToList()
      );
    }

    return null;
  }

  public async Task<List<NetworkUserModel>>
    NetworkUserByRepresentativeId(string id)
  {
    var rep =
      await context.Representatives
        .Where(context.PrimaryKeyEquals<RepresentativeEntity>(id))
        .Include(x => x.NetworkUsers)
        .FirstOrDefaultAsync();
    if (rep is not null)
    {
      return rep.NetworkUsers.Select(x => x.ToModel()).ToList();
    }

    return [];
  }
}

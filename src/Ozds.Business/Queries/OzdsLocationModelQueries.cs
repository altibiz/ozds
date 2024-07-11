using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Models.Composite;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Enums;

namespace Ozds.Business.Queries;

public class OzdsLocationModelQueries(OzdsDataDbContext context) : IOzdsQueries
{
  private readonly OzdsDataDbContext context = context;

  public async Task<LocationModel?>
    LocationById(string id)
  {
    var locationEntity =
      await context.Locations
        .Where(context.PrimaryKeyEquals<LocationEntity>(id))
        .Include(location => location.RedLowNetworkUserCatalogue)
        .Include(location => location.BlueLowNetworkUserCatalogue)
        .Include(location => location.WhiteLowNetworkUserCatalogue)
        .Include(location => location.WhiteMediumNetworkUserCatalogue)
        .Include(location => location.RegulatoryCatalogue)
        .FirstOrDefaultAsync();
    if (locationEntity is not null)
    {
      return locationEntity.ToModel();
    }

    return null;
  }

  public async Task<PaginatedList<RedLowNetworkUserCatalogueModel>>
    GetRedLowNetworkUserCatalogues(
      string title,
      int pageNumber = QueryConstants.StartingPage,
      int pageCount = QueryConstants.DefaultPageCount
    )
  {
    var filtered = context.NetworkUserCatalogues
      .OfType<RedLowNetworkUserCatalogueEntity>()
      .Where(
        catalogue => catalogue.Title
          .StartsWith(title));
    var count = await filtered.CountAsync();
    var items = await filtered
      .OrderBy(context.PrimaryKeyOf<RedLowNetworkUserCatalogueEntity>())
      .Skip((pageNumber - 1) * pageCount)
      .Take(pageCount)
      .ToListAsync();
    return items
      .Select(item => item.ToModel())
      .ToPaginatedList(count);
  }

  public async Task<PaginatedList<BlueLowNetworkUserCatalogueModel>>
    GetBlueLowNetworkUserCatalogues(
      string title,
      int pageNumber = QueryConstants.StartingPage,
      int pageCount = QueryConstants.DefaultPageCount
    )
  {
    var filtered = context.NetworkUserCatalogues
      .OfType<BlueLowNetworkUserCatalogueEntity>()
      .Where(
        catalogue => catalogue.Title
          .StartsWith(title));
    var count = await filtered.CountAsync();
    var items = await filtered
      .OrderBy(context.PrimaryKeyOf<BlueLowNetworkUserCatalogueEntity>())
      .Skip((pageNumber - 1) * pageCount)
      .Take(pageCount)
      .ToListAsync();
    return items
      .Select(item => item.ToModel())
      .ToPaginatedList(count);
  }

  public async Task<PaginatedList<WhiteLowNetworkUserCatalogueModel>>
    GetWhiteLowNetworkUserCatalogues(
      string title,
      int pageNumber = QueryConstants.StartingPage,
      int pageCount = QueryConstants.DefaultPageCount
    )
  {
    var filtered = context.NetworkUserCatalogues
      .OfType<WhiteLowNetworkUserCatalogueEntity>()
      .Where(
        catalogue => catalogue.Title
          .StartsWith(title));
    var count = await filtered.CountAsync();
    var items = await filtered
      .OrderBy(context.PrimaryKeyOf<WhiteLowNetworkUserCatalogueEntity>())
      .Skip((pageNumber - 1) * pageCount)
      .Take(pageCount)
      .ToListAsync();
    return items
      .Select(item => item.ToModel())
      .ToPaginatedList(count);
  }

  public async Task<PaginatedList<WhiteMediumNetworkUserCatalogueModel>>
    GetWhiteMediumNetworkUserCatalogues(
      string title,
      int pageNumber = QueryConstants.StartingPage,
      int pageCount = QueryConstants.DefaultPageCount
    )
  {
    var filtered = context.NetworkUserCatalogues
      .OfType<WhiteMediumNetworkUserCatalogueEntity>()
      .Where(
        catalogue => catalogue.Title
          .StartsWith(title));
    var count = await filtered.CountAsync();
    var items = await filtered
      .OrderBy(context.PrimaryKeyOf<WhiteMediumNetworkUserCatalogueEntity>())
      .Skip((pageNumber - 1) * pageCount)
      .Take(pageCount)
      .ToListAsync();
    return items
      .Select(item => item.ToModel())
      .ToPaginatedList(count);
  }

  public async Task<PaginatedList<RegulatoryCatalogueModel>>
    GetRegulatoryCatalogues(
      string title,
      int pageNumber = QueryConstants.StartingPage,
      int pageCount = QueryConstants.DefaultPageCount
    )
  {
    var filtered = context.RegulatoryCatalogues
      .Where(
        catalogue => catalogue.Title
          .StartsWith(title));
    var count = await filtered.CountAsync();
    var items = await filtered
      .OrderBy(context.PrimaryKeyOf<RegulatoryCatalogueEntity>())
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
      .Where(rep => rep.Role == RoleEntity.LocationRepresentative);
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

  public async Task<LocationWithRepresentativesModel?>
    LocationWithRepresentativesById(string id)
  {
    var location =
      await context.Locations
        .Where(context.PrimaryKeyEquals<LocationEntity>(id))
        .Include(x => x.Representatives)
        .FirstOrDefaultAsync();
    if (location is not null)
    {
      return new LocationWithRepresentativesModel(
        location.ToModel(),
        location.Representatives.Select(x => x.ToModel()).ToList()
      );
    }

    return null;
  }
}

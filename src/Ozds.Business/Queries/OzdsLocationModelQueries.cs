using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data;
using Ozds.Data.Entities;
using Ozds.Data.Extensions;

namespace Ozds.Business.Queries;

public class OzdsLocationModelQueries : IOzdsQueries
{
  protected readonly OzdsDbContext context;

  public OzdsLocationModelQueries(OzdsDbContext context)
  {
    this.context = context;
  }

  public async Task<LocationModel?>
    LocationById(string id)
  {
    var locationEntity =
      await context.Locations
        .WithId(id)
        .Include(location => location.RedLowNetworkUserCatalogue)
        .Include(location => location.BlueLowNetworkUserCatalogue)
        .Include(location => location.WhiteLowNetworkUserCatalogue)
        .Include(location => location.WhiteMediumNetworkUserCatalogue)
        .Include(location => location.RegulatoryCatalogue)
        .FirstOrDefaultAsync();
    if (locationEntity is not null)
    {
      var redLowNetworkUserCatalogue =
        locationEntity.RedLowNetworkUserCatalogue;
      var blueLowNetworkUserCatalogue =
        locationEntity.BlueLowNetworkUserCatalogue;
      var whiteLowNetworkUserCatalogue =
        locationEntity.WhiteLowNetworkUserCatalogue;
      var whiteMediumNetworkUserCatalogue =
        locationEntity.WhiteMediumNetworkUserCatalogue;
      var regulatoryNetworkUserCatalogue = locationEntity.RegulatoryCatalogue;
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
      .Where(catalogue => catalogue.Title
        .StartsWith(title));
    var count = await filtered.CountAsync();
    var items = await filtered
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
      .Where(catalogue => catalogue.Title
        .StartsWith(title));
    var count = await filtered.CountAsync();
    var items = await filtered
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
      .Where(catalogue => catalogue.Title
        .StartsWith(title));
    var count = await filtered.CountAsync();
    var items = await filtered
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
      .Where(catalogue => catalogue.Title
        .StartsWith(title));
    var count = await filtered.CountAsync();
    var items = await filtered
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
      .Where(catalogue => catalogue.Title
        .StartsWith(title));
    var count = await filtered.CountAsync();
    var items = await filtered
      .Skip((pageNumber - 1) * pageCount)
      .Take(pageCount)
      .ToListAsync();
    return items
      .Select(item => item.ToModel())
      .ToPaginatedList(count);
  }
}

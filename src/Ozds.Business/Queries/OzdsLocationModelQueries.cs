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
        .Include(location => location.RedLowCatalogue)
        .Include(location => location.BlueLowCatalogue)
        .Include(location => location.WhiteLowCatalogue)
        .Include(location => location.WhiteMediumCatalogue)
        .Include(location => location.RegulatoryCatalogue)
        .FirstOrDefaultAsync();
    if (locationEntity is not null)
    {
      var redLowCatalogue = locationEntity.RedLowCatalogue;
      var blueLowCatalogue = locationEntity.BlueLowCatalogue;
      var whiteLowCatalogue = locationEntity.WhiteLowCatalogue;
      var whiteMediumCatalogue = locationEntity.WhiteMediumCatalogue;
      var regulatoryCatalogue = locationEntity.RegulatoryCatalogue;
      return locationEntity.ToModel();
    }

    return null;
  }

  public async Task<PaginatedList<RedLowCatalogueModel>> GetRedLowCatalogues(
    string title,
    int pageNumber = QueryConstants.StartingPage,
    int pageCount = QueryConstants.DefaultPageCount
  )
  {
    var filtered = context.Catalogues
      .OfType<RedLowCatalogueEntity>()
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

  public async Task<PaginatedList<BlueLowCatalogueModel>> GetBlueLowCatalogues(
    string title,
    int pageNumber = QueryConstants.StartingPage,
    int pageCount = QueryConstants.DefaultPageCount
  )
  {
    var filtered = context.Catalogues
      .OfType<BlueLowCatalogueEntity>()
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

  public async Task<PaginatedList<WhiteLowCatalogueModel>>
    GetWhiteLowCatalogues(
      string title,
      int pageNumber = QueryConstants.StartingPage,
      int pageCount = QueryConstants.DefaultPageCount
    )
  {
    var filtered = context.Catalogues
      .OfType<WhiteLowCatalogueEntity>()
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

  public async Task<PaginatedList<WhiteMediumCatalogueModel>>
    GetWhiteMediumCatalogues(
      string title,
      int pageNumber = QueryConstants.StartingPage,
      int pageCount = QueryConstants.DefaultPageCount
    )
  {
    var filtered = context.Catalogues
      .OfType<WhiteMediumCatalogueEntity>()
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
    var filtered = context.Catalogues
      .OfType<RegulatoryCatalogueEntity>()
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

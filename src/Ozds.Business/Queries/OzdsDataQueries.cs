using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data;
using Ozds.Data.Entities;

namespace Ozds.Business.Queries;

public class OzdsDataQueries(OzdsDbContext context) : IOzdsQueries
{
  private readonly OzdsDbContext context = context;

  public async Task<PaginatedList<LocationModel>> GetLocations(
    string title,
    int pageNumber = QueryConstants.StartingPage,
    int pageCount = QueryConstants.DefaultPageCount
  )
  {
    var filtered = context.Locations
      .Where(
        x => x.Title
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

  public async Task<PaginatedList<NetworkUserModel>> GetNetworkUsers(
    string title,
    int pageNumber = QueryConstants.StartingPage,
    int pageCount = QueryConstants.DefaultPageCount
  )
  {
    var filtered = context.NetworkUsers
      .Where(
        x => x.Title
          .StartsWith(title));
    var count = await filtered.CountAsync();
    var items = await filtered
      .OrderBy(context.PrimaryKeyOf<NetworkUserEntity>())
      .Skip((pageNumber - 1) * pageCount)
      .Take(pageCount)
      .ToListAsync();
    return items
      .Select(item => item.ToModel())
      .ToPaginatedList(count);
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

  public async Task<PaginatedList<MessengerModel>> GetMessengers(
    string title,
    int pageNumber = QueryConstants.StartingPage,
    int pageCount = QueryConstants.DefaultPageCount
  )
  {
    var filtered = context.Messengers
      .OfType<MessengerEntity>()
      .Where(
        catalogue => catalogue.Title
          .StartsWith(title));
    var count = await filtered.CountAsync();
    var items = await filtered
      .OrderBy(context.PrimaryKeyOf<MessengerEntity>())
      .Skip((pageNumber - 1) * pageCount)
      .Take(pageCount)
      .ToListAsync();
    return items
      .Select(item => item.ToModel())
      .ToPaginatedList(count);
  }

  public async Task<PaginatedList<AbbB2xMeasurementValidatorModel>>
    GetAbbB2xMeasurementValidators(
      string title,
      int pageNumber = QueryConstants.StartingPage,
      int pageCount = QueryConstants.DefaultPageCount
    )
  {
    var filtered = context.MeasurementValidators
      .OfType<AbbB2xMeasurementValidatorEntity>()
      .Where(
        catalogue => catalogue.Title
          .StartsWith(title));
    var count = await filtered.CountAsync();
    var items = await filtered
      .OrderBy(context.PrimaryKeyOf<AbbB2xMeasurementValidatorEntity>())
      .Skip((pageNumber - 1) * pageCount)
      .Take(pageCount)
      .ToListAsync();
    return items
      .Select(item => item.ToModel())
      .ToPaginatedList(count);
  }

  public async Task<PaginatedList<SchneideriEM3xxxMeasurementValidatorModel>>
    GetSchneideriEM3xxxMeasurementValidators(
      string title,
      int pageNumber = QueryConstants.StartingPage,
      int pageCount = QueryConstants.DefaultPageCount
    )
  {
    var filtered = context.MeasurementValidators
      .OfType<SchneideriEM3xxxMeasurementValidatorEntity>()
      .Where(
        catalogue => catalogue.Title
          .StartsWith(title));
    var count = await filtered.CountAsync();
    var items = await filtered
      .OrderBy(
        context.PrimaryKeyOf<SchneideriEM3xxxMeasurementValidatorEntity>())
      .Skip((pageNumber - 1) * pageCount)
      .Take(pageCount)
      .ToListAsync();
    return items
      .Select(item => item.ToModel())
      .ToPaginatedList(count);
  }

  public async Task<PaginatedList<AbbB2xMeterModel>> GetAbbB2xMeters(
    string title,
    int pageNumber = QueryConstants.StartingPage,
    int pageCount = QueryConstants.DefaultPageCount
  )
  {
    var filtered = context.Meters
      .OfType<AbbB2xMeterEntity>()
      .Where(
        catalogue => catalogue.Title
          .StartsWith(title));
    var count = await filtered.CountAsync();
    var items = await filtered
      .OrderBy(context.PrimaryKeyOf<AbbB2xMeterEntity>())
      .Skip((pageNumber - 1) * pageCount)
      .Take(pageCount)
      .ToListAsync();
    return items
      .Select(item => item.ToModel())
      .ToPaginatedList(count);
  }

  public async Task<PaginatedList<SchneideriEM3xxxMeterModel>>
    GetSchneideriEM3xxxMeters(
      string title,
      int pageNumber = QueryConstants.StartingPage,
      int pageCount = QueryConstants.DefaultPageCount
    )
  {
    var filtered = context.Meters
      .OfType<SchneideriEM3xxxMeterEntity>()
      .Where(
        catalogue => catalogue.Title
          .StartsWith(title));
    var count = await filtered.CountAsync();
    var items = await filtered
      .OrderBy(context.PrimaryKeyOf<SchneideriEM3xxxMeterEntity>())
      .Skip((pageNumber - 1) * pageCount)
      .Take(pageCount)
      .ToListAsync();
    return items
      .Select(item => item.ToModel())
      .ToPaginatedList(count);
  }

  public async Task<PaginatedList<LocationMeasurementLocationModel>>
    GetLocationMeasurementLocations(
      string title,
      int pageNumber = QueryConstants.StartingPage,
      int pageCount = QueryConstants.DefaultPageCount
    )
  {
    var filtered = context.MeasurementLocations
      .OfType<LocationMeasurementLocationEntity>()
      .Where(
        catalogue => catalogue.Title
          .StartsWith(title));
    var count = await filtered.CountAsync();
    var items = await filtered
      .OrderBy(context.PrimaryKeyOf<LocationMeasurementLocationEntity>())
      .Skip((pageNumber - 1) * pageCount)
      .Take(pageCount)
      .ToListAsync();
    return items
      .Select(item => item.ToModel())
      .ToPaginatedList(count);
  }

  public async Task<PaginatedList<NetworkUserMeasurementLocationModel>>
    GetNetworkUserMeasurementLocations(
      string title,
      int pageNumber = QueryConstants.StartingPage,
      int pageCount = QueryConstants.DefaultPageCount
    )
  {
    var filtered = context.MeasurementLocations
      .OfType<NetworkUserMeasurementLocationEntity>()
      .Where(
        catalogue => catalogue.Title
          .StartsWith(title));
    var count = await filtered.CountAsync();
    var items = await filtered
      .OrderBy(context.PrimaryKeyOf<NetworkUserMeasurementLocationEntity>())
      .Skip((pageNumber - 1) * pageCount)
      .Take(pageCount)
      .ToListAsync();
    return items
      .Select(item => item.ToModel())
      .ToPaginatedList(count);
  }
}

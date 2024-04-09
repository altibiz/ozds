using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data;
using Ozds.Data.Entities;
using Ozds.Data.Extensions;
using SQLitePCL;

namespace Ozds.Business.Queries;

public class OzdsSchneideriEM3xxxMeterModelQueries : IOzdsQueries
{
  protected readonly OzdsDbContext context;

  public OzdsSchneideriEM3xxxMeterModelQueries(OzdsDbContext context)
  {
    this.context = context;
  }

  public async Task<SchneideriEM3xxxMeterModel?>
    SchneideriEM3xxxMeterById(string id)
  {
    var meterEntity =
      await context.Meters
        .OfType<SchneideriEM3xxxMeterEntity>()
        .WithId(id)
        .FirstOrDefaultAsync();
    if (meterEntity is not null)
    {
      return meterEntity.ToModel();
    }
    return null;
  }
  public async Task<PaginatedList<MessengerModel>> GetMessengers(
    string title,
    int pageNumber = QueryConstants.StartingPage,
    int pageCount = QueryConstants.DefaultPageCount
  )
  {
    var filtered = context.Messengers
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
  // public async Task<PaginatedList<CatalogueModel>> GetCatalogues(
  //   string title,
  //   int pageNumber = QueryConstants.StartingPage,
  //   int pageCount = QueryConstants.DefaultPageCount
  // )
  // {
  //   var filtered = context.Catalogues
  //     .Where(catalogue => catalogue.Title
  //       .StartsWith(title));
  //   var count = await filtered.CountAsync();
  //   var items = await filtered
  //     .Skip((pageNumber - 1) * pageCount)
  //     .Take(pageCount)
  //     .ToListAsync();
  //   return items
  //     .Select(item => item.ToModel())
  //     .ToPaginatedList(count);
  // }
  public async Task<PaginatedList<SchneideriEM3xxxMeasurementValidatorModel>> GetValidators(
    string title,
    int pageNumber = QueryConstants.StartingPage,
    int pageCount = QueryConstants.DefaultPageCount
  )
  {
    var filtered = context.MeasurementValidators
      .OfType<SchneideriEM3xxxMeasurementValidatorEntity>()
      .Where(catalogue => catalogue.Title.StartsWith(title));
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


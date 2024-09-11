using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Extensions;

namespace Ozds.Business.Queries;

public class OzdsSchneideriEM3xxxMeterModelQueries(DataDbContext context)
  : IOzdsQueries
{
  private readonly DataDbContext context = context;

  public async Task<SchneideriEM3xxxMeterModel?>
    SchneideriEM3xxxMeterById(string id)
  {
    var meterEntity =
      await context.Meters
        .OfType<SchneideriEM3xxxMeterEntity>()
        .Where(context.PrimaryKeyEquals<SchneideriEM3xxxMeterEntity>(id))
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

  public async Task<PaginatedList<SchneideriEM3xxxMeasurementValidatorModel>>
    GetValidators(
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
      .OrderBy(
        context.PrimaryKeyOf<SchneideriEM3xxxMeasurementValidatorEntity>())
      .Skip((pageNumber - 1) * pageCount)
      .Take(pageCount)
      .ToListAsync();
    return items
      .Select(item => item.ToModel())
      .ToPaginatedList(count);
  }
}

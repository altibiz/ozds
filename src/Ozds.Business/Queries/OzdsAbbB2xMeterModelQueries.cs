using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data;
using Ozds.Data.Entities;

namespace Ozds.Business.Queries;

public class OzdsAbbB2xMeterModelQueries(OzdsDataDbContext context)
  : IOzdsQueries
{
  private readonly OzdsDataDbContext context = context;

  public async Task<AbbB2xMeterModel?>
    AbbB2xMeterById(string id)
  {
    var meterEntity =
      await context.Meters
        .OfType<AbbB2xMeterEntity>()
        .Where(context.PrimaryKeyEquals<AbbB2xMeterEntity>(id))
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

  public async Task<PaginatedList<AbbB2xMeasurementValidatorModel>>
    GetValidators(
      string title,
      int pageNumber = QueryConstants.StartingPage,
      int pageCount = QueryConstants.DefaultPageCount
    )
  {
    var filtered = context.MeasurementValidators
      .OfType<AbbB2xMeasurementValidatorEntity>()
      .Where(catalogue => catalogue.Title.StartsWith(title));
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
}

using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data;
using Ozds.Data.Entities;
using Ozds.Data.Extensions;

namespace Ozds.Business.Queries;

public class OzdsNetworkUserMeasurementLocationModelQueries : IOzdsQueries
{
  protected readonly OzdsDbContext context;

  public OzdsNetworkUserMeasurementLocationModelQueries(OzdsDbContext context)
  {
    this.context = context;
  }

  public async Task<NetworkUserMeasurementLocationModel?>
    NetworkUserMeasurementLocationById(string id)
  {
    var meterEntity =
      await context.MeasurementLocations
        .OfType<NetworkUserMeasurementLocationEntity>()
        .WithId(id)
        .FirstOrDefaultAsync();
    if (meterEntity is not null)
    {
      return meterEntity.ToModel();
    }

    return null;
  }

  public async Task<PaginatedList<NetworkUserModel>> GetNetworkUsers(
    string title,
    int pageNumber = QueryConstants.StartingPage,
    int pageCount = QueryConstants.DefaultPageCount
  )
  {
    var filtered = context.NetworkUsers
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

  public async Task<PaginatedList<MeterModel>> GetMeters(
    string title,
    int pageNumber = QueryConstants.StartingPage,
    int pageCount = QueryConstants.DefaultPageCount
  )
  {
    var filtered = context.Meters
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

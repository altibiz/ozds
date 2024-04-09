using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Composite;
using Ozds.Data;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;

// TODO: remove any direct references to aggregate model/entity types

namespace Ozds.Business.Queries;

public class OzdsBillingQueries
{
  private readonly OzdsDbContext _dbContext;

  public OzdsBillingQueries(OzdsDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<List<NetworkUserNetworkUserCalculationBasisModel>>
    NetworkUserCalculationBasesByNetworkUser(
      string networkUserId,
      DateTimeOffset fromDate,
      DateTimeOffset toDate
    )
  {
    return (await _dbContext.NetworkUsers
        .WithId(networkUserId)
        .Join(
          _dbContext.MeasurementLocations
            .OfType<NetworkUserMeasurementLocationEntity>()
            .Include(x => x.Catalogue)
            .Include(x => x.NetworkUser)
            .Include(x => x.NetworkUser.Location),
          networkUser => networkUser.Id,
          measurementLocation => measurementLocation.NetworkUserId,
          (networkUser, measurementLocation) => new
          {
            Location = measurementLocation.NetworkUser.Location.ToModel(),
            NetworkUser = measurementLocation.NetworkUser.ToModel(),
            MeasurementLocation = measurementLocation.ToModel(),
            Catalogue = measurementLocation.Catalogue.ToModel()
          }
        )
        .Join(
          _dbContext.Meters,
          x => x.MeasurementLocation.MeterId,
          meter => meter.Id,
          (x, meter) => new
          {
            x.Location,
            x.NetworkUser,
            x.MeasurementLocation,
            x.Catalogue,
            Meter = meter.ToModel()
          }
        )
        .GroupJoin(
          _dbContext.AbbB2xAggregates
            .Where(x => x.Timestamp >= fromDate)
            .Where(x => x.Timestamp <= toDate)
            .Where(x =>
              x.Interval == IntervalEntity.QuarterHour ||
              x.Interval == IntervalEntity.Month),
          x => x.Meter.Id,
          aggregate => aggregate.MeterId,
          (x, abbB2xAggregates) => new
          {
            x.Location,
            x.NetworkUser,
            x.Meter,
            x.MeasurementLocation,
            x.Catalogue,
            AbbB2xAggregates = abbB2xAggregates
              .Select(abbB2xAggregate => abbB2xAggregate
                .ToModel())
          }
        )
        .GroupJoin(
          _dbContext.SchneideriEM3xxxAggregates
            .Where(x => x.Timestamp >= fromDate)
            .Where(x => x.Timestamp <= toDate)
            .Where(x =>
              x.Interval == IntervalEntity.QuarterHour ||
              x.Interval == IntervalEntity.Month),
          x => x.Meter.Id,
          aggregate => aggregate.MeterId,
          (x, schneideriEM3xxxAggregates) => new
          {
            x.Location,
            x.NetworkUser,
            x.Meter,
            x.MeasurementLocation,
            x.Catalogue,
            x.AbbB2xAggregates,
            SchneideriEM3xxxAggregates = schneideriEM3xxxAggregates
              .Select(schneideriEM3xxxAggregate => schneideriEM3xxxAggregate
                .ToModel())
          }
        )
        .ToListAsync())
      .Select(x => new NetworkUserNetworkUserCalculationBasisModel(
        fromDate,
        toDate,
        Location: x.Location,
        NetworkUser: x.NetworkUser,
        MeasurementLocation: x.MeasurementLocation,
        Meter: x.Meter,
        Catalogue: x.Catalogue,
        Aggregates: Enumerable.Empty<AggregateModel>()
          .Concat(x.AbbB2xAggregates)
          .Concat(x.SchneideriEM3xxxAggregates)
          .ToList()
      ))
      .ToList();
  }

  public async Task<NetworkUserInvoiceIssuingBasisModel>
    IssuingBasisForNetworkUser(
      string networkUserId,
      DateTimeOffset fromDate,
      DateTimeOffset toDate
    )
  {
    var networkUser = await _dbContext.NetworkUsers
                        .WithId(networkUserId)
                        .Include(x => x.Location)
                        .FirstOrDefaultAsync() ??
                      throw new InvalidOperationException(
                        "Network user not found");
    var calculationBases = await NetworkUserCalculationBasesByNetworkUser(
      networkUserId,
      fromDate,
      toDate
    );

    return new NetworkUserInvoiceIssuingBasisModel(
      networkUser.Location.ToModel(),
      networkUser.ToModel(),
      fromDate,
      toDate,
      calculationBases
    );
  }

  public Task<List<LocationNetworkUserCalculationBasisModel>>
    NetworkUserCalculationBasesByLocation(
      string locationId,
      DateTimeOffset fromDate,
      DateTimeOffset toDate
    )
  {
    throw new NotImplementedException();
  }

  public Task<LocationInvoiceIssuingBasisModel> IssuingBasisForLocation(
    string locationId,
    DateTimeOffset fromDate,
    DateTimeOffset toDate
  )
  {
    throw new NotImplementedException();
  }
}

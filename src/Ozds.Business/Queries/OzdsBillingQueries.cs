using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Composite;
using Ozds.Business.Models.Enums;
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

  public async Task<List<CalculationBasisModel>> CalculationSpecificationsByNetworkUser(
    string networkUserId,
    DateTimeOffset fromDate,
    DateTimeOffset toDate
  ) =>
    (await _dbContext.NetworkUsers
      .WithId(networkUserId)
      .Join(
        _dbContext.MeasurementLocations.OfType<NetworkUserMeasurementLocationEntity>(),
        networkUser => networkUser.Id,
        measurementLocation => measurementLocation.NetworkUserId,
        (networkUser, measurementLocation) => new
        {
          NetworkUser = networkUser.ToModel(),
          MeasurementLocation = measurementLocation.ToModel()
        }
      )
      .Join(
        _dbContext.Meters,
        x => x.MeasurementLocation.MeterId,
        meter => meter.Id,
        (x, meter) => new
        {
          x.MeasurementLocation,
          x.NetworkUser,
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
          x.Meter,
          x.NetworkUser,
          x.MeasurementLocation,
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
          x.Meter,
          x.NetworkUser,
          x.MeasurementLocation,
          x.AbbB2xAggregates,
          SchneideriEM3xxxAggregates = schneideriEM3xxxAggregates
            .Select(schneideriEM3xxxAggregate => schneideriEM3xxxAggregate
              .ToModel())
        }
      )
      .ToListAsync())
      .Select(x => new CalculationBasisModel(
        NetworkUser: x.NetworkUser,
        MeasurementLocation: x.MeasurementLocation,
        Meter: x.Meter,
        Aggregates: Enumerable.Empty<IAggregate>()
          .Concat(x.AbbB2xAggregates)
          .Concat(x.SchneideriEM3xxxAggregates)
          .ToList()
      ))
      .ToList();
}

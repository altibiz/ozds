using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Models.Enums;
using Ozds.Data;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;

// TODO: remove any direct references to aggregate model/entity types

namespace Ozds.Business.Queries;

public record OzdsCalculationSpecification(
  string NetworkUserId,
  string MeasurementLocationId,
  string MeterId,
  DateTimeOffset FromDate,
  DateTimeOffset ToDate,
  float ActiveEnergyTotalImportT1Min_Wh,
  float ActiveEnergyTotalImportT1Max_Wh,
  float ActiveEnergyTotalImportT2Min_Wh,
  float ActiveEnergyTotalImportT2Max_Wh,
  float ActiveEnergyTotalImportT0Min_Wh,
  float ActiveEnergyTotalImportT0Max_Wh,
  float ReactiveEnergyTotalImportT0Max_VARh,
  float ReactiveEnergyTotalImportT0Min_VARh,
  float ReactiveEnergyTotalExportT0Max_VARh,
  float ReactiveEnergyTotalExportT0Min_VARh,
  float ActivePowerTotalImportT1Max_W
);

public class OzdsBillingQueries
{
  private readonly OzdsDbContext _dbContext;

  public OzdsBillingQueries(OzdsDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<List<OzdsCalculationSpecification>> GetBillingData(
    string networkUserId,
    DateTimeOffset fromDate,
    DateTimeOffset toDate
  )
  {
    var responses = await _dbContext.NetworkUsers
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
          Meter = meter
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
      .ToListAsync();

    return responses
      .Select(response => new OzdsCalculationSpecification(
          NetworkUserId: response.NetworkUser.Id,
          MeasurementLocationId: response.MeasurementLocation.Id,
          MeterId: response.Meter.Id,
          FromDate: fromDate,
          ToDate: toDate,
          ActiveEnergyTotalImportT1Min_Wh: response.AbbB2xAggregates
            .FirstOrDefault(x => x.Interval == IntervalModel.Month)!
            .ActiveEnergyTotalImportT1Min_Wh,
          ActiveEnergyTotalImportT1Max_Wh: response.AbbB2xAggregates
            .FirstOrDefault(x => x.Interval == IntervalModel.Month)!
            .ActiveEnergyTotalImportT1Max_Wh,
          ActiveEnergyTotalImportT2Min_Wh: response.AbbB2xAggregates
            .FirstOrDefault(x => x.Interval == IntervalModel.Month)!
            .ActiveEnergyTotalImportT2Min_Wh,
          ActiveEnergyTotalImportT2Max_Wh: response.AbbB2xAggregates
            .FirstOrDefault(x => x.Interval == IntervalModel.Month)!
            .ActiveEnergyTotalImportT2Max_Wh,
          ActiveEnergyTotalImportT0Min_Wh: response.AbbB2xAggregates
            .FirstOrDefault(x => x.Interval == IntervalModel.Month)!
            .ActiveEnergyTotalImportT0Min_Wh,
          ActiveEnergyTotalImportT0Max_Wh: response.AbbB2xAggregates
            .FirstOrDefault(x => x.Interval == IntervalModel.Month)!
            .ActiveEnergyTotalImportT0Max_Wh,
          ReactiveEnergyTotalImportT0Max_VARh: response.AbbB2xAggregates
            .FirstOrDefault(x => x.Interval == IntervalModel.Month)!
            .ReactiveEnergyTotalImportT0Max_VARh,
          ReactiveEnergyTotalImportT0Min_VARh: response.AbbB2xAggregates
            .FirstOrDefault(x => x.Interval == IntervalModel.Month)!
            .ReactiveEnergyTotalImportT0Min_VARh,
          ReactiveEnergyTotalExportT0Max_VARh: response.AbbB2xAggregates
            .FirstOrDefault(x => x.Interval == IntervalModel.Month)!
            .ReactiveEnergyTotalExportT0Max_VARh,
          ReactiveEnergyTotalExportT0Min_VARh: response.AbbB2xAggregates
            .FirstOrDefault(x => x.Interval == IntervalModel.Month)!
            .ReactiveEnergyTotalExportT0Min_VARh,
          ActivePowerTotalImportT1Max_W: response.AbbB2xAggregates
            .Where(x => x.Interval == IntervalModel.QuarterHour)
            .Max(x => x.ActivePowerL1NetT0Avg_W)
        )
      )
      .ToList();
  }
}

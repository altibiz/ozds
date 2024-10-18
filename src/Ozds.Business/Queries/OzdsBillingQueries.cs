using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Composite;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;

// TODO: remove any direct references to aggregate model/entity types

namespace Ozds.Business.Queries;

public class OzdsBillingQueries(DataDbContext dbContext) : IQueries
{
  private readonly DataDbContext _dbContext = dbContext;

  public async Task<NetworkUserInvoiceIssuingBasisModel>
    IssuingBasisForNetworkUser(
      string networkUserId,
      DateTimeOffset fromDate,
      DateTimeOffset toDate
    )
  {
    var networkUser = await _dbContext.NetworkUsers
        .Where(
          _dbContext.PrimaryKeyEquals<NetworkUserEntity>(
            networkUserId))
        .Include(x => x.Location)
        .Include(x => x.Location.RegulatoryCatalogue)
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
      networkUser.Location.RegulatoryCatalogue.ToModel(),
      fromDate,
      toDate,
      calculationBases
    );
  }

  private async Task<List<NetworkUserCalculationBasisModel>>
    NetworkUserCalculationBasesByNetworkUser(
      string networkUserId,
      DateTimeOffset fromDate,
      DateTimeOffset toDate
    )
  {
    return (await _dbContext.NetworkUsers
        .Where(_dbContext.PrimaryKeyEquals<NetworkUserEntity>(networkUserId))
        .Join(
          _dbContext.MeasurementLocations
            .OfType<NetworkUserMeasurementLocationEntity>()
            .Include(x => x.NetworkUserCatalogue)
            .Include(x => x.NetworkUser)
            .Include(x => x.NetworkUser.Location)
            .Include(x => x.NetworkUser.Location.RegulatoryCatalogue),
          _dbContext.PrimaryKeyOf<NetworkUserEntity>(),
          _dbContext.ForeignKeyOf<NetworkUserMeasurementLocationEntity>(
            nameof(NetworkUserMeasurementLocationEntity.NetworkUser)),
          (networkUser, measurementLocation) =>
            new NetworkUserCalculationBasesByNetworkUserIntermediary
            {
              Location = measurementLocation.NetworkUser.Location,
              NetworkUser = measurementLocation.NetworkUser,
              MeasurementLocation = measurementLocation,
              UsageNetworkUserCatalogue =
                measurementLocation.NetworkUserCatalogue,
              SupplyRegulatoryCatalogue = measurementLocation.NetworkUser
                .Location
                .RegulatoryCatalogue
            }
        )
        .Join(
          _dbContext.Meters,
          _dbContext
            .ForeignKeyOf<MeasurementLocationEntity>(
              nameof(MeasurementLocationEntity.Meter))
            .Prefix(
              (NetworkUserCalculationBasesByNetworkUserIntermediary x) =>
                x.MeasurementLocation),
          _dbContext.PrimaryKeyOf<MeterEntity>(),
          (x, meter) =>
            new NetworkUserCalculationBasesByNetworkUserIntermediary
            {
              Location = x.Location,
              NetworkUser = x.NetworkUser,
              MeasurementLocation = x.MeasurementLocation,
              UsageNetworkUserCatalogue = x.UsageNetworkUserCatalogue,
              SupplyRegulatoryCatalogue = x.SupplyRegulatoryCatalogue,
              Meter = meter
            }
        )
        .GroupJoin(
          _dbContext.AbbB2xAggregates
            .Where(x => x.Timestamp >= fromDate)
            .Where(x => x.Timestamp <= toDate)
            .Where(x => x.Interval == IntervalEntity.QuarterHour),
          _dbContext
            .PrimaryKeyOf<MeterEntity>()
            .Prefix(
              (NetworkUserCalculationBasesByNetworkUserIntermediary x) =>
                x.Meter),
          _dbContext.ForeignKeyOf<AbbB2xAggregateEntity>(
            nameof(AbbB2xAggregateEntity.Meter)
          ),
          (x, abbB2xAggregates) =>
            new
            {
              x.Location,
              x.NetworkUser,
              x.MeasurementLocation,
              x.Meter,
              x.UsageNetworkUserCatalogue,
              x.SupplyRegulatoryCatalogue,
              abbB2xAggregates
            }
        )
        .SelectMany(
          x => x.abbB2xAggregates.DefaultIfEmpty(),
          (x, abbAggregate) =>
            new NetworkUserCalculationBasesByNetworkUserIntermediary
            {
              Location = x.Location,
              NetworkUser = x.NetworkUser,
              MeasurementLocation = x.MeasurementLocation,
              Meter = x.Meter,
              UsageNetworkUserCatalogue = x.UsageNetworkUserCatalogue,
              SupplyRegulatoryCatalogue = x.SupplyRegulatoryCatalogue,
              AbbB2xAggregate = abbAggregate
            }
        )
        .GroupJoin(
          _dbContext.SchneideriEM3xxxAggregates
            .Where(x => x.Timestamp >= fromDate)
            .Where(x => x.Timestamp <= toDate)
            .Where(x => x.Interval == IntervalEntity.QuarterHour),
          _dbContext
            .PrimaryKeyOf<MeterEntity>()
            .Prefix(
              (NetworkUserCalculationBasesByNetworkUserIntermediary x) =>
                x.Meter),
          _dbContext.ForeignKeyOf<SchneideriEM3xxxAggregateEntity>(
            nameof(AbbB2xAggregateEntity.Meter)
          ),
          (x, schneideriEM3xxxAggregates) =>
            new
            {
              x.Location,
              x.NetworkUser,
              x.MeasurementLocation,
              x.Meter,
              x.UsageNetworkUserCatalogue,
              x.SupplyRegulatoryCatalogue,
              x.AbbB2xAggregate,
              schneideriEM3xxxAggregates
            }
        )
        .SelectMany(
          x => x.schneideriEM3xxxAggregates.DefaultIfEmpty(),
          (x, schneiderAggregate) =>
            new NetworkUserCalculationBasesByNetworkUserIntermediary
            {
              Location = x.Location,
              NetworkUser = x.NetworkUser,
              MeasurementLocation = x.MeasurementLocation,
              Meter = x.Meter,
              UsageNetworkUserCatalogue = x.UsageNetworkUserCatalogue,
              SupplyRegulatoryCatalogue = x.SupplyRegulatoryCatalogue,
              AbbB2xAggregate = x.AbbB2xAggregate,
              SchneideriEM3xxxAggregate = schneiderAggregate
            }
        )
        .ToListAsync())
      .GroupBy(x => x.MeasurementLocation.Id)
      .Select(
        x => new NetworkUserCalculationBasisModel(
          fromDate,
          toDate,
          Location: x.First().Location.ToModel(),
          NetworkUser: x.First().NetworkUser.ToModel(),
          MeasurementLocation: x.First().MeasurementLocation.ToModel(),
          Meter: x.First().Meter.ToModel(),
          UsageNetworkUserCatalogue: x.First().UsageNetworkUserCatalogue
            .ToModel(),
          SupplyRegulatoryCatalogue: x.First().SupplyRegulatoryCatalogue
            .ToModel(),
          Aggregates: Enumerable.Empty<AggregateModel>()
            .Concat(
              x
                .Where(x => x.AbbB2xAggregate is not null)
                .Select(x => x.AbbB2xAggregate!.ToModel()))
            .Concat(
              x
                .Where(x => x.SchneideriEM3xxxAggregate is not null)
                .Select(x => x.SchneideriEM3xxxAggregate!.ToModel()))
            .ToList()
        )
      )
      .ToList();
  }

  private readonly struct NetworkUserCalculationBasesByNetworkUserIntermediary
  {
    public LocationEntity Location { get; init; }
    public NetworkUserEntity NetworkUser { get; init; }

    public NetworkUserMeasurementLocationEntity MeasurementLocation
    {
      get;
      init;
    }

    public NetworkUserCatalogueEntity UsageNetworkUserCatalogue { get; init; }
    public RegulatoryCatalogueEntity SupplyRegulatoryCatalogue { get; init; }
    public MeterEntity Meter { get; init; }
    public AbbB2xAggregateEntity? AbbB2xAggregate { get; init; }

    public SchneideriEM3xxxAggregateEntity? SchneideriEM3xxxAggregate
    {
      get;
      init;
    }
  }
}

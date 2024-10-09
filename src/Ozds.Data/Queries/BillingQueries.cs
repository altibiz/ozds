using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Composite;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;
using Ozds.Data.Queries.Abstractions;

// TODO: remove any direct references to aggregate entity types
// TODO: location invoices

namespace Ozds.Data.Queries;

public class BillingQueries(
  IDbContextFactory<DataDbContext> factory
) : IQueries
{
  public async Task<NetworkUserInvoiceIssuingBasisEntity>
    IssuingBasisForNetworkUser(
      string networkUserId,
      DateTimeOffset fromDate,
      DateTimeOffset toDate,
      CancellationToken cancellationToken
    )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);
    var networkUser = await context.NetworkUsers
        .Where(
          context.PrimaryKeyEquals<NetworkUserEntity>(
            networkUserId))
        .Include(x => x.Location)
        .Include(x => x.Location.RegulatoryCatalogue)
        .FirstOrDefaultAsync(cancellationToken) ??
      throw new InvalidOperationException(
        "Network user not found");
    var calculationBases = await NetworkUserCalculationBasesByNetworkUser(
      networkUserId,
      fromDate,
      toDate,
      cancellationToken
    );

    return new NetworkUserInvoiceIssuingBasisEntity(
      networkUser.Location,
      networkUser,
      networkUser.Location.RegulatoryCatalogue,
      fromDate,
      toDate,
      calculationBases
    );
  }

  public Task<LocationInvoiceIssuingBasisEntity> IssuingBasisForLocation(
    string locationId,
    DateTimeOffset fromDate,
    DateTimeOffset toDate,
    CancellationToken cancellationToken
  )
  {
    throw new NotImplementedException();
  }

  private async Task<List<NetworkUserCalculationBasisEntity>>
    NetworkUserCalculationBasesByNetworkUser(
      string networkUserId,
      DateTimeOffset fromDate,
      DateTimeOffset toDate,
      CancellationToken cancellationToken
    )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    return (await context.NetworkUsers
        .Where(context.PrimaryKeyEquals<NetworkUserEntity>(networkUserId))
        .Join(
          context.MeasurementLocations
            .OfType<NetworkUserMeasurementLocationEntity>()
            .Include(x => x.NetworkUserCatalogue)
            .Include(x => x.NetworkUser)
            .Include(x => x.NetworkUser.Location)
            .Include(x => x.NetworkUser.Location.RegulatoryCatalogue),
          context.PrimaryKeyOf<NetworkUserEntity>(),
          context.ForeignKeyOf<NetworkUserMeasurementLocationEntity>(
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
          context.Meters,
          context
            .ForeignKeyOf<MeasurementLocationEntity>(
              nameof(MeasurementLocationEntity.Meter))
            .Prefix(
              (NetworkUserCalculationBasesByNetworkUserIntermediary x) =>
                x.MeasurementLocation),
          context.PrimaryKeyOf<MeterEntity>(),
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
          context.AbbB2xAggregates
            .Where(x => x.Timestamp >= fromDate)
            .Where(x => x.Timestamp <= toDate)
            .Where(x => x.Interval == IntervalEntity.QuarterHour),
          context
            .PrimaryKeyOf<MeterEntity>()
            .Prefix(
              (NetworkUserCalculationBasesByNetworkUserIntermediary x) =>
                x.Meter),
          context.ForeignKeyOf<AbbB2xAggregateEntity>(
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
          context.SchneideriEM3xxxAggregates
            .Where(x => x.Timestamp >= fromDate)
            .Where(x => x.Timestamp <= toDate)
            .Where(x => x.Interval == IntervalEntity.QuarterHour),
          context
            .PrimaryKeyOf<MeterEntity>()
            .Prefix(
              (NetworkUserCalculationBasesByNetworkUserIntermediary x) =>
                x.Meter),
          context.ForeignKeyOf<SchneideriEM3xxxAggregateEntity>(
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
        .ToListAsync(cancellationToken))
      .GroupBy(x => x.MeasurementLocation.Id)
      .Select(
        x => new NetworkUserCalculationBasisEntity(
          fromDate,
          toDate,
          Location: x.First().Location,
          NetworkUser: x.First().NetworkUser,
          MeasurementLocation: x.First().MeasurementLocation,
          Meter: x.First().Meter,
          UsageNetworkUserCatalogue: x.First().UsageNetworkUserCatalogue,
          SupplyRegulatoryCatalogue: x.First().SupplyRegulatoryCatalogue,
          Aggregates: Enumerable.Empty<AggregateEntity>()
            .Concat(x
              .Where(x => x.AbbB2xAggregate is not null)
              .Select(x => x.AbbB2xAggregate!))
            .Concat(x
              .Where(x => x.SchneideriEM3xxxAggregate is not null)
              .Select(x => x.SchneideriEM3xxxAggregate!))
            .ToList()
        )
      )
      .ToList();
  }

  public Task<List<LocationNetworkUserCalculationBasisEntity>>
    NetworkUserCalculationBasesByLocation(
      string locationId,
      DateTimeOffset fromDate,
      DateTimeOffset toDate,
      CancellationToken cancellationToken
    )
  {
    throw new NotImplementedException();
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

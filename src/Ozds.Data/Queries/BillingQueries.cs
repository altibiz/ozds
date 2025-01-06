using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Composite;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;
using Ozds.Data.Queries.Abstractions;

namespace Ozds.Data.Queries;

public class BillingQueries(
  IDbContextFactory<DataDbContext> factory
) : IQueries
{
  public async Task<NetworkUserInvoiceIssuingBasisEntity>
    ReadIssuingBasisForNetworkUser(
      string networkUserId,
      DateTimeOffset fromDate,
      DateTimeOffset toDate,
      CancellationToken cancellationToken
    )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    await context.Database
      .ExecuteSqlRawAsync("SET jit = on;", cancellationToken);

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
        .Include(x => x.Location)
        .ThenInclude(x => x.RegulatoryCatalogue)
        .Where(context.PrimaryKeyEquals<NetworkUserEntity>(networkUserId))
        .Join(
          context.MeasurementLocations
            .OfType<NetworkUserMeasurementLocationEntity>()
            .Include(x => x.NetworkUserCatalogue)
            .Include(x => x.Meter),
          context.PrimaryKeyOf<NetworkUserEntity>(),
          context.ForeignKeyOf<NetworkUserMeasurementLocationEntity>(
            nameof(NetworkUserMeasurementLocationEntity.NetworkUser)),
          (networkUser, measurementLocation) =>
            new NetworkUserCalculationBasesByNetworkUserIntermediary
            {
              Location = networkUser.Location,
              NetworkUser = networkUser,
              MeasurementLocation = measurementLocation,
              UsageNetworkUserCatalogue =
                measurementLocation.NetworkUserCatalogue,
              SupplyRegulatoryCatalogue = networkUser
                .Location
                .RegulatoryCatalogue,
              Meter = measurementLocation.Meter
            }
        )
        .GroupJoin(
          context.AbbB2xAggregates
            .Where(x => x.Timestamp >= fromDate)
            .Where(x => x.Timestamp <= toDate)
            .Where(x => x.Interval == IntervalEntity.QuarterHour),
          context
            .PrimaryKeyOf<MeasurementLocationEntity>()
            .Prefix(
              (NetworkUserCalculationBasesByNetworkUserIntermediary x) =>
                x.MeasurementLocation),
          context.ForeignKeyOf<AbbB2xAggregateEntity>(
            nameof(AbbB2xAggregateEntity.MeasurementLocation)
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
            .PrimaryKeyOf<MeasurementLocationEntity>()
            .Prefix(
              (NetworkUserCalculationBasesByNetworkUserIntermediary x) =>
                x.MeasurementLocation),
          context.ForeignKeyOf<SchneideriEM3xxxAggregateEntity>(
            nameof(AbbB2xAggregateEntity.MeasurementLocation)
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

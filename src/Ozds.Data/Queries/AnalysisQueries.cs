using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Composite;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Entities.Joins;
using Ozds.Data.Extensions;
using Ozds.Data.Queries.Abstractions;

// TODO: remove references to aggregate entity types
// TODO: location measurement locations
// TODO: separate out timezone stuff and make it testable
// TODO: all in one when https://github.com/dotnet/efcore/issues/28125

namespace Ozds.Data.Queries;

public class AnalysisQueries(
  IDbContextFactory<DataDbContext> factory
) : IQueries
{
  public async Task<PaginatedList<AnalysisBasisEntity>>
    ReadAnalysisBasesByRepresentative(
      string representativeId,
      RoleEntity role,
      DateTimeOffset fromDate,
      DateTimeOffset toDate,
      int pageNumber,
      CancellationToken cancellationToken,
      int pageCount = QueryConstants.DefaultPageCount
    )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    await context.Database
      .ExecuteSqlRawAsync("SET jit = on;", cancellationToken);

    var byRepresentative =
      MakeReadDetailedMeasurementLocationsByRepresentativeQuery(
        context,
        representativeId,
        role
      );

    var count = await byRepresentative.CountAsync();
    var itemsByRepresentative = await byRepresentative
      .Skip((pageNumber - 1) * pageCount)
      .Take(pageCount)
      .ToListAsync(cancellationToken);

    var byMeasurementLocation =
      MakeReadDetailedMeasurementLocationsByMeasurementLocationIntermediary(
        context,
        itemsByRepresentative.Select(x => x.MeasurementLocation).ToList(),
        itemsByRepresentative.Select(x => x.Meter).ToList(),
        fromDate,
        toDate
      );

    var itemsByMeasurementLocation = await byMeasurementLocation
      .ToListAsync(cancellationToken);

    return MakeAnalysisBases(itemsByRepresentative, itemsByMeasurementLocation)
      .ToPaginatedList(count);
  }

  public async Task<List<AnalysisBasisEntity>>
    ReadAnalysisBasesByRepresentative(
      string representativeId,
      RoleEntity role,
      DateTimeOffset fromDate,
      DateTimeOffset toDate,
      CancellationToken cancellationToken
    )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    await context.Database
      .ExecuteSqlRawAsync("SET jit = on;", cancellationToken);

    var byRepresentative =
      MakeReadDetailedMeasurementLocationsByRepresentativeQuery(
        context,
        representativeId,
        role
      );

    var itemsByRepresentative = await byRepresentative
      .ToListAsync(cancellationToken);

    var byMeasurementLocation =
      MakeReadDetailedMeasurementLocationsByMeasurementLocationIntermediary(
        context,
        itemsByRepresentative.Select(x => x.MeasurementLocation).ToList(),
        itemsByRepresentative.Select(x => x.Meter).ToList(),
        fromDate,
        toDate
      );

    var itemsByMeasurementLocation = await byMeasurementLocation
      .ToListAsync(cancellationToken);

    return MakeAnalysisBases(itemsByRepresentative, itemsByMeasurementLocation)
      .ToList();
  }

  private static
    IEnumerable<AnalysisBasisEntity>
    MakeAnalysisBases(
      List<DetailedMeasurementLocationsByRepresentativeIntermediary>
        byRepresentative,
      List<DetailedMeasurementLocationsByMeasurementLocationIntermediary>
        byMeasurementLocation
    )
  {
    var analyses = byRepresentative
      .Select(
        byRepresentative =>
        {
          var lastMeasurement = Enumerable
            .Empty<MeasurementEntity>()
            .Concat(byMeasurementLocation
              .Where(x => x.AbbLastMeasurement?.MeterId
                == byRepresentative.Meter.Id)
              .Select(x => x.AbbLastMeasurement)
              .OfType<MeasurementEntity>())
            .Concat(byMeasurementLocation
              .Where(x => x.SchneiderLastMeasurement?.MeterId
                == byRepresentative.Meter.Id)
              .Select(x => x.SchneiderLastMeasurement)
              .OfType<MeasurementEntity>())
            .FirstOrDefault();
          if (lastMeasurement is null)
          {
            return null;
          }

          var monthlyAggregates = Enumerable
            .Empty<AggregateEntity>()
            .Concat(byMeasurementLocation
              .Select(x => x.AbbMonthlyAggregate)
              .OfType<AggregateEntity>()
              .Where(x => x.MeterId == byRepresentative.Meter.Id))
            .Concat(byMeasurementLocation
              .Select(x => x.SchneiderMonthlyAggregate)
              .OfType<AggregateEntity>())
              .Where(x => x.MeterId == byRepresentative.Meter.Id)
            .ToList();

          var monthlyMaxPowerAggregates = Enumerable
            .Empty<AggregateEntity>()
            .Concat(byMeasurementLocation
              .Select(x => x.AbbMaxPowerAggregate)
              .OfType<AggregateEntity>()
              .Where(x => x.MeterId == byRepresentative.Meter.Id))
            .Concat(byMeasurementLocation
              .Select(x => x.SchneiderMaxPowerAggregate)
              .OfType<AggregateEntity>()
              .Where(x => x.MeterId == byRepresentative.Meter.Id))
            .ToList();

          var calculations = byMeasurementLocation
            .Select(x => x.NetworkUserCalculation)
            .OfType<NetworkUserCalculationEntity>()
            .Where(x => x.NetworkUserMeasurementLocationId
              == byRepresentative.MeasurementLocation.Id)
            .OfType<CalculationEntity>()
            .ToList();

          var invoices = byMeasurementLocation
            .Where(x =>
              x.NetworkUserCalculation?.NetworkUserMeasurementLocationId
              == byRepresentative.MeasurementLocation.Id)
            .Select(x => x.NetworkUserInvoice)
            .OfType<InvoiceEntity>()
            .DistinctBy(x => x.Id)
            .ToList();

          return new AnalysisBasisEntity(
            byRepresentative.Location,
            byRepresentative.NetworkUser,
            byRepresentative.MeasurementLocation,
            byRepresentative.Meter,
            calculations,
            invoices,
            lastMeasurement,
            monthlyAggregates,
            monthlyMaxPowerAggregates
          );
        })
      .OfType<AnalysisBasisEntity>();

    return analyses;
  }

  private static
    IQueryable<DetailedMeasurementLocationsByRepresentativeIntermediary>
    MakeReadDetailedMeasurementLocationsByRepresentativeQuery(
      DataDbContext context,
      string representativeId,
      RoleEntity role
    )
  {
    var query = role switch
    {
      RoleEntity.LocationRepresentative
        or RoleEntity.NetworkUserRepresentative =>
        context.NetworkUserRepresentatives
          .Where(context.ForeignKeyEquals<NetworkUserRepresentativeEntity>(
            nameof(NetworkUserRepresentativeEntity.Representative),
            representativeId
          ))
          .Include(x => x.NetworkUser)
          .ThenInclude(x => x.Location)
          .Include(x => x.NetworkUser)
          .ThenInclude(x => x.NetworkUserMeasurementLocations)
          .ThenInclude(y => y.Meter)
          .SelectMany(x => x.NetworkUser.NetworkUserMeasurementLocations
            .Select(y =>
              new DetailedMeasurementLocationsByRepresentativeIntermediary
              {
                Location = x.NetworkUser.Location,
                NetworkUser = x.NetworkUser,
                MeasurementLocation = y,
                Meter = y.Meter
              }))
          .Union(context.LocationRepresentatives
            .Where(context.ForeignKeyEquals<LocationRepresentativeEntity>(
              nameof(LocationRepresentativeEntity.Representative),
              representativeId
            ))
            .Include(x => x.Location)
            .ThenInclude(x => x.NetworkUsers)
            .ThenInclude(x => x.NetworkUserMeasurementLocations)
            .ThenInclude(x => x.Meter)
            .SelectMany(x => x.Location.NetworkUsers
              .SelectMany(y => y.NetworkUserMeasurementLocations
                .Select(z =>
                  new DetailedMeasurementLocationsByRepresentativeIntermediary
                  {
                    Location = x.Location,
                    NetworkUser = y,
                    MeasurementLocation = z,
                    Meter = z.Meter
                  }))))
          .DistinctBy(context
            .PrimaryKeyOf<NetworkUserMeasurementLocationEntity>()
            .Prefix(
              (DetailedMeasurementLocationsByRepresentativeIntermediary x) =>
                x.MeasurementLocation)),
      RoleEntity.OperatorRepresentative =>
        context.MeasurementLocations
          .OfType<NetworkUserMeasurementLocationEntity>()
          .Include(x => x.NetworkUser)
          .ThenInclude(x => x.Location)
          .Include(x => x.Meter)
          .Select(x =>
            new DetailedMeasurementLocationsByRepresentativeIntermediary
            {
              Location = x.NetworkUser.Location,
              NetworkUser = x.NetworkUser,
              MeasurementLocation = x,
              Meter = x.Meter
            }),
      _ => throw new ArgumentOutOfRangeException(nameof(role))
    };

    return query;
  }

  private static
    IQueryable<DetailedMeasurementLocationsByMeasurementLocationIntermediary>
    MakeReadDetailedMeasurementLocationsByMeasurementLocationIntermediary(
      DataDbContext context,
      List<NetworkUserMeasurementLocationEntity> measurementLocations,
      List<MeterEntity> meters,
      DateTimeOffset fromDate,
      DateTimeOffset toDate
    )
  {
    var query = context.NetworkUserCalculations
      .Where(context.ForeignKeyIn<NetworkUserCalculationEntity>(
        nameof(NetworkUserCalculationEntity.NetworkUserMeasurementLocation),
        measurementLocations.Select(x => x.Id)
      ))
      .Where(x => x.FromDate >= fromDate)
      .Where(x => x.FromDate < toDate)
      .Include(x => x.NetworkUserInvoice)
      .Select(x =>
        new DetailedMeasurementLocationsByMeasurementLocationIntermediary
        {
          NetworkUserCalculation = x,
          NetworkUserInvoice = x.NetworkUserInvoice,
          AbbLastMeasurement = null,
          SchneiderLastMeasurement = null,
          AbbMonthlyAggregate = null,
          SchneiderMonthlyAggregate = null,
          AbbMaxPowerAggregate = null,
          SchneiderMaxPowerAggregate = null
        })
      .Union(context.AbbB2xMeasurements
        .Where(context.ForeignKeyIn<AbbB2xMeasurementEntity>(
          nameof(AbbB2xMeasurementEntity.Meter),
          meters.Select(x => x.Id)
        ))
        .Where(x => x.Timestamp >= fromDate)
        .Where(x => x.Timestamp < toDate)
        .OrderByDescending(x => x.Timestamp)
        .Select(x =>
          new DetailedMeasurementLocationsByMeasurementLocationIntermediary
          {
            NetworkUserCalculation = null,
            NetworkUserInvoice = null,
            AbbLastMeasurement = x,
            SchneiderLastMeasurement = null,
            AbbMonthlyAggregate = null,
            SchneiderMonthlyAggregate = null,
            AbbMaxPowerAggregate = null,
            SchneiderMaxPowerAggregate = null
          })
        .Take(1))
      .Union(context.SchneideriEM3xxxMeasurements
        .Where(context.ForeignKeyIn<SchneideriEM3xxxMeasurementEntity>(
          nameof(SchneideriEM3xxxMeasurementEntity.Meter),
          meters.Select(x => x.Id)
        ))
        .Where(x => x.Timestamp >= fromDate)
        .Where(x => x.Timestamp < toDate)
        .OrderByDescending(x => x.Timestamp)
        .Select(x =>
          new DetailedMeasurementLocationsByMeasurementLocationIntermediary
          {
            NetworkUserCalculation = null,
            NetworkUserInvoice = null,
            AbbLastMeasurement = null,
            SchneiderLastMeasurement = x,
            AbbMonthlyAggregate = null,
            SchneiderMonthlyAggregate = null,
            AbbMaxPowerAggregate = null,
            SchneiderMaxPowerAggregate = null
          })
        .Take(1))
      .Union(context.AbbB2xAggregates
        .Where(context.ForeignKeyIn<AbbB2xAggregateEntity>(
          nameof(AbbB2xAggregateEntity.Meter),
          meters.Select(x => x.Id)
        ))
        .Where(x => x.Timestamp >= fromDate)
        .Where(x => x.Timestamp < toDate)
        .Where(x => x.Interval == IntervalEntity.Month)
        .Select(x =>
          new DetailedMeasurementLocationsByMeasurementLocationIntermediary
          {
            NetworkUserCalculation = null,
            NetworkUserInvoice = null,
            AbbLastMeasurement = null,
            SchneiderLastMeasurement = null,
            AbbMonthlyAggregate = x,
            SchneiderMonthlyAggregate = null,
            AbbMaxPowerAggregate = null,
            SchneiderMaxPowerAggregate = null
          }))
      .Union(context.SchneideriEM3xxxAggregates
        .Where(context.ForeignKeyIn<SchneideriEM3xxxAggregateEntity>(
          nameof(SchneideriEM3xxxAggregateEntity.Meter),
          meters.Select(x => x.Id)
        ))
        .Where(x => x.Timestamp >= fromDate)
        .Where(x => x.Timestamp < toDate)
        .Where(x => x.Interval == IntervalEntity.Month)
        .Select(x =>
          new DetailedMeasurementLocationsByMeasurementLocationIntermediary
          {
            NetworkUserCalculation = null,
            NetworkUserInvoice = null,
            AbbLastMeasurement = null,
            SchneiderLastMeasurement = null,
            AbbMonthlyAggregate = null,
            SchneiderMonthlyAggregate = x,
            AbbMaxPowerAggregate = null,
            SchneiderMaxPowerAggregate = null
          }))
      .Union(context.AbbB2xAggregates
        .Where(context.ForeignKeyIn<AbbB2xAggregateEntity>(
          nameof(AbbB2xAggregateEntity.Meter),
          meters.Select(x => x.Id)
        ))
        .Where(x => x.Timestamp >= fromDate)
        .Where(x => x.Timestamp < toDate)
        .Where(x => x.Interval == IntervalEntity.QuarterHour)
        .GroupBy(x => new
        {
          x.MeterId,
          EF.Functions.AtTimeZone(x.Timestamp, "Europe/Zagreb").Year,
          EF.Functions.AtTimeZone(x.Timestamp, "Europe/Zagreb").Month
        })
        .Select(x => x
          .Select(x => new
          {
            ActivePowerTotalNetT0Avg_W = x == null
              ? 0
              : x.ActivePowerL1NetT0Avg_W
                + x.ActivePowerL2NetT0Avg_W
                + x.ActivePowerL3NetT0Avg_W,
            AbbB2xAggregate = x
          })
          .OrderByDescending(x => x.ActivePowerTotalNetT0Avg_W)
          .Select(x => x.AbbB2xAggregate)
          .FirstOrDefault())
        .OfType<AbbB2xAggregateEntity>()
        .Select(x =>
          new DetailedMeasurementLocationsByMeasurementLocationIntermediary
          {
            NetworkUserCalculation = null,
            NetworkUserInvoice = null,
            AbbLastMeasurement = null,
            SchneiderLastMeasurement = null,
            AbbMonthlyAggregate = null,
            SchneiderMonthlyAggregate = null,
            AbbMaxPowerAggregate = x,
            SchneiderMaxPowerAggregate = null
          }))
      .Union(context.SchneideriEM3xxxAggregates
        .Where(context.ForeignKeyIn<SchneideriEM3xxxAggregateEntity>(
          nameof(SchneideriEM3xxxAggregateEntity.Meter),
          meters.Select(x => x.Id)
        ))
        .Where(x => x.Timestamp >= fromDate)
        .Where(x => x.Timestamp < toDate)
        .Where(x => x.Interval == IntervalEntity.QuarterHour)
        .GroupBy(x => new
        {
          x.MeterId,
          EF.Functions.AtTimeZone(x.Timestamp, "Europe/Zagreb").Year,
          EF.Functions.AtTimeZone(x.Timestamp, "Europe/Zagreb").Month
        })
        .Select(x => x
          .Select(x => new
          {
            ActivePowerTotalNetT0Avg_W = x == null
              ? 0
              : x.ActivePowerL1NetT0Avg_W
                + x.ActivePowerL2NetT0Avg_W
                + x.ActivePowerL3NetT0Avg_W,
            SchneideriEM3xxxAggregate = x
          })
          .OrderByDescending(x => x.ActivePowerTotalNetT0Avg_W)
          .Select(x => x.SchneideriEM3xxxAggregate)
          .FirstOrDefault())
        .OfType<SchneideriEM3xxxAggregateEntity>()
        .Select(x =>
          new DetailedMeasurementLocationsByMeasurementLocationIntermediary
          {
            NetworkUserCalculation = null,
            NetworkUserInvoice = null,
            AbbLastMeasurement = null,
            SchneiderLastMeasurement = null,
            AbbMonthlyAggregate = null,
            SchneiderMonthlyAggregate = null,
            AbbMaxPowerAggregate = null,
            SchneiderMaxPowerAggregate = x
          }));

    return query;
  }

  private sealed class DetailedMeasurementLocationsByRepresentativeIntermediary
  {
    public LocationEntity Location { get; init; } = default!;

    public NetworkUserEntity? NetworkUser { get; init; }

    public NetworkUserMeasurementLocationEntity MeasurementLocation
    {
      get;
      init;
    } = default!;

    public MeterEntity Meter { get; init; } = default!;
  }

  private sealed class
    DetailedMeasurementLocationsByMeasurementLocationIntermediary
  {
    public NetworkUserCalculationEntity? NetworkUserCalculation { get; init; }

    public NetworkUserInvoiceEntity? NetworkUserInvoice { get; init; }

    public AbbB2xMeasurementEntity? AbbLastMeasurement { get; init; }

    public SchneideriEM3xxxMeasurementEntity? SchneiderLastMeasurement
    {
      get;
      init;
    }

    public AbbB2xAggregateEntity? AbbMonthlyAggregate { get; init; }

    public SchneideriEM3xxxAggregateEntity? SchneiderMonthlyAggregate
    {
      get;
      init;
    }

    public AbbB2xAggregateEntity? AbbMaxPowerAggregate { get; init; }

    public SchneideriEM3xxxAggregateEntity? SchneiderMaxPowerAggregate
    {
      get;
      init;
    }
  }
}

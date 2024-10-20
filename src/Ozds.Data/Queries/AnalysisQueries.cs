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

    var filtered = MakeReadDetailedMeasurementLocationsByRepresentativeQuery(
      context,
      representativeId,
      role,
      fromDate,
      toDate
    );

    var ordered = filtered
      .OrderByDescending(x => x
        .Aggregate(0f, (current, next) => current
          + (next.AbbLastMeasurement == null
            ? 0f
            : next.AbbLastMeasurement.ActivePowerL1NetT0_W
              + next.AbbLastMeasurement.ActivePowerL2NetT0_W
              + next.AbbLastMeasurement.ActivePowerL3NetT0_W)
          + (next.SchneiderLastMeasurement == null
            ? 0f
            : next.SchneiderLastMeasurement.ActivePowerL1NetT0_W
              + next.SchneiderLastMeasurement.ActivePowerL2NetT0_W
              + next.SchneiderLastMeasurement.ActivePowerL3NetT0_W)));

    var count = await filtered.CountAsync(cancellationToken);
    var items = await ordered
      .Skip((pageNumber - 1) * pageCount)
      .Take(pageCount)
      .ToListAsync(cancellationToken);

    return items
      .Select(
        x =>
        {
          var first = x.FirstOrDefault();
          if (first is null)
          {
            return null;
          }

          var lastMeasurement =
            Enumerable.Empty<MeasurementEntity>()
              .Concat(x
                .Select(x => x.AbbLastMeasurement)
                .OfType<MeasurementEntity>())
              .Concat(x
                .Select(x => x.SchneiderLastMeasurement)
                .OfType<MeasurementEntity>())
              .FirstOrDefault();
          if (lastMeasurement is null)
          {
            return null;
          }

          var monthlyAggregates =
            Enumerable.Empty<AggregateEntity>()
              .Concat(x
                .Select(x => x.AbbMonthlyAggregate)
                .OfType<AggregateEntity>())
              .Concat(x
                .Select(x => x.SchneiderMonthlyAggregate)
                .OfType<AggregateEntity>())
              .ToList();

          var monthlyMaxPowerAggregates =
            Enumerable.Empty<AggregateEntity>()
              .Concat(x
                .Select(x => x.AbbMaxPowerAggregate)
                .OfType<AggregateEntity>())
              .Concat(x
                .Select(x => x.SchneiderMaxPowerAggregate)
                .OfType<AggregateEntity>())
              .ToList();

          var calculations = x
            .Select(x => x.NetworkUserCalculation)
            .OfType<CalculationEntity>()
            .ToList();

          var invoices = x
            .Select(x => x.NetworkUserInvoice)
            .OfType<InvoiceEntity>()
            .DistinctBy(x => x.Id)
            .ToList();

          return new AnalysisBasisEntity(
            first.Location,
            first.NetworkUser,
            first.MeasurementLocation,
            first.Meter,
            calculations,
            invoices,
            lastMeasurement,
            monthlyAggregates,
            monthlyMaxPowerAggregates
          );
        })
      .OfType<AnalysisBasisEntity>()
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

    var filtered = MakeReadDetailedMeasurementLocationsByRepresentativeQuery(
      context,
      representativeId,
      role,
      fromDate,
      toDate
    );

    var items = await filtered.ToListAsync();

    return items
      .Select(
        x =>
        {
          var first = x.FirstOrDefault();
          if (first is null)
          {
            return null;
          }

          var lastMeasurement =
            Enumerable.Empty<MeasurementEntity>()
              .Concat(x
                .Select(x => x.AbbLastMeasurement)
                .OfType<MeasurementEntity>())
              .Concat(x
                .Select(x => x.SchneiderLastMeasurement)
                .OfType<MeasurementEntity>())
              .FirstOrDefault();
          if (lastMeasurement is null)
          {
            return null;
          }

          var monthlyAggregates =
            Enumerable.Empty<AggregateEntity>()
              .Concat(x
                .Select(x => x.AbbMonthlyAggregate)
                .OfType<AggregateEntity>())
              .Concat(x
                .Select(x => x.SchneiderMonthlyAggregate)
                .OfType<AggregateEntity>())
              .ToList();

          var monthlyMaxPowerAggregates =
            Enumerable.Empty<AggregateEntity>()
              .Concat(x
                .Select(x => x.AbbMaxPowerAggregate)
                .OfType<AggregateEntity>())
              .Concat(x
                .Select(x => x.SchneiderMaxPowerAggregate)
                .OfType<AggregateEntity>())
              .ToList();

          var calculations = x
            .Select(x => x.NetworkUserCalculation)
            .OfType<CalculationEntity>()
            .ToList();

          var invoices = x
            .Select(x => x.NetworkUserInvoice)
            .OfType<InvoiceEntity>()
            .DistinctBy(x => x.Id)
            .ToList();

          return new AnalysisBasisEntity(
            first.Location,
            first.NetworkUser,
            first.MeasurementLocation,
            first.Meter,
            calculations,
            invoices,
            lastMeasurement,
            monthlyAggregates,
            monthlyMaxPowerAggregates
          );
        })
      .OfType<AnalysisBasisEntity>()
      .ToList();
  }

  private static
    IQueryable<IGrouping<string, DetailedMeasurementLocationsIntermediary>>
    MakeReadDetailedMeasurementLocationsByRepresentativeQuery(
      DataDbContext context,
      string representativeId,
      RoleEntity role,
      DateTimeOffset fromDate,
      DateTimeOffset toDate
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
              new DetailedMeasurementLocationsIntermediary
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
            .SelectMany(x => x.Location.NetworkUsers
              .SelectMany(y => y.NetworkUserMeasurementLocations
                .Select(z => new DetailedMeasurementLocationsIntermediary
                {
                  Location = x.Location,
                  NetworkUser = y,
                  MeasurementLocation = z,
                })))),
      RoleEntity.OperatorRepresentative =>
        context.MeasurementLocations
          .OfType<NetworkUserMeasurementLocationEntity>()
          .Include(x => x.NetworkUser)
          .Include(x => x.NetworkUser.Location)
          .Include(x => x.Meter)
          .Select(x => new DetailedMeasurementLocationsIntermediary
          {
            Location = x.NetworkUser.Location,
            NetworkUser = x.NetworkUser,
            MeasurementLocation = x,
            Meter = x.Meter
          }),
      _ => throw new ArgumentOutOfRangeException(nameof(role))
    };

    var filtered = query
      .Select(x => new DetailedMeasurementLocationsIntermediary
      {
        Location = x.Location,
        NetworkUser = x.NetworkUser,
        MeasurementLocation = x.MeasurementLocation,
        Meter = x.MeasurementLocation.Meter,
      })
      .GroupJoin(
        context.NetworkUserCalculations
          .Where(x => x.FromDate >= fromDate)
          .Where(x => x.FromDate < toDate)
          .Include(x => x.NetworkUserInvoice),
        context
          .PrimaryKeyOf<NetworkUserMeasurementLocationEntity>()
          .Prefix((DetailedMeasurementLocationsIntermediary x) => x.MeasurementLocation),
        context.ForeignKeyOf<NetworkUserCalculationEntity>(
          nameof(NetworkUserCalculationEntity.NetworkUserMeasurementLocation)),
        (x, networkUserCalculations) => new
        {
          x.Location,
          x.NetworkUser,
          x.MeasurementLocation,
          x.Meter,
          networkUserCalculations
        }
      )
      .SelectMany(
        x => x.networkUserCalculations.DefaultIfEmpty(),
        (x, networkUserCalculation) => new DetailedMeasurementLocationsIntermediary
        {
          Location = x.Location,
          NetworkUser = x.NetworkUser,
          MeasurementLocation = x.MeasurementLocation,
          Meter = x.Meter,
          NetworkUserCalculation = networkUserCalculation,
          NetworkUserInvoice = networkUserCalculation == null
            ? null
            : networkUserCalculation.NetworkUserInvoice
        })
      .GroupJoin(
        context.AbbB2xMeasurements
          .Where(x => x.Timestamp >= fromDate)
          .Where(x => x.Timestamp < toDate)
          .OrderByDescending(x => x.Timestamp)
          .Take(1),
        context
          .PrimaryKeyOf<MeterEntity>()
          .Prefix((DetailedMeasurementLocationsIntermediary x) => x.Meter),
        context.ForeignKeyOf<AbbB2xMeasurementEntity>(
          nameof(AbbB2xMeasurementEntity.Meter)),
        (x, abbB2xMeasurements) => new
        {
          x.Location,
          x.NetworkUser,
          x.MeasurementLocation,
          x.Meter,
          x.NetworkUserCalculation,
          x.NetworkUserInvoice,
          abbB2xMeasurements
        }
      )
      .SelectMany(
        x => x.abbB2xMeasurements.DefaultIfEmpty(),
        (x, abbMeasurement) => new DetailedMeasurementLocationsIntermediary
        {
          Location = x.Location,
          NetworkUser = x.NetworkUser,
          MeasurementLocation = x.MeasurementLocation,
          Meter = x.Meter,
          NetworkUserCalculation = x.NetworkUserCalculation,
          NetworkUserInvoice = x.NetworkUserInvoice,
          AbbLastMeasurement = abbMeasurement
        })
      .GroupJoin(
        context.SchneideriEM3xxxMeasurements
          .Where(x => x.Timestamp >= fromDate)
          .Where(x => x.Timestamp < toDate)
          .OrderByDescending(x => x.Timestamp)
          .Take(1),
        context
          .PrimaryKeyOf<MeterEntity>()
          .Prefix((DetailedMeasurementLocationsIntermediary x) => x.Meter),
        context.ForeignKeyOf<SchneideriEM3xxxMeasurementEntity>(
          nameof(SchneideriEM3xxxMeasurementEntity.Meter)),
        (x, schneideriEM3xxxMeasurements) => new
        {
          x.Location,
          x.NetworkUser,
          x.MeasurementLocation,
          x.Meter,
          x.NetworkUserCalculation,
          x.NetworkUserInvoice,
          x.AbbLastMeasurement,
          schneideriEM3xxxMeasurements
        }
      )
      .SelectMany(
        x => x.schneideriEM3xxxMeasurements.DefaultIfEmpty(),
        (x, schneiderMeasurement) => new DetailedMeasurementLocationsIntermediary
        {
          Location = x.Location,
          NetworkUser = x.NetworkUser,
          MeasurementLocation = x.MeasurementLocation,
          Meter = x.Meter,
          NetworkUserCalculation = x.NetworkUserCalculation,
          NetworkUserInvoice = x.NetworkUserInvoice,
          AbbLastMeasurement = x.AbbLastMeasurement,
          SchneiderLastMeasurement = schneiderMeasurement
        })
      .GroupJoin(
        context.AbbB2xAggregates
          .Where(x => x.Timestamp >= fromDate)
          .Where(x => x.Timestamp < toDate)
          .Where(x => x.Interval == IntervalEntity.Month),
        context
          .PrimaryKeyOf<MeterEntity>()
          .Prefix((DetailedMeasurementLocationsIntermediary x) => x.Meter),
        context.ForeignKeyOf<AbbB2xAggregateEntity>(
          nameof(AbbB2xAggregateEntity.Meter)),
        (x, abbB2xAggregates) => new
        {
          x.Location,
          x.NetworkUser,
          x.MeasurementLocation,
          x.Meter,
          x.NetworkUserCalculation,
          x.NetworkUserInvoice,
          x.AbbLastMeasurement,
          x.SchneiderLastMeasurement,
          abbB2xAggregates
        }
      )
      .SelectMany(
        x => x.abbB2xAggregates.DefaultIfEmpty(),
        (x, abbAggregate) => new DetailedMeasurementLocationsIntermediary
        {
          Location = x.Location,
          NetworkUser = x.NetworkUser,
          MeasurementLocation = x.MeasurementLocation,
          Meter = x.Meter,
          NetworkUserCalculation = x.NetworkUserCalculation,
          NetworkUserInvoice = x.NetworkUserInvoice,
          AbbLastMeasurement = x.AbbLastMeasurement,
          SchneiderLastMeasurement = x.SchneiderLastMeasurement,
          AbbMonthlyAggregate = abbAggregate
        })
      .GroupJoin(
        context.SchneideriEM3xxxAggregates
          .Where(x => x.Timestamp >= fromDate)
          .Where(x => x.Timestamp < toDate)
          .Where(x => x.Interval == IntervalEntity.Month),
        context
          .PrimaryKeyOf<MeterEntity>()
          .Prefix((DetailedMeasurementLocationsIntermediary x) => x.Meter),
        context.ForeignKeyOf<SchneideriEM3xxxAggregateEntity>(
          nameof(SchneideriEM3xxxAggregateEntity.Meter)),
        (x, schneideriEM3xxxAggregates) => new
        {
          x.Location,
          x.NetworkUser,
          x.MeasurementLocation,
          x.Meter,
          x.NetworkUserCalculation,
          x.NetworkUserInvoice,
          x.AbbLastMeasurement,
          x.SchneiderLastMeasurement,
          x.AbbMonthlyAggregate,
          schneideriEM3xxxAggregates
        }
      )
      .SelectMany(
        x => x.schneideriEM3xxxAggregates.DefaultIfEmpty(),
        (x, schneiderAggregate) => new DetailedMeasurementLocationsIntermediary
        {
          Location = x.Location,
          NetworkUser = x.NetworkUser,
          MeasurementLocation = x.MeasurementLocation,
          Meter = x.Meter,
          NetworkUserCalculation = x.NetworkUserCalculation,
          NetworkUserInvoice = x.NetworkUserInvoice,
          AbbLastMeasurement = x.AbbLastMeasurement,
          SchneiderLastMeasurement = x.SchneiderLastMeasurement,
          AbbMonthlyAggregate = x.AbbMonthlyAggregate,
          SchneiderMonthlyAggregate = schneiderAggregate
        })
      .GroupJoin(
        context.AbbB2xAggregates
          .Where(x => x.Timestamp >= fromDate)
          .Where(x => x.Timestamp < toDate)
          .Where(x => x.Interval == IntervalEntity.QuarterHour),
        context
          .PrimaryKeyOf<MeterEntity>()
          .Prefix((DetailedMeasurementLocationsIntermediary x) => x.Meter),
        context.ForeignKeyOf<AbbB2xAggregateEntity>(
          nameof(AbbB2xAggregateEntity.Meter)),
        (x, abbB2xAggregates) => new
        {
          x.Location,
          x.NetworkUser,
          x.MeasurementLocation,
          x.Meter,
          x.NetworkUserCalculation,
          x.NetworkUserInvoice,
          x.AbbLastMeasurement,
          x.SchneiderLastMeasurement,
          x.AbbMonthlyAggregate,
          x.SchneiderMonthlyAggregate,
          abbB2xAggregates
        }
      )
      .SelectMany(
        x => x.abbB2xAggregates
          .DefaultIfEmpty()
          .GroupBy(x => x == null
            ? null
            : new
            {
              EF.Functions.AtTimeZone(x.Timestamp, "Europe/Zagreb").Year,
              EF.Functions.AtTimeZone(x.Timestamp, "Europe/Zagreb").Month
            })
          .Select(x => x
            .MaxBy(x => x == null
              ? 0
              : x.ActivePowerL1NetT0Avg_W
                + x.ActivePowerL2NetT0Avg_W
                + x.ActivePowerL3NetT0Avg_W)),
        (x, abbAggregate) => new DetailedMeasurementLocationsIntermediary
        {
          Location = x.Location,
          NetworkUser = x.NetworkUser,
          MeasurementLocation = x.MeasurementLocation,
          Meter = x.Meter,
          NetworkUserCalculation = x.NetworkUserCalculation,
          NetworkUserInvoice = x.NetworkUserInvoice,
          AbbLastMeasurement = x.AbbLastMeasurement,
          SchneiderLastMeasurement = x.SchneiderLastMeasurement,
          AbbMonthlyAggregate = x.AbbMonthlyAggregate,
          SchneiderMonthlyAggregate = x.SchneiderMonthlyAggregate,
          AbbMaxPowerAggregate = abbAggregate
        })
      .GroupJoin(
        context.SchneideriEM3xxxAggregates
          .Where(x => x.Timestamp >= fromDate)
          .Where(x => x.Timestamp < toDate)
          .Where(x => x.Interval == IntervalEntity.QuarterHour),
        context
          .PrimaryKeyOf<MeterEntity>()
          .Prefix((DetailedMeasurementLocationsIntermediary x) => x.Meter),
        context.ForeignKeyOf<SchneideriEM3xxxAggregateEntity>(
          nameof(SchneideriEM3xxxAggregateEntity.Meter)),
        (x, schneideriEM3xxxAggregates) => new
        {
          x.Location,
          x.NetworkUser,
          x.MeasurementLocation,
          x.Meter,
          x.NetworkUserCalculation,
          x.NetworkUserInvoice,
          x.AbbLastMeasurement,
          x.SchneiderLastMeasurement,
          x.AbbMonthlyAggregate,
          x.SchneiderMonthlyAggregate,
          x.AbbMaxPowerAggregate,
          schneideriEM3xxxAggregates
        }
      )
      .SelectMany(
        x => x.schneideriEM3xxxAggregates
          .DefaultIfEmpty()
          .GroupBy(x => x == null
            ? null
            : new
            {
              EF.Functions.AtTimeZone(x.Timestamp, "Europe/Zagreb").Year,
              EF.Functions.AtTimeZone(x.Timestamp, "Europe/Zagreb").Month
            })
          .Select(x => x
            .MaxBy(x => x == null
              ? 0
              : x.ActivePowerL1NetT0Avg_W
                + x.ActivePowerL2NetT0Avg_W
                + x.ActivePowerL3NetT0Avg_W)),
        (x, schneiderAggregate) => new DetailedMeasurementLocationsIntermediary
        {
          Location = x.Location,
          NetworkUser = x.NetworkUser,
          MeasurementLocation = x.MeasurementLocation,
          Meter = x.Meter,
          NetworkUserCalculation = x.NetworkUserCalculation,
          NetworkUserInvoice = x.NetworkUserInvoice,
          AbbLastMeasurement = x.AbbLastMeasurement,
          SchneiderLastMeasurement = x.SchneiderLastMeasurement,
          AbbMonthlyAggregate = x.AbbMonthlyAggregate,
          SchneiderMonthlyAggregate = x.SchneiderMonthlyAggregate,
          AbbMaxPowerAggregate = x.AbbMaxPowerAggregate,
          SchneiderMaxPowerAggregate = schneiderAggregate
        })
      .GroupBy(x => x.MeasurementLocation.Id);

    return filtered;
  }

  private sealed class DetailedMeasurementLocationsIntermediary
  {
    public LocationEntity Location { get; init; } = default!;

    public NetworkUserEntity? NetworkUser { get; init; }

    public NetworkUserMeasurementLocationEntity MeasurementLocation
    {
      get;
      init;
    } = default!;

    public MeterEntity Meter { get; init; } = default!;

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

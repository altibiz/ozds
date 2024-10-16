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

namespace Ozds.Data.Queries;

public class AnalysisQueries(
  IDbContextFactory<DataDbContext> factory
) : IQueries
{
  public async Task<PaginatedList<AnalysisBasisEntity>>
    ReadAnalysisBasesByRepresentativePaged(
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
          + (next.AbbMeasurement == null
            ? 0f
            : next.AbbMeasurement.ActivePowerL1NetT0_W
              + next.AbbMeasurement.ActivePowerL2NetT0_W
              + next.AbbMeasurement.ActivePowerL3NetT0_W)
          + (next.SchneiderMeasurement == null
            ? 0f
            : next.SchneiderMeasurement.ActivePowerL1NetT0_W
              + next.SchneiderMeasurement.ActivePowerL2NetT0_W
              + next.SchneiderMeasurement.ActivePowerL3NetT0_W)));

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
                .Select(x => x.AbbMeasurement)
                .OfType<MeasurementEntity>())
              .Concat(x
                .Select(x => x.SchneiderMeasurement)
                .OfType<MeasurementEntity>())
              .FirstOrDefault();
          if (lastMeasurement is null)
          {
            return null;
          }

          var monthlyAggregates =
            Enumerable.Empty<AggregateEntity>()
              .Concat(x
                .Select(x => x.AbbAggregate)
                .OfType<AggregateEntity>())
              .Concat(x
                .Select(x => x.SchneiderAggregate)
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
            monthlyAggregates
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
                .Select(x => x.AbbMeasurement)
                .OfType<MeasurementEntity>())
              .Concat(x
                .Select(x => x.SchneiderMeasurement)
                .OfType<MeasurementEntity>())
              .FirstOrDefault();
          if (lastMeasurement is null)
          {
            return null;
          }

          var monthlyAggregates =
            Enumerable.Empty<AggregateEntity>()
              .Concat(x
                .Select(x => x.AbbAggregate)
                .OfType<AggregateEntity>())
              .Concat(x
                .Select(x => x.SchneiderAggregate)
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
            monthlyAggregates
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
          .SelectMany(x => x.NetworkUser.NetworkUserMeasurementLocations
            .Select(y =>
              new DetailedMeasurementLocationsIntermediary
              {
                Location = x.NetworkUser.Location,
                NetworkUser = x.NetworkUser,
                MeasurementLocation = y,
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
          .Select(x => new DetailedMeasurementLocationsIntermediary
          {
            Location = x.NetworkUser.Location,
            NetworkUser = x.NetworkUser,
            MeasurementLocation = x,
          }),
      _ => throw new ArgumentOutOfRangeException(nameof(role))
    };

    var filtered = query
      .Include(x => x.MeasurementLocation.Meter)
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
          AbbMeasurement = abbMeasurement
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
          x.AbbMeasurement,
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
          AbbMeasurement = x.AbbMeasurement,
          SchneiderMeasurement = schneiderMeasurement
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
          x.AbbMeasurement,
          x.SchneiderMeasurement,
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
          AbbMeasurement = x.AbbMeasurement,
          SchneiderMeasurement = x.SchneiderMeasurement,
          AbbAggregate = abbAggregate
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
          x.AbbMeasurement,
          x.SchneiderMeasurement,
          x.AbbAggregate,
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
          AbbMeasurement = x.AbbMeasurement,
          SchneiderMeasurement = x.SchneiderMeasurement,
          AbbAggregate = x.AbbAggregate,
          SchneiderAggregate = schneiderAggregate
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

    public AbbB2xMeasurementEntity? AbbMeasurement { get; init; }

    public SchneideriEM3xxxMeasurementEntity? SchneiderMeasurement
    {
      get;
      init;
    }

    public AbbB2xAggregateEntity? AbbAggregate { get; init; }

    public SchneideriEM3xxxAggregateEntity? SchneiderAggregate
    {
      get;
      init;
    }
  }
}

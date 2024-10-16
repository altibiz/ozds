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

public class MeasurementLocationQueries(
  IDbContextFactory<DataDbContext> factory
) : IQueries
{
  public async Task<PaginatedList<DetailedMeasurementLocationEntity>>
    ReadDetailedMeasurementLocationsByRepresentative(
      string representativeId,
      DateTimeOffset fromDate,
      DateTimeOffset toDate,
      int pageNumber,
      CancellationToken cancellationToken,
      int pageCount = QueryConstants.DefaultPageCount
    )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    var filtered = context.NetworkUserRepresentatives
      .Where(context.ForeignKeyEquals<NetworkUserRepresentativeEntity>(
        nameof(NetworkUserRepresentativeEntity.Representative),
        representativeId
      ))
      .Include(x => x.NetworkUser)
      .ThenInclude(x => x.Location)
      .ThenInclude(x => x.RegulatoryCatalogue)
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
      .Include(x => x.MeasurementLocation)
      .ThenInclude(x => x.Meter)
      .Select(x => new DetailedMeasurementLocationsIntermediary
      {
        Location = x.Location,
        NetworkUser = x.NetworkUser,
        MeasurementLocation = x.MeasurementLocation,
        Meter = x.Meter,
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
          AbbMeasurement = x.AbbMeasurement,
          SchneiderMeasurement = x.SchneiderMeasurement,
          AbbAggregate = x.AbbAggregate,
          SchneiderAggregate = schneiderAggregate
        })
      .GroupBy(x => x.MeasurementLocation.Id);

    var ordered = filtered
      .OrderByDescending(x => x
        .Aggregate(0f, (current, next) => current
          + next.AbbMeasurement!.ActivePowerL1NetT0_W
          + next.AbbMeasurement!.ActivePowerL2NetT0_W
          + next.AbbMeasurement!.ActivePowerL3NetT0_W
          + next.SchneiderMeasurement!.ActivePowerL1NetT0_W
          + next.SchneiderMeasurement!.ActivePowerL2NetT0_W
          + next.SchneiderMeasurement!.ActivePowerL3NetT0_W));

    var count = await ordered.CountAsync(cancellationToken);
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

          return new DetailedMeasurementLocationEntity(
            first.Location,
            first.NetworkUser,
            first.MeasurementLocation,
            first.Meter,
            Enumerable.Empty<MeasurementEntity>()
              .Concat(x
                .Select(x => x.AbbMeasurement)
                .OfType<MeasurementEntity>())
              .Concat(x
                .Select(x => x.SchneiderMeasurement)
                .OfType<MeasurementEntity>())
              .ToList(),
            Enumerable.Empty<AggregateEntity>()
              .Concat(x
                .Select(x => x.AbbAggregate)
                .OfType<AggregateEntity>())
              .Concat(x
                .Select(x => x.SchneiderAggregate)
                .OfType<AggregateEntity>())
          );
        })
      .OfType<DetailedMeasurementLocationEntity>()
      .ToPaginatedList(count);
  }

  private sealed class DetailedMeasurementLocationsIntermediary
  {
    public LocationEntity Location { get; init; } = default!;

    public NetworkUserEntity? NetworkUser { get; init; } = default!;

    public NetworkUserMeasurementLocationEntity MeasurementLocation
    {
      get;
      init;
    } = default!;

    public MeterEntity Meter { get; init; } = default!;

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

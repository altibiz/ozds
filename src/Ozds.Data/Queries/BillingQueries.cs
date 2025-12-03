using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Composite;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;
using Ozds.Data.Queries.Abstractions;
using Ozds.Data.Reflection;
using ITimeQueries = Ozds.Time.Queries.Abstractions.ITimeQueries;

namespace Ozds.Data.Queries;

public class BillingQueries(
  IDbContextFactory<DataDbContext> factory,
  EntityReflector reflector,
  ITimeQueries timeQueries
) : IQueries
{
  public async Task<NetworkUserInvoiceBasisEntity>
    ReadInvoiceBasisByNetworkUser(
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

    var calculationBases = await ReadCalculationBasesByNetworkUser(
      networkUserId,
      fromDate,
      toDate,
      cancellationToken
    );

    return new NetworkUserInvoiceBasisEntity
    {
      Location = networkUser.Location,
      NetworkUser = networkUser,
      RegulatoryCatalogue = networkUser.Location.RegulatoryCatalogue,
      FromDate = fromDate,
      ToDate = toDate,
      NetworkUserCalculationBases = calculationBases
    };
  }

  private async Task<List<NetworkUserCalculationBasisEntity>>
    ReadCalculationBasesByNetworkUser(
      string networkUserId,
      DateTimeOffset fromDate,
      DateTimeOffset toDate,
      CancellationToken cancellationToken
    )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    var bases =
      await ReadCalculationBasesByNetworkUser(
        networkUserId,
        cancellationToken);

    foreach (var aggregateType in reflector.AggregateTypes)
    {
      var applicableBases = bases.Where(
          x =>
            x.Meter.GetType()
            == reflector.ResolveMeasurementMeterType(aggregateType))
        .ToList();

      if (applicableBases.Count == 0)
      {
        continue;
      }

      var enrichedBases = await ReadCalculationBaseAggregates(
        aggregateType,
        applicableBases,
        fromDate,
        toDate,
        cancellationToken
      );

      foreach (var enriched in enrichedBases)
      {
        var original = bases.First(
          x =>
            x.MeasurementLocation.Id
            == enriched.MeasurementLocation.Id);
        original.Aggregates = enriched.Aggregates;
        original.MeasuredFromDate = enriched.MeasuredFromDate;
        original.MeasuredToDate = enriched.MeasuredToDate;
        original.BilledFromDate = enriched.BilledFromDate;
        original.BilledToDate = enriched.BilledToDate;
        original.FromDate = enriched.FromDate;
        original.ToDate = enriched.ToDate;
      }
    }

    return bases;
  }

  private async Task<List<NetworkUserCalculationBasisEntity>>
    ReadCalculationBasesByNetworkUser(
      string networkUserId,
      CancellationToken cancellationToken
    )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    return await context.MeasurementLocations
      .OfType<NetworkUserMeasurementLocationEntity>()
      .Where(
        context.ForeignKeyEquals<NetworkUserMeasurementLocationEntity>(
          nameof(NetworkUserMeasurementLocationEntity.NetworkUser),
          networkUserId))
      .Include(x => x.NetworkUserCatalogue)
      .Include(x => x.Meter)
      .Include(x => x.NetworkUser)
      .ThenInclude(x => x.Location)
      .ThenInclude(x => x.RegulatoryCatalogue)
      .Select(
        x => new NetworkUserCalculationBasisEntity
        {
          Location = x.NetworkUser.Location,
          NetworkUser = x.NetworkUser,
          MeasurementLocation = x,
          UsageNetworkUserCatalogue =
            x.NetworkUserCatalogue,
          SupplyRegulatoryCatalogue =
            x.NetworkUser.Location.RegulatoryCatalogue,
          Meter = x.Meter
        })
      .ToListAsync(cancellationToken);
  }

  private async Task<List<NetworkUserCalculationBasisEntity>>
    ReadCalculationBaseAggregates(
      Type aggregateType,
      List<NetworkUserCalculationBasisEntity> bases,
      DateTimeOffset fromDate,
      DateTimeOffset toDate,
      CancellationToken cancellationToken
    )
  {
    if (bases.Count == 0)
    {
      return new List<NetworkUserCalculationBasisEntity>();
    }

    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    var table = reflector.ResolveEntityTable(aggregateType);

    var quarterHourIntervalValue =
      StringExtensions.ToSnakeCase(IntervalEntity.QuarterHour.ToString());
    var intervalTypeName = StringExtensions.ToSnakeCase(nameof(IntervalEntity));

    var parameters = new Dictionary<string, object?>
    {
      { "from", fromDate },
      { "to", toDate }
    };

    var locationValueRows = new List<string>();
    var index = 0;
    foreach (var id in bases.Select(x => x.MeasurementLocation.Id))
    {
      var paramName = $"location{index++}";
      parameters[paramName] = long.Parse(id);
      locationValueRows.Add($"(@{paramName})");
    }

    var joinLocationsClause = $@"
        JOIN (
            VALUES {string.Join(", ", locationValueRows)}
        ) AS selected_locations(location_id)
          ON selected_locations.location_id
            = aggregates.measurement_location_id
    ";

    var inWindowAggregates = await context
      .DapperCommand<AggregateEntity>(
        aggregateType,
        $@"
          SELECT *
          FROM {table} aggregates
          {joinLocationsClause}
          WHERE aggregates.interval
              = '{quarterHourIntervalValue}'::{intervalTypeName}
            AND aggregates.timestamp >= @from
            AND aggregates.timestamp < @to
        ",
        cancellationToken,
        parameters);

    var nextBoundaries = await context
      .DapperCommand<AggregateEntity>(
        aggregateType,
        $@"
          SELECT *
          FROM (
            SELECT aggregates.*,
            ROW_NUMBER() OVER (
              PARTITION BY aggregates.measurement_location_id
              ORDER BY aggregates.timestamp ASC
            ) AS row_number
            FROM {table} aggregates
            {joinLocationsClause}
            WHERE aggregates.interval
                = '{quarterHourIntervalValue}'::{intervalTypeName}
              AND aggregates.timestamp >= @to
          ) end_candidates
          WHERE row_number = 1
        ",
        cancellationToken,
        parameters);

    var locationsWithData = inWindowAggregates
      .Select(x => x.MeasurementLocationId)
      .Distinct()
      .ToHashSet();

    var blackoutLocationIds = bases
      .Select(x => x.MeasurementLocation.Id)
      .Where(id => !locationsWithData.Contains(id))
      .ToList();

    var actualStartBoundaries = new List<AggregateEntity>();

    if (blackoutLocationIds.Count != 0)
    {
      var blackoutParameters = new Dictionary<string, object?>(parameters);
      var blackoutRows = new List<string>();
      var blackoutIndex = 0;
      foreach (var id in blackoutLocationIds)
      {
        var parameterName = $"blackout_location{blackoutIndex++}";
        blackoutParameters[parameterName] = long.Parse(id);
        blackoutRows.Add($"(@{parameterName})");
      }

      var blackoutJoin = $@"
        JOIN (
            VALUES {string.Join(", ", blackoutRows)}
        ) AS selected_locations(location_id)
          ON selected_locations.location_id
            = aggregates.measurement_location_id
      ";

      var lastReadingsBeforeBlackout = await context
        .DapperCommand<AggregateEntity>(
          aggregateType,
          $@"
            SELECT *
            FROM (
              SELECT aggregates.*,
              ROW_NUMBER() OVER (
                PARTITION BY aggregates.measurement_location_id
                ORDER BY aggregates.timestamp DESC
              ) AS row_number
              FROM {table} aggregates
              {blackoutJoin}
              WHERE aggregates.interval
                  = '{quarterHourIntervalValue}'::{intervalTypeName}
                AND aggregates.timestamp < @from
            ) start_fallback
            WHERE row_number = 1
          ",
          cancellationToken,
          blackoutParameters);

      if (lastReadingsBeforeBlackout.Count != 0)
      {
        var targetParameters = new Dictionary<string, object?>
        {
          {
            "interval",
            StringExtensions.ToSnakeCase(nameof(IntervalEntity.QuarterHour))
          }
        };

        var targetRows = new List<string>();
        var targetIndex = 0;

        foreach (var reading in lastReadingsBeforeBlackout)
        {
          var monthStart = timeQueries.GetStartOfMonth(reading.Timestamp);

          var locationParameter = $"target_location{targetIndex}";
          var dateParameter = $"target_date{targetIndex}";

          targetParameters[locationParameter] =
            long.Parse(reading.MeasurementLocationId);
          targetParameters[dateParameter] = monthStart;

          targetRows.Add($"(@{locationParameter}, @{dateParameter})");

          targetIndex++;
        }

        if (targetRows.Count != 0)
        {
          actualStartBoundaries = await context
            .DapperCommand<AggregateEntity>(
              aggregateType,
              $@"
                SELECT *
                FROM (
                  SELECT aggregates.*,
                  ROW_NUMBER() OVER (
                    PARTITION BY aggregates.measurement_location_id
                    ORDER BY aggregates.timestamp ASC
                  ) AS row_number
                  FROM {table} aggregates
                  JOIN (
                      VALUES {string.Join(", ", targetRows)}
                  ) AS targets(location_id, target_start)
                    ON targets.location_id = aggregates.measurement_location_id
                  WHERE aggregates.interval
                      = '{quarterHourIntervalValue}'::{intervalTypeName}
                    AND aggregates.timestamp >= targets.target_start
                ) real_starts
                WHERE row_number = 1
              ",
              cancellationToken,
              targetParameters);
        }
      }
    }

    return bases.Select(
      basis =>
      {
        var locationId = basis.MeasurementLocation.Id;

        var locationAggregates = inWindowAggregates
          .Where(x => x.MeasurementLocationId == locationId)
          .OrderBy(x => x.Timestamp)
          .ToList();

        var next = nextBoundaries
          .FirstOrDefault(x => x.MeasurementLocationId == locationId);

        var previous = actualStartBoundaries
          .FirstOrDefault(x => x.MeasurementLocationId == locationId);

        AggregateEntity? startAggregate = null;
        AggregateEntity? endAggregate = null;

        if (locationAggregates.Count != 0)
        {
          startAggregate = locationAggregates.First();
          endAggregate = next;
        }
        else
        {
          startAggregate = previous;
          endAggregate = next;
        }

        var resultAggregates = new List<AggregateEntity>(locationAggregates);

        if (startAggregate != null && !resultAggregates
          .Exists(x => x.Timestamp == startAggregate.Timestamp))
        {
          resultAggregates.Insert(0, startAggregate);
        }

        if (endAggregate != null && !resultAggregates
          .Exists(x => x.Timestamp == endAggregate.Timestamp))
        {
          resultAggregates.Add(endAggregate);
        }

        var measuredFrom = startAggregate?.Timestamp ?? fromDate;
        var measuredTo = endAggregate?.Timestamp ?? toDate;

        var billedFrom = startAggregate is not null
          ? timeQueries.GetStartOfMonth(startAggregate.Timestamp)
          : fromDate;

        var billedTo = endAggregate is not null
          ? timeQueries.GetStartOfMonth(endAggregate.Timestamp)
          : toDate;

        return new NetworkUserCalculationBasisEntity
        {
          Location = basis.Location,
          NetworkUser = basis.NetworkUser,
          MeasurementLocation = basis.MeasurementLocation,
          UsageNetworkUserCatalogue = basis.UsageNetworkUserCatalogue,
          SupplyRegulatoryCatalogue = basis.SupplyRegulatoryCatalogue,
          Meter = basis.Meter,
          Aggregates = resultAggregates.OrderBy(x => x.Timestamp).ToList(),
          MeasuredFromDate = measuredFrom,
          MeasuredToDate = measuredTo,
          BilledFromDate = billedFrom,
          BilledToDate = billedTo,
          FromDate = fromDate,
          ToDate = toDate
        };
      }).ToList();
  }
}

using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Composite;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;
using Ozds.Data.Queries.Abstractions;
using Ozds.Data.Reflection;
using ITimeQueries = Ozds.Time.Queries.Abstractions.ITimeQueries;

namespace Ozds.Data.Queries;

// NOTE: as explained in MeasurementQueries.cs
// <= toDate because of invoice/report calculations

public class AggregateWindowQueries(
  IDbContextFactory<DataDbContext> factory,
  EntityReflector reflector,
  ITimeQueries timeQueries
) : IQueries
{
  public async Task<
    List<AggregateWindowLoadCurveBasisEntity>
  > ReadAggregateWindowLoadCurveBasesByMeasurementLocation(
    IEnumerable<
      KeyValuePair<Type, IReadOnlyList<string>>
    > measurementLocationsByAggregateType,
    DateTimeOffset fromDate,
    DateTimeOffset toDate,
    CancellationToken cancellationToken
  )
  {
    var totalWindowAggregates = new List<AggregateWindowLoadCurveBasisEntity>();

    foreach (
      var (
        aggregateType,
        measurementLocationIds
      ) in measurementLocationsByAggregateType
    )
    {
      if (measurementLocationIds.Count == 0)
      {
        continue;
      }

      await using var context = await factory.CreateDbContextAsync(
        cancellationToken
      );

      var table = reflector.ResolveEntityTable(aggregateType);
      var quarterHourIntervalValue = StringExtensions.ToSnakeCase(
        IntervalEntity.QuarterHour.ToString()
      );
      var intervalTypeName = StringExtensions.ToSnakeCase(
        nameof(IntervalEntity)
      );

      var parameters = new Dictionary<string, object?>
      {
        { "from", fromDate },
        { "to", toDate },
      };

      var locationValueRows = new List<string>();
      var index = 0;
      foreach (var id in measurementLocationIds)
      {
        var paramName = $"location{index++}";
        parameters[paramName] = long.Parse(id);
        locationValueRows.Add($"(@{paramName})");
      }

      var valuesClause = string.Join(", ", locationValueRows);

      var sql =
        $@"
        SELECT aggregates.*
        FROM {table} aggregates
        JOIN (
          VALUES {valuesClause}
        ) AS selected_locations(location_id)
          ON selected_locations.location_id
            = aggregates.measurement_location_id
        WHERE aggregates.interval
            = '{quarterHourIntervalValue}'::{intervalTypeName}
          AND aggregates.timestamp >= @from
          AND aggregates.timestamp <= @to
      ";

      var returnedAggregates = await context.DapperCommand<AggregateEntity>(
        aggregateType,
        sql,
        cancellationToken,
        parameters,
        300
      );

      totalWindowAggregates.AddRange(
        returnedAggregates
          .GroupBy(a => a.MeasurementLocationId)
          .Select(group =>
          {
            var ordered = group.OrderBy(a => a.Timestamp).ToList();
            return new AggregateWindowLoadCurveBasisEntity
            {
              MeasurementLocationId = group.Key,
              StartAggregate = ordered.FirstOrDefault(),
              EndAggregate = ordered.LastOrDefault(),
              InWindowAggregates = ordered,
            };
          })
      );
    }
    return totalWindowAggregates;
  }

  public async Task<
    List<AggregateWindowBoundaryBasisEntity>
  > ReadAggregateWindowBoundaryBasesByMeasurementLocation(
    IEnumerable<
      KeyValuePair<Type, IReadOnlyList<string>>
    > measurementLocationsByAggregateType,
    DateTimeOffset fromDate,
    DateTimeOffset toDate,
    CancellationToken cancellationToken
  )
  {
    var totalWindowAggregates = new List<AggregateWindowBoundaryBasisEntity>();

    foreach (
      var (
        aggregateType,
        measurementLocationIds
      ) in measurementLocationsByAggregateType
    )
    {
      if (measurementLocationIds.Count == 0)
      {
        continue;
      }

      await using var context = await factory.CreateDbContextAsync(
        cancellationToken
      );

      var table = reflector.ResolveEntityTable(aggregateType);
      var quarterHourIntervalValue = StringExtensions.ToSnakeCase(
        IntervalEntity.QuarterHour.ToString()
      );
      var intervalTypeName = StringExtensions.ToSnakeCase(
        nameof(IntervalEntity)
      );

      var parameters = new Dictionary<string, object?>
      {
        { "from", fromDate },
        { "to", toDate },
      };

      var locationValueRows = new List<string>();
      var index = 0;
      foreach (var id in measurementLocationIds)
      {
        var paramName = $"location{index++}";
        parameters[paramName] = long.Parse(id);
        locationValueRows.Add($"(@{paramName})");
      }

      var valuesClause = string.Join(", ", locationValueRows);

      var sql =
        $@"
        WITH selected_locations AS (
          SELECT * FROM (VALUES {valuesClause}) AS v(location_id)
        )
        SELECT agg.*
        FROM selected_locations
        CROSS JOIN LATERAL (
          SELECT * FROM {table} aggregates
          WHERE aggregates.measurement_location_id
              = selected_locations.location_id
            AND aggregates.interval
                = '{quarterHourIntervalValue}'::{intervalTypeName}
            AND aggregates.timestamp >= @from
            AND aggregates.timestamp < @to
          ORDER BY aggregates.timestamp ASC
          LIMIT 1
        ) agg
        UNION ALL
        SELECT agg.*
        FROM selected_locations
        CROSS JOIN LATERAL (
          SELECT * FROM {table} aggregates
          WHERE aggregates.measurement_location_id
              = selected_locations.location_id
            AND aggregates.interval
                = '{quarterHourIntervalValue}'::{intervalTypeName}
            AND aggregates.timestamp >= @from
            AND aggregates.timestamp < @to
          ORDER BY aggregates.timestamp DESC
          LIMIT 1
        ) agg
      ";

      var inWindowAggregates = await context.DapperCommand<AggregateEntity>(
        aggregateType,
        sql,
        cancellationToken,
        parameters,
        300
      );

      var nextBoundariesSql =
        $@"
        SELECT picked.*
        FROM (
          VALUES {string.Join(", ", locationValueRows)}
        ) AS selected_locations(location_id)
        CROSS JOIN LATERAL (
          SELECT aggregates.*
          FROM {table} aggregates
          WHERE aggregates.measurement_location_id
              = selected_locations.location_id
            AND aggregates.interval
              = '{quarterHourIntervalValue}'::{intervalTypeName}
            AND aggregates.timestamp >= @to
          ORDER BY aggregates.timestamp ASC
          LIMIT 1
        ) AS picked
      ";

      var nextBoundaries = await context.DapperCommand<AggregateEntity>(
        aggregateType,
        nextBoundariesSql,
        cancellationToken,
        parameters,
        300
      );

      var locationsWithData = inWindowAggregates
        .Select(x => x.MeasurementLocationId)
        .Distinct()
        .ToHashSet();

      var blackoutLocationIds = measurementLocationIds
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

        var lastReadingsBeforeBlackoutSql =
          $@"
            SELECT picked.*
            FROM (
              VALUES {string.Join(", ", blackoutRows)}
            ) AS selected_locations(location_id)
            CROSS JOIN LATERAL (
              SELECT aggregates.*
              FROM {table} aggregates
              WHERE aggregates.measurement_location_id
                  = selected_locations.location_id
                AND aggregates.interval
                  = '{quarterHourIntervalValue}'::{intervalTypeName}
                AND aggregates.timestamp < @from
              ORDER BY aggregates.timestamp DESC
              LIMIT 1
            ) AS picked
          ";

        var lastReadingsBeforeBlackout =
          await context.DapperCommand<AggregateEntity>(
            aggregateType,
            lastReadingsBeforeBlackoutSql,
            cancellationToken,
            blackoutParameters,
            300
          );

        if (lastReadingsBeforeBlackout.Count != 0)
        {
          var targetParameters = new Dictionary<string, object?>
          {
            {
              "interval",
              StringExtensions.ToSnakeCase(nameof(IntervalEntity.QuarterHour))
            },
          };

          var targetRows = new List<string>();
          var targetIndex = 0;

          foreach (var reading in lastReadingsBeforeBlackout)
          {
            var monthStart = timeQueries.GetStartOfMonth(reading.Timestamp);

            var locationParameter = $"target_location{targetIndex}";
            var dateParameter = $"target_date{targetIndex}";

            targetParameters[locationParameter] = long.Parse(
              reading.MeasurementLocationId
            );
            targetParameters[dateParameter] = monthStart;

            targetRows.Add($"(@{locationParameter}, @{dateParameter})");

            targetIndex++;
          }

          if (targetRows.Count != 0)
          {
            var actualStartBoundariesSql =
              $@"
                SELECT picked.*
                FROM (
                  VALUES {string.Join(", ", targetRows)}
                ) AS targets(location_id, target_start)
                CROSS JOIN LATERAL (
                  SELECT aggregates.*
                  FROM {table} aggregates
                  WHERE aggregates.measurement_location_id = targets.location_id
                    AND aggregates.interval = '{quarterHourIntervalValue}'::{intervalTypeName}
                    AND aggregates.timestamp >= targets.target_start
                  ORDER BY aggregates.timestamp ASC
                  LIMIT 1
                ) AS picked
              ";

            actualStartBoundaries =
              await context.DapperCommand<AggregateEntity>(
                aggregateType,
                actualStartBoundariesSql,
                cancellationToken,
                targetParameters,
                300
              );
          }
        }
      }

      var inWindowAggregatesByLocation = inWindowAggregates
        .GroupBy(a => a.MeasurementLocationId)
        .ToDictionary(g => g.Key, g => g.OrderBy(x => x.Timestamp).ToList());

      totalWindowAggregates.AddRange(
        measurementLocationIds.Select(locationId =>
        {
          var orderedLocationAggregates =
          inWindowAggregatesByLocation
           .TryGetValue(locationId, out var v)
           ? new List<AggregateEntity>(v) : new List<AggregateEntity>();

          var next = nextBoundaries.FirstOrDefault(x =>
             x.MeasurementLocationId == locationId
           );

          var previous = actualStartBoundaries.FirstOrDefault(x =>
            x.MeasurementLocationId == locationId
          );

          AggregateEntity? startAggregate = null;
          AggregateEntity? endAggregate = null;

          if (orderedLocationAggregates.Count != 0)
          {
            startAggregate = orderedLocationAggregates.First();
            endAggregate = next;
          }
          else
          {
            startAggregate = previous;
            endAggregate = next;
          }

          return new AggregateWindowBoundaryBasisEntity
          {
            MeasurementLocationId = locationId,
            StartAggregate = startAggregate,
            EndAggregate = endAggregate,
          };
        })
      );
    }

    return totalWindowAggregates;
  }
}

using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Composite;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;
using Ozds.Data.Queries.Abstractions;
using Ozds.Data.Reflection;

namespace Ozds.Data.Queries;

public class AggregateWindowQueries(
  IDbContextFactory<DataDbContext> factory,
  EntityReflector reflector
) : IQueries
{
  public async Task<
    List<AggregateWindowLoadCurveBasisEnity>
  > ReadAggregateWindowLoadCurveBasesByMeasurementLocation(
    IEnumerable<KeyValuePair<Type, IReadOnlyList<string>>> measurementLocationsByAggregateType,
    DateTimeOffset fromDate,
    DateTimeOffset toDate,
    CancellationToken cancellationToken
  )
  {

    var totalWindowAggregates = new List<AggregateWindowLoadCurveBasisEnity>();

    foreach (var (aggregateType, measurementLocationIds) in measurementLocationsByAggregateType)
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
          AND aggregates.timestamp < @to
      ";

      var returnedAggregates = await context.DapperCommand<AggregateEntity>(
        aggregateType,
        sql,
        cancellationToken,
        parameters,
        300
      );

      totalWindowAggregates.AddRange(
        returnedAggregates.GroupBy(a => a.MeasurementLocationId)
        .Select(group =>
        {
          var ordered = group.OrderBy(a => a.Timestamp).ToList();
          return new AggregateWindowLoadCurveBasisEnity
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
  > ReadAggregateWindowBoundryBasesByMeasurementLocation(
    IEnumerable<KeyValuePair<Type, IReadOnlyList<string>>> measurementLocationsByAggregateType,
    DateTimeOffset fromDate,
    DateTimeOffset toDate,
    CancellationToken cancellationToken
  )
  {
    var totalWindowAggregates = new List<AggregateWindowBoundaryBasisEntity>();

    foreach (var (aggregateType, measurementLocationIds) in measurementLocationsByAggregateType)
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
            return new AggregateWindowBoundaryBasisEntity
            {
              MeasurementLocationId = group.Key,
              StartAggregate = ordered.FirstOrDefault(),
              EndAggregate = ordered.LastOrDefault(),
            };
          })
      );
    }

    return totalWindowAggregates;
  }
}

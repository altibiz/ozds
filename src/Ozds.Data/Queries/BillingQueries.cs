using System.Text.Json;
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
  ITimeQueries timeQueries,
  ILogger<BillingQueries> logger
) : IQueries
{
  private static readonly JsonSerializerOptions JsonSerializerOptions = new()
  {
    WriteIndented = true
  };

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
      // NOTE: IsAssignableTo is used because proxies
      var applicableBases = bases.Where(x =>
          x.Meter.GetType()
            .IsAssignableTo(
              reflector
                .ResolveMeasurementMeterType(aggregateType)))
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
        var original = bases.First(x =>
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
      .Select(x => new NetworkUserCalculationBasisEntity
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

    var inWindowAggregatesSql = $@"
      SELECT *
      FROM {table} aggregates
      {joinLocationsClause}
      WHERE aggregates.interval
          = '{quarterHourIntervalValue}'::{intervalTypeName}
        AND aggregates.timestamp >= @from
        AND aggregates.timestamp < @to
    ";

    logger.LogDebug(
      "In window aggregates\nSql: {Sql}\nParameters: {Parameters}",
      inWindowAggregatesSql,
      JsonSerializer.Serialize(
        parameters,
        JsonSerializerOptions));

    var inWindowAggregates = await context
      .DapperCommand<AggregateEntity>(
        aggregateType,
        inWindowAggregatesSql,
        cancellationToken,
        parameters,
        300);

    var nextBoundariesSql = $@"
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

    logger.LogDebug(
      "Next boundaries\nSql: {Sql}\nParameters: {Parameters}",
      nextBoundariesSql,
      JsonSerializer.Serialize(
        parameters,
        JsonSerializerOptions));

    var nextBoundaries = await context
      .DapperCommand<AggregateEntity>(
        aggregateType,
        nextBoundariesSql,
        cancellationToken,
        parameters,
        300);

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

      var lastReadingsBeforeBlackoutSql = $@"
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

      logger.LogDebug(
        "Last readings before blackout\nSql: {Sql}\nParameters: {Parameters}",
        lastReadingsBeforeBlackoutSql,
        JsonSerializer.Serialize(
          blackoutParameters,
          JsonSerializerOptions));

      var lastReadingsBeforeBlackout = await context
        .DapperCommand<AggregateEntity>(
          aggregateType,
          lastReadingsBeforeBlackoutSql,
          cancellationToken,
          blackoutParameters,
          300);

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
          var actualStartBoundariesSql = $@"
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

          logger.LogDebug(
            "Actual start boundaries\nSql: {Sql}\nParameters: {Parameters}",
            actualStartBoundariesSql,
            JsonSerializer.Serialize(
              targetParameters,
#pragma warning disable CA1869 // Cache and reuse 'JsonSerializerOptions' instances
              new JsonSerializerOptions { WriteIndented = true }));
#pragma warning restore CA1869 // Cache and reuse 'JsonSerializerOptions' instances

          actualStartBoundaries = await context
            .DapperCommand<AggregateEntity>(
              aggregateType,
              actualStartBoundariesSql,
              cancellationToken,
              targetParameters,
              300);
        }
      }
    }

    return bases.Select(basis =>
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

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
      var paramName = $"loc{index++}";
      parameters[paramName] = long.Parse(id);
      locationValueRows.Add($"(@{paramName})");
    }

    var joinLocationsClause = $@"
        JOIN (
            VALUES {string.Join(", ", locationValueRows)}
        ) AS selected_locations(location_id)
          ON selected_locations.location_id = a.measurement_location_id
    ";

    var inWindowAggregates = await context
      .DapperCommand<AggregateEntity>(
        aggregateType,
        $@"
          SELECT *
          FROM {table} a
          {joinLocationsClause}
          WHERE a.interval
              = '{quarterHourIntervalValue}'::{intervalTypeName}
            AND a.timestamp >= @from
            AND a.timestamp < @to
        ",
        cancellationToken,
        parameters);

    var nextBoundaries = await context
      .DapperCommand<AggregateEntity>(
        aggregateType,
        $@"
          SELECT *
          FROM (
            SELECT a.*,
            ROW_NUMBER() OVER (
              PARTITION BY a.measurement_location_id
              ORDER BY a.timestamp ASC
            ) AS rn
            FROM {table} a
            {joinLocationsClause}
            WHERE a.interval
                = '{quarterHourIntervalValue}'::{intervalTypeName}
              AND a.timestamp >= @to
          ) end_candidates
          WHERE rn = 1
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
      var blackoutParams = new Dictionary<string, object?>(parameters);
      var blackoutRows = new List<string>();
      int bIndex = 0;
      foreach (var id in blackoutLocationIds)
      {
        var pName = $"bloc{bIndex++}";
        blackoutParams[pName] = long.Parse(id);
        blackoutRows.Add($"(@{pName})");
      }

      var blackoutJoin = $@"
        JOIN (
            VALUES {string.Join(", ", blackoutRows)}
        ) AS selected_locations(location_id)
          ON selected_locations.location_id = a.measurement_location_id
      ";

      var lastReadingsBeforeBlackout = await context
        .DapperCommand<AggregateEntity>(
          aggregateType,
          $@"
            SELECT *
            FROM (
              SELECT a.*,
              ROW_NUMBER() OVER (
                PARTITION BY a.measurement_location_id
                ORDER BY a.timestamp DESC
              ) AS rn
              FROM {table} a
              {blackoutJoin}
              WHERE a.interval
                  = '{quarterHourIntervalValue}'::{intervalTypeName}
                AND a.timestamp < @from
            ) start_fallback
            WHERE rn = 1
          ",
          cancellationToken,
          blackoutParams);

      if (lastReadingsBeforeBlackout.Count != 0)
      {
        var targetParams = new Dictionary<string, object?>
        {
          {
            "interval",
            StringExtensions.ToSnakeCase(nameof(IntervalEntity.QuarterHour))
          }
        };

        var targetRows = new List<string>();
        int tIndex = 0;

        foreach (var reading in lastReadingsBeforeBlackout)
        {
          var monthStart = timeQueries.GetStartOfMonth(reading.Timestamp);

          var pLoc = $"tloc{tIndex}";
          var pDate = $"tdate{tIndex}";

          targetParams[pLoc] = long.Parse(reading.MeasurementLocationId);
          targetParams[pDate] = monthStart;

          targetRows.Add($"(@{pLoc}, @{pDate})");

          tIndex++;
        }

        if (targetRows.Count != 0)
        {
           actualStartBoundaries = await context
            .DapperCommand<AggregateEntity>(
              aggregateType,
              $@"
                SELECT *
                FROM (
                  SELECT a.*,
                  ROW_NUMBER() OVER (
                    PARTITION BY a.measurement_location_id
                    ORDER BY a.timestamp ASC
                  ) AS rn
                  FROM {table} a
                  JOIN (
                      VALUES {string.Join(", ", targetRows)}
                  ) AS targets(location_id, target_start)
                    ON targets.location_id = a.measurement_location_id
                  WHERE a.interval
                      = '{quarterHourIntervalValue}'::{intervalTypeName}
                    AND a.timestamp >= targets.target_start
                ) real_starts
                WHERE rn = 1
              ",
              cancellationToken,
              targetParams);
        }
      }
    }

    return bases.Select(basis =>
    {
      var locId = basis.MeasurementLocation.Id;

      var windowData = inWindowAggregates
        .Where(x => x.MeasurementLocationId == locId)
        .OrderBy(x => x.Timestamp)
        .ToList();

      var next = nextBoundaries
        .FirstOrDefault(x => x.MeasurementLocationId == locId);

      var prev = actualStartBoundaries
        .FirstOrDefault(x => x.MeasurementLocationId == locId);

      AggregateEntity? startAgg = null;
      AggregateEntity? endAgg = null;

      if (windowData.Count != 0)
      {
        startAgg = windowData.First();
        endAgg = next;
      }
      else
      {
        startAgg = prev;
        endAgg = next;
      }

      var resultAggregates = new List<AggregateEntity>(windowData);

      if (startAgg != null && !resultAggregates
        .Exists(x => x.Timestamp == startAgg.Timestamp))
      {
        resultAggregates.Insert(0, startAgg);
      }
      if (endAgg != null && !resultAggregates
        .Exists(x => x.Timestamp == endAgg.Timestamp))
      {
        resultAggregates.Add(endAgg);
      }

      var measuredFrom = startAgg?.Timestamp ?? fromDate;
      var measuredTo = endAgg?.Timestamp ?? toDate;

      var billedFrom = startAgg is not null
        ? timeQueries.GetStartOfMonth(startAgg.Timestamp)
        : fromDate;

      var billedTo = endAgg is not null
        ? timeQueries.GetStartOfMonth(endAgg.Timestamp)
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

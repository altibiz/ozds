using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Composite;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;
using Ozds.Data.Queries.Abstractions;
using Ozds.Data.Reflection;

namespace Ozds.Data.Queries;

public class BillingQueries(
  IDbContextFactory<DataDbContext> factory,
  EntityReflector reflector
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
      var aggregates = await ReadCalculationBaseAggregates(
        aggregateType,
        bases.Where(x =>
          x.Meter.GetType()
            == reflector.ResolveAggregateMeterType(aggregateType)),
        fromDate,
        toDate,
        cancellationToken
      );

      foreach (var intermediary in bases)
      {
        intermediary.Aggregates = aggregates
          .FirstOrDefault(x =>
            x.MeasurementLocation.Id == intermediary.MeasurementLocation.Id)
          ?.Aggregates
          ?? intermediary.Aggregates;
      }
    }

    return bases
      .Select(x => new NetworkUserCalculationBasisEntity
      {
        FromDate = fromDate,
        ToDate = toDate,
        Location = x.Location,
        NetworkUser = x.NetworkUser,
        MeasurementLocation = x.MeasurementLocation,
        Meter = x.Meter,
        UsageNetworkUserCatalogue = x.UsageNetworkUserCatalogue,
        SupplyRegulatoryCatalogue = x.SupplyRegulatoryCatalogue,
        Aggregates = x.Aggregates ?? new()
      })
      .ToList();
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
        .Where(context.ForeignKeyEquals<NetworkUserMeasurementLocationEntity>(
          networkUserId,
          nameof(NetworkUserMeasurementLocationEntity.NetworkUser)))
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
          SupplyRegulatoryCatalogue = x.NetworkUser.Location.RegulatoryCatalogue,
          Meter = x.Meter
        })
        .ToListAsync(cancellationToken);
  }

  private async Task<List<NetworkUserCalculationBasisEntity>>
    ReadCalculationBaseAggregates(
      Type aggregateType,
      IEnumerable<NetworkUserCalculationBasisEntity>
        intermediaries,
      DateTimeOffset fromDate,
      DateTimeOffset toDate,
      CancellationToken cancellationToken
    )
  {
    if (!intermediaries.Any())
    {
      return new List<NetworkUserCalculationBasisEntity>();
    }

    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    var table = reflector.ResolveEntityTable(aggregateType);

    var parameters = new Dictionary<string, object?>
    {
        {
          "interval",
          StringExtensions.ToSnakeCase(nameof(IntervalEntity.QuarterHour))
        },
        { "from", fromDate.ToString("o", CultureInfo.InvariantCulture) },
        { "to",   toDate.ToString("o", CultureInfo.InvariantCulture) },
    };

    var locationValueRows = new List<string>();
    int index = 0;
    foreach (var id in intermediaries.Select(x => x.MeasurementLocation.Id))
    {
        string paramName = $"loc{index++}";
        parameters[paramName] = id;
        locationValueRows.Add($"(@{paramName})");
    }

    string joinLocationsClause = $@"
        JOIN (
            VALUES {string.Join(", ", locationValueRows)}
        ) AS selected_locations(location_id)
          ON selected_locations.location_id = a.measurement_location_id
    ";

    var inRange = await context
      .DapperCommand<AggregateEntity>(
        aggregateType,
        $@"
          SELECT *
          FROM {table} a
          {joinLocationsClause}
          WHERE a.interval = @interval
            AND a.timestamp >= @from
            AND a.timestamp <= @to
        ",
        cancellationToken,
        parameters);

    if (intermediaries.All(i => inRange
      .Exists(p => p.MeasurementLocationId == i.MeasurementLocation.Id)))
    {
      return intermediaries
        .Select(x => new NetworkUserCalculationBasisEntity
        {
          Location = x.Location,
          NetworkUser = x.NetworkUser,
          MeasurementLocation = x.MeasurementLocation,
          UsageNetworkUserCatalogue = x.UsageNetworkUserCatalogue,
          SupplyRegulatoryCatalogue = x.SupplyRegulatoryCatalogue,
          Meter = x.Meter,
          Aggregates = inRange.Where(y =>
              y.MeasurementLocationId == x.MeasurementLocation.Id)
            .OrderBy(x => x.Timestamp)
            .ToList()
        })
        .ToList();
    }

    var start = await context
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
              WHERE a.interval = @interval
                AND a.timestamp >= @from
            ) start_candidates
            WHERE rn = 1
          ",
          cancellationToken,
          parameters);

    var startMissing = await context
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
            {joinLocationsClause}
            WHERE a.interval = @interval
              AND a.timestamp < @from
          ) start_fallback
          WHERE rn = 1
        ",
        cancellationToken,
        parameters);

    var end = await context
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
            {joinLocationsClause}
            WHERE a.interval = @interval
              AND a.timestamp <= @to
          ) end_candidates
          WHERE rn = 1
        ",
        cancellationToken,
        parameters);

    var endMissing = await context
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
            WHERE a.interval = @interval
              AND a.timestamp > @to
          ) end_fallback
          WHERE rn = 1
        ",
        cancellationToken,
        parameters);

    return intermediaries
      .Select(x =>
      {
        var aggregates = inRange.Where(y =>
            y.MeasurementLocationId == x.MeasurementLocation.Id)
          .OrderBy(x => x.Timestamp)
          .ToList();

        if (aggregates.Count == 0)
        {
          var xStart = start.FirstOrDefault(y =>
            y.MeasurementLocationId == x.MeasurementLocation.Id) ??
            startMissing.FirstOrDefault(y =>
              y.MeasurementLocationId == x.MeasurementLocation.Id);
          if (xStart is not null)
          {
            aggregates.Insert(0, xStart);
          }

          var xEnd = end.FirstOrDefault(y =>
            y.MeasurementLocationId == x.MeasurementLocation.Id) ??
            endMissing.FirstOrDefault(y =>
              y.MeasurementLocationId == x.MeasurementLocation.Id);
          if (xEnd is not null && xEnd.Timestamp != xStart?.Timestamp)
          {
            aggregates.Add(xEnd);
          }
        }

        return new NetworkUserCalculationBasisEntity
        {
          Location = x.Location,
          NetworkUser = x.NetworkUser,
          MeasurementLocation = x.MeasurementLocation,
          UsageNetworkUserCatalogue = x.UsageNetworkUserCatalogue,
          SupplyRegulatoryCatalogue = x.SupplyRegulatoryCatalogue,
          Meter = x.Meter,
          Aggregates = aggregates
        };
      })
      .ToList();
  }
}

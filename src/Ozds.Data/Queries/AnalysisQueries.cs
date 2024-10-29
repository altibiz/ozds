using System.Data;
using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Composite;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Entities.Joins;
using Ozds.Data.Extensions;
using Ozds.Data.Queries.Abstractions;

namespace Ozds.Data.Queries;

public class AnalysisQueries(
  IDbContextFactory<DataDbContext> factory
) : IQueries
{
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

    return await ReadAnalysisBasesByRepresentative(
      context,
      representativeId,
      role,
      fromDate,
      toDate,
      cancellationToken
    );
  }

#pragma warning disable CA1822 // Mark members as static
  internal async Task<List<AnalysisBasisEntity>>
    ReadAnalysisBasesByRepresentative(
      DataDbContext context,
      string representativeId,
      RoleEntity role,
      DateTimeOffset fromDate,
      DateTimeOffset toDate,
      CancellationToken cancellationToken
    )
  {
    var results = await GetDetailedMeasurementLocationsByRepresentative(
      context,
      representativeId,
      role,
      fromDate,
      toDate,
      cancellationToken
    );

    var analysisBases = MakeAnalysisBases(results);

    return analysisBases;
  }
#pragma warning restore CA1822 // Mark members as static

  private static async
    Task<List<DetailedMeasurementLocationsByRepresentativeIntermediary>>
    GetDetailedMeasurementLocationsByRepresentative(
      DataDbContext context,
      string representativeId,
      RoleEntity role,
      DateTimeOffset fromDate,
      DateTimeOffset toDate,
      CancellationToken cancellationToken
    )
  {
    var initialMeasurementLocationsQuery = role switch
    {
      RoleEntity.LocationRepresentative
        or RoleEntity.NetworkUserRepresentative =>
        MakeInitialMeasurementLocationsLocationNetworkUserQuery(
          context,
          "@representativeId"
        ),
      RoleEntity.OperatorRepresentative =>
        MakeInitialMeasurementLocationsOperatorQuery(
          context,
          "@representativeId"
        ),
      _ => throw new ArgumentOutOfRangeException(nameof(role))
    };

    var sql = $@"
      WITH
        initial_measurement_locations AS (
          {initialMeasurementLocationsQuery}
        ),
        abb_b2x_aggregates_max_power AS (
          {MakeAbbB2xAggregatesMaxPowerQuery(
            context,
            fromDateExpression: "@fromDate",
            toDateExpression: "@toDate"
          )}
        ),
        schneider_iem3xxx_aggregates_max_power AS (
          {MakeSchneideriEM3xxxAggregatesMaxPowerQuery(
            context,
            fromDateExpression: "@fromDate",
            toDateExpression: "@toDate"
          )}
        )

      SELECT
        initial_measurement_locations.*,
        {context.GetTableName<NetworkUserCalculationEntity>()}.*,
        {context.GetTableName<NetworkUserInvoiceEntity>()}.*,
        abb_b2x_aggregates_monthly.*,
        schneider_iem3xxx_aggregates_monthly.*,
        abb_b2x_measurements_last.*,
        schneider_iem3xxx_measurements_last.*,
        abb_b2x_aggregates_max_power.*,
        schneider_iem3xxx_aggregates_max_power.*
      FROM
        initial_measurement_locations
      LEFT JOIN {context.GetTableName<NetworkUserCalculationEntity>()}
        ON {context.GetTableName<NetworkUserCalculationEntity>()}
          .{context.GetForeignKeyColumnName<NetworkUserCalculationEntity>(
            nameof(NetworkUserCalculationEntity.NetworkUserMeasurementLocation))}
          = initial_measurement_locations.measurement_location_id
        AND {context.GetTableName<NetworkUserCalculationEntity>()}
          .{context.GetColumnName<NetworkUserCalculationEntity>(
          nameof(NetworkUserCalculationEntity.FromDate))} >= @fromDate
        AND {context.GetTableName<NetworkUserCalculationEntity>()}
          .{context.GetColumnName<NetworkUserCalculationEntity>(
            nameof(NetworkUserCalculationEntity.FromDate))} < @toDate
      LEFT JOIN {context.GetTableName<NetworkUserInvoiceEntity>()}
        ON {context.GetTableName<NetworkUserCalculationEntity>()}
          .{context.GetForeignKeyColumnName<NetworkUserCalculationEntity>(
            nameof(NetworkUserCalculationEntity.NetworkUserInvoice))}
          = {context.GetTableName<NetworkUserInvoiceEntity>()}
            .{context.GetPrimaryKeyColumnName<NetworkUserInvoiceEntity>()}
      LEFT JOIN {context.GetTableName<AbbB2xAggregateEntity>()}
        AS abb_b2x_aggregates_monthly
        ON abb_b2x_aggregates_monthly
          .{context.GetForeignKeyColumnName<AbbB2xAggregateEntity>(
            nameof(AbbB2xAggregateEntity.Meter))}
          = initial_measurement_locations
            .{context.GetForeignKeyColumnName<
              NetworkUserMeasurementLocationEntity>(
              nameof(NetworkUserMeasurementLocationEntity.Meter))}
        AND abb_b2x_aggregates_monthly
          .{context.GetColumnName<AbbB2xAggregateEntity>(
            nameof(AbbB2xAggregateEntity.Timestamp))} >= @fromDate
        AND abb_b2x_aggregates_monthly
          .{context.GetColumnName<AbbB2xAggregateEntity>(
            nameof(AbbB2xAggregateEntity.Timestamp))} < @toDate
        AND abb_b2x_aggregates_monthly
          .{context.GetColumnName<AbbB2xAggregateEntity>(
            nameof(AbbB2xAggregateEntity.Interval))}
          = '{nameof(IntervalEntity.Month).ToSnakeCase()}'
      LEFT JOIN {context.GetTableName<SchneideriEM3xxxAggregateEntity>()}
        AS schneider_iem3xxx_aggregates_monthly
        ON schneider_iem3xxx_aggregates_monthly
          .{context.GetForeignKeyColumnName<SchneideriEM3xxxAggregateEntity>(
            nameof(SchneideriEM3xxxAggregateEntity.Meter))}
          = initial_measurement_locations
            .{context.GetForeignKeyColumnName<
              NetworkUserMeasurementLocationEntity>(
              nameof(NetworkUserMeasurementLocationEntity.Meter))}
        AND schneider_iem3xxx_aggregates_monthly
          .{context.GetColumnName<SchneideriEM3xxxAggregateEntity>(
            nameof(SchneideriEM3xxxAggregateEntity.Timestamp))} >= @fromDate
        AND schneider_iem3xxx_aggregates_monthly
          .{context.GetColumnName<SchneideriEM3xxxAggregateEntity>(
            nameof(SchneideriEM3xxxAggregateEntity.Timestamp))} < @toDate
        AND schneider_iem3xxx_aggregates_monthly
          .{context.GetColumnName<SchneideriEM3xxxAggregateEntity>(
            nameof(SchneideriEM3xxxAggregateEntity.Interval))}
          = '{nameof(IntervalEntity.Month).ToSnakeCase()}'
      LEFT JOIN LATERAL (
        {MakeAbbB2xMeasurementsLastQuery(
          context,
          meterIdExpression: "initial_measurement_locations."
            + context.GetForeignKeyColumnName<
              NetworkUserMeasurementLocationEntity>(
              nameof(NetworkUserMeasurementLocationEntity.Meter)
            ),
          fromDateExpression: "@fromDate",
          toDateExpression: "@toDate"
        )}
      ) as abb_b2x_measurements_last ON TRUE
      LEFT JOIN LATERAL (
        {MakeSchneideriEM3xxxMeasurementsLastQuery(
          context,
          meterIdExpression: "initial_measurement_locations."
            + context.GetForeignKeyColumnName<
              NetworkUserMeasurementLocationEntity>(
              nameof(NetworkUserMeasurementLocationEntity.Meter)
            ),
          fromDateExpression: "@fromDate",
          toDateExpression: "@toDate"
        )}
      ) AS schneider_iem3xxx_measurements_last ON TRUE
      LEFT JOIN abb_b2x_aggregates_max_power
        ON abb_b2x_aggregates_max_power
          .{context.GetForeignKeyColumnName<AbbB2xAggregateEntity>(
            nameof(AbbB2xAggregateEntity.Meter))}
          = initial_measurement_locations
            .{context.GetForeignKeyColumnName<
              NetworkUserMeasurementLocationEntity>(
              nameof(NetworkUserMeasurementLocationEntity.Meter))}
      LEFT JOIN schneider_iem3xxx_aggregates_max_power
        ON schneider_iem3xxx_aggregates_max_power
          .{context.GetForeignKeyColumnName<SchneideriEM3xxxAggregateEntity>(
            nameof(AbbB2xAggregateEntity.Meter))}
          = initial_measurement_locations
            .{context.GetForeignKeyColumnName<
              NetworkUserMeasurementLocationEntity>(
              nameof(NetworkUserMeasurementLocationEntity.Meter))}
    ";

    var parameters = new { representativeId, fromDate, toDate };
    var result = await context
      .DapperCommand<DetailedMeasurementLocationsByRepresentativeIntermediary>(
        sql,
        cancellationToken,
        parameters);
    return result.ToList();
  }

  private static string MakeInitialMeasurementLocationsLocationNetworkUserQuery(
    DbContext context,
    string representativeIdExpression
  ) => $@"
    SELECT
      {context.GetTableName<NetworkUserMeasurementLocationEntity>()}
        .{context.GetPrimaryKeyColumnName<
          NetworkUserMeasurementLocationEntity>()}
        AS measurement_location_id,
      {context.GetTableName<NetworkUserMeasurementLocationEntity>()}.*,
      {context.GetTableName<NetworkUserEntity>()}.*,
      {context.GetTableName<LocationEntity>()}.*,
      {context.GetTableName<MeterEntity>()}.*
    FROM
      {context.GetTableName<NetworkUserRepresentativeEntity>()}
    INNER JOIN {context.GetTableName<NetworkUserEntity>()}
      ON {context.GetTableName<NetworkUserRepresentativeEntity>()}.
        {context.GetForeignKeyColumnName<NetworkUserRepresentativeEntity>(
          nameof(NetworkUserRepresentativeEntity.NetworkUser))}
        = {context.GetTableName<NetworkUserEntity>()}
          .{context.GetPrimaryKeyColumnName<NetworkUserEntity>()}
    INNER JOIN {context.GetTableName<LocationEntity>()}
      ON {context.GetTableName<NetworkUserEntity>()}
        .{context.GetForeignKeyColumnName<NetworkUserEntity>(
          nameof(NetworkUserEntity.Location))}
        = {context.GetTableName<LocationEntity>()}
          .{context.GetPrimaryKeyColumnName<LocationEntity>()}
    INNER JOIN {context
      .GetTableName<NetworkUserMeasurementLocationEntity>()}
      ON {context.GetTableName<NetworkUserEntity>()}
        .{context.GetPrimaryKeyColumnName<NetworkUserEntity>()}
          = {context.GetTableName<NetworkUserMeasurementLocationEntity>()}
            .{context.GetForeignKeyColumnName<
              NetworkUserMeasurementLocationEntity>(
              nameof(NetworkUserMeasurementLocationEntity.NetworkUser))}
    LEFT JOIN {context.GetTableName<MeterEntity>()}
      ON {context.GetTableName<NetworkUserMeasurementLocationEntity>()}
        .{context.GetForeignKeyColumnName<
          NetworkUserMeasurementLocationEntity>(
          nameof(NetworkUserMeasurementLocationEntity.Meter))}
        = {context.GetTableName<MeterEntity>()}
          .{context.GetPrimaryKeyColumnName<MeterEntity>()}
    WHERE
      {context.GetTableName<NetworkUserRepresentativeEntity>()}
        .{context.GetForeignKeyColumnName<
          NetworkUserRepresentativeEntity>(
          nameof(NetworkUserRepresentativeEntity.Representative))}
        = {representativeIdExpression}
    UNION
    SELECT
      {context.GetTableName<NetworkUserMeasurementLocationEntity>()}
        .{context.GetPrimaryKeyColumnName<
          NetworkUserMeasurementLocationEntity>()}
        AS measurement_location_id,
      {context.GetTableName<NetworkUserMeasurementLocationEntity>()}.*,
      {context.GetTableName<NetworkUserEntity>()}.*,
      {context.GetTableName<LocationEntity>()}.*,
      {context.GetTableName<MeterEntity>()}.*
    FROM
      {context.GetTableName<LocationRepresentativeEntity>()}
    INNER JOIN {context.GetTableName<LocationEntity>()}
      ON {context.GetTableName<LocationRepresentativeEntity>()}
        .{context.GetForeignKeyColumnName<LocationRepresentativeEntity>(
          nameof(LocationRepresentativeEntity.Location))}
        = {context.GetTableName<LocationEntity>()}
          .{context.GetPrimaryKeyColumnName<LocationEntity>()}
    INNER JOIN {context.GetTableName<NetworkUserEntity>()}
      ON {context.GetTableName<LocationEntity>()}
        .{context.GetPrimaryKeyColumnName<LocationEntity>()}
        = {context.GetTableName<NetworkUserEntity>()}
          .{context.GetForeignKeyColumnName<NetworkUserEntity>(
            nameof(NetworkUserEntity.Location))}
    INNER JOIN {context
      .GetTableName<NetworkUserMeasurementLocationEntity>()}
      ON {context.GetTableName<NetworkUserEntity>()}
        .{context.GetPrimaryKeyColumnName<NetworkUserEntity>()}
        = {context.GetTableName<NetworkUserMeasurementLocationEntity>()}
          .{context.GetForeignKeyColumnName<
            NetworkUserMeasurementLocationEntity>(
            nameof(NetworkUserMeasurementLocationEntity.NetworkUser))}
    LEFT JOIN {context.GetTableName<MeterEntity>()}
      ON {context.GetTableName<NetworkUserMeasurementLocationEntity>()}
        .{context.GetForeignKeyColumnName<
          NetworkUserMeasurementLocationEntity>(
          nameof(NetworkUserMeasurementLocationEntity.Meter))}
        = {context.GetTableName<MeterEntity>()}
          .{context.GetPrimaryKeyColumnName<MeterEntity>()}
    WHERE
      {context.GetTableName<LocationRepresentativeEntity>()}
        .{context.GetForeignKeyColumnName<LocationRepresentativeEntity>(
          nameof(LocationRepresentativeEntity.Representative))}
        = {representativeIdExpression}
  ";

  private static string MakeInitialMeasurementLocationsOperatorQuery(
    DbContext context,
#pragma warning disable S1172 // Unused method parameters should be removed
    string representativeIdExpression
#pragma warning restore S1172 // Unused method parameters should be removed
  ) => $@"
    SELECT
      {context.GetTableName<NetworkUserMeasurementLocationEntity>()}
        .{context.GetPrimaryKeyColumnName<
          NetworkUserMeasurementLocationEntity>()}
        AS measurement_location_id,
      {context.GetTableName<NetworkUserMeasurementLocationEntity>()}.*,
      {context.GetTableName<NetworkUserEntity>()}.*,
      {context.GetTableName<LocationEntity>()}.*,
      {context.GetTableName<MeterEntity>()}.*
    FROM
      {context.GetTableName<NetworkUserMeasurementLocationEntity>()}
    INNER JOIN {context.GetTableName<NetworkUserEntity>()}
      ON {context.GetTableName<NetworkUserMeasurementLocationEntity>()}
        .{context.GetForeignKeyColumnName<
          NetworkUserMeasurementLocationEntity>(
          nameof(NetworkUserMeasurementLocationEntity.NetworkUser))}
        = {context.GetTableName<NetworkUserEntity>()}
          .{context.GetPrimaryKeyColumnName<NetworkUserEntity>()}
    INNER JOIN {context.GetTableName<LocationEntity>()}
      ON {context.GetTableName<NetworkUserEntity>()}
        .{context.GetForeignKeyColumnName<NetworkUserEntity>(
          nameof(NetworkUserEntity.Location))}
        = {context.GetTableName<LocationEntity>()}
          .{context.GetPrimaryKeyColumnName<LocationEntity>()}
    LEFT JOIN {context.GetTableName<MeterEntity>()}
      ON {context.GetTableName<NetworkUserMeasurementLocationEntity>()}
        .{context.GetForeignKeyColumnName<
          NetworkUserMeasurementLocationEntity>(
          nameof(NetworkUserMeasurementLocationEntity.Meter))}
        = {context.GetTableName<MeterEntity>()}
          .{context.GetPrimaryKeyColumnName<MeterEntity>()}
  ";

  private static string MakeAbbB2xMeasurementsLastQuery(
    DbContext context,
    string meterIdExpression,
    string fromDateExpression,
    string toDateExpression
  ) => $@"
    SELECT *
    FROM {context.GetTableName<AbbB2xMeasurementEntity>()}
    WHERE
      {context.GetTableName<AbbB2xMeasurementEntity>()}
        .{context.GetForeignKeyColumnName<AbbB2xMeasurementEntity>(
          nameof(AbbB2xMeasurementEntity.Meter))}
        = {meterIdExpression}
      AND {context.GetTableName<AbbB2xMeasurementEntity>()}
        .{context.GetColumnName<AbbB2xMeasurementEntity>(
          nameof(AbbB2xMeasurementEntity.Timestamp))} >= {fromDateExpression}
      AND {context.GetTableName<AbbB2xMeasurementEntity>()}
        .{context.GetColumnName<AbbB2xMeasurementEntity>(
          nameof(AbbB2xMeasurementEntity.Timestamp))} < {toDateExpression}
    ORDER BY {context.GetTableName<AbbB2xMeasurementEntity>()}
      .{context.GetColumnName<AbbB2xMeasurementEntity>(
        nameof(AbbB2xMeasurementEntity.Timestamp))} DESC
    LIMIT 1
  ";

  private static string MakeSchneideriEM3xxxMeasurementsLastQuery(
    DbContext context,
    string meterIdExpression,
    string fromDateExpression,
    string toDateExpression
  ) => $@"
    SELECT *
    FROM {context.GetTableName<SchneideriEM3xxxMeasurementEntity>()}
    WHERE
      {context.GetTableName<SchneideriEM3xxxMeasurementEntity>()}
        .{context.GetForeignKeyColumnName<
          SchneideriEM3xxxMeasurementEntity>(
          nameof(SchneideriEM3xxxMeasurementEntity.Meter))}
        = {meterIdExpression}
      AND {context.GetTableName<SchneideriEM3xxxMeasurementEntity>()}
        .{context.GetColumnName<SchneideriEM3xxxMeasurementEntity>(
          nameof(SchneideriEM3xxxMeasurementEntity.Timestamp))}
        >= {fromDateExpression}
      AND {context.GetTableName<SchneideriEM3xxxMeasurementEntity>()}
        .{context.GetColumnName<SchneideriEM3xxxMeasurementEntity>(
          nameof(SchneideriEM3xxxMeasurementEntity.Timestamp))}
        < {toDateExpression}
    ORDER BY {context.GetTableName<SchneideriEM3xxxMeasurementEntity>()}
      .{context.GetColumnName<SchneideriEM3xxxMeasurementEntity>(
        nameof(SchneideriEM3xxxMeasurementEntity.Timestamp))} DESC
    LIMIT 1
  ";

  private static string MakeAbbB2xAggregatesMaxPowerQuery(
    DbContext context,
    string fromDateExpression,
    string toDateExpression
  ) => $@"
    SELECT
      {string.Join(
        ", ",
        context
          .GetColumnNames<AbbB2xAggregateEntity>()
          .Select(x => $"abb_b2x_aggregates_max_power_intermediary.{x}"))}
    FROM (
      SELECT
        abb_b2x_aggregates_max_power_window.*,
        EXTRACT(YEAR FROM abb_b2x_aggregates_max_power_window
          .{context.GetColumnName<AbbB2xAggregateEntity>(
            nameof(AbbB2xAggregateEntity.Timestamp))}
          AT TIME ZONE 'Europe/Zagreb') AS year,
        EXTRACT(MONTH FROM abb_b2x_aggregates_max_power_window
          .{context.GetColumnName<AbbB2xAggregateEntity>(
            nameof(AbbB2xAggregateEntity.Timestamp))}
          AT TIME ZONE 'Europe/Zagreb') AS month,
        COALESCE(abb_b2x_aggregates_max_power_window
          .{context.GetColumnName<AbbB2xAggregateEntity>(
            nameof(AbbB2xAggregateEntity.ActivePowerL1NetT0Avg_W))}, 0)
        + COALESCE(abb_b2x_aggregates_max_power_window
          .{context.GetColumnName<AbbB2xAggregateEntity>(
            nameof(AbbB2xAggregateEntity.ActivePowerL2NetT0Avg_W))}, 0)
        + COALESCE(abb_b2x_aggregates_max_power_window
          .{context.GetColumnName<AbbB2xAggregateEntity>(
            nameof(AbbB2xAggregateEntity.ActivePowerL3NetT0Avg_W))}, 0)
          AS active_power_total_net_t0_avg_w,
        MAX(
          COALESCE(
            abb_b2x_aggregates_max_power_window
              .{context.GetColumnName<AbbB2xAggregateEntity>(
                nameof(AbbB2xAggregateEntity.ActivePowerL1NetT0Avg_W))},
            0)
          + COALESCE(
              abb_b2x_aggregates_max_power_window
                .{context.GetColumnName<AbbB2xAggregateEntity>(
                  nameof(AbbB2xAggregateEntity.ActivePowerL2NetT0Avg_W))},
                0)
          + COALESCE(
              abb_b2x_aggregates_max_power_window
                .{context.GetColumnName<AbbB2xAggregateEntity>(
                  nameof(AbbB2xAggregateEntity.ActivePowerL3NetT0Avg_W))},
              0)
        ) OVER (
          PARTITION BY
            abb_b2x_aggregates_max_power_window
              .{context.GetForeignKeyColumnName<AbbB2xAggregateEntity>(
                nameof(AbbB2xAggregateEntity.Meter))},
            EXTRACT(YEAR FROM abb_b2x_aggregates_max_power_window
              .{context.GetColumnName<AbbB2xAggregateEntity>(
                nameof(AbbB2xAggregateEntity.Timestamp))}
              AT TIME ZONE 'Europe/Zagreb'),
            EXTRACT(MONTH FROM abb_b2x_aggregates_max_power_window
                .{context.GetColumnName<AbbB2xAggregateEntity>(
              nameof(AbbB2xAggregateEntity.Timestamp))}
              AT TIME ZONE 'Europe/Zagreb')
        ) AS active_power_total_net_t0_max_per_meter_month_w
      FROM {context.GetTableName<AbbB2xAggregateEntity>()}
        AS abb_b2x_aggregates_max_power_window
      WHERE
        abb_b2x_aggregates_max_power_window
          .{context.GetColumnName<AbbB2xAggregateEntity>(
            nameof(AbbB2xAggregateEntity.Timestamp))} >= {fromDateExpression}
        AND abb_b2x_aggregates_max_power_window
          .{context.GetColumnName<AbbB2xAggregateEntity>(
            nameof(AbbB2xAggregateEntity.Timestamp))} < {toDateExpression}
        AND abb_b2x_aggregates_max_power_window
          .{context.GetColumnName<AbbB2xAggregateEntity>(
            nameof(AbbB2xAggregateEntity.Interval))}
          = '{nameof(IntervalEntity.QuarterHour).ToSnakeCase()}'
    ) AS abb_b2x_aggregates_max_power_intermediary
    WHERE
      abb_b2x_aggregates_max_power_intermediary
        .active_power_total_net_t0_avg_w
        = abb_b2x_aggregates_max_power_intermediary
          .active_power_total_net_t0_max_per_meter_month_w
  ";

  private static string MakeSchneideriEM3xxxAggregatesMaxPowerQuery(
    DbContext context,
    string fromDateExpression,
    string toDateExpression
  ) => $@"
    SELECT
      {string.Join(
        ", ",
        context
          .GetColumnNames<SchneideriEM3xxxAggregateEntity>()
          .Select(x =>
            $"schneider_iem3xxx_aggregates_max_power_intermediary.{x}"))}
    FROM (
      SELECT
        schneider_iem3xxx_aggregates_max_power_window.*,
        EXTRACT(YEAR FROM schneider_iem3xxx_aggregates_max_power_window
          .{context.GetColumnName<SchneideriEM3xxxAggregateEntity>(
            nameof(SchneideriEM3xxxAggregateEntity.Timestamp))}
          AT TIME ZONE 'Europe/Zagreb') AS year,
        EXTRACT(MONTH FROM schneider_iem3xxx_aggregates_max_power_window
          .{context.GetColumnName<SchneideriEM3xxxAggregateEntity>(
            nameof(SchneideriEM3xxxAggregateEntity.Timestamp))}
          AT TIME ZONE 'Europe/Zagreb') AS month,
        COALESCE(schneider_iem3xxx_aggregates_max_power_window
          .{context.GetColumnName<SchneideriEM3xxxAggregateEntity>(
            nameof(SchneideriEM3xxxAggregateEntity.ActivePowerL1NetT0Avg_W))}, 0)
        + COALESCE(schneider_iem3xxx_aggregates_max_power_window
          .{context.GetColumnName<SchneideriEM3xxxAggregateEntity>(
            nameof(SchneideriEM3xxxAggregateEntity.ActivePowerL2NetT0Avg_W))}, 0)
        + COALESCE(schneider_iem3xxx_aggregates_max_power_window
          .{context.GetColumnName<SchneideriEM3xxxAggregateEntity>(
            nameof(SchneideriEM3xxxAggregateEntity.ActivePowerL3NetT0Avg_W))}, 0)
          AS active_power_total_net_t0_avg_w,
        MAX(
          COALESCE(
            schneider_iem3xxx_aggregates_max_power_window
              .{context.GetColumnName<SchneideriEM3xxxAggregateEntity>(
                nameof(SchneideriEM3xxxAggregateEntity.ActivePowerL1NetT0Avg_W))},
            0)
          + COALESCE(
              schneider_iem3xxx_aggregates_max_power_window
                .{context.GetColumnName<SchneideriEM3xxxAggregateEntity>(
                  nameof(SchneideriEM3xxxAggregateEntity.ActivePowerL2NetT0Avg_W))},
                0)
          + COALESCE(
              schneider_iem3xxx_aggregates_max_power_window
                .{context.GetColumnName<SchneideriEM3xxxAggregateEntity>(
                  nameof(SchneideriEM3xxxAggregateEntity.ActivePowerL3NetT0Avg_W))},
              0)
        ) OVER (
          PARTITION BY
            schneider_iem3xxx_aggregates_max_power_window
              .{context.GetForeignKeyColumnName<SchneideriEM3xxxAggregateEntity>(
                nameof(SchneideriEM3xxxAggregateEntity.Meter))},
            EXTRACT(YEAR FROM schneider_iem3xxx_aggregates_max_power_window
              .{context.GetColumnName<SchneideriEM3xxxAggregateEntity>(
                nameof(SchneideriEM3xxxAggregateEntity.Timestamp))}
              AT TIME ZONE 'Europe/Zagreb'),
            EXTRACT(MONTH FROM schneider_iem3xxx_aggregates_max_power_window
                .{context.GetColumnName<SchneideriEM3xxxAggregateEntity>(
              nameof(SchneideriEM3xxxAggregateEntity.Timestamp))}
              AT TIME ZONE 'Europe/Zagreb')
        ) AS active_power_total_net_t0_max_per_meter_month_w
      FROM {context.GetTableName<SchneideriEM3xxxAggregateEntity>()}
        AS schneider_iem3xxx_aggregates_max_power_window
      WHERE
        schneider_iem3xxx_aggregates_max_power_window
          .{context.GetColumnName<SchneideriEM3xxxAggregateEntity>(
            nameof(SchneideriEM3xxxAggregateEntity.Timestamp))}
          >= {fromDateExpression}
        AND schneider_iem3xxx_aggregates_max_power_window
          .{context.GetColumnName<SchneideriEM3xxxAggregateEntity>(
            nameof(SchneideriEM3xxxAggregateEntity.Timestamp))}
          < {toDateExpression}
        AND schneider_iem3xxx_aggregates_max_power_window
          .{context.GetColumnName<SchneideriEM3xxxAggregateEntity>(
            nameof(SchneideriEM3xxxAggregateEntity.Interval))}
          = '{nameof(IntervalEntity.QuarterHour).ToSnakeCase()}'
    ) AS schneider_iem3xxx_aggregates_max_power_intermediary
    WHERE
      schneider_iem3xxx_aggregates_max_power_intermediary
        .active_power_total_net_t0_avg_w
        = schneider_iem3xxx_aggregates_max_power_intermediary
          .active_power_total_net_t0_max_per_meter_month_w
  ";

  private static List<AnalysisBasisEntity>
    MakeAnalysisBases(
      IEnumerable<DetailedMeasurementLocationsByRepresentativeIntermediary> items
    )
  {
    return items
      .GroupBy(x => x.MeasurementLocation.Id)
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

  private sealed class DetailedMeasurementLocationsByRepresentativeIntermediary
  {
    public LocationEntity Location
    {
      get;
      init;
    } = default!;

    public NetworkUserEntity? NetworkUser
    {
      get;
      init;
    } = default!;

    public NetworkUserMeasurementLocationEntity MeasurementLocation
    {
      get;
      init;
    } = default!;

    public MeterEntity Meter
    {
      get;
      init;
    } = default!;

    public NetworkUserCalculationEntity? NetworkUserCalculation
    {
      get;
      init;
    } = default!;

    public NetworkUserInvoiceEntity? NetworkUserInvoice
    {
      get;
      init;
    } = default!;

    public AbbB2xAggregateEntity? AbbMonthlyAggregate
    {
      get;
      init;
    } = default!;

    public SchneideriEM3xxxAggregateEntity? SchneiderMonthlyAggregate
    {
      get;
      init;
    } = default!;

    public AbbB2xMeasurementEntity AbbLastMeasurement
    {
      get;
      init;
    } = default!;

    public SchneideriEM3xxxMeasurementEntity? SchneiderLastMeasurement
    {
      get;
      init;
    } = default!;

    public AbbB2xAggregateEntity? AbbMaxPowerAggregate
    {
      get;
      init;
    } = default!;

    public SchneideriEM3xxxAggregateEntity? SchneiderMaxPowerAggregate
    {
      get;
      init;
    } = default!;
  }
}

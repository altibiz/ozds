using System.Data;
using Dapper;
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
    var sql = role switch
    {
      RoleEntity.LocationRepresentative
        or RoleEntity.NetworkUserRepresentative => $@"
        WITH initial_measurement_locations AS (
          SELECT
            {context.GetTableName<NetworkUserMeasurementLocationEntity>()}
              .{context.GetColumnName<NetworkUserMeasurementLocationEntity>(
                nameof(NetworkUserMeasurementLocationEntity.Id))}
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
              = @representativeId

          UNION

          SELECT
            {context.GetTableName<NetworkUserMeasurementLocationEntity>()}
              .{context.GetColumnName<NetworkUserMeasurementLocationEntity>(
                nameof(NetworkUserMeasurementLocationEntity.Id))}
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
              = @representativeId
        )
      ",
      RoleEntity.OperatorRepresentative => $@"
        WITH initial_measurement_locations AS (
          SELECT
            {context.GetTableName<NetworkUserMeasurementLocationEntity>()}
              .{context.GetColumnName<NetworkUserMeasurementLocationEntity>(
              nameof(NetworkUserMeasurementLocationEntity.Id))}
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
        )
      ",
      _ => throw new ArgumentOutOfRangeException(nameof(role))
    };

    sql += $@"
      SELECT
        initial_measurement_locations.*,
        {context.GetTableName<NetworkUserCalculationEntity>()}.*,
        {context.GetTableName<NetworkUserInvoiceEntity>()}.*,
        abb_b2x_measurements_last.*,
        schneider_iem3xxx_measurements_last.*,
        abb_b2x_aggregates_monthly.*,
        schneider_iem3xxx_aggregates_monthly.*,
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
      LEFT JOIN LATERAL (
        SELECT *
        FROM {context.GetTableName<AbbB2xMeasurementEntity>()}
        WHERE
          {context.GetTableName<AbbB2xMeasurementEntity>()}
            .{context.GetForeignKeyColumnName<AbbB2xMeasurementEntity>(
              nameof(AbbB2xMeasurementEntity.Meter))}
            = initial_measurement_locations
              .{context.GetForeignKeyColumnName<
                NetworkUserMeasurementLocationEntity>(
                nameof(NetworkUserMeasurementLocationEntity.Meter))}
          AND {context.GetTableName<AbbB2xMeasurementEntity>()}
            .{context.GetColumnName<AbbB2xMeasurementEntity>(
              nameof(AbbB2xMeasurementEntity.Timestamp))} >= @fromDate
          AND {context.GetTableName<AbbB2xMeasurementEntity>()}
            .{context.GetColumnName<AbbB2xMeasurementEntity>(
              nameof(AbbB2xMeasurementEntity.Timestamp))} < @toDate
        ORDER BY {context.GetTableName<AbbB2xMeasurementEntity>()}
          .{context.GetColumnName<AbbB2xMeasurementEntity>(
            nameof(AbbB2xMeasurementEntity.Timestamp))} DESC
        LIMIT 1
      ) AS abb_b2x_measurements_last ON TRUE
      LEFT JOIN LATERAL (
        SELECT *
        FROM {context.GetTableName<SchneideriEM3xxxMeasurementEntity>()}
        WHERE
          {context.GetTableName<SchneideriEM3xxxMeasurementEntity>()}
            .{context.GetForeignKeyColumnName<
              SchneideriEM3xxxMeasurementEntity>(
              nameof(SchneideriEM3xxxMeasurementEntity.Meter))}
            = initial_measurement_locations
              .{context.GetForeignKeyColumnName<
                NetworkUserMeasurementLocationEntity>(
                nameof(NetworkUserMeasurementLocationEntity.Meter))}
          AND {context.GetTableName<SchneideriEM3xxxMeasurementEntity>()}
            .{context.GetColumnName<SchneideriEM3xxxMeasurementEntity>(
              nameof(SchneideriEM3xxxMeasurementEntity.Timestamp))} >= @fromDate
          AND {context.GetTableName<SchneideriEM3xxxMeasurementEntity>()}
            .{context.GetColumnName<SchneideriEM3xxxMeasurementEntity>(
              nameof(SchneideriEM3xxxMeasurementEntity.Timestamp))} < @toDate
        ORDER BY {context.GetTableName<SchneideriEM3xxxMeasurementEntity>()}
          .{context.GetColumnName<SchneideriEM3xxxMeasurementEntity>(
            nameof(SchneideriEM3xxxMeasurementEntity.Timestamp))} DESC
        LIMIT 1
      ) AS schneider_iem3xxx_measurements_last ON TRUE
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
            nameof(AbbB2xAggregateEntity.Interval))} = 'Month'
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
            nameof(SchneideriEM3xxxAggregateEntity.Interval))} = 'Month'
      LEFT JOIN (
        SELECT
          {context.GetTableName<AbbB2xAggregateEntity>()}
            .{context.GetForeignKeyColumnName<AbbB2xAggregateEntity>(
              nameof(AbbB2xAggregateEntity.Meter))} AS meter_id,
          MAX(
            COALESCE({context.GetTableName<AbbB2xAggregateEntity>()}
              .{context.GetColumnName<AbbB2xAggregateEntity>(
                nameof(AbbB2xAggregateEntity.ActivePowerL1NetT0Avg_W))}, 0)
            + COALESCE({context.GetTableName<AbbB2xAggregateEntity>()}
                .{context.GetColumnName<AbbB2xAggregateEntity>(
                  nameof(AbbB2xAggregateEntity.ActivePowerL2NetT0Avg_W))}, 0)
            + COALESCE({context.GetTableName<AbbB2xAggregateEntity>()}
                .{context.GetColumnName<AbbB2xAggregateEntity>(
                  nameof(AbbB2xAggregateEntity.ActivePowerL3NetT0Avg_W))}, 0)
          ) AS max_power,
          EXTRACT(YEAR FROM {context.GetTableName<AbbB2xAggregateEntity>()}
            .{context.GetColumnName<AbbB2xAggregateEntity>(
              nameof(AbbB2xAggregateEntity.Timestamp))}
            AT TIME ZONE 'Europe/Zagreb') AS year,
          EXTRACT(MONTH FROM {context.GetTableName<AbbB2xAggregateEntity>()}
            .{context.GetColumnName<AbbB2xAggregateEntity>(
              nameof(AbbB2xAggregateEntity.Timestamp))}
            AT TIME ZONE 'Europe/Zagreb') AS month
        FROM {context.GetTableName<AbbB2xAggregateEntity>()}
        WHERE
          {context.GetTableName<AbbB2xAggregateEntity>()}
            .{context.GetColumnName<AbbB2xAggregateEntity>(
            nameof(AbbB2xAggregateEntity.Timestamp))} >= @fromDate
          AND {context.GetTableName<AbbB2xAggregateEntity>()}
            .{context.GetColumnName<AbbB2xAggregateEntity>(
              nameof(AbbB2xAggregateEntity.Timestamp))} < @toDate
          AND {context.GetTableName<AbbB2xAggregateEntity>()}
            .{context.GetColumnName<AbbB2xAggregateEntity>(
              nameof(AbbB2xAggregateEntity.Interval))} = 'QuarterHour'
        GROUP BY {context.GetTableName<AbbB2xAggregateEntity>()}
          .{context.GetForeignKeyColumnName<AbbB2xAggregateEntity>(
            nameof(AbbB2xAggregateEntity.Meter))}, year, month
      ) AS abb_b2x_aggregates_max_power
        ON abb_b2x_aggregates_max_power.meter_id
          = initial_measurement_locations
            .{context.GetForeignKeyColumnName<
              NetworkUserMeasurementLocationEntity>(
              nameof(NetworkUserMeasurementLocationEntity.Meter))}
      LEFT JOIN (
        SELECT
          {context.GetTableName<SchneideriEM3xxxAggregateEntity>()}
            .{context.GetForeignKeyColumnName<SchneideriEM3xxxAggregateEntity>(
              nameof(SchneideriEM3xxxAggregateEntity.Meter))} AS meter_id,
          MAX(
            COALESCE({context.GetTableName<SchneideriEM3xxxAggregateEntity>()}
              .{context.GetColumnName<SchneideriEM3xxxAggregateEntity>(
                nameof(SchneideriEM3xxxAggregateEntity.ActivePowerL1NetT0Avg_W))}, 0)
            + COALESCE({context.GetTableName<SchneideriEM3xxxAggregateEntity>()}
                .{context.GetColumnName<SchneideriEM3xxxAggregateEntity>(
                nameof(SchneideriEM3xxxAggregateEntity.ActivePowerL2NetT0Avg_W))}, 0)
            + COALESCE({context.GetTableName<SchneideriEM3xxxAggregateEntity>()}
                .{context.GetColumnName<SchneideriEM3xxxAggregateEntity>(
                  nameof(SchneideriEM3xxxAggregateEntity.ActivePowerL3NetT0Avg_W))}, 0)
          ) AS max_power,
          EXTRACT(YEAR FROM {context.GetTableName<SchneideriEM3xxxAggregateEntity>()}
            .{context.GetColumnName<SchneideriEM3xxxAggregateEntity>(
              nameof(SchneideriEM3xxxAggregateEntity.Timestamp))}
            AT TIME ZONE 'Europe/Zagreb') AS year,
          EXTRACT(MONTH FROM {context.GetTableName<SchneideriEM3xxxAggregateEntity>()}
            .{context.GetColumnName<SchneideriEM3xxxAggregateEntity>(
              nameof(SchneideriEM3xxxAggregateEntity.Timestamp))}
            AT TIME ZONE 'Europe/Zagreb') AS month
        FROM {context.GetTableName<SchneideriEM3xxxAggregateEntity>()}
        WHERE
          {context.GetTableName<SchneideriEM3xxxAggregateEntity>()}
            .{context.GetColumnName<SchneideriEM3xxxAggregateEntity>(
              nameof(SchneideriEM3xxxAggregateEntity.Timestamp))} >= @fromDate
          AND {context.GetTableName<SchneideriEM3xxxAggregateEntity>()}
            .{context.GetColumnName<SchneideriEM3xxxAggregateEntity>(
              nameof(SchneideriEM3xxxAggregateEntity.Timestamp))} < @toDate
          AND {context.GetTableName<SchneideriEM3xxxAggregateEntity>()}
            .{context.GetColumnName<SchneideriEM3xxxAggregateEntity>(
              nameof(SchneideriEM3xxxAggregateEntity.Interval))} = 'QuarterHour'
        GROUP BY {context.GetTableName<SchneideriEM3xxxAggregateEntity>()}
          .{context.GetForeignKeyColumnName<SchneideriEM3xxxAggregateEntity>(
            nameof(SchneideriEM3xxxAggregateEntity.Meter))}, year, month
      ) AS schneider_iem3xxx_aggregates_max_power
        ON schneider_iem3xxx_aggregates_max_power.meter_id
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

  private List<AnalysisBasisEntity>
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

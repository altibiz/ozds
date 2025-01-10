using System.Data;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Ozds.Data.Attributes;
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
  IDbContextFactory<DataDbContext> factory,
  ILogger<AnalysisQueries> logger
) : IQueries
{
  public async Task<List<AnalysisBasisEntity>>
    ReadAnalysisBasesByRepresentativeAndLocation(
      string representativeId,
      RoleEntity role,
      DateTimeOffset fromDate,
      DateTimeOffset toDate,
      string? locationId,
      CancellationToken cancellationToken
    )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    return await ReadAnalysisBasesByRepresentativeAndLocation(
      context,
      representativeId,
      role,
      fromDate,
      toDate,
      locationId,
      cancellationToken
    );
  }

#pragma warning disable CA1822 // Mark members as static
  internal async Task<List<AnalysisBasisEntity>>
    ReadAnalysisBasesByRepresentativeAndLocation(
      DataDbContext context,
      string representativeId,
      RoleEntity role,
      DateTimeOffset fromDate,
      DateTimeOffset toDate,
      string? locationId,
      CancellationToken cancellationToken
    )
  {
    var stopwatch = Stopwatch.StartNew();
    var results = await GetDetailedMeasurementLocationsByRepresentative(
      context,
      representativeId,
      role,
      fromDate,
      toDate,
      locationId,
      cancellationToken
    );
    var analysisBases = MakeAnalysisBases(results, fromDate, toDate);
    stopwatch.Stop();
    logger.LogDebug(
      "Read {Count} analysis bases in {Elapsed}",
      analysisBases.Count,
      stopwatch.Elapsed
    );

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
      string? locationId,
      CancellationToken cancellationToken
    )
  {
    var initialMeasurementLocationsQuery = role switch
    {
      RoleEntity.LocationRepresentative
        or RoleEntity.NetworkUserRepresentative =>
        MakeInitialMeasurementLocationsLocationNetworkUserQuery(
          context,
          "@representativeId",
          locationId is null ? null : "@locationId"
        ),
      RoleEntity.OperatorRepresentative =>
        MakeInitialMeasurementLocationsOperatorQuery(
          context,
          locationId is null ? null : "@locationId"
        ),
      _ => throw new ArgumentOutOfRangeException(nameof(role))
    };

    var sql = $@"
      WITH
        initial_measurement_locations AS (
          {initialMeasurementLocationsQuery}
        )

      SELECT
        initial_measurement_locations.*,
        {context.GetTableName<NetworkUserCalculationEntity>()}.*,
        {context.GetTableName<NetworkUserInvoiceEntity>()}.*,
        abb_b2x_aggregates_monthly.*,
        schneider_iem3xxx_aggregates_monthly.*,
        abb_b2x_measurements_last.*,
        schneider_iem3xxx_measurements_last.*
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
        AND {context.GetTableName<NetworkUserInvoiceEntity>()}
          .{context.GetColumnName<NetworkUserInvoiceEntity>(
          nameof(NetworkUserInvoiceEntity.FromDate))} >= @fromDate
        AND {context.GetTableName<NetworkUserInvoiceEntity>()}
          .{context.GetColumnName<NetworkUserInvoiceEntity>(
            nameof(NetworkUserInvoiceEntity.FromDate))} < @toDate
      LEFT JOIN {context.GetTableName<AbbB2xAggregateEntity>()}
        AS abb_b2x_aggregates_monthly
        ON abb_b2x_aggregates_monthly
          .{context.GetForeignKeyColumnName<AbbB2xAggregateEntity>(
            nameof(AbbB2xAggregateEntity.MeasurementLocation))}
          = initial_measurement_locations.measurement_location_id
        AND abb_b2x_aggregates_monthly
          .{context.GetColumnName<AbbB2xAggregateEntity>(
            nameof(AbbB2xAggregateEntity.Timestamp))} >= @fromDate
        AND abb_b2x_aggregates_monthly
          .{context.GetColumnName<AbbB2xAggregateEntity>(
            nameof(AbbB2xAggregateEntity.Timestamp))} < @toDate
        AND abb_b2x_aggregates_monthly
          .{context.GetColumnName<AbbB2xAggregateEntity>(
            nameof(AbbB2xAggregateEntity.Interval))}
          = '{StringExtensions.ToSnakeCase(nameof(IntervalEntity.Month))}'
      LEFT JOIN {context.GetTableName<SchneideriEM3xxxAggregateEntity>()}
        AS schneider_iem3xxx_aggregates_monthly
        ON schneider_iem3xxx_aggregates_monthly
          .{context.GetForeignKeyColumnName<SchneideriEM3xxxAggregateEntity>(
            nameof(SchneideriEM3xxxAggregateEntity.MeasurementLocation))}
          = initial_measurement_locations.measurement_location_id
        AND schneider_iem3xxx_aggregates_monthly
          .{context.GetColumnName<SchneideriEM3xxxAggregateEntity>(
            nameof(SchneideriEM3xxxAggregateEntity.Timestamp))} >= @fromDate
        AND schneider_iem3xxx_aggregates_monthly
          .{context.GetColumnName<SchneideriEM3xxxAggregateEntity>(
            nameof(SchneideriEM3xxxAggregateEntity.Timestamp))} < @toDate
        AND schneider_iem3xxx_aggregates_monthly
          .{context.GetColumnName<SchneideriEM3xxxAggregateEntity>(
            nameof(SchneideriEM3xxxAggregateEntity.Interval))}
          = '{StringExtensions.ToSnakeCase(nameof(IntervalEntity.Month))}'
      LEFT JOIN LATERAL (
        {MakeAbbB2xMeasurementsLastQuery(
          context,
          measurementLocationIdExpression:
            "initial_measurement_locations.measurement_location_id",
          fromDateExpression: "@fromDate",
          toDateExpression: "@toDate"
        )}
      ) as abb_b2x_measurements_last ON TRUE
      LEFT JOIN LATERAL (
        {MakeSchneideriEM3xxxMeasurementsLastQuery(
          context,
          measurementLocationIdExpression:
            "initial_measurement_locations.measurement_location_id",
          fromDateExpression: "@fromDate",
          toDateExpression: "@toDate"
        )}
      ) AS schneider_iem3xxx_measurements_last ON TRUE
    ";

    var parameters = locationId is null ? (object)new
    {
      representativeId,
      fromDate,
      toDate
    } : new
    {
      representativeId,
      locationId = long.Parse(locationId),
      fromDate,
      toDate
    };
    var result = await context
      .DapperCommand<DetailedMeasurementLocationsByRepresentativeIntermediary>(
        sql,
        cancellationToken,
        parameters);
    return result.ToList();
  }

  private static string MakeInitialMeasurementLocationsLocationNetworkUserQuery(
    DbContext context,
    string representativeIdExpression,
    string? locationIdExpression
  )
  {
    var locationWhereClause = locationIdExpression is null
      ? ""
      : $@"
          AND {context.GetTableName<LocationEntity>()}
            .{context.GetPrimaryKeyColumnName<LocationEntity>()}
            = {locationIdExpression}
        ";

    return $@"
      SELECT
        {context.GetTableName<NetworkUserMeasurementLocationEntity>()}
          .{context.GetPrimaryKeyColumnName<
            NetworkUserMeasurementLocationEntity>()}
          AS measurement_location_id,
        {context.GetTableName<RepresentativeEntity>()}.*,
        {context.GetTableName<LocationEntity>()}.*,
        {context.GetTableName<NetworkUserEntity>()}.*,
        {context.GetTableName<NetworkUserMeasurementLocationEntity>()}.*,
        {context.GetTableName<MeterEntity>()}.*
      FROM
        {context.GetTableName<NetworkUserRepresentativeEntity>()}
      INNER JOIN {context.GetTableName<RepresentativeEntity>()}
        ON {context.GetTableName<NetworkUserRepresentativeEntity>()}.
          {context.GetForeignKeyColumnName<NetworkUserRepresentativeEntity>(
            nameof(NetworkUserRepresentativeEntity.Representative))}
          = {context.GetTableName<RepresentativeEntity>()}
            .{context.GetPrimaryKeyColumnName<RepresentativeEntity>()}
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
        {locationWhereClause}
      UNION
      SELECT
        {context.GetTableName<NetworkUserMeasurementLocationEntity>()}
          .{context.GetPrimaryKeyColumnName<
            NetworkUserMeasurementLocationEntity>()}
          AS measurement_location_id,
        {context.GetTableName<RepresentativeEntity>()}.*,
        {context.GetTableName<LocationEntity>()}.*,
        {context.GetTableName<NetworkUserEntity>()}.*,
        {context.GetTableName<NetworkUserMeasurementLocationEntity>()}.*,
        {context.GetTableName<MeterEntity>()}.*
      FROM
        {context.GetTableName<LocationRepresentativeEntity>()}
      INNER JOIN {context.GetTableName<RepresentativeEntity>()}
        ON {context.GetTableName<LocationRepresentativeEntity>()}
          .{context.GetForeignKeyColumnName<LocationRepresentativeEntity>(
            nameof(LocationRepresentativeEntity.Representative))}
          = {context.GetTableName<RepresentativeEntity>()}
            .{context.GetPrimaryKeyColumnName<RepresentativeEntity>()}
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
        {locationWhereClause}
    ";
  }

  private static string MakeInitialMeasurementLocationsOperatorQuery(
    DbContext context,
    string? locationIdExpression
  )
  {
    var locationWhereClause = locationIdExpression is null
      ? ""
      : $@"
          WHERE {context.GetTableName<LocationEntity>()}
            .{context.GetPrimaryKeyColumnName<LocationEntity>()}
            = {locationIdExpression}
        ";

    return $@"
      SELECT
        {context.GetTableName<NetworkUserMeasurementLocationEntity>()}
          .{context.GetPrimaryKeyColumnName<
            NetworkUserMeasurementLocationEntity>()}
          AS measurement_location_id,
        {context.GetTableName<RepresentativeEntity>()}.*,
        {context.GetTableName<LocationEntity>()}.*,
        {context.GetTableName<NetworkUserEntity>()}.*,
        {context.GetTableName<NetworkUserMeasurementLocationEntity>()}.*,
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
      INNER JOIN {context.GetTableName<NetworkUserRepresentativeEntity>()}
        ON {context.GetTableName<NetworkUserMeasurementLocationEntity>()}
          .{context.GetForeignKeyColumnName<
            NetworkUserMeasurementLocationEntity>(
            nameof(NetworkUserMeasurementLocationEntity.NetworkUser))}
          = {context.GetTableName<NetworkUserRepresentativeEntity>()}
            .{context.GetForeignKeyColumnName<
              NetworkUserRepresentativeEntity>(
              nameof(NetworkUserRepresentativeEntity.NetworkUser))}
      INNER JOIN {context.GetTableName<RepresentativeEntity>()}
        ON {context.GetTableName<NetworkUserRepresentativeEntity>()}
          .{context.GetForeignKeyColumnName<NetworkUserRepresentativeEntity>(
            nameof(NetworkUserRepresentativeEntity.Representative))}
          = {context.GetTableName<RepresentativeEntity>()}
            .{context.GetPrimaryKeyColumnName<RepresentativeEntity>()}
      {locationWhereClause}
    ";
  }

  private static string MakeAbbB2xMeasurementsLastQuery(
    DbContext context,
    string measurementLocationIdExpression,
    string fromDateExpression,
    string toDateExpression
  ) => $@"
    SELECT *
    FROM {context.GetTableName<AbbB2xMeasurementEntity>()}
    WHERE
      {context.GetTableName<AbbB2xMeasurementEntity>()}
        .{context.GetForeignKeyColumnName<AbbB2xMeasurementEntity>(
          nameof(AbbB2xMeasurementEntity.MeasurementLocation))}
        = {measurementLocationIdExpression}
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
    string measurementLocationIdExpression,
    string fromDateExpression,
    string toDateExpression
  ) => $@"
    SELECT *
    FROM {context.GetTableName<SchneideriEM3xxxMeasurementEntity>()}
    WHERE
      {context.GetTableName<SchneideriEM3xxxMeasurementEntity>()}
        .{context.GetForeignKeyColumnName<
          SchneideriEM3xxxMeasurementEntity>(
          nameof(SchneideriEM3xxxMeasurementEntity.MeasurementLocation))}
        = {measurementLocationIdExpression}
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

  private static List<AnalysisBasisEntity>
    MakeAnalysisBases(
      IEnumerable<DetailedMeasurementLocationsByRepresentativeIntermediary> items,
      DateTimeOffset fromDate,
      DateTimeOffset toDate
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
              .Where(x => x.MeterId is not null)
              .DistinctBy(x => (x.MeterId, x.Interval, x.Timestamp))
              .ToList();

          var calculations = x
            .Select(x => x.NetworkUserCalculation)
            .OfType<CalculationEntity>()
            .DistinctBy(x => x.Id)
            .ToList();

          var invoices = x
            .Select(x => x.NetworkUserInvoice)
            .OfType<InvoiceEntity>()
            .DistinctBy(x => x.Id)
            .ToList();

          return new AnalysisBasisEntity(
            first.Representative,
            fromDate,
            toDate,
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

  [DapperResult]
  private sealed class DetailedMeasurementLocationsByRepresentativeIntermediary
  {
    public RepresentativeEntity Representative
    {
      get;
      init;
    } = default!;

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
  }
}

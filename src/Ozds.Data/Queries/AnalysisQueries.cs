using System.Data;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Ozds.Data.Attributes;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Abstractions;
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
        ),
        calculations_and_invoices AS (
          {MakeCalculationsAndInvoicesQuery(
            context,
            "initial_measurement_locations.measurement_location_id",
            "@fromDate",
            "@toDate"
          )}
        ),
        last_measurements AS (
          SELECT
            abb_b2x_last_measurement.*,
            schneider_iem3xxx_last_measurement.*
          FROM
            initial_measurement_locations
          LEFT JOIN LATERAL (
            {MakeLastMeasurementQuery<AbbB2xMeasurementEntity>(
              context,
              "initial_measurement_locations.measurement_location_id",
              "@fromDate",
              "@toDate"
            )}
          ) as abb_b2x_last_measurement ON TRUE
          LEFT JOIN LATERAL (
            {MakeLastMeasurementQuery<SchneideriEM3xxxMeasurementEntity>(
              context,
              "initial_measurement_locations.measurement_location_id",
              "@fromDate",
              "@toDate"
            )}
          ) AS schneider_iem3xxx_last_measurement ON TRUE
        ),
        monthly_aggregates AS (
          SELECT
            abb_b2x_monthly_aggregates.*,
            schneider_iem3xxx_monthly_aggregates.*
          FROM
            initial_measurement_locations
          LEFT JOIN (
            {MakeMonthlyAggregatesQuery<AbbB2xAggregateEntity>(
              context,
              "initial_measurement_locations.measurement_location_id",
              "@fromDate",
              "@toDate"
            )}
          ) AS abb_b2x_monthly_aggregates
          LEFT JOIN (
            {MakeMonthlyAggregatesQuery<SchneideriEM3xxxAggregateEntity>(
              context,
              "initial_measurement_locations.measurement_location_id",
              "@fromDate",
              "@toDate"
            )}
          ) as schneider_iem3xxx_monthly_aggregates
        )

      SELECT
        initial_measurement_locations.*,
        calculations_and_invoices.*,
        last_measurements.*,
        monthly_aggregates.*
      FROM
        initial_measurement_locations
      LEFT JOIN calculations_and_invoices
        ON initial_measurement_locations.id
          = calculations_and_invoices.measurement_location_id
      LEFT JOIN last_measurements
        ON initial_measurement_locations.id
          = last_measurements.measurement_location_id
      LEFT JOIN monthly_aggregates
        ON initial_measurement_locations.id
          = monthly_aggregates.measurement_location_id
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

  private static string MakeCalculationsAndInvoicesQuery(
    DbContext context,
    string measurementLocationIdExpression,
    string fromDateExpression,
    string toDateExpression
  ) => $@"
    SELECT
      {context.GetTableName<NetworkUserCalculationEntity>()}.*,
      {context.GetTableName<NetworkUserInvoiceEntity>()}.*
    FROM
      {context.GetTableName<NetworkUserMeasurementLocationEntity>()}
    INNER JOIN {context.GetTableName<NetworkUserCalculationEntity>()}
      ON {context.GetTableName<NetworkUserCalculationEntity>()}
        .{context.GetForeignKeyColumnName<NetworkUserCalculationEntity>(
          nameof(NetworkUserCalculationEntity.NetworkUserMeasurementLocation))}
        = {measurementLocationIdExpression}
        AND {context.GetTableName<NetworkUserCalculationEntity>()}
          .{context.GetColumnName<NetworkUserCalculationEntity>(
          nameof(NetworkUserCalculationEntity.FromDate))}
          >= {fromDateExpression}
        AND {context.GetTableName<NetworkUserCalculationEntity>()}
          .{context.GetColumnName<NetworkUserCalculationEntity>(
            nameof(NetworkUserCalculationEntity.FromDate))}
          < {toDateExpression}
    LEFT JOIN {context.GetTableName<NetworkUserInvoiceEntity>()}
      ON {context.GetTableName<NetworkUserCalculationEntity>()}
        .{context.GetForeignKeyColumnName<NetworkUserCalculationEntity>(
          nameof(NetworkUserCalculationEntity.NetworkUserInvoice))}
        = {context.GetTableName<NetworkUserInvoiceEntity>()}
          .{context.GetPrimaryKeyColumnName<NetworkUserInvoiceEntity>()}
      AND {context.GetTableName<NetworkUserInvoiceEntity>()}
        .{context.GetColumnName<NetworkUserInvoiceEntity>(
          nameof(NetworkUserInvoiceEntity.FromDate))}
        >= {fromDateExpression}
      AND {context.GetTableName<NetworkUserInvoiceEntity>()}
        .{context.GetColumnName<NetworkUserInvoiceEntity>(
          nameof(NetworkUserInvoiceEntity.FromDate))}
        < {toDateExpression}
  ";

  private static string MakeLastMeasurementQuery<T>(
    DbContext context,
    string measurementLocationIdExpression,
    string fromDateExpression,
    string toDateExpression
  )
  where T : IMeasurementEntity
  => $@"
    SELECT *
    FROM {context.GetTableName<T>()}
    WHERE
      {context.GetTableName<T>()}
        .{context.GetForeignKeyColumnName<T>(
          nameof(MeasurementEntity.MeasurementLocation))}
        = {measurementLocationIdExpression}
      AND {context.GetTableName<T>()}
        .{context.GetColumnName<T>(
          nameof(MeasurementEntity.Timestamp))} >= {fromDateExpression}
      AND {context.GetTableName<T>()}
        .{context.GetColumnName<T>(
          nameof(MeasurementEntity.Timestamp))} < {toDateExpression}
    ORDER BY {context.GetTableName<T>()}
      .{context.GetColumnName<T>(
        nameof(MeasurementEntity.Timestamp))} DESC
    LIMIT 1
  ";

  private static string MakeMonthlyAggregatesQuery<T>(
    DbContext context,
    string measurementLocationIdExpression,
    string fromDateExpression,
    string toDateExpression
  )
  where T : IAggregateEntity
  => $@"
    SELECT *
    FROM {context.GetTableName<T>()}
    WHERE {context.GetTableName<AbbB2xAggregateEntity>()}
      ON {context.GetTableName<AbbB2xAggregateEntity>()}
        .{context.GetForeignKeyColumnName<AbbB2xAggregateEntity>(
          nameof(AbbB2xAggregateEntity.MeasurementLocation))}
        = {measurementLocationIdExpression}
      AND {context.GetTableName<AbbB2xAggregateEntity>()}
        .{context.GetColumnName<AbbB2xAggregateEntity>(
          nameof(AbbB2xAggregateEntity.Timestamp))}
        >= {fromDateExpression}
      AND {context.GetTableName<AbbB2xAggregateEntity>()}
        .{context.GetColumnName<AbbB2xAggregateEntity>(
          nameof(AbbB2xAggregateEntity.Timestamp))}
        < {toDateExpression}
      AND {context.GetTableName<AbbB2xAggregateEntity>()}
        .{context.GetColumnName<AbbB2xAggregateEntity>(
          nameof(AbbB2xAggregateEntity.Interval))}
        = '{StringExtensions.ToSnakeCase(nameof(IntervalEntity.Month))}'
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

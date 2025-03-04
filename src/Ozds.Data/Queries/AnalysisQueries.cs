using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
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
            "initial_measurement_locations",
            "measurement_location_id",
            "@fromDate",
            "@toDate"
          )}
        ),
        abb_b2x_last_measurements AS (
          SELECT abb_b2x_last_measurements.*
          FROM initial_measurement_locations
          LEFT JOIN LATERAL (
            {MakeLastMeasurementQuery<AbbB2xMeasurementEntity>(
              context,
              "initial_measurement_locations.measurement_location_id",
              "@fromDate",
              "@toDate"
            )}
          ) as abb_b2x_last_measurements ON TRUE
        ),
        schneider_iem3xxx_last_measurements AS (
          SELECT schneider_iem3xxx_last_measurements.*
          FROM initial_measurement_locations
          LEFT JOIN LATERAL (
            {MakeLastMeasurementQuery<SchneideriEM3xxxMeasurementEntity>(
              context,
              "initial_measurement_locations.measurement_location_id",
              "@fromDate",
              "@toDate"
            )}
          ) AS schneider_iem3xxx_last_measurements ON TRUE
        ),
        abb_b2x_monthly_aggregates AS (
          SELECT {context.GetTableName<AbbB2xAggregateEntity>()}.*
          FROM initial_measurement_locations
          LEFT JOIN
          {MakeMonthlyAggregatesJoin<AbbB2xAggregateEntity>(
            context,
            "initial_measurement_locations.measurement_location_id",
            "@fromDate",
            "@toDate"
          )}
        ),
        schneider_iem3xxx_monthly_aggregates AS (
          SELECT {context.GetTableName<SchneideriEM3xxxAggregateEntity>()}.*
          FROM initial_measurement_locations
          LEFT JOIN
          {MakeMonthlyAggregatesJoin<SchneideriEM3xxxAggregateEntity>(
            context,
            "initial_measurement_locations.measurement_location_id",
            "@fromDate",
            "@toDate"
          )}
        )

      SELECT
        initial_measurement_locations.*,
        calculations_and_invoices.*,
        abb_b2x_last_measurements.*,
        schneider_iem3xxx_last_measurements.*,
        abb_b2x_monthly_aggregates.*,
        schneider_iem3xxx_monthly_aggregates.*
      FROM
        initial_measurement_locations
      LEFT JOIN calculations_and_invoices
        ON initial_measurement_locations.measurement_location_id
          = calculations_and_invoices.measurement_location_id
      LEFT JOIN abb_b2x_last_measurements
        ON initial_measurement_locations.measurement_location_id
          = abb_b2x_last_measurements.measurement_location_id
      LEFT JOIN schneider_iem3xxx_last_measurements
        ON initial_measurement_locations.measurement_location_id
          = schneider_iem3xxx_last_measurements.measurement_location_id
      LEFT JOIN abb_b2x_monthly_aggregates
        ON initial_measurement_locations.measurement_location_id
          = abb_b2x_monthly_aggregates.measurement_location_id
      LEFT JOIN schneider_iem3xxx_monthly_aggregates
        ON initial_measurement_locations.measurement_location_id
          = schneider_iem3xxx_monthly_aggregates.measurement_location_id
    ";

    var parameters = locationId is null
      ? (object)new
      {
        representativeId,
        fromDate,
        toDate
      }
      : new
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
      LEFT JOIN {context.GetTableName<NetworkUserRepresentativeEntity>()}
        ON {context.GetTableName<NetworkUserMeasurementLocationEntity>()}
          .{context.GetForeignKeyColumnName<
            NetworkUserMeasurementLocationEntity>(
            nameof(NetworkUserMeasurementLocationEntity.NetworkUser))}
          = {context.GetTableName<NetworkUserRepresentativeEntity>()}
            .{context.GetForeignKeyColumnName<
              NetworkUserRepresentativeEntity>(
              nameof(NetworkUserRepresentativeEntity.NetworkUser))}
      LEFT JOIN {context.GetTableName<RepresentativeEntity>()}
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
    string measurementLocationsExpression,
    string measurementLocationIdProperty,
    string fromDateExpression,
    string toDateExpression
  )
  {
    return $@"
    SELECT
      {measurementLocationsExpression}.{measurementLocationIdProperty},
      {context.GetTableName<NetworkUserCalculationEntity>()}.*,
      {context.GetTableName<NetworkUserInvoiceEntity>()}.*
    FROM
      {measurementLocationsExpression}
    INNER JOIN {context.GetTableName<NetworkUserCalculationEntity>()}
      ON {context.GetTableName<NetworkUserCalculationEntity>()}
        .{context.GetForeignKeyColumnName<NetworkUserCalculationEntity>(
          nameof(NetworkUserCalculationEntity.NetworkUserMeasurementLocation))}
        = {measurementLocationsExpression}.{measurementLocationIdProperty}
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
  }

  private static string MakeLastMeasurementQuery<T>(
    DbContext context,
    string measurementLocationIdExpression,
    string fromDateExpression,
    string toDateExpression
  )
    where T : IMeasurementEntity
  {
    return $@"
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
  }

  private static string MakeMonthlyAggregatesJoin<T>(
    DbContext context,
    string measurementLocationIdExpression,
    string fromDateExpression,
    string toDateExpression
  )
    where T : IAggregateEntity
  {
    return $@"
    {context.GetTableName<T>()}
    ON {context.GetTableName<T>()}
      .{context.GetForeignKeyColumnName<T>(
        nameof(AggregateEntity.MeasurementLocation))}
      = {measurementLocationIdExpression}
    AND {context.GetTableName<T>()}
      .{context.GetColumnName<T>(
        nameof(AggregateEntity.Timestamp))}
      >= {fromDateExpression}
    AND {context.GetTableName<T>()}
      .{context.GetColumnName<T>(
        nameof(AggregateEntity.Timestamp))}
      < {toDateExpression}
    AND {context.GetTableName<T>()}
      .{context.GetColumnName<T>(
        nameof(AggregateEntity.Interval))}
      = '{StringExtensions.ToSnakeCase(nameof(IntervalEntity.Month))}'
  ";
  }

  private static List<AnalysisBasisEntity>
    MakeAnalysisBases(
      IEnumerable<DetailedMeasurementLocationsByRepresentativeIntermediary>
        items,
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
              .Concat(
                x
                  .Select(x => x.AbbLastMeasurement)
                  .OfType<MeasurementEntity>())
              .Concat(
                x
                  .Select(x => x.SchneiderLastMeasurement)
                  .OfType<MeasurementEntity>())
              .FirstOrDefault();
          if (lastMeasurement is null)
          {
            return null;
          }

          var monthlyAggregates =
            Enumerable.Empty<AggregateEntity>()
              .Concat(
                x
                  .Select(x => x.AbbMonthlyAggregate)
                  .OfType<AggregateEntity>())
              .Concat(
                x
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

          return new AnalysisBasisEntity
          {
            Representative = first.Representative,
            FromDate = fromDate,
            ToDate = toDate,
            Location = first.Location,
            NetworkUser = first.NetworkUser,
            MeasurementLocation = first.MeasurementLocation,
            Meter = first.Meter,
            Calculations = calculations,
            Invoices = invoices,
            LastMeasurement = lastMeasurement,
            MonthlyAggregates = monthlyAggregates
          };
        })
      .OfType<AnalysisBasisEntity>()
      .ToList();
  }

#pragma warning disable S3459 // Unassigned members should be removed
#pragma warning disable S1144 // Unused private types or members should be removed
  [DapperResult]
  [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
  private sealed class DetailedMeasurementLocationsByRepresentativeIntermediary
  {
    public RepresentativeEntity? Representative { get; set; } = default!;

    public LocationEntity Location { get; set; } = default!;

    public NetworkUserEntity NetworkUser { get; set; } = default!;

    public NetworkUserMeasurementLocationEntity MeasurementLocation
    {
      get;
      set;
    } =
      default!;

    public MeterEntity Meter { get; set; } = default!;

    public NetworkUserCalculationEntity? NetworkUserCalculation { get; set; }

    public NetworkUserInvoiceEntity? NetworkUserInvoice { get; set; }

    public AbbB2xMeasurementEntity AbbLastMeasurement { get; set; } = default!;

    public SchneideriEM3xxxMeasurementEntity? SchneiderLastMeasurement
    {
      get;
      set;
    }

    public AbbB2xAggregateEntity? AbbMonthlyAggregate { get; set; }

    public SchneideriEM3xxxAggregateEntity? SchneiderMonthlyAggregate
    {
      get;
      set;
    }
  }
#pragma warning restore S1144 // Unused private types or members should be removed
#pragma warning restore S3459 // Unassigned members should be removed
}

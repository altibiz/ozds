using System.Collections.Concurrent;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Npgsql;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;
using Ozds.Data.Mutations.Abstractions;

namespace Ozds.Data.Mutations;

// TODO: enum by model configuration not clr type
// TODO: determining clauses by column name is flaky - needs to be improved
// TODO: handle nesting hierarchical properties

public class MeasurementUpsertMutations(
  IDbContextFactory<DataDbContext> factory
) : IMutations
{
  private const int ChunkSize = 10;

  public async Task<List<IMeasurementEntity>> UpsertMeasurements(
    IEnumerable<IMeasurementEntity> measurements,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    return await UpsertMeasurements(
      context,
      measurements,
      cancellationToken
    );
  }

  internal async Task<List<IMeasurementEntity>> UpsertMeasurements(
    DataDbContext context,
    IEnumerable<IMeasurementEntity> measurements,
    CancellationToken cancellationToken
  )
  {
    if (context.Database.CurrentTransaction is { })
    {
      var results = await ExecuteCommands(
        context,
        measurements,
        cancellationToken
      );

      return results;
    }

    while (true)
    {
      try
      {
        var isolationLevel = IsolationLevel.RepeatableRead;

        await context.Database
          .BeginTransactionAsync(isolationLevel, cancellationToken);

        var results = await ExecuteCommands(
          context,
          measurements,
          cancellationToken
        );

        await context.Database
          .CommitTransactionAsync(cancellationToken);

        return results;
      }
      catch (PostgresException ex)
      {
        if (ex.Message.StartsWith("40001"))
        {
          await context.Database.RollbackTransactionAsync(cancellationToken);
          continue;
        }

        if (ex.Message.StartsWith("40P01"))
        {
          await context.Database.RollbackTransactionAsync(cancellationToken);
          continue;
        }

        if (ex.Message.StartsWith("P0002"))
        {
          await context.Database.RollbackTransactionAsync(cancellationToken);
          continue;
        }

        throw;
      }
    }
  }

  private async Task<List<IMeasurementEntity>> ExecuteCommands(
    DataDbContext context,
    IEnumerable<IMeasurementEntity> measurements,
    CancellationToken cancellationToken
  )
  {
    var grouped = measurements
      .Select((x, i) => (Index: i, Measurement: x))
      .OrderBy(x => x.Measurement is IAggregateEntity aggregate
        ? aggregate.Interval switch
        {
          IntervalEntity.Month => 1,
          IntervalEntity.Day => 2,
          IntervalEntity.QuarterHour => 3,
          _ => throw new InvalidOperationException(
            $"Unknown interval {aggregate.Interval}.")
        }
        : 0)
      .GroupBy(x => x.Measurement.GetType());

    var results = new List<IMeasurementEntity>();
    foreach (var group in grouped)
    {
      var type = group.Key;

      foreach (var chunk in group.Chunk(ChunkSize))
      {
        var parameters = context.CreateBulkParameters(
          chunk.Select(x => (x.Measurement as object, x.Index)));
        var commands = chunk
          .Select(x => CreateCommand(
            context,
            type,
            x.Measurement is IAggregateEntity aggregate
              ? aggregate.Interval
              : null,
            x.Index))
          .ToList();
        var queryClauses = commands
          .Select(x => x.Query)
          .ToList();
        var withClauses = commands
          .Select(x => x.With)
          .Where(x => x is not null)
          .ToList();
        var sql = string.Join("\nUNION ALL\n", queryClauses);
        if (withClauses.Count > 0)
        {
          sql = $@"
            WITH {string.Join(",\n", withClauses)}
            {sql}
          ";
        }

        // NOTE: good for debugging
#pragma warning disable S125 // Sections of code should not be commented out
        // var parameterString = string.Join(
        //   "\n",
        //   parameters.Select(kv =>
        //   {
        //     var key = kv.Key.Replace("@", "");
        //     var val = kv.Value is DateTimeOffset dt
        //       ? $"'{dt.ToString("o", System.Globalization.CultureInfo.InvariantCulture)}'"
        //       : kv.Value is string s
        //         ? $"'{s}'"
        //         : kv.Value;
        //     return $"\\set {key} {val}";
        //   }));
        // Console.WriteLine(parameterString);
        // Console.WriteLine(sql);
#pragma warning restore S125 // Sections of code should not be commented out

        var objects = await context.DapperCommand(
          type,
          sql,
          cancellationToken,
          parameters
        );
        results.AddRange(objects.Select(x => (IMeasurementEntity)x));
      }
    }

    return results;
  }

  private static (string? With, string Query) CreateCommand(
    DataDbContext context,
    Type type,
    IntervalEntity? interval,
    int index
  )
  {
    var template = UpsertTemplateCache.GetOrAdd(
      (type, interval),
      _ => interval is { } nonNullInterval
        ? CreateUpsertAggregateTemplate(
            context, type, nonNullInterval, "{{INDEX}}")
        : CreateUpsertMeasurementTemplate(
            context, type, "{{INDEX}}")
    );
    return (
      template.With?.Replace("{{INDEX}}", index.ToString()),
      template.Query.Replace("{{INDEX}}", index.ToString())
    );
  }

  private static (string? With, string Query) CreateUpsertMeasurementTemplate(
    DataDbContext context,
    Type type,
    string indexSubstitution)
  {
    var entityType = context.Model.FindEntityType(type)
        ?? throw new InvalidOperationException(
          $"No entity type found for {type}.");
    var storeObjectIdentifier = StoreObjectIdentifier
      .Create(entityType, StoreObjectType.Table)
      ?? throw new InvalidOperationException(
        $"No store object identifier found for {type}.");
    var tableName = entityType.GetTableName()
        ?? throw new InvalidOperationException(
          $"No table name found for {type}.");
    var properties = entityType.GetProperties()
      .Concat(entityType
        .GetComplexProperties()
        .SelectMany(p => p.ComplexType.GetProperties()))
      .ToList();

    var columnNames = properties
      .Select(p => p.GetColumnName(storeObjectIdentifier))
      .ToList();
    var primaryKey = entityType.FindPrimaryKey()
      ?? throw new InvalidOperationException(
        $"No primary key found for {entityType.Name}.");
    var primaryKeyColumns = primaryKey.Properties
      .Select(p => new
      {
        Property = p,
        ColumnName = p.GetColumnName(storeObjectIdentifier)
      })
      .ToList();

    var columns = string.Join(", ", columnNames);
    var values = string.Join(", ", properties
      .Select(property =>
      {
        var columnName = property.GetColumnName(storeObjectIdentifier);
        return $"@p{indexSubstitution}_{columnName}"
        + (property.ClrType.IsEnum
          ? $"::{property.ClrType.Name.ToSnakeCase()}"
          : "");
      }));
    var conflict = string.Join(
      ", ",
      primaryKeyColumns.Select(c => c.ColumnName));

    var updateClause = "DO NOTHING";

    return (
      $@"
        value{indexSubstitution} AS (
          INSERT INTO {tableName} ({columns})
          VALUES ({values})
          ON CONFLICT ({conflict}) {updateClause}
          RETURNING {tableName}.*
        )
      ",
      $@"
        SELECT * FROM value{indexSubstitution}
      "
    );
  }

  private static (string? With, string Query) CreateUpsertAggregateTemplate(
    DataDbContext context,
    Type type,
    IntervalEntity interval,
    string indexSubstitution)
  {
    var entityType = context.Model.FindEntityType(type)
        ?? throw new InvalidOperationException(
          $"No entity type found for {type}.");
    var storeObjectIdentifier = StoreObjectIdentifier
      .Create(entityType, StoreObjectType.Table)
      ?? throw new InvalidOperationException(
        $"No store object identifier found for {type}.");
    var tableName = entityType.GetTableName()
        ?? throw new InvalidOperationException(
          $"No table name found for {type}.");
    var properties = entityType.GetProperties()
      .Concat(entityType
        .GetComplexProperties()
        .SelectMany(p => p.ComplexType.GetProperties()))
      .ToList();

    var columnNames = properties
      .Select(p => p.GetColumnName(storeObjectIdentifier))
      .ToList();
    var primaryKey = entityType.FindPrimaryKey()
      ?? throw new InvalidOperationException(
        $"No primary key found for {entityType.Name}.");
    var primaryKeyColumns = primaryKey.Properties
      .Select(p => new
      {
        Property = p,
        ColumnName = p.GetColumnName(storeObjectIdentifier)
      })
      .ToList();

    var columns = string.Join(", ", columnNames);
    var values = string.Join(", ", properties
      .Select(property =>
      {
        var columnName = property.GetColumnName(storeObjectIdentifier);
        return $"@p{indexSubstitution}_{columnName}"
        + (property.ClrType.IsEnum
          ? $"::{property.ClrType.Name.ToSnakeCase()}"
          : "");
      }));
    var conflict = string.Join(
      ", ",
      primaryKeyColumns.Select(c => c.ColumnName)
    );

    var updateClause = string.Empty;
    var setClauses = new List<string>();
    var deltaClauses = new List<string>();
    var deltaUpsertClauses = new List<string>();

    var countColumn = context
      .GetColumnName(type,
      nameof(IAggregateEntity.Count));
    var quarterHourCountColumn = context
      .GetColumnName(type,
      nameof(IAggregateEntity.QuarterHourCount));
    var timestampColumn = context
      .GetColumnName(type,
      nameof(IAggregateEntity.Timestamp));
    var intervalColumn = context
      .GetColumnName(type,
      nameof(IAggregateEntity.Interval));
    setClauses.Add(
      $"{countColumn} = {tableName}.{countColumn} "
        + $"+ EXCLUDED.{countColumn}");

    foreach (var prop in properties)
    {
      var col = prop.GetColumnName(storeObjectIdentifier);
      if (col is null || col == countColumn)
      {
        continue;
      }
      if (col == quarterHourCountColumn
        && interval == IntervalEntity.QuarterHour)
      {
        setClauses.Add($"{quarterHourCountColumn} = 1");

        deltaUpsertClauses.Add(
          $"{quarterHourCountColumn} = {tableName}.{quarterHourCountColumn}"
          + $" + delta{indexSubstitution}.new_count"
        );
      }
      if (col.Contains("derived")
        && !col.Contains("timestamp")
        && interval == IntervalEntity.QuarterHour)
      {
        var energyCol = col
          .Replace("derived_", "")
          .Replace("power", "energy");
        if (energyCol.EndsWith("_w"))
        {
          energyCol =
            string.Join("_", energyCol.Split("_").SkipLast(1))
            + "_wh";
        }
        else if (energyCol.EndsWith("_var"))
        {
          energyCol =
            string.Join("_", energyCol.Split("_").SkipLast(1))
            + "_varh";
        }
        else if (energyCol.EndsWith("_va"))
        {
          energyCol =
            string.Join("_", energyCol.Split("_").SkipLast(1))
            + "_vah";
        }
        var maxCol = energyCol
          .Replace("min", "max")
          .Replace("avg", "max");
        var minCol = energyCol
          .Replace("max", "min")
          .Replace("avg", "min");
        setClauses.Add(
          $"{col} ="
          + $" (GREATEST({tableName}.{maxCol}, EXCLUDED.{maxCol})"
          + $" - LEAST({tableName}.{minCol}, EXCLUDED.{minCol}))"
          + $" * 4");

        if (col.Contains("avg"))
        {
          deltaClauses.Add(
            $"new{indexSubstitution}.{col}"
            + $" - COALESCE(old{indexSubstitution}.{col}, 0) {col}"
          );
          deltaUpsertClauses.Add(
            $"{col} = ({tableName}.{col}"
            + $" * {tableName}.{quarterHourCountColumn}"
            + $" + delta{indexSubstitution}.{col})"
            + $" / ({tableName}.{quarterHourCountColumn}"
            + $" + delta{indexSubstitution}.new_count)"
          );
        }
        else if (col.Contains("min"))
        {
          deltaClauses.Add(
            $"LEAST(new{indexSubstitution}.{col},"
            + $" COALESCE(old{indexSubstitution}.{col},"
            + $" new{indexSubstitution}.{col})) {col}"
          );
          deltaUpsertClauses.Add(
            $"{col} = LEAST(delta{indexSubstitution}.{col},"
            + $" {tableName}.{col})"
          );

          if (!col.Contains("energy"))
          {
            var timestampCol =
              string.Join("_", col.Split("_").SkipLast(1))
              + "_timestamp";
            deltaClauses.Add(
                $"CASE"
                + $" WHEN new{indexSubstitution}.{col}"
                + $" < COALESCE(old{indexSubstitution}.{col},"
                + $" new{indexSubstitution}.{col})"
                + $" THEN new{indexSubstitution}.{timestampCol}"
                + $" ELSE COALESCE(old{indexSubstitution}.{timestampCol},"
                + $" new{indexSubstitution}.{timestampCol})"
                + $" END {timestampCol}"
            );
            deltaUpsertClauses.Add(
                $"{timestampCol} = CASE"
                + $" WHEN delta{indexSubstitution}.{col}"
                + $" < {tableName}.{col}"
                + $" THEN delta{indexSubstitution}.{timestampCol}"
                + $" ELSE {tableName}.{timestampCol}"
                + $" END"
            );
          }
        }
        else if (col.Contains("max"))
        {
          deltaClauses.Add(
            $"GREATEST(new{indexSubstitution}.{col},"
            + $" COALESCE(old{indexSubstitution}.{col},"
            + $" new{indexSubstitution}.{col})) {col}"
          );
          deltaUpsertClauses.Add(
            $"{col} = GREATEST(delta{indexSubstitution}.{col},"
            + $" {tableName}.{col})"
          );

          if (!col.Contains("energy"))
          {
            var timestampCol =
              string.Join("_", col.Split("_").SkipLast(1))
              + "_timestamp";
            deltaClauses.Add(
                $"CASE"
                + $" WHEN new{indexSubstitution}.{col}"
                + $" > COALESCE(old{indexSubstitution}.{col},"
                + $" new{indexSubstitution}.{col})"
                + $" THEN new{indexSubstitution}.{timestampCol}"
                + $" ELSE COALESCE(old{indexSubstitution}.{timestampCol},"
                + $" new{indexSubstitution}.{timestampCol})"
                + $" END {timestampCol}"
            );
            deltaUpsertClauses.Add(
                $"{timestampCol} = CASE"
                + $" WHEN delta{indexSubstitution}.{col}"
                + $" > {tableName}.{col}"
                + $" THEN delta{indexSubstitution}.{timestampCol} "
                + $" ELSE {tableName}.{timestampCol}"
                + " END"
            );
          }
        }
      }
      else if (col.Contains("avg"))
      {
        setClauses.Add(
            $"{col} = ({tableName}.{col} * {tableName}.{countColumn}"
            + $" + EXCLUDED.{col} * EXCLUDED.{countColumn})"
            + $" / ({tableName}.{countColumn} + EXCLUDED.{countColumn})");
      }
      else if (col.Contains("min") && !col.Contains("timestamp"))
      {
        setClauses.Add(
          $"{col} = LEAST({tableName}.{col}, EXCLUDED.{col})");

        if (!col.Contains("energy"))
        {
          var timestampCol =
            string.Join("_", col.Split("_").SkipLast(1))
            + "_timestamp";
          setClauses.Add(
              $"{timestampCol} = CASE"
              + $" WHEN EXCLUDED.{col} < {tableName}.{col}"
              + $" THEN EXCLUDED.{timestampCol}"
              + $" ELSE {tableName}.{timestampCol} END");
        }
      }
      else if (col.Contains("max") && !col.Contains("timestamp"))
      {
        setClauses.Add(
          $"{col} = GREATEST({tableName}.{col}, EXCLUDED.{col})");

        if (!col.Contains("energy"))
        {
          var timestampCol =
            string.Join("_", col.Split("_").SkipLast(1))
            + "_timestamp";
          setClauses.Add(
              $"{timestampCol} = CASE"
              + $" WHEN EXCLUDED.{col} > {tableName}.{col}"
              + $" THEN EXCLUDED.{timestampCol}"
              + $" ELSE {tableName}.{timestampCol} END");
        }
      }
    }

    updateClause =
      "DO UPDATE SET "
      + string.Join(", ", setClauses);

    if (interval != IntervalEntity.QuarterHour)
    {
      return (
        $@"
          value{indexSubstitution} AS (
            INSERT INTO {tableName} ({columns})
            VALUES ({values})
            ON CONFLICT ({conflict}) {updateClause}
            RETURNING {tableName}.*
          )
        ",
        $@"
          SELECT * FROM value{indexSubstitution}
        "
      );
    }

    var dayIntervalValue = IntervalEntity.Day.ToString().ToSnakeCase();
    var monthIntervalValue = IntervalEntity.Month.ToString().ToSnakeCase();
    var intervalTypeName = nameof(IntervalEntity).ToSnakeCase();

    return (
      $@"
        old{indexSubstitution} AS (
          SELECT {tableName}.*
          FROM {tableName}
          WHERE {string.Join(
            " AND ",
            primaryKeyColumns.Select(c =>
              $"{tableName}.{c.ColumnName}"
                + $" = @p{indexSubstitution}_{c.ColumnName}"
                + (c.Property.ClrType.IsEnum
                  ? $"::{c.Property.ClrType.Name.ToSnakeCase()}"
                  : "")))}
        ), new{indexSubstitution} AS (
          INSERT INTO {tableName} ({columns})
          VALUES ({values})
          ON CONFLICT ({conflict}) {updateClause}
          RETURNING {tableName}.*
        ), delta{indexSubstitution} AS (
          SELECT
            date_trunc(
              'day',
              new{indexSubstitution}.{timestampColumn}
                AT TIME ZONE 'Europe/Zagreb')
              AT TIME ZONE 'Europe/Zagreb'
              daily_timestamp,
            date_trunc(
              'month',
              new{indexSubstitution}.{timestampColumn}
                AT TIME ZONE 'Europe/Zagreb')
              AT TIME ZONE 'Europe/Zagreb'
              monthly_timestamp,
              {string.Join(
                ", ",
                primaryKeyColumns.Select(c =>
                  $"new{indexSubstitution}.{c.ColumnName}"))},
            CASE
              WHEN old{indexSubstitution}.{timestampColumn} is null then 1
              ELSE 0
            END new_count,
            {string.Join(", ", deltaClauses)}
          FROM new{indexSubstitution}
          LEFT JOIN old{indexSubstitution} ON
            {string.Join(
              " AND ",
              primaryKeyColumns.Select(
                c => $"old{indexSubstitution}.{c.ColumnName}"
                  + $" = new{indexSubstitution}.{c.ColumnName}"))}
        ), daily{indexSubstitution} AS (
          UPDATE {tableName} SET
            {string.Join(", ", deltaUpsertClauses)}
          FROM delta{indexSubstitution}
          WHERE
            {tableName}.{timestampColumn}
              = delta{indexSubstitution}.daily_timestamp
            AND {tableName}.{intervalColumn}
              = '{dayIntervalValue}'::{intervalTypeName}
            AND {string.Join(
              " AND ",
              primaryKeyColumns
                .Where(c =>
                  c.ColumnName != timestampColumn
                  && c.ColumnName != intervalColumn)
                .Select(c =>
                  $"{tableName}.{c.ColumnName}"
                  + $" = delta{indexSubstitution}.{c.ColumnName}"))}
          RETURNING {tableName}.*
        ), monthly{indexSubstitution} AS (
          UPDATE {tableName} SET
            {string.Join(", ", deltaUpsertClauses)}
          FROM delta{indexSubstitution}
          WHERE
            {tableName}.{timestampColumn}
              = delta{indexSubstitution}.monthly_timestamp
            AND {tableName}.{intervalColumn}
              = '{monthIntervalValue}'::{intervalTypeName}
            AND {string.Join(
              " AND ",
              primaryKeyColumns
                .Where(c =>
                  c.ColumnName != timestampColumn
                  && c.ColumnName != intervalColumn)
                .Select(c =>
                  $"{tableName}.{c.ColumnName}"
                  + $" = delta{indexSubstitution}.{c.ColumnName}"))}
          RETURNING {tableName}.*
        )
      ",
      $@"
        SELECT * FROM new{indexSubstitution}
        UNION ALL
        SELECT * FROM daily{indexSubstitution}
        UNION ALL
        SELECT * FROM monthly{indexSubstitution}
      "
    );
  }

  private static readonly ConcurrentDictionary<
    (Type, IntervalEntity?),
    (string? With, string Query)>
    UpsertTemplateCache = new ConcurrentDictionary<
      (Type, IntervalEntity?),
      (string? With, string Query)>();
}

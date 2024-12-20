using System.Collections.Concurrent;
using System.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
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
  private const int ChunkSize = 100;

  public async Task UpsertMeasurements(
    IEnumerable<IMeasurementEntity> measurements,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);
    await UpsertMeasurements(
      context,
      measurements,
      cancellationToken
    );
  }

  internal async Task UpsertMeasurements(
    DataDbContext context,
    IEnumerable<IMeasurementEntity> measurements,
    CancellationToken cancellationToken
  )
  {
    if (context.Database.CurrentTransaction is { } transaction)
    {
      await ExecuteCommands(
        context,
        measurements,
        transaction,
        cancellationToken
      );

      return;
    }

    while (true)
    {
      try
      {
        var isolationLevel = IsolationLevel.RepeatableRead;

        transaction = await context.Database
          .BeginTransactionAsync(isolationLevel, cancellationToken);

        await ExecuteCommands(
          context,
          measurements,
          transaction,
          cancellationToken
        );

        await context.Database
          .CommitTransactionAsync(cancellationToken);

        break;
      }
      catch (PostgresException ex)
      {
        if (ex.Message.StartsWith("40001"))
        {
          await context.Database.RollbackTransactionAsync();
          continue;
        }

        if (ex.Message.StartsWith("40P01"))
        {
          await context.Database.RollbackTransactionAsync();
          continue;
        }

        if (ex.Message.StartsWith("P0002"))
        {
          await context.Database.RollbackTransactionAsync();
          continue;
        }

        throw;
      }
    }
  }

  private async Task ExecuteCommands(
    DataDbContext context,
    IEnumerable<IMeasurementEntity> measurements,
    IDbContextTransaction transaction,
    CancellationToken cancellationToken
  )
  {
    var connection = context.GetDapperDbConnection();
    var commands = Upsert(context, measurements);

    foreach (var x in commands.Chunk(ChunkSize))
    {
      var parameters = context.CreateBulkParameters(
        x.Select(y => (y.Measurement as object, y.Index)));
      var sql = string.Join("\n", x.Select(y => y.Sql));

      var command = new CommandDefinition(
        sql,
        parameters,
        transaction: transaction.GetDbTransaction(),
        cancellationToken: cancellationToken
      );
      await connection.ExecuteAsync(command);
    }
  }

  private IEnumerable<(string Sql, int Index, IMeasurementEntity Measurement)> Upsert(
      DataDbContext context,
      IEnumerable<IMeasurementEntity> measurements)
  {
    var ordered = measurements
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
        : 0);

    foreach (var (index, measurement) in ordered)
    {
      var sql = Upsert(
        context,
        measurement.GetType(),
        measurement is IAggregateEntity aggregate ? aggregate.Interval : null,
        index);
      yield return (sql, index, measurement);
    }
  }

  private static string Upsert(
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
    return template.Replace("{{INDEX}}", index.ToString());
  }

  private static string CreateUpsertMeasurementTemplate(
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

    return $@"
      INSERT INTO {tableName} ({columns})
      VALUES ({values})
      ON CONFLICT ({conflict}) {updateClause};
    ";
  }

  private static string CreateUpsertAggregateTemplate(
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
          + $" + delta.new_count"
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
            $"new.{col} - COALESCE(old.{col}, 0) {col}"
          );
          deltaUpsertClauses.Add(
            $"{col} = ({tableName}.{col} * {tableName}.{quarterHourCountColumn}"
            + $" + delta.{col})"
            + $" / ({tableName}.{quarterHourCountColumn} + delta.new_count)"
          );
        }
        else if (col.Contains("min"))
        {
          deltaClauses.Add(
            $"LEAST(new.{col}, COALESCE(old.{col}, new.{col})) {col}"
          );
          deltaUpsertClauses.Add(
            $"{col} = LEAST(delta.{col}, {tableName}.{col})"
          );

          if (!col.Contains("energy"))
          {
            var timestampCol =
              string.Join("_", col.Split("_").SkipLast(1))
              + "_timestamp";
            deltaClauses.Add(
                $"CASE"
                + $" WHEN new.{col} < COALESCE(old.{col}, new.{col})"
                + $" THEN new.{timestampCol}"
                + $" ELSE COALESCE(old.{timestampCol}, new.{timestampCol})"
                + $" END {timestampCol}"
            );
            deltaUpsertClauses.Add(
                $"{timestampCol} = CASE"
                + $" WHEN delta.{col} < {tableName}.{col}"
                + $" THEN delta.{timestampCol}"
                + $" ELSE {tableName}.{timestampCol}"
                + $" END"
            );
          }
        }
        else if (col.Contains("max"))
        {
          deltaClauses.Add(
            $"GREATEST(new.{col}, COALESCE(old.{col}, new.{col})) {col}"
          );
          deltaUpsertClauses.Add(
            $"{col} = GREATEST(delta.{col}, {tableName}.{col})"
          );

          if (!col.Contains("energy"))
          {
            var timestampCol =
              string.Join("_", col.Split("_").SkipLast(1))
              + "_timestamp";
            deltaClauses.Add(
                $"CASE"
                + $" WHEN new.{col} > COALESCE(old.{col}, new.{col})"
                + $" THEN new.{timestampCol}"
                + $" ELSE COALESCE(old.{timestampCol}, new.{timestampCol})"
                + $" END {timestampCol}"
            );
            deltaUpsertClauses.Add(
                $"{timestampCol} = CASE"
                + $" WHEN delta.{col} > {tableName}.{col}"
                + $" THEN delta.{timestampCol} "
                + $" ELSE {tableName}.{timestampCol} END"
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
      return $@"
        INSERT INTO {tableName} ({columns})
        VALUES ({values})
        ON CONFLICT ({conflict}) {updateClause};
      ";
    }

    return $@"
      WITH old AS (
        SELECT {tableName}.*
        FROM {tableName}
        WHERE {string.Join(
          " AND ",
          primaryKeyColumns.Select(c =>
            $"{tableName}.{c.ColumnName} = @p{indexSubstitution}_{c.ColumnName}"
              + (c.Property.ClrType.IsEnum
                ? $"::{c.Property.ClrType.Name.ToSnakeCase()}"
                : "")))}
      ), new AS (
        INSERT INTO {tableName} ({columns})
        VALUES ({values})
        ON CONFLICT ({conflict}) {updateClause}
        RETURNING *
      ), delta AS (
        SELECT
          date_trunc(
            'day',
            new.{timestampColumn} AT TIME ZONE 'Europe/Zagreb')
            AT TIME ZONE 'Europe/Zagreb'
            daily_timestamp,
          date_trunc(
            'month',
            new.{timestampColumn} AT TIME ZONE 'Europe/Zagreb')
            AT TIME ZONE 'Europe/Zagreb'
            monthly_timestamp,
            {string.Join(", ", primaryKeyColumns.Select(c => $"new.{c.ColumnName}"))},
          CASE
            WHEN old.{timestampColumn} is null then 1
            ELSE 0
          END new_count,
          {string.Join(", ", deltaClauses)}
        FROM new
        LEFT JOIN old ON
          {string.Join(
            " AND ",
            primaryKeyColumns.Select(
              c => $"old.{c.ColumnName} = new.{c.ColumnName}"))}
      ), daily AS (
        UPDATE {tableName} SET
          {string.Join(", ", deltaUpsertClauses)}
        FROM delta
        WHERE
          {tableName}.{timestampColumn} = delta.daily_timestamp
          AND {tableName}.{intervalColumn}
            = '{IntervalEntity.Day.ToString().ToSnakeCase()}'::{nameof(IntervalEntity).ToSnakeCase()}
          AND {string.Join(
            " AND ",
            primaryKeyColumns
              .Where(c =>
                c.ColumnName != timestampColumn
                && c.ColumnName != intervalColumn)
              .Select(c =>
                $"{tableName}.{c.ColumnName} = delta.{c.ColumnName}"))}
        RETURNING *
      ), monthly AS (
        UPDATE {tableName} SET
          {string.Join(", ", deltaUpsertClauses)}
        FROM delta
        WHERE
          {tableName}.{timestampColumn} = delta.monthly_timestamp
          AND {tableName}.{intervalColumn}
            = '{IntervalEntity.Month.ToString().ToSnakeCase()}'::{nameof(IntervalEntity).ToSnakeCase()}
          AND {string.Join(
            " AND ",
            primaryKeyColumns
              .Where(c =>
                c.ColumnName != timestampColumn
                && c.ColumnName != intervalColumn)
              .Select(c =>
                $"{tableName}.{c.ColumnName} = delta.{c.ColumnName}"))}
        RETURNING *
      )
      SELECT * FROM daily
      UNION ALL
      SELECT * FROM monthly;
    ";
  }

  private static readonly ConcurrentDictionary<(Type, IntervalEntity?), string>
    UpsertTemplateCache =
      new ConcurrentDictionary<(Type, IntervalEntity?), string>();
}

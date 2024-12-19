using System.Data;
using System.Text;
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

#pragma warning disable CA1822 // Mark members as static
  internal async Task UpsertMeasurements(
    DataDbContext context,
    IEnumerable<IMeasurementEntity> measurements,
    CancellationToken cancellationToken
  )
#pragma warning restore CA1822 // Mark members as static
  {
    var connection = context.GetDapperDbConnection();

    var parameters = context.CreateBulkParameters(measurements);
    var sql = Upsert(context, measurements);

    if (context.Database.CurrentTransaction is { } transaction)
    {
      var command = new CommandDefinition(
        sql,
        parameters,
        transaction: transaction.GetDbTransaction(),
        cancellationToken: cancellationToken
      );

      await connection.ExecuteAsync(command);

      return;
    }

    while (true)
    {
      try
      {
        var isolationLevel = IsolationLevel.RepeatableRead;

        transaction = await context.Database
          .BeginTransactionAsync(isolationLevel, cancellationToken);

        var command = new CommandDefinition(
          sql,
          parameters,
          transaction: transaction.GetDbTransaction(),
          cancellationToken: cancellationToken
        );

        await connection.ExecuteAsync(command);

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

  private static string Upsert(
      DataDbContext context,
      IEnumerable<IMeasurementEntity> measurements)
  {
    var sql = new StringBuilder();

    var groupedMeasurements = measurements
        .Select((x, i) => (Index: i, Measurement: x))
        .GroupBy(x => x.Measurement.GetType());

    foreach (var group in groupedMeasurements)
    {
      var entityType = context.Model.FindEntityType(group.Key)
          ?? throw new InvalidOperationException(
            $"No entity type found for {group.Key}.");
      var storeObjectIdentifier = StoreObjectIdentifier
        .Create(entityType, StoreObjectType.Table)
        ?? throw new InvalidOperationException(
          $"No store object identifier found for {group.Key}.");
      var tableName = entityType.GetTableName()
          ?? throw new InvalidOperationException(
            $"No table name found for {group.Key}.");
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

      var groupMeasurements = group
        .Where(x => x.Measurement is not IAggregateEntity)
        .ToList();
      foreach (var (index, measurement) in groupMeasurements)
      {
        var columns = string.Join(", ", columnNames);
        var values = string.Join(", ", properties
          .Select(property =>
          {
            var columnName = property.GetColumnName(storeObjectIdentifier);
            return $"@p{index}_{columnName}"
            + (property.ClrType.IsEnum
              ? $"::{property.ClrType.Name.ToSnakeCase()}"
              : "");
          }));
        var conflict = string.Join(
          ", ",
          primaryKeyColumns.Select(c => c.ColumnName));

        var updateClause = "DO NOTHING";

        var row = $@"
          INSERT INTO {tableName} ({columns})
          VALUES ({values})
          ON CONFLICT ({conflict}) {updateClause};
        ";

        sql.AppendLine(row);
      }

      var groupAggregates = group
        .Where(x => x.Measurement is IAggregateEntity)
        .Zip(group
          .Select(x => x.Measurement)
          .OfType<IAggregateEntity>())
        .Select((x) => new
        {
          x.First.Index,
          Aggregate = x.Second
        })
        .OrderBy(measurement => measurement.Aggregate.Interval switch
        {
          IntervalEntity.QuarterHour => 0,
          IntervalEntity.Day => 1,
          IntervalEntity.Month => 2,
          _ => throw new InvalidOperationException(
            $"Unknown interval {measurement.Aggregate.Interval}.")
        })
        .Select(measurement => (measurement.Index, measurement.Aggregate))
        .ToList();
      foreach (var (index, aggregate) in groupAggregates)
      {
        var columns = string.Join(", ", columnNames);
        var values = string.Join(", ", properties
          .Select(property =>
          {
            var columnName = property.GetColumnName(storeObjectIdentifier);
            return $"@p{index}_{columnName}"
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
          .GetColumnName(aggregate.GetType(),
          nameof(IAggregateEntity.Count));
        var timestampColumn = context
          .GetColumnName(aggregate.GetType(),
          nameof(IAggregateEntity.Timestamp));
        var intervalColumn = context
          .GetColumnName(aggregate.GetType(),
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
          if (col.Contains("derived")
            && !col.Contains("timestamp")
            && aggregate.Interval == IntervalEntity.QuarterHour)
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
                $"new.{col} - old.{col} {col}"
              );
              deltaUpsertClauses.Add(
                $"{col} = ({tableName}.{col} * {tableName}.{countColumn}"
                + $" + delta.{col})"
                + $" / ({tableName}.{countColumn} + delta.new_count)"
              );
            }
            else if (col.Contains("min"))
            {
              deltaClauses.Add(
                $"LEAST(new.{col}, old.{col}) {col}"
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
                    + $" WHEN new.{col} < old.{col}"
                    + $" THEN new.{timestampCol}" +
                    $" ELSE old.{timestampCol} END {timestampCol}"
                );
                deltaUpsertClauses.Add(
                    $"{timestampCol} = CASE"
                    + $" WHEN delta.{col} < {tableName}.{col}"
                    + $" THEN delta.{timestampCol}" +
                    $" ELSE {tableName}.{timestampCol} END"
                );
              }
            }
            else if (col.Contains("max"))
            {
              deltaClauses.Add(
                $"GREATEST(new.{col}, old.{col}) {col}"
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
                    + $" WHEN new.{col} > old.{col}"
                    + $" THEN new.{timestampCol}"
                    + $" ELSE old.{timestampCol} END {timestampCol}"
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
                $"{col} = (({tableName}.{col} * {tableName}.{countColumn})"
                + $" + (EXCLUDED.{col} * EXCLUDED.{countColumn}))"
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

        if (aggregate.Interval == IntervalEntity.QuarterHour)
        {
          var row = $@"
            WITH old AS (
              SELECT {tableName}.*
              FROM {tableName}
              WHERE {string.Join(
                " AND ",
                primaryKeyColumns.Select(c =>
                  $"{tableName}.{c.ColumnName} = @p{index}_{c.ColumnName}"
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
                  daily_timestamp,
                date_trunc(
                  'month',
                  new.{timestampColumn} AT TIME ZONE 'Europe/Zagreb')
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
          sql.AppendLine(row);
        }
        else
        {
          var row = $@"
            INSERT INTO {tableName} ({columns})
            VALUES ({values})
            ON CONFLICT ({conflict}) {updateClause};
          ";
          sql.AppendLine(row);
        }
      }
    }

    return sql.ToString();
  }
}

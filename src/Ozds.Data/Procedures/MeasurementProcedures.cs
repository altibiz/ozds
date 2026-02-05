using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;
using Ozds.Data.Procedures.Abstractions;
using Ozds.Data.Procedures.Compilers;

namespace Ozds.Data.Procedures;

public class MeasurementProcedures(PostgresqlProcedures postgresqlProcedures)
  : IProcedures
{
  public string CallUpsertMeasurements(
    DataDbContext context,
    Type type,
    string jsonParameter
  )
  {
    return postgresqlProcedures.CallBatchMutation(
      context,
      type,
      "upsert",
      jsonParameter
    );
  }

  public string CallUpsertAggregates(
    DataDbContext context,
    Type aggregateType,
    IntervalEntity interval,
    string jsonParameter
  )
  {
    var intervalValue = StringExtensions.ToSnakeCase(interval.ToString());

    return postgresqlProcedures.CallBatchMutation(
      context,
      aggregateType,
      $"upsert_{intervalValue}",
      jsonParameter
    );
  }

  public string OverwriteUpsertMeasurements(DataDbContext context, Type type)
  {
    var entityType =
      context.Model.FindEntityType(type)
      ?? throw new InvalidOperationException(
        $"No entity type found for {type}."
      );
    var storeObjectIdentifier =
      StoreObjectIdentifier.Create(entityType, StoreObjectType.Table)
      ?? throw new InvalidOperationException(
        $"No store object identifier found for {type}."
      );
    var primaryKey =
      entityType.FindPrimaryKey()
      ?? throw new InvalidOperationException(
        $"No primary key found for {entityType.Name}."
      );
    var properties = entityType.GetScalarPropertiesRecursive().ToList();

    var tableName =
      entityType.GetTableName()
      ?? throw new InvalidOperationException(
        $"No table name found for {type}."
      );
    var columnNames = properties
      .Select(p => p.GetColumnName(storeObjectIdentifier))
      .ToList();
    var primaryKeyColumns = primaryKey
      .Properties.Select(p => new
      {
        Property = p,
        ColumnName = p.GetColumnName(storeObjectIdentifier),
      })
      .ToList();
    var columns = string.Join(", ", columnNames);
    var values = string.Join(", ", columnNames.Select(x => $"{tableName}.{x}"));
    var conflict = string.Join(
      ", ",
      primaryKeyColumns.Select(c => c.ColumnName)
    );

    var query =
      $@"
      INSERT INTO {tableName} ({columns})
      SELECT {values}
      FROM jsonb_populate_recordset(null::{tableName}, $1) AS {tableName}
      ON CONFLICT ({conflict}) DO NOTHING
      RETURNING {tableName}.*;
    ";

    return postgresqlProcedures.OverwriteBatchMutation(
      context,
      "upsert",
      type,
      query
    );
  }

  public string OverwriteUpsertAggregates(
    DataDbContext context,
    Type aggregateType,
    IntervalEntity interval
  )
  {
    var entityType =
      context.Model.FindEntityType(aggregateType)
      ?? throw new InvalidOperationException(
        $"No entity type found for {aggregateType}."
      );
    var storeObjectIdentifier =
      StoreObjectIdentifier.Create(entityType, StoreObjectType.Table)
      ?? throw new InvalidOperationException(
        $"No store object identifier found for {aggregateType}."
      );
    var properties = entityType.GetScalarPropertiesRecursive().ToList();
    var primaryKey =
      entityType.FindPrimaryKey()
      ?? throw new InvalidOperationException(
        $"No primary key found for {entityType.Name}."
      );

    var tableName =
      context.GetTableName(aggregateType)
      ?? throw new InvalidOperationException(
        $"No table name found for {aggregateType}."
      );
    var timestampColumn = context.GetColumnName(
      aggregateType,
      [nameof(IAggregateEntity.Timestamp)]
    );
    var intervalColumn = context.GetColumnName(
      aggregateType,
      [nameof(IAggregateEntity.Interval)]
    );
    var insertedPrimaryKeyValues = string.Join(
      ", ",
      primaryKey
        .Properties.Where(x =>
          x.GetColumnName(storeObjectIdentifier) != timestampColumn
        )
        .Select(property =>
          $"inserted.{property.GetColumnName(storeObjectIdentifier)}"
        )
    );
    var primaryKeyValues = string.Join(
      ", ",
      primaryKey.Properties.Select(property =>
        property.GetColumnName(storeObjectIdentifier)
      )
    );
    var primaryKeyInputEqualityCheck = string.Join(
      " AND ",
      primaryKey.Properties.Select(property =>
      {
        var columnName = property.GetColumnName(storeObjectIdentifier);
        return $"{tableName}.{columnName}"
          + $" = input.{columnName}"
          + (
            property.ClrType.IsEnum
              ? $"::{StringExtensions.ToSnakeCase(property.ClrType.Name)}"
              : ""
          );
      })
    );

    string PrimaryKeyDeltaEqualityCheck(string deltaTable)
    {
      return string.Join(
        " AND ",
        primaryKey
          .Properties.Where(property =>
          {
            var columnName = property.GetColumnName(storeObjectIdentifier);
            return columnName != timestampColumn
              && columnName != intervalColumn;
          })
          .Select(property =>
          {
            var columnName = property.GetColumnName(storeObjectIdentifier);
            return $"{tableName}.{columnName}"
              + $" = {deltaTable}.{columnName}"
              + (
                property.ClrType.IsEnum
                  ? $"::{StringExtensions.ToSnakeCase(property.ClrType.Name)}"
                  : ""
              );
          })
      );
    }

    var primaryKeyInsertedOldEqualityCheck = string.Join(
      " AND ",
      primaryKey.Properties.Select(property =>
      {
        var columnName = property.GetColumnName(storeObjectIdentifier);
        return $"old.{columnName} = inserted.{columnName}";
      })
    );
    var columns = string.Join(", ", properties.Select(p => p.GetColumnName()));
    var values = string.Join(
      ", ",
      properties.Select(x => $"{tableName}.{x.GetColumnName()}")
    );
    var inputValues = string.Join(
      ", ",
      properties.Select(x => $"input.{x.GetColumnName()}")
    );
    var dayIntervalValue = StringExtensions.ToSnakeCase(
      IntervalEntity.Day.ToString()
    );
    var monthIntervalValue = StringExtensions.ToSnakeCase(
      IntervalEntity.Month.ToString()
    );
    var intervalTypeName = StringExtensions.ToSnakeCase(nameof(IntervalEntity));
    var intervalValue = StringExtensions.ToSnakeCase(interval.ToString());

    var parts = MeasurementProcedureCompiler.Find(aggregateType);
    if (parts is null)
    {
      throw new InvalidOperationException(
        $"No measurement procedure parts found for {aggregateType}."
      );
    }

    var upsertParts = parts
      .UpsertMeasurementProcedureParts.Select(part =>
        MeasurementProcedureCompiler.CompileUpsert(part, context, aggregateType)
      )
      .ToList();
    upsertParts.Add(UpsertCount(context, aggregateType));
    if (interval != IntervalEntity.QuarterHour)
    {
      upsertParts.Add(UpsertQuarterHourCount(context, aggregateType));
    }
    else
    {
      upsertParts.AddRange(
        parts.DerivativeMeasurementProcedureParts.Select(part =>
          MeasurementProcedureCompiler.CompileDerivative(
            part,
            context,
            aggregateType
          )
        )
      );
    }

    var deltaParts = parts.DeltaMeasurementProcedureParts.Select(part =>
      MeasurementProcedureCompiler.CompileDelta(
        part,
        context,
        aggregateType,
        "inserted",
        "old"
      )
    );
    var dailyDeriveParts = parts
      .DeriveMeasurementProcedureParts.Select(part =>
        MeasurementProcedureCompiler.CompileDerive(
          part,
          context,
          aggregateType,
          "daily_delta",
          "new_count"
        )
      )
      .ToList();
    dailyDeriveParts.Add(
      DeriveQuarterHourCount(context, aggregateType, "daily_delta", "new_count")
    );
    var monthlyDeriveParts = parts
      .DeriveMeasurementProcedureParts.Select(part =>
        MeasurementProcedureCompiler.CompileDerive(
          part,
          context,
          aggregateType,
          "monthly_delta",
          "new_count"
        )
      )
      .ToList();
    monthlyDeriveParts.Add(
      DeriveQuarterHourCount(
        context,
        aggregateType,
        "monthly_delta",
        "new_count"
      )
    );

    var query =
      $@"
      INSERT INTO {tableName} ({columns})
      SELECT {inputValues}
      FROM jsonb_populate_recordset(null::{tableName}, $1) AS input
      ON CONFLICT ({primaryKeyValues})
      DO UPDATE SET {string.Join(", ", upsertParts)}
      RETURNING {tableName}.*;
    ";
    if (interval == IntervalEntity.QuarterHour)
    {
      query =
        $@"
        WITH
          input AS (
            SELECT {values}
            FROM jsonb_populate_recordset(null::{tableName}, $1) AS {tableName}
          ),
          old AS (
            SELECT {values}
            FROM {tableName}
            WHERE EXISTS (
              SELECT 1 FROM input
              WHERE {primaryKeyInputEqualityCheck}
            )
          ),
          inserted AS (
            INSERT INTO {tableName} ({columns})
            SELECT {inputValues}
            FROM input
            ON CONFLICT ({primaryKeyValues})
            DO UPDATE SET {string.Join(", ", upsertParts)}
            RETURNING {tableName}.*
          ),
          daily_delta AS (
            SELECT
              date_trunc(
                'day',
                inserted.{timestampColumn}
                  AT TIME ZONE 'Europe/Zagreb')
                AT TIME ZONE 'Europe/Zagreb'
                daily_timestamp,
              {insertedPrimaryKeyValues},
              COUNT(inserted.*) - COUNT(old.*) AS new_count,
              {string.Join(", ", deltaParts)}
            FROM inserted
            LEFT JOIN old
            ON {primaryKeyInsertedOldEqualityCheck}
            GROUP BY daily_timestamp, {insertedPrimaryKeyValues}
          ),
          daily AS (
            UPDATE {tableName}
            SET {string.Join(", ", dailyDeriveParts)}
            FROM daily_delta
            WHERE
              {tableName}.{timestampColumn}
                = daily_delta.daily_timestamp
              AND {tableName}.{intervalColumn}
                = '{dayIntervalValue}'::{intervalTypeName}
              AND {PrimaryKeyDeltaEqualityCheck("daily_delta")}
            RETURNING {tableName}.*
          ),
          monthly_delta AS (
            SELECT
              date_trunc(
                'month',
                inserted.{timestampColumn}
                  AT TIME ZONE 'Europe/Zagreb')
                AT TIME ZONE 'Europe/Zagreb'
                monthly_timestamp,
              {insertedPrimaryKeyValues},
              COUNT(inserted.*) - COUNT(old.*) AS new_count,
              {string.Join(", ", deltaParts)}
            FROM inserted
            LEFT JOIN old
            ON {primaryKeyInsertedOldEqualityCheck}
            GROUP BY monthly_timestamp, {insertedPrimaryKeyValues}
          ),
          monthly AS (
            UPDATE {tableName}
            SET {string.Join(", ", monthlyDeriveParts)}
            FROM monthly_delta
            WHERE
              {tableName}.{timestampColumn}
                = monthly_delta.monthly_timestamp
              AND {tableName}.{intervalColumn}
                = '{monthIntervalValue}'::{intervalTypeName}
              AND {PrimaryKeyDeltaEqualityCheck("monthly_delta")}
            RETURNING {tableName}.*
          )
        SELECT * FROM inserted
        UNION ALL
        SELECT * FROM daily
        UNION ALL
        SELECT * FROM monthly;
      ";
    }

    return postgresqlProcedures.OverwriteBatchMutation(
      context,
      $"upsert_{intervalValue}",
      aggregateType,
      query
    );
  }

  private static string UpsertCount(DataDbContext context, Type aggregateType)
  {
    var tableName = context.GetTableName(aggregateType);
    var countColumn = context.GetColumnName(
      aggregateType,
      [nameof(IAggregateEntity.Count)]
    );
    return $@"
      {countColumn} = {tableName}.{countColumn} +
        EXCLUDED.{countColumn}
    ";
  }

  private static string UpsertQuarterHourCount(
    DataDbContext context,
    Type aggregateType
  )
  {
    var quarterHourCountColumn = context.GetColumnName(
      aggregateType,
      [nameof(IAggregateEntity.QuarterHourCount)]
    );
    return $@"{quarterHourCountColumn} = 1";
  }

  private static string DeriveQuarterHourCount(
    DataDbContext context,
    Type aggregateType,
    string deltaTable,
    string newCountColumn
  )
  {
    var quarterHourCountColumn = context.GetColumnName(
      aggregateType,
      [nameof(IAggregateEntity.QuarterHourCount)]
    );
    return $@"
      {quarterHourCountColumn} = GREATEST(
        1,
        ({quarterHourCountColumn} + {deltaTable}.{newCountColumn})
      )
    ";
  }

  public string DeleteUpsertMeasurements(DataDbContext context, Type type)
  {
    return postgresqlProcedures.DeleteBatchMutation(context, type, "upsert");
  }

  public string DeleteUpsertAggregates(
    DataDbContext context,
    Type type,
    IntervalEntity interval
  )
  {
    var intervalValue = StringExtensions.ToSnakeCase(interval.ToString());

    return postgresqlProcedures.DeleteBatchMutation(
      context,
      type,
      $"upsert_{intervalValue}"
    );
  }
}

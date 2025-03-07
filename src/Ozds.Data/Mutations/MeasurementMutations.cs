using System.Collections.Concurrent;
using System.Data;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Npgsql;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;
using Ozds.Data.Mutations.Abstractions;
using Ozds.Data.Observers.Abstractions;
using Ozds.Data.Observers.EventArgs;

namespace Ozds.Data.Mutations;

// TODO: enum by model configuration not clr type
// TODO: determining clauses by column name is flaky - needs to be improved

public class MeasurementMutations(
  IDbContextFactory<DataDbContext> factory,
  ILogger<MeasurementMutations> logger,
  IEntitiesChangingPublisher changingPublisher,
  IEntitiesChangedPublisher changedPublisher
) : IMutations
{
  private const int QuarterHourChunkSize = 1;
  private const int UpsertChunkSize = 50;
  private const int TryInsertChunkSize = 10000;

  private static readonly ConcurrentDictionary<
      UpsertTemplateCacheKey,
      UpsertTemplate>
    UpsertTemplateCache = new();

  public async Task<List<IMeasurementEntity>> CreateMeasurements(
    IEnumerable<IMeasurementEntity> measurements,
    CancellationToken cancellationToken
  )
  {
    var measurementsList = measurements.ToList();
    if (measurementsList.Count == 0)
    {
      return new List<IMeasurementEntity>();
    }

    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    changingPublisher.Publish(
      new EntitiesChangingEventArgs
      {
        Entities = measurementsList
          .Select(
            measurement => new EntityChangingEntry(
              EntityChangingState.Adding,
              measurement))
          .ToList()
      });

    var result = await CreateMeasurements(
      context,
      measurementsList,
      cancellationToken
    );

    changedPublisher.Publish(
      new EntitiesChangedEventArgs
      {
        Entities = result
          .Select(
            measurement => new EntityChangedEntry(
              EntityChangedState.Added,
              measurement))
          .ToList()
      });

    return result;
  }

  internal async Task<List<IMeasurementEntity>> CreateMeasurements(
    DataDbContext context,
    IEnumerable<IMeasurementEntity> measurements,
    CancellationToken cancellationToken
  )
  {
    var measurementsList = measurements.ToList();
    if (measurementsList.Count == 0)
    {
      return new List<IMeasurementEntity>();
    }

    List<IMeasurementEntity>? results = null;
    if (context.Database.CurrentTransaction is not null)
    {
      results = await Execute(
        context,
        measurementsList,
        cancellationToken
      );
    }
    else
    {
      while (true)
      {
        try
        {
          var isolationLevel = IsolationLevel.RepeatableRead;

          await using var transaction = await context.Database
            .BeginTransactionAsync(isolationLevel, cancellationToken);

          results = await Execute(
            context,
            measurementsList,
            cancellationToken
          );

          await context.Database
            .CommitTransactionAsync(cancellationToken);

          break;
        }
        catch (PostgresException ex)
        {
          if (context.Database.CurrentTransaction is { } transaction)
          {
            await transaction.RollbackAsync(cancellationToken);
          }

          // NOTE: 40001 = could not serialize access due to
          // read/write dependencies among transactions
          // NOTE: 40P01 = deadlock detected
          if (ex.SqlState == "40001" || ex.SqlState == "40P01")
          {
            logger.LogDebug(
              ex,
              "Retying insert of {Count} measurements...",
              measurementsList.Count);
            continue;
          }

          throw;
        }
        catch (Exception)
        {
          if (context.Database.CurrentTransaction is { } transaction)
          {
            await transaction.RollbackAsync(cancellationToken);
          }

          throw;
        }
      }
    }

    return results
      .GroupBy(
        x => new
        {
          x.MeterId,
          x.MeasurementLocationId,
          x.Timestamp,
          Interval = x is IAggregateEntity aggregate
            ? aggregate.Interval
            : (IntervalEntity?)null
        })
      .Select(x => x.Last())
      .ToList();
  }

  private async Task<List<IMeasurementEntity>> Execute(
    DataDbContext context,
    IEnumerable<IMeasurementEntity> measurements,
    CancellationToken cancellationToken
  )
  {
    var grouped = measurements
      .Select((x, i) => (Index: i, Measurement: x))
      .OrderBy(
        x => x.Measurement is IAggregateEntity aggregate
          ? aggregate.Interval switch
          {
            IntervalEntity.Month => 1,
            IntervalEntity.Day => 2,
            IntervalEntity.QuarterHour => 3,
            _ => throw new InvalidOperationException(
              $"Unknown interval {aggregate.Interval}.")
          }
          : 0)
      .GroupBy(
        x => (
          x.Measurement.GetType(),
          x.Measurement is AggregateEntity aggregate
          && aggregate.Interval == IntervalEntity.QuarterHour));

    var results = new List<IMeasurementEntity>();
    foreach (var group in grouped)
    {
      var type = group.Key.Item1;
      var isQuarterHour = group.Key.Item2;
      var chunkSize = isQuarterHour
        ? QuarterHourChunkSize
        : type.IsAssignableTo(typeof(IAggregateEntity))
          ? UpsertChunkSize
          : TryInsertChunkSize;

      foreach (var chunk in group.Chunk(chunkSize))
      {
        var objects = await ExecuteChunk(
          context,
          chunk,
          type,
          cancellationToken
        );
        results.AddRange(objects.Select(x => (IMeasurementEntity)x));
      }
    }

    return results;
  }

  private async Task<List<object>> ExecuteChunk(
    DataDbContext context,
    IEnumerable<(int Index, IMeasurementEntity Measurement)> chunk,
    Type type,
    CancellationToken cancellationToken
  )
  {
    var parameters = context.CreateBulkParameters(
      chunk.Select(x => (x.Measurement as object, x.Index)));
    var commands = new List<UpsertTemplate>();
    var measurementIndices = new HashSet<int>();
    foreach (var (index, measurement) in chunk)
    {
      if (measurement is IAggregateEntity aggregate)
      {
        commands.Add(
          CreateCommand(
            context,
            type,
            aggregate.Interval,
            index));
      }
      else
      {
        measurementIndices.Add(index);
      }
    }

    // NOTE: good for debugging - please don't remove
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

    var count = 0;
    var stopwatch = Stopwatch.StartNew();
    var objects = new List<object>();
    {
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

      if (!string.IsNullOrEmpty(sql))
      {
        count++;
        objects.AddRange(
          await context.DapperCommand(
            type,
            sql,
            cancellationToken,
            parameters
          ));
        var elapsed = stopwatch.Elapsed;
        logger.LogDebug(
          "Executed mixed command at {Elapsed}",
          elapsed);
      }
    }
    foreach (var sql in
      CreateMeasurementCommands(
          context,
          type,
          measurementIndices)
        .Select(x => x.Query)
        .Where(x => !string.IsNullOrEmpty(x)))
    {
      count++;
      objects.AddRange(
        await context.DapperCommand(
          type,
          sql,
          cancellationToken,
          parameters
        ));
      var elapsed = stopwatch.Elapsed;
      logger.LogDebug(
        "Executed measurement command at {Elapsed}",
        elapsed);
    }

    stopwatch.Stop();
    logger.LogDebug(
      "Executed {Count} commands in {Elapsed}",
      count,
      stopwatch.Elapsed);

    return objects;
  }

  private static UpsertTemplate CreateCommand(
    DataDbContext context,
    Type type,
    IntervalEntity? interval,
    int index
  )
  {
    var template = UpsertTemplateCache.GetOrAdd(
      new UpsertTemplateCacheKey(type, interval),
      _ => interval is { } nonNullInterval
        ? CreateUpsertAggregateTemplate(
          context, type, nonNullInterval, "{{INDEX}}")
        : CreateUpsertMeasurementTemplate(
          context, type, "{{INDEX}}")
    );
    return new UpsertTemplate(
      template.With?.Replace("{{INDEX}}", index.ToString()),
      template.Query.Replace("{{INDEX}}", index.ToString())
    );
  }

  private static List<UpsertTemplate> CreateMeasurementCommands(
    DataDbContext context,
    Type type,
    IEnumerable<int> indices
  )
  {
    var entityType = context.Model.FindEntityType(type)
      ?? throw new InvalidOperationException(
        $"No entity type found for {type}.");
    var properties = entityType.GetScalarPropertiesRecursive().ToList();

    // NOTE: postgresql: A statement cannot have more than 65535 parameters
    var chunkSize = (int)(Math.Pow(2, 15) / properties.Count);
    var templates = new List<UpsertTemplate>();
    foreach (var indexChunk in indices.Chunk(chunkSize))
    {
      var values = new List<string>();
      foreach (var index in indexChunk)
      {
        values.Add(
          CreateValuesMeasurementTemplate(
              context,
              type,
              index.ToString())
            .Replace("{{INDEX}}", index.ToString()));
      }

      var valuesString = string.Join(",\n", values);
      var template = CreateUpsertMeasurementTemplateFromValues(
        context,
        type,
        valuesString);
      templates.Add(template);
    }

    return templates;
  }

  private static UpsertTemplate CreateUpsertMeasurementTemplate(
    DataDbContext context,
    Type type,
    string indexSubstitution)
  {
    var values = CreateValuesMeasurementTemplate(
      context,
      type,
      indexSubstitution);
    var template = CreateUpsertMeasurementTemplateFromValues(
      context,
      type,
      values);

    return template;
  }

  private static UpsertTemplate CreateUpsertAggregateTemplate(
    DataDbContext context,
    Type type,
    IntervalEntity interval,
    string indexSubstitution)
  {
    var values = CreateValuesMeasurementTemplate(
      context,
      type,
      indexSubstitution);
    var template = CreateUpsertAggregateTemplateFromValues(
      context,
      type,
      interval,
      values,
      indexSubstitution);

    return template;
  }

  private static string CreateValuesMeasurementTemplate(
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
    var properties = entityType.GetScalarPropertiesRecursive().ToList();

    var values = $"({string.Join(
      ", ", properties
        .Select(
          property =>
          {
            var columnName = property.GetColumnName(storeObjectIdentifier);
            return $"@p{indexSubstitution}_{columnName}"
              + (property.ClrType.IsEnum
                ? $"::{StringExtensions.ToSnakeCase(property.ClrType.Name)}"
                : "");
          }))})";

    return values;
  }

  private static UpsertTemplate CreateUpsertMeasurementTemplateFromValues(
    DataDbContext context,
    Type type,
    string values)
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
    var properties = entityType.GetScalarPropertiesRecursive().ToList();

    var columnNames = properties
      .Select(p => p.GetColumnName(storeObjectIdentifier))
      .ToList();
    var primaryKey = entityType.FindPrimaryKey()
      ?? throw new InvalidOperationException(
        $"No primary key found for {entityType.Name}.");
    var primaryKeyColumns = primaryKey.Properties
      .Select(
        p => new
        {
          Property = p,
          ColumnName = p.GetColumnName(storeObjectIdentifier)
        })
      .ToList();

    var columns = string.Join(", ", columnNames);
    var conflict = string.Join(
      ", ",
      primaryKeyColumns.Select(c => c.ColumnName));

    var updateClause = "DO NOTHING";

    return new UpsertTemplate(
      null, $@"
      INSERT INTO {tableName} ({columns})
      VALUES {values}
      ON CONFLICT ({conflict}) {updateClause}
      RETURNING {tableName}.*
    ");
  }

  private static UpsertTemplate CreateUpsertAggregateTemplateFromValues(
    DataDbContext context,
    Type type,
    IntervalEntity interval,
    string values,
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
    var properties = entityType.GetScalarPropertiesRecursive().ToList();

    var columnNames = properties
      .Select(p => p.GetColumnName(storeObjectIdentifier))
      .ToList();
    var primaryKey = entityType.FindPrimaryKey()
      ?? throw new InvalidOperationException(
        $"No primary key found for {entityType.Name}.");
    var primaryKeyColumns = primaryKey.Properties
      .Select(
        p => new
        {
          Property = p,
          ColumnName = p.GetColumnName(storeObjectIdentifier)
        })
      .ToList();

    var columns = string.Join(", ", columnNames);
    var conflict = string.Join(
      ", ",
      primaryKeyColumns.Select(c => c.ColumnName)
    );

    var updateClause = string.Empty;
    var setClauses = new List<string>();
    var deltaClauses = new List<string>();
    var deltaUpsertClauses = new List<string>();

    var countColumn = context
      .GetColumnName(
        type,
        nameof(IAggregateEntity.Count));
    var quarterHourCountColumn = context
      .GetColumnName(
        type,
        nameof(IAggregateEntity.QuarterHourCount));
    var timestampColumn = context
      .GetColumnName(
        type,
        nameof(IAggregateEntity.Timestamp));
    var intervalColumn = context
      .GetColumnName(
        type,
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
          $"{quarterHourCountColumn} = GREATEST(1, ("
          + $" {tableName}.{quarterHourCountColumn}"
          + $" + delta{indexSubstitution}.new_count))"
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
            + $" / GREATEST(1,"
            + $" ({tableName}.{quarterHourCountColumn}"
            + $" + delta{indexSubstitution}.new_count))"
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
      return new UpsertTemplate(
        $@"
          value{indexSubstitution} AS (
            INSERT INTO {tableName} ({columns})
            VALUES {values}
            ON CONFLICT ({conflict}) {updateClause}
            RETURNING {tableName}.*
          )
        ",
        $@"
          SELECT * FROM value{indexSubstitution}
        "
      );
    }

    var dayIntervalValue =
      StringExtensions.ToSnakeCase(IntervalEntity.Day.ToString());
    var monthIntervalValue =
      StringExtensions.ToSnakeCase(IntervalEntity.Month.ToString());
    var intervalTypeName = StringExtensions.ToSnakeCase(nameof(IntervalEntity));

    return new UpsertTemplate(
      $@"
        old{indexSubstitution} AS (
          SELECT {tableName}.*
          FROM {tableName}
          WHERE {string.Join(
            " AND ",
            primaryKeyColumns.Select(
              c =>
                $"{tableName}.{c.ColumnName}"
                + $" = @p{indexSubstitution}_{c.ColumnName}"
                + (c.Property.ClrType.IsEnum
                  ? $"::{StringExtensions.ToSnakeCase(c.Property.ClrType.Name)}"
                  : "")))}
        ), new{indexSubstitution} AS (
          INSERT INTO {tableName} ({columns})
          VALUES {values}
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
                primaryKeyColumns.Select(
                  c =>
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
                .Where(
                  c =>
                    c.ColumnName != timestampColumn
                    && c.ColumnName != intervalColumn)
                .Select(
                  c =>
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
                .Where(
                  c =>
                    c.ColumnName != timestampColumn
                    && c.ColumnName != intervalColumn)
                .Select(
                  c =>
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

  private sealed record UpsertTemplateCacheKey(
    Type Type,
    IntervalEntity? Interval
  );

  private sealed record UpsertTemplate(
    string? With,
    string Query
  );
}

using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Extensions;
using Ozds.Data.Procedures.Parts;

namespace Ozds.Data.Procedures.Compilers;

public static class MeasurementProcedureCompiler
{
  private const string FloatEpsilon = "1e-6";

  public static IMeasurementProcedureParts Find(Type aggregateType)
  {
    var parts =
      typeof(IMeasurementProcedureParts)
        .Assembly.GetTypes()
        .Where(type =>
          !type.IsAbstract
          && !type.IsGenericType
          && typeof(IMeasurementProcedureParts).IsAssignableFrom(type)
        )
        .Select(Activator.CreateInstance)
        .OfType<IMeasurementProcedureParts>()
        .FirstOrDefault(x => x.AggregateType == aggregateType)
      ?? throw new InvalidOperationException(
        $"No measurement procedure parts found for {aggregateType}."
      );

    return parts;
  }

  public static string CompileUpsert(
    IUpsertMeasurementProcedurePart part,
    DbContext context,
    Type aggregateType
  )
  {
    return part switch
    {
      IUpsertAverageMeasurementProcedurePart upsertAveragePart => UpsertAverage(
        context,
        aggregateType,
        upsertAveragePart.Value.GetMemberExpressionPath()
      ),
      IUpsertMinMeasurementProcedurePart upsertMinPart => UpsertMin(
        context,
        aggregateType,
        upsertMinPart.Value.GetMemberExpressionPath()
      ),
      IUpsertMinTimestampMeasurementProcedurePart upsertMinTimestampPart =>
        UpsertMinTimestamp(
          context,
          aggregateType,
          upsertMinTimestampPart.Value.GetMemberExpressionPath(),
          upsertMinTimestampPart.Timestamp.GetMemberExpressionPath()
        ),
      IUpsertMaxMeasurementProcedurePart upsertMaxPart => UpsertMax(
        context,
        aggregateType,
        upsertMaxPart.Value.GetMemberExpressionPath()
      ),
      IUpsertMaxTimestampMeasurementProcedurePart upsertMaxTimestampPart =>
        UpsertMaxTimestamp(
          context,
          aggregateType,
          upsertMaxTimestampPart.Value.GetMemberExpressionPath(),
          upsertMaxTimestampPart.Timestamp.GetMemberExpressionPath()
        ),
      _ => throw new ArgumentOutOfRangeException(
        nameof(part),
        $"Unknown {nameof(part)} type {part.GetType()}."
      ),
    };
  }

  public static string CompileDerivative(
    IDerivativeMeasurementProcedurePart part,
    DbContext context,
    Type aggregateType
  )
  {
    return part switch
    {
      IDerivativePowerMeasurementProcedurePart derivativePower =>
        DerivativePower(
          context,
          aggregateType,
          derivativePower.Value.GetMemberExpressionPath(),
          derivativePower.MinEnergy.GetMemberExpressionPath(),
          derivativePower.MaxEnergy.GetMemberExpressionPath()
        ),
      IDerivativePowerTimestampMeasurementProcedurePart derivativePowerTimestamp =>
        DerivativePowerTimestamp(
          context,
          aggregateType,
          derivativePowerTimestamp.Value.GetMemberExpressionPath(),
          derivativePowerTimestamp.Timestamp.GetMemberExpressionPath()
        ),
      _ => throw new ArgumentOutOfRangeException(
        nameof(part),
        $"Unknown {nameof(part)} type {part.GetType()}."
      ),
    };
  }

  public static string CompileDerive(
    IDeriveMeasurementProcedurePart part,
    DbContext context,
    Type aggregateType,
    string newTable,
    string newCount
  )
  {
    return part switch
    {
      IDeriveAverageMeasurementProcedurePart deriveAveragePart => DeriveAverage(
        context,
        aggregateType,
        deriveAveragePart.Value.GetMemberExpressionPath(),
        newTable,
        newCount
      ),
      IDeriveMinMeasurementProcedurePart deriveMinPart => DeriveMin(
        context,
        aggregateType,
        deriveMinPart.Value.GetMemberExpressionPath(),
        newTable
      ),
      IDeriveMinTimestampMeasurementProcedurePart deriveMinTimestampPart =>
        DeriveMinTimestamp(
          context,
          aggregateType,
          deriveMinTimestampPart.Value.GetMemberExpressionPath(),
          deriveMinTimestampPart.Timestamp.GetMemberExpressionPath(),
          newTable
        ),
      IDeriveMaxMeasurementProcedurePart deriveMaxPart => DeriveMax(
        context,
        aggregateType,
        deriveMaxPart.Value.GetMemberExpressionPath(),
        newTable
      ),
      IDeriveMaxTimestampMeasurementProcedurePart deriveMaxTimestampPart =>
        DeriveMaxTimestamp(
          context,
          aggregateType,
          deriveMaxTimestampPart.Value.GetMemberExpressionPath(),
          deriveMaxTimestampPart.Timestamp.GetMemberExpressionPath(),
          newTable
        ),
      _ => throw new ArgumentOutOfRangeException(
        nameof(part),
        $"Unknown {nameof(part)} type {part.GetType()}."
      ),
    };
  }

  public static string CompileDelta(
    IDeltaMeasurementProcedurePart part,
    DbContext context,
    Type aggregateType,
    string newTable,
    string oldTable
  )
  {
    return part switch
    {
      IDeltaAverageMeasurementProcedurePart deltaAveragePart => DeltaAverage(
        context,
        aggregateType,
        deltaAveragePart.Value.GetMemberExpressionPath(),
        newTable,
        oldTable
      ),
      IDeltaMinMeasurementProcedurePart deltaMinPart => DeltaMin(
        context,
        aggregateType,
        deltaMinPart.Value.GetMemberExpressionPath(),
        newTable,
        oldTable
      ),
      IDeltaMinTimestampMeasurementProcedurePart deltaMinTimestampPart =>
        DeltaMinTimestamp(
          context,
          aggregateType,
          deltaMinTimestampPart.Value.GetMemberExpressionPath(),
          deltaMinTimestampPart.Timestamp.GetMemberExpressionPath(),
          newTable,
          oldTable
        ),
      IDeltaMaxMeasurementProcedurePart deltaMaxPart => DeltaMax(
        context,
        aggregateType,
        deltaMaxPart.Value.GetMemberExpressionPath(),
        newTable,
        oldTable
      ),
      IDeltaMaxTimestampMeasurementProcedurePart deltaMaxTimestampPart =>
        DeltaMaxTimestamp(
          context,
          aggregateType,
          deltaMaxTimestampPart.Value.GetMemberExpressionPath(),
          deltaMaxTimestampPart.Timestamp.GetMemberExpressionPath(),
          newTable,
          oldTable
        ),
      _ => throw new ArgumentOutOfRangeException(
        nameof(part),
        $"Unknown {nameof(part)} type {part.GetType()}."
      ),
    };
  }

  // NOTE: used SELECT switch statement since I don't
  // want to recalculate expression multiple times
  private static string ClampNearZeroValues(
    string valueToSafeGuard,
    string epsilonValue = FloatEpsilon
  )
  {
    return $@"
      (
        SELECT CASE
          WHEN ABS((v)::double precision) < {epsilonValue} THEN 0
          ELSE v
        END
        FROM (VALUES (({valueToSafeGuard}))) AS _safeguard(v)
      )
    ";
  }

  private static string AssignValue(
    string columnName,
    string expression,
    bool clamp,
    string epsilonValue = FloatEpsilon
  )
  {
    var value = clamp
      ? ClampNearZeroValues(expression, epsilonValue)
      : expression;
    return $@"
      {columnName} = {value}
    ";
  }

  private static string SelectValue(
    string columnName,
    string expression,
    bool clamp,
    string epsilonValue = FloatEpsilon
  )
  {
    var value = clamp
      ? ClampNearZeroValues(expression, epsilonValue)
      : expression;
    return $@"
      {value} AS {columnName}
    ";
  }

  private static Type GetPropertyClrType(
    DbContext context,
    Type aggregateType,
    IEnumerable<string> propertyName
  )
  {
    static Type Recursive(ITypeBase type, string[] propertyNames)
    {
      if (propertyNames.Length == 0)
      {
        throw new ArgumentException("Property name is required.");
      }

      if (propertyNames.Length == 1)
      {
        var property =
          type.FindProperty(propertyNames[0])
          ?? throw new InvalidOperationException(
            $"No property {propertyNames[0]} on type {type.Name}."
          );

        return property.ClrType;
      }

      var complexType =
        type.GetComplexProperties()
          .FirstOrDefault(x => x.Name == propertyNames[0])
        ?? throw new InvalidOperationException(
          $"No property {propertyNames[0]} on type {type.Name} while resolving {string.Join(".", propertyNames)}."
        );

      return Recursive(
        complexType.ComplexType,
        propertyNames.Skip(1).ToArray()
      );
    }

    var entityType =
      context.Model.FindEntityType(aggregateType)
      ?? throw new InvalidOperationException(
        $"Entity type {aggregateType.Name} not found."
      );

    return Recursive(entityType, propertyName.ToArray());
  }

  private static bool ShouldClamp(
    DbContext context,
    Type aggregateType,
    IEnumerable<string> propertyName
  )
  {
    var clrType =
      Nullable.GetUnderlyingType(
        GetPropertyClrType(context, aggregateType, propertyName)
      ) ?? GetPropertyClrType(context, aggregateType, propertyName);

    return clrType == typeof(decimal)
      || clrType == typeof(float)
      || clrType == typeof(double);
  }

  private static string UpsertAverage(
    DbContext context,
    Type aggregateType,
    IEnumerable<string> propertyName
  )
  {
    var columnName = context.GetColumnName(aggregateType, propertyName);
    var tableName = context.GetTableName(aggregateType);
    var countColumn = context.GetColumnName(
      aggregateType,
      [nameof(IAggregateEntity.Count)]
    );

    return AssignValue(
      columnName!,
      $@"
      ({ClampNearZeroValues($@"({tableName}.{columnName} * {tableName}.{countColumn}
        + EXCLUDED.{columnName} * EXCLUDED.{countColumn})")})
        / ({tableName}.{countColumn} + EXCLUDED.{countColumn})
      ",
      true
    );
  }

  private static string UpsertMin(
    DbContext context,
    Type aggregateType,
    IEnumerable<string> propertyName
  )
  {
    var columnName = context.GetColumnName(aggregateType, propertyName);
    var tableName = context.GetTableName(aggregateType);

    return AssignValue(
      columnName!,
      $@"LEAST({tableName}.{columnName}, EXCLUDED.{columnName})",
      ShouldClamp(context, aggregateType, propertyName)
    );
  }

  private static string UpsertMinTimestamp(
    DbContext context,
    Type aggregateType,
    IEnumerable<string> propertyName,
    IEnumerable<string> timestampPropertyName
  )
  {
    var columnName = context.GetColumnName(aggregateType, propertyName);
    var timestampColumnName = context.GetColumnName(
      aggregateType,
      timestampPropertyName
    );
    var tableName = context.GetTableName(aggregateType);
    return $@"
      {timestampColumnName} = CASE
        WHEN EXCLUDED.{columnName} < {tableName}.{columnName}
        THEN EXCLUDED.{timestampColumnName}
        ELSE {tableName}.{timestampColumnName}
      END
    ";
  }

  private static string UpsertMax(
    DbContext context,
    Type aggregateType,
    IEnumerable<string> propertyName
  )
  {
    var columnName = context.GetColumnName(aggregateType, propertyName);
    var tableName = context.GetTableName(aggregateType);

    return AssignValue(
      columnName!,
      $@"GREATEST({tableName}.{columnName}, EXCLUDED.{columnName})",
      ShouldClamp(context, aggregateType, propertyName)
    );
  }

  private static string UpsertMaxTimestamp(
    DbContext context,
    Type aggregateType,
    IEnumerable<string> propertyName,
    IEnumerable<string> timestampPropertyName
  )
  {
    var columnName = context.GetColumnName(aggregateType, propertyName);
    var timestampColumnName = context.GetColumnName(
      aggregateType,
      timestampPropertyName
    );
    var tableName = context.GetTableName(aggregateType);
    return $@"
      {timestampColumnName} = CASE
        WHEN EXCLUDED.{columnName} > {tableName}.{columnName}
        THEN EXCLUDED.{timestampColumnName}
        ELSE {tableName}.{timestampColumnName}
      END
    ";
  }

  private static string DerivativePower(
    DbContext context,
    Type aggregateType,
    IEnumerable<string> propertyName,
    IEnumerable<string> minEnergyPropertyName,
    IEnumerable<string> maxEnergyPropertyName
  )
  {
    var columnName = context.GetColumnName(aggregateType, propertyName);
    var minEnergyColumnName = context.GetColumnName(
      aggregateType,
      minEnergyPropertyName
    );
    var maxEnergyColumnName = context.GetColumnName(
      aggregateType,
      maxEnergyPropertyName
    );
    var tableName = context.GetTableName(aggregateType);

    return AssignValue(
      columnName!,
      @$"((GREATEST(
        {tableName}.{maxEnergyColumnName},
        EXCLUDED.{maxEnergyColumnName})
        - LEAST(
        {tableName}.{minEnergyColumnName},
        EXCLUDED.{minEnergyColumnName}))
        * 4)",
      ShouldClamp(context, aggregateType, propertyName)
    );
  }

  private static string DerivativePowerTimestamp(
    DbContext context,
    Type aggregateType,
#pragma warning disable S1172 // Unused method parameters should be removed
    IEnumerable<string> propertyName,
#pragma warning restore S1172 // Unused method parameters should be removed
    IEnumerable<string> timestampPropertyName
  )
  {
    var tableName = context.GetTableName(aggregateType);
    var timestampColumnName = context.GetColumnName(
      aggregateType,
      timestampPropertyName
    );
    var actualTimestampColumnName = context.GetColumnName(
      aggregateType,
      [nameof(IAggregateEntity.Timestamp)]
    );

    return $@"
      {timestampColumnName} = {tableName}.{actualTimestampColumnName}
    ";
  }

  private static string DeriveAverage(
    DbContext context,
    Type aggregateType,
    IEnumerable<string> propertyName,
    string deltaTable,
    string newCountColumn
  )
  {
    var columnName = context.GetColumnName(aggregateType, propertyName);
    var quarterHourCountColumn = context.GetColumnName(
      aggregateType,
      [nameof(IAggregateEntity.QuarterHourCount)]
    );
    var tableName = context.GetTableName(aggregateType);
    var expression =
      $@"
      (
        {ClampNearZeroValues(@$"({tableName}.{columnName} * {tableName}.{quarterHourCountColumn}
        + {deltaTable}.{columnName})")}
        / GREATEST(1,
          {tableName}.{quarterHourCountColumn}
          + {deltaTable}.{newCountColumn})
      )
    ";

    return AssignValue(columnName!, expression, true);
  }

  private static string DeriveMin(
    DbContext context,
    Type aggregateType,
    IEnumerable<string> propertyName,
    string deltaTable
  )
  {
    var columnName = context.GetColumnName(aggregateType, propertyName);
    var tableName = context.GetTableName(aggregateType);

    return AssignValue(
      columnName!,
      $@"LEAST({tableName}.{columnName}, {deltaTable}.{columnName})",
      ShouldClamp(context, aggregateType, propertyName)
    );
  }

  private static string DeriveMinTimestamp(
    DbContext context,
    Type aggregateType,
    IEnumerable<string> propertyName,
    IEnumerable<string> timestampPropertyName,
    string deltaTable
  )
  {
    var columnName = context.GetColumnName(aggregateType, propertyName);
    var timestampColumnName = context.GetColumnName(
      aggregateType,
      timestampPropertyName
    );
    var tableName = context.GetTableName(aggregateType);
    return $@"
      {timestampColumnName} = CASE
        WHEN {deltaTable}.{columnName} < {tableName}.{columnName}
        THEN {deltaTable}.{timestampColumnName}
        ELSE {tableName}.{timestampColumnName}
      END
    ";
  }

  private static string DeriveMax(
    DbContext context,
    Type aggregateType,
    IEnumerable<string> propertyName,
    string deltaTable
  )
  {
    var columnName = context.GetColumnName(aggregateType, propertyName);
    var tableName = context.GetTableName(aggregateType);
    return AssignValue(
      columnName!,
      $@"GREATEST({tableName}.{columnName}, {deltaTable}.{columnName})",
      ShouldClamp(context, aggregateType, propertyName)
    );
  }

  private static string DeriveMaxTimestamp(
    DbContext context,
    Type aggregateType,
    IEnumerable<string> propertyName,
    IEnumerable<string> timestampPropertyName,
    string deltaTable
  )
  {
    var columnName = context.GetColumnName(aggregateType, propertyName);
    var timestampColumnName = context.GetColumnName(
      aggregateType,
      timestampPropertyName
    );
    var tableName = context.GetTableName(aggregateType);
    return $@"
      {timestampColumnName} =
        CASE
          WHEN {deltaTable}.{columnName} > {tableName}.{columnName}
          THEN {deltaTable}.{timestampColumnName}
          ELSE {tableName}.{timestampColumnName}
        END
    ";
  }

  private static string DeltaAverage(
    DbContext context,
    Type aggregateType,
    IEnumerable<string> propertyName,
    string newTable,
    string oldTable
  )
  {
    var columnName = context.GetColumnName(aggregateType, propertyName);

    return SelectValue(
      columnName!,
      $@"SUM({newTable}.{columnName} - COALESCE({oldTable}.{columnName}, 0))",
      ShouldClamp(context, aggregateType, propertyName)
    );
  }

  private static string DeltaMin(
    DbContext context,
    Type aggregateType,
    IEnumerable<string> propertyName,
    string newTable,
    string oldTable
  )
  {
    var columnName = context.GetColumnName(aggregateType, propertyName);
    return SelectValue(
      columnName!,
      $@"MIN(LEAST(
      {newTable}.{columnName},
      COALESCE({oldTable}.{columnName}, {newTable}.{columnName})
    ))",
      ShouldClamp(context, aggregateType, propertyName)
    );
  }

  private static string DeltaMinTimestamp(
    DbContext context,
    Type aggregateType,
    IEnumerable<string> propertyName,
    IEnumerable<string> timestampPropertyName,
    string newTable,
    string oldTable
  )
  {
    var columnName = context.GetColumnName(aggregateType, propertyName);
    var timestampColumnName = context.GetColumnName(
      aggregateType,
      timestampPropertyName
    );
    return $@"
      (ARRAY_AGG(
        CASE WHEN
          {newTable}.{columnName}
          < COALESCE({oldTable}.{columnName}, {newTable}.{columnName})
        THEN {newTable}.{timestampColumnName}
        ELSE COALESCE(
          {oldTable}.{timestampColumnName},
          {newTable}.{timestampColumnName})
        END
        ORDER BY LEAST(
          {newTable}.{columnName},
          COALESCE({oldTable}.{columnName}, {newTable}.{columnName})) ASC
      ))[1] AS {timestampColumnName}
    ";
  }

  private static string DeltaMax(
    DbContext context,
    Type aggregateType,
    IEnumerable<string> propertyName,
    string newTable,
    string oldTable
  )
  {
    var columnName = context.GetColumnName(
      aggregateType,
      propertyName.ToArray()
    );

    return SelectValue(
      columnName!,
      $@"MAX(
        GREATEST(
          {newTable}.{columnName},
          COALESCE({oldTable}.{columnName}, {newTable}.{columnName})
        )
     )",
      ShouldClamp(context, aggregateType, propertyName)
    );
  }

  private static string DeltaMaxTimestamp(
    DbContext context,
    Type aggregateType,
    IEnumerable<string> propertyName,
    IEnumerable<string> timestampPropertyName,
    string newTable,
    string oldTable
  )
  {
    var columnName = context.GetColumnName(aggregateType, propertyName);
    var timestampColumnName = context.GetColumnName(
      aggregateType,
      timestampPropertyName
    );
    return $@"
      (ARRAY_AGG(
        CASE WHEN
          {newTable}.{columnName}
          > COALESCE({oldTable}.{columnName}, {newTable}.{columnName})
        THEN {newTable}.{timestampColumnName}
        ELSE
          COALESCE(
            {oldTable}.{timestampColumnName},
            {newTable}.{timestampColumnName})
        END
        ORDER BY GREATEST(
          {newTable}.{columnName},
          COALESCE({oldTable}.{columnName}, {newTable}.{columnName})) DESC
      ))[1] AS {timestampColumnName}
    ";
  }
}

using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Extensions;
using Ozds.Data.Procedures.Parts;

namespace Ozds.Data.Procedures.Compilers;

public static class MeasurementProcedureCompiler
{
  private const float FloatEpsilon = 1e-12f;

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

  private static string RealResultSafeGuard(string valueToSafeGuard, float epsilonValue = FloatEpsilon)
  {
    var eps = epsilonValue.ToString("G17", CultureInfo.InvariantCulture);

    return $@"
      (
        SELECT CASE
          WHEN ABS((v)::double precision) < {eps}
          ELSE COALESCE(v, 0)
        END
        FROM (VALUES (({valueToSafeGuard}))) AS _safeguard(v)
      )
   ";
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

    var ret_value = $@"
      {columnName} = ({RealResultSafeGuard($@"({tableName}.{columnName} * {tableName}.{countColumn}
        + EXCLUDED.{columnName} * EXCLUDED.{countColumn})")})
        / ({tableName}.{countColumn} + EXCLUDED.{countColumn})
    ";

    var guarded_value = RealResultSafeGuard(ret_value);
    return guarded_value;
  }

  private static string UpsertMin(
    DbContext context,
    Type aggregateType,
    IEnumerable<string> propertyName
  )
  {
    var columnName = context.GetColumnName(aggregateType, propertyName);
    var tableName = context.GetTableName(aggregateType);

    return RealResultSafeGuard($@"
      {columnName} = LEAST({tableName}.{columnName}, EXCLUDED.{columnName})
    ");
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
    return RealResultSafeGuard($@"
      {columnName} = GREATEST({tableName}.{columnName}, EXCLUDED.{columnName})
    ");
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
    return RealResultSafeGuard($@"
      {columnName} = (GREATEST(
        {tableName}.{maxEnergyColumnName},
        EXCLUDED.{maxEnergyColumnName})
        - LEAST(
        {tableName}.{minEnergyColumnName},
        EXCLUDED.{minEnergyColumnName}))
        * 4
    ");
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
    var ret_value = $@"
      {columnName} =
        {RealResultSafeGuard(@$"({tableName}.{columnName} * {tableName}.{quarterHourCountColumn}
        + {deltaTable}.{columnName})")}
        / GREATEST(1,
          {tableName}.{quarterHourCountColumn}
          + {deltaTable}.{newCountColumn})
    ";

    var guarded_value = RealResultSafeGuard(ret_value);
    return guarded_value;
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
    return RealResultSafeGuard($@"
      {columnName} = LEAST({tableName}.{columnName}, {deltaTable}.{columnName})
    ");
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
    return $@"
      {columnName} =
        GREATEST({tableName}.{columnName}, {deltaTable}.{columnName})
    ";
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
    return RealResultSafeGuard($@"
      SUM({newTable}.{columnName} - COALESCE({oldTable}.{columnName}, 0))
        AS {columnName}
    ");
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
    return RealResultSafeGuard($@"
      MIN(LEAST(
        {newTable}.{columnName},
        COALESCE({oldTable}.{columnName}, {newTable}.{columnName})))
        AS {columnName}
    ");
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
    return RealResultSafeGuard($@"
      MAX(GREATEST(
        {newTable}.{columnName},
        COALESCE({oldTable}.{columnName}, {newTable}.{columnName})))
        AS {columnName}
    ");
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

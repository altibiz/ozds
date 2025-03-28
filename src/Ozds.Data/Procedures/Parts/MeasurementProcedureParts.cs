using System.Linq.Expressions;

namespace Ozds.Data.Procedures.Parts;

public interface IMeasurementProcedurePart
{
  public MemberExpression Value { get; }
}

public interface IUpsertMeasurementProcedurePart : IMeasurementProcedurePart
{
}

public interface
  IUpsertAverageMeasurementProcedurePart : IUpsertMeasurementProcedurePart
{
}

public interface
  IUpsertMinMeasurementProcedurePart : IUpsertMeasurementProcedurePart
{
}

public interface
  IUpsertMinTimestampMeasurementProcedurePart : IUpsertMeasurementProcedurePart
{
  public MemberExpression Timestamp { get; }
}

public interface
  IUpsertMaxMeasurementProcedurePart : IUpsertMeasurementProcedurePart
{
}

public interface
  IUpsertMaxTimestampMeasurementProcedurePart : IUpsertMeasurementProcedurePart
{
  public MemberExpression Timestamp { get; }
}

public interface IDerivativeMeasurementProcedurePart : IMeasurementProcedurePart
{
}

public interface
  IDerivativePowerMeasurementProcedurePart : IDerivativeMeasurementProcedurePart
{
  public MemberExpression MinEnergy { get; }

  public MemberExpression MaxEnergy { get; }
}

public interface
  IDerivativePowerTimestampMeasurementProcedurePart :
  IDerivativeMeasurementProcedurePart
{
  public MemberExpression Timestamp { get; }
}

public interface IDeriveMeasurementProcedurePart : IMeasurementProcedurePart
{
}

public interface
  IDeriveAverageMeasurementProcedurePart : IDeriveMeasurementProcedurePart
{
}

public interface
  IDeriveMinMeasurementProcedurePart : IDeriveMeasurementProcedurePart
{
}

public interface
  IDeriveMinTimestampMeasurementProcedurePart : IDeriveMeasurementProcedurePart
{
  public MemberExpression Timestamp { get; }
}

public interface
  IDeriveMaxMeasurementProcedurePart : IDeriveMeasurementProcedurePart
{
}

public interface
  IDeriveMaxTimestampMeasurementProcedurePart : IDeriveMeasurementProcedurePart
{
  public MemberExpression Timestamp { get; }
}

public interface IDeltaMeasurementProcedurePart : IMeasurementProcedurePart
{
}

public interface
  IDeltaAverageMeasurementProcedurePart : IDeltaMeasurementProcedurePart
{
}

public interface
  IDeltaMinMeasurementProcedurePart : IDeltaMeasurementProcedurePart
{
}

public interface
  IDeltaMinTimestampMeasurementProcedurePart : IDeltaMeasurementProcedurePart
{
  public MemberExpression Timestamp { get; }
}

public interface
  IDeltaMaxMeasurementProcedurePart : IDeltaMeasurementProcedurePart
{
}

public interface
  IDeltaMaxTimestampMeasurementProcedurePart : IDeltaMeasurementProcedurePart
{
  public MemberExpression Timestamp { get; }
}

public interface IMeasurementProcedureParts
{
  public Type AggregateType { get; }

  public IEnumerable<IUpsertMeasurementProcedurePart>
    UpsertMeasurementProcedureParts { get; }

  public IEnumerable<IDeriveMeasurementProcedurePart>
    DeriveMeasurementProcedureParts { get; }

  public IEnumerable<IDerivativeMeasurementProcedurePart>
    DerivativeMeasurementProcedureParts { get; }

  public IEnumerable<IDeltaMeasurementProcedurePart>
    DeltaMeasurementProcedureParts { get; }
}

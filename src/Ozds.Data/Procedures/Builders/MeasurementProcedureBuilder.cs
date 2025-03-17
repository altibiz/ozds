using System.Linq.Expressions;
using Ozds.Data.Extensions;
using Ozds.Data.Procedures.Parts;

namespace Ozds.Data.Procedures.Builders;

public abstract class MeasurementProcedureParts<TEntity>
  : IMeasurementProcedureParts
{
  private readonly MeasurementProcedureBuilder<TEntity> builder =
    new MeasurementProcedureBuilder<TEntity>()
      .AggregateType(typeof(TEntity));

  private bool configured;

  public Type AggregateType
  {
    get { return Build().AggregateType; }
  }

  public IEnumerable<IUpsertMeasurementProcedurePart>
    UpsertMeasurementProcedureParts
  {
    get { return Build().UpsertMeasurementProcedureParts; }
  }

  public IEnumerable<IDerivativeMeasurementProcedurePart>
    DerivativeMeasurementProcedureParts
  {
    get { return Build().DerivativeMeasurementProcedureParts; }
  }

  public IEnumerable<IDeriveMeasurementProcedurePart>
    DeriveMeasurementProcedureParts
  {
    get { return Build().DeriveMeasurementProcedureParts; }
  }

  public IEnumerable<IDeltaMeasurementProcedurePart>
    DeltaMeasurementProcedureParts
  {
    get { return Build().DeltaMeasurementProcedureParts; }
  }

  protected abstract void Configure(
    MeasurementProcedureBuilder<TEntity> builder
  );

  private IMeasurementProcedureParts Build()
  {
    if (!configured)
    {
      Configure(builder);
      configured = true;
    }

    return builder.Build();
  }
}

public class MeasurementProcedureBuilder<TEntity>
{
  private readonly MeasurementsProcedurePartsImpl impl = new();

  public MeasurementProcedureBuilder<TEntity> AggregateType(
    Type aggregateType
  )
  {
    impl.AggregateType = aggregateType;
    return this;
  }

  public MeasurementProcedureBuilder<TEntity> UpsertAverage<TReturn>(
    Expression<Func<TEntity, TReturn>> value
  )
  {
    impl.MeasurementProcedureParts.Add(
      new UpsertAverageMeasurementProcedurePartImpl
      {
        Value = value.ToMemberExpression()
      });
    return this;
  }

  public MeasurementProcedureBuilder<TEntity> UpsertMin<TReturn>(
    Expression<Func<TEntity, TReturn>> value
  )
  {
    impl.MeasurementProcedureParts.Add(
      new UpsertMinMeasurementProcedurePartImpl
      {
        Value = value.ToMemberExpression()
      });
    return this;
  }

  public MeasurementProcedureBuilder<TEntity> UpsertMinTimestamp<TReturn>(
    Expression<Func<TEntity, TReturn>> value,
    Expression<Func<TEntity, DateTimeOffset>> timestamp
  )
  {
    impl.MeasurementProcedureParts.Add(
      new UpsertMinTimestampMeasurementProcedurePartImpl
      {
        Value = value.ToMemberExpression(),
        Timestamp = timestamp.ToMemberExpression()
      });
    return this;
  }

  public MeasurementProcedureBuilder<TEntity> UpsertMax<TReturn>(
    Expression<Func<TEntity, TReturn>> value
  )
  {
    impl.MeasurementProcedureParts.Add(
      new UpsertMaxMeasurementProcedurePartImpl
      {
        Value = value.ToMemberExpression()
      });
    return this;
  }

  public MeasurementProcedureBuilder<TEntity> UpsertMaxTimestamp<TReturn>(
    Expression<Func<TEntity, TReturn>> value,
    Expression<Func<TEntity, DateTimeOffset>> timestamp
  )
  {
    impl.MeasurementProcedureParts.Add(
      new UpsertMaxTimestampMeasurementProcedurePartImpl
      {
        Value = value.ToMemberExpression(),
        Timestamp = timestamp.ToMemberExpression()
      });
    return this;
  }

  public MeasurementProcedureBuilder<TEntity> DeltaAverage<TReturn>(
    Expression<Func<TEntity, TReturn>> value
  )
  {
    impl.MeasurementProcedureParts.Add(
      new DeltaAverageMeasurementProcedurePartImpl
      {
        Value = value.ToMemberExpression()
      });
    return this;
  }

  public MeasurementProcedureBuilder<TEntity> DeltaMin<TReturn>(
    Expression<Func<TEntity, TReturn>> value
  )
  {
    impl.MeasurementProcedureParts.Add(
      new DeltaMinMeasurementProcedurePartImpl
      {
        Value = value.ToMemberExpression()
      });
    return this;
  }

  public MeasurementProcedureBuilder<TEntity> DeltaMinTimestamp<TReturn>(
    Expression<Func<TEntity, TReturn>> value,
    Expression<Func<TEntity, DateTimeOffset>> timestamp
  )
  {
    impl.MeasurementProcedureParts.Add(
      new DeltaMinTimestampMeasurementProcedurePartImpl
      {
        Value = value.ToMemberExpression(),
        Timestamp = timestamp.ToMemberExpression()
      });
    return this;
  }

  public MeasurementProcedureBuilder<TEntity> DeltaMax<TReturn>(
    Expression<Func<TEntity, TReturn>> value
  )
  {
    impl.MeasurementProcedureParts.Add(
      new DeltaMaxMeasurementProcedurePartImpl
      {
        Value = value.ToMemberExpression()
      });
    return this;
  }

  public MeasurementProcedureBuilder<TEntity> DeltaMaxTimestamp<TReturn>(
    Expression<Func<TEntity, TReturn>> value,
    Expression<Func<TEntity, DateTimeOffset>> timestamp
  )
  {
    impl.MeasurementProcedureParts.Add(
      new DeltaMaxTimestampMeasurementProcedurePartImpl
      {
        Value = value.ToMemberExpression(),
        Timestamp = timestamp.ToMemberExpression()
      });
    return this;
  }

  public MeasurementProcedureBuilder<TEntity> DerivativePower<TValue, TEnergy>(
    Expression<Func<TEntity, TValue>> value,
    Expression<Func<TEntity, TEnergy>> minEnergy,
    Expression<Func<TEntity, TEnergy>> maxEnergy
  )
  {
    impl.MeasurementProcedureParts.Add(
      new DerivativePowerMeasurementProcedurePartImpl
      {
        Value = value.ToMemberExpression(),
        MinEnergy = minEnergy.ToMemberExpression(),
        MaxEnergy = maxEnergy.ToMemberExpression()
      });
    return this;
  }

  public MeasurementProcedureBuilder<TEntity> DerivativePowerTimestamp<TValue>(
    Expression<Func<TEntity, TValue>> value,
    Expression<Func<TEntity, DateTimeOffset>> timestamp
  )
  {
    impl.MeasurementProcedureParts.Add(
      new DerivativePowerTimestampMeasurementProcedurePartImpl
      {
        Value = value.ToMemberExpression(),
        Timestamp = timestamp.ToMemberExpression()
      });
    return this;
  }

  public MeasurementProcedureBuilder<TEntity> DeriveAverage<TReturn>(
    Expression<Func<TEntity, TReturn>> value
  )
  {
    impl.MeasurementProcedureParts.Add(
      new DeriveAverageMeasurementProcedurePartImpl
      {
        Value = value.ToMemberExpression()
      });
    return this;
  }

  public MeasurementProcedureBuilder<TEntity> DeriveMin<TReturn>(
    Expression<Func<TEntity, TReturn>> value
  )
  {
    impl.MeasurementProcedureParts.Add(
      new DeriveMinMeasurementProcedurePartImpl
      {
        Value = value.ToMemberExpression()
      });
    return this;
  }

  public MeasurementProcedureBuilder<TEntity> DeriveMinTimestamp<TReturn>(
    Expression<Func<TEntity, TReturn>> value,
    Expression<Func<TEntity, DateTimeOffset>> timestamp
  )
  {
    impl.MeasurementProcedureParts.Add(
      new DeriveMinTimestampMeasurementProcedurePartImpl
      {
        Value = value.ToMemberExpression(),
        Timestamp = timestamp.ToMemberExpression()
      });
    return this;
  }

  public MeasurementProcedureBuilder<TEntity> DeriveMax<TReturn>(
    Expression<Func<TEntity, TReturn>> value
  )
  {
    impl.MeasurementProcedureParts.Add(
      new DeriveMaxMeasurementProcedurePartImpl
      {
        Value = value.ToMemberExpression()
      });
    return this;
  }

  public MeasurementProcedureBuilder<TEntity> DeriveMaxTimestamp<TReturn>(
    Expression<Func<TEntity, TReturn>> value,
    Expression<Func<TEntity, DateTimeOffset>> timestamp
  )
  {
    impl.MeasurementProcedureParts.Add(
      new DeriveMaxTimestampMeasurementProcedurePartImpl
      {
        Value = value.ToMemberExpression(),
        Timestamp = timestamp.ToMemberExpression()
      });
    return this;
  }

  public IMeasurementProcedureParts Build()
  {
    return impl;
  }

  internal object DeriveMax(object value)
  {
    throw new NotImplementedException();
  }

  private sealed class MeasurementsProcedurePartsImpl
    : IMeasurementProcedureParts
  {
    public List<IMeasurementProcedurePart>
      MeasurementProcedureParts { get; } = new();

    public Type? AggregateType { get; set; }

    Type IMeasurementProcedureParts.AggregateType
    {
      get
      {
        return AggregateType
          ?? throw new InvalidOperationException("Aggregate type not set.");
      }
    }

    IEnumerable<IUpsertMeasurementProcedurePart>
      IMeasurementProcedureParts.UpsertMeasurementProcedureParts
    {
      get
      {
        return MeasurementProcedureParts
          .OfType<IUpsertMeasurementProcedurePart>();
      }
    }

    IEnumerable<IDerivativeMeasurementProcedurePart>
      IMeasurementProcedureParts.DerivativeMeasurementProcedureParts
    {
      get
      {
        return MeasurementProcedureParts
          .OfType<IDerivativeMeasurementProcedurePart>();
      }
    }

    IEnumerable<IDeltaMeasurementProcedurePart>
      IMeasurementProcedureParts.DeltaMeasurementProcedureParts
    {
      get
      {
        return MeasurementProcedureParts
          .OfType<IDeltaMeasurementProcedurePart>();
      }
    }

    IEnumerable<IDeriveMeasurementProcedurePart>
      IMeasurementProcedureParts.DeriveMeasurementProcedureParts
    {
      get
      {
        return MeasurementProcedureParts
          .OfType<IDeriveMeasurementProcedurePart>();
      }
    }
  }

  private abstract class MeasurementProcedurePartImpl
    : IMeasurementProcedurePart
  {
    public required MemberExpression Value { get; set; }
  }

  private abstract class UpsertMeasurementProcedurePartImpl
    : MeasurementProcedurePartImpl, IUpsertMeasurementProcedurePart
  {
  }

  private sealed class UpsertAverageMeasurementProcedurePartImpl
    : UpsertMeasurementProcedurePartImpl,
      IUpsertAverageMeasurementProcedurePart
  {
  }

  private sealed class UpsertMinMeasurementProcedurePartImpl
    : UpsertMeasurementProcedurePartImpl, IUpsertMinMeasurementProcedurePart
  {
  }

  private sealed class UpsertMinTimestampMeasurementProcedurePartImpl
    : UpsertMeasurementProcedurePartImpl,
      IUpsertMinTimestampMeasurementProcedurePart
  {
    public required MemberExpression Timestamp { get; set; }
  }

  private sealed class UpsertMaxMeasurementProcedurePartImpl
    : UpsertMeasurementProcedurePartImpl,
      IUpsertMaxMeasurementProcedurePart
  {
  }

  private sealed class UpsertMaxTimestampMeasurementProcedurePartImpl
    : UpsertMeasurementProcedurePartImpl,
      IUpsertMaxTimestampMeasurementProcedurePart
  {
    public required MemberExpression Timestamp { get; set; }
  }

  private abstract class DerivativeMeasurementProcedurePartImpl
    : MeasurementProcedurePartImpl, IDerivativeMeasurementProcedurePart
  {
  }

  private sealed class DerivativePowerMeasurementProcedurePartImpl
    : DerivativeMeasurementProcedurePartImpl,
      IDerivativePowerMeasurementProcedurePart
  {
    public required MemberExpression MinEnergy { get; set; }

    public required MemberExpression MaxEnergy { get; set; }
  }

  private sealed class DerivativePowerTimestampMeasurementProcedurePartImpl
    : DerivativeMeasurementProcedurePartImpl,
      IDerivativePowerTimestampMeasurementProcedurePart
  {
    public required MemberExpression Timestamp { get; set; }
  }

  private abstract class DeriveMeasurementProcedurePartImpl
    : MeasurementProcedurePartImpl, IDeriveMeasurementProcedurePart
  {
  }

  private sealed class DeriveAverageMeasurementProcedurePartImpl
    : DeriveMeasurementProcedurePartImpl,
      IDeriveAverageMeasurementProcedurePart
  {
  }

  private sealed class DeriveMinMeasurementProcedurePartImpl
    : DeriveMeasurementProcedurePartImpl, IDeriveMinMeasurementProcedurePart
  {
  }

  private sealed class DeriveMinTimestampMeasurementProcedurePartImpl
    : DeriveMeasurementProcedurePartImpl,
      IDeriveMinTimestampMeasurementProcedurePart
  {
    public required MemberExpression Timestamp { get; set; }
  }

  private sealed class DeriveMaxMeasurementProcedurePartImpl
    : DeriveMeasurementProcedurePartImpl, IDeriveMaxMeasurementProcedurePart
  {
  }

  private sealed class DeriveMaxTimestampMeasurementProcedurePartImpl
    : DeriveMeasurementProcedurePartImpl,
      IDeriveMaxTimestampMeasurementProcedurePart
  {
    public required MemberExpression Timestamp { get; set; }
  }

  private abstract class DeltaMeasurementProcedurePartImpl
    : MeasurementProcedurePartImpl, IDeltaMeasurementProcedurePart
  {
  }

  private sealed class DeltaAverageMeasurementProcedurePartImpl
    : DeltaMeasurementProcedurePartImpl,
      IDeltaAverageMeasurementProcedurePart
  {
  }

  private sealed class DeltaMinMeasurementProcedurePartImpl
    : DeltaMeasurementProcedurePartImpl, IDeltaMinMeasurementProcedurePart
  {
  }

  private sealed class DeltaMinTimestampMeasurementProcedurePartImpl
    : DeltaMeasurementProcedurePartImpl,
      IDeltaMinTimestampMeasurementProcedurePart
  {
    public required MemberExpression Timestamp { get; set; }
  }

  private sealed class DeltaMaxMeasurementProcedurePartImpl
    : DeltaMeasurementProcedurePartImpl, IDeltaMaxMeasurementProcedurePart
  {
  }

  private sealed class DeltaMaxTimestampMeasurementProcedurePartImpl
    : DeltaMeasurementProcedurePartImpl,
      IDeltaMaxTimestampMeasurementProcedurePart
  {
    public required MemberExpression Timestamp { get; set; }
  }
}

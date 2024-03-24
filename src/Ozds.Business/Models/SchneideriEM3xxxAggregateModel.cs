using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

using IUpsertAggregate = IUpsertAggregate<SchneideriEM3xxxAggregateModel>;

public record SchneideriEM3xxxAggregateModel(
  string MeterId,
  DateTimeOffset Timestamp,
  IntervalModel Interval,
  long Count,
  float VoltageL1AnyT0Avg_V,
  float VoltageL2AnyT0Avg_V,
  float VoltageL3AnyT0Avg_V,
  float CurrentL1AnyT0Avg_A,
  float CurrentL2AnyT0Avg_A,
  float CurrentL3AnyT0Avg_A,
  float ActivePowerL1NetT0Avg_W,
  float ActivePowerL2NetT0Avg_W,
  float ActivePowerL3NetT0Avg_W,
  float ReactivePowerTotalNetT0Avg_VAR,
  float ApparentPowerTotalNetT0Avg_VA,
  float ActiveEnergyTotalImportT0Min_Wh,
  float ActiveEnergyTotalImportT0Max_Wh,
  float ActiveEnergyTotalExportT0Min_Wh,
  float ActiveEnergyTotalExportT0Max_Wh,
  float ReactiveEnergyTotalImportT0Min_VARh,
  float ReactiveEnergyTotalImportT0Max_VARh,
  float ReactiveEnergyTotalExportT0Min_VARh,
  float ReactiveEnergyTotalExportT0Max_VARh,
  float ActiveEnergyTotalImportT1Min_Wh,
  float ActiveEnergyTotalImportT1Max_Wh,
  float ActiveEnergyTotalImportT2Min_Wh,
  float ActiveEnergyTotalImportT2Max_Wh
) : AggregateModel(
  MeterId: MeterId,
  Timestamp: Timestamp,
  Interval: Interval,
  Count: Count
), IUpsertAggregate
{
  public override object ToDbEntity()
  {
    return this.ToEntity();
  }

  static IUpsertAggregate.UpsertExpressionHolder IUpsertAggregate.UpsertExpression => _upsertExpression.Value;

  static IUpsertAggregate.UpsertHolder IUpsertAggregate.Upsert => _upsert.Value;

  public override TariffMeasure Current_A
  {
    get
    {
      return new UnaryTariffMeasure(
        new AnyDuplexMeasure(
          new TriPhasicMeasure(
            CurrentL1AnyT0Avg_A,
            CurrentL2AnyT0Avg_A,
            CurrentL3AnyT0Avg_A
          )
        )
      );
    }
  }

  public override TariffMeasure Voltage_V
  {
    get
    {
      return new UnaryTariffMeasure(
        new AnyDuplexMeasure(
          new TriPhasicMeasure(
            VoltageL1AnyT0Avg_V,
            VoltageL2AnyT0Avg_V,
            VoltageL3AnyT0Avg_V
          )
        )
      );
    }
  }

  public override TariffMeasure ActivePower_W
  {
    get
    {
      return new CompositeTariffMeasure(new()
      {
        base.ActivePower_W,
        new UnaryTariffMeasure(
          new NetDuplexMeasure(
            new TriPhasicMeasure(
              ActivePowerL1NetT0Avg_W,
              ActivePowerL2NetT0Avg_W,
              ActivePowerL3NetT0Avg_W
            )
          )
        )
      });
    }
  }

  public override TariffMeasure ReactivePower_VAR
  {
    get
    {
      return new CompositeTariffMeasure(new()
      {
        base.ReactivePower_VAR,
        new UnaryTariffMeasure(
          new NetDuplexMeasure(
            new SinglePhasicMeasure(
              ReactivePowerTotalNetT0Avg_VAR
            )
          )
        )
      });
    }
  }

  public override TariffMeasure ApparentPower_VA
  {
    get
    {
      return new CompositeTariffMeasure(new()
      {
        base.ApparentPower_VA,
        new UnaryTariffMeasure(
          new NetDuplexMeasure(
            new SinglePhasicMeasure(
              ApparentPowerTotalNetT0Avg_VA
            )
          )
        )
      });
    }
  }

  public override SpanningMeasure ActiveEnergySpan_Wh
  {
    get
    {
      return new MinMaxSpanningMeasure(
        new UnaryTariffMeasure(
          new ImportExportDuplexMeasure(
            new SinglePhasicMeasure(ActiveEnergyTotalImportT0Min_Wh),
            new SinglePhasicMeasure(ActiveEnergyTotalExportT0Min_Wh)
          )
        ),
        new UnaryTariffMeasure(
          new ImportExportDuplexMeasure(
            new SinglePhasicMeasure(ActiveEnergyTotalImportT0Max_Wh),
            new SinglePhasicMeasure(ActiveEnergyTotalExportT0Max_Wh)
          )
        )
      );
    }
  }

  public override SpanningMeasure ReactiveEnergySpan_VARh
  {
    get
    {
      return new MinMaxSpanningMeasure(
        new UnaryTariffMeasure(
          new ImportExportDuplexMeasure(
            new SinglePhasicMeasure(ReactiveEnergyTotalImportT0Min_VARh),
            new SinglePhasicMeasure(ReactiveEnergyTotalExportT0Min_VARh)
          )
        ),
        new UnaryTariffMeasure(
          new ImportExportDuplexMeasure(
            new SinglePhasicMeasure(ReactiveEnergyTotalImportT0Max_VARh),
            new SinglePhasicMeasure(ReactiveEnergyTotalExportT0Max_VARh)
          )
        )
      );
    }
  }

  public override SpanningMeasure ApparentEnergySpan_VAh
  {
    get { return SpanningMeasure.Null; }
  }

  public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
  {
    throw new NotImplementedException();
  }

  private static readonly Lazy<IUpsertAggregate.UpsertExpressionHolder> _upsertExpression =
    new(() =>
      new((SchneideriEM3xxxAggregateModel lhs, SchneideriEM3xxxAggregateModel rhs) =>
        new(
          lhs.MeterId,
          lhs.Timestamp,
          lhs.Interval,
          lhs.Count + rhs.Count,
          (lhs.VoltageL1AnyT0Avg_V * lhs.Count
            + rhs.VoltageL1AnyT0Avg_V * rhs.Count)
            / (lhs.Count + rhs.Count),
          (lhs.VoltageL2AnyT0Avg_V * lhs.Count
            + rhs.VoltageL2AnyT0Avg_V * rhs.Count)
            / (lhs.Count + rhs.Count),
          (lhs.VoltageL3AnyT0Avg_V * lhs.Count
            + rhs.VoltageL3AnyT0Avg_V * rhs.Count)
            / (lhs.Count + rhs.Count),
          (lhs.CurrentL1AnyT0Avg_A * lhs.Count
            + rhs.CurrentL1AnyT0Avg_A * rhs.Count)
            / (lhs.Count + rhs.Count),
          (lhs.CurrentL2AnyT0Avg_A * lhs.Count
            + rhs.CurrentL2AnyT0Avg_A * rhs.Count)
            / (lhs.Count + rhs.Count),
          (lhs.CurrentL3AnyT0Avg_A * lhs.Count
            + rhs.CurrentL3AnyT0Avg_A * rhs.Count)
            / (lhs.Count + rhs.Count),
          (lhs.ActivePowerL1NetT0Avg_W * lhs.Count
            + rhs.ActivePowerL1NetT0Avg_W * rhs.Count)
            / (lhs.Count + rhs.Count),
          (lhs.ActivePowerL2NetT0Avg_W * lhs.Count
            + rhs.ActivePowerL2NetT0Avg_W * rhs.Count)
            / (lhs.Count + rhs.Count),
          (lhs.ActivePowerL3NetT0Avg_W * lhs.Count
            + rhs.ActivePowerL3NetT0Avg_W * rhs.Count)
            / (lhs.Count + rhs.Count),
          (lhs.ReactivePowerTotalNetT0Avg_VAR * lhs.Count
            + rhs.ReactivePowerTotalNetT0Avg_VAR * rhs.Count)
            / (lhs.Count + rhs.Count),
          (lhs.ApparentPowerTotalNetT0Avg_VA * lhs.Count
            + rhs.ApparentPowerTotalNetT0Avg_VA * rhs.Count)
            / (lhs.Count + rhs.Count),
          lhs.ActiveEnergyTotalImportT0Min_Wh >
            rhs.ActiveEnergyTotalImportT0Min_Wh
            ? rhs.ActiveEnergyTotalImportT0Min_Wh
            : lhs.ActiveEnergyTotalImportT0Min_Wh,
          lhs.ActiveEnergyTotalImportT0Max_Wh <
            rhs.ActiveEnergyTotalImportT0Max_Wh
            ? rhs.ActiveEnergyTotalImportT0Max_Wh
            : lhs.ActiveEnergyTotalImportT0Max_Wh,
          lhs.ActiveEnergyTotalExportT0Min_Wh >
            rhs.ActiveEnergyTotalExportT0Min_Wh
            ? rhs.ActiveEnergyTotalExportT0Min_Wh
            : lhs.ActiveEnergyTotalExportT0Min_Wh,
          lhs.ActiveEnergyTotalExportT0Max_Wh <
            rhs.ActiveEnergyTotalExportT0Max_Wh
            ? rhs.ActiveEnergyTotalExportT0Max_Wh
            : lhs.ActiveEnergyTotalExportT0Max_Wh,
          lhs.ReactiveEnergyTotalImportT0Min_VARh >
            rhs.ReactiveEnergyTotalImportT0Min_VARh
            ? rhs.ReactiveEnergyTotalImportT0Min_VARh
            : lhs.ReactiveEnergyTotalImportT0Min_VARh,
          lhs.ReactiveEnergyTotalImportT0Max_VARh <
            rhs.ReactiveEnergyTotalImportT0Max_VARh
            ? rhs.ReactiveEnergyTotalImportT0Max_VARh
            : lhs.ReactiveEnergyTotalImportT0Max_VARh,
          lhs.ReactiveEnergyTotalExportT0Min_VARh >
            rhs.ReactiveEnergyTotalExportT0Min_VARh
            ? rhs.ReactiveEnergyTotalExportT0Min_VARh
            : lhs.ReactiveEnergyTotalExportT0Min_VARh,
          lhs.ReactiveEnergyTotalExportT0Max_VARh <
            rhs.ReactiveEnergyTotalExportT0Max_VARh
            ? rhs.ReactiveEnergyTotalExportT0Max_VARh
            : lhs.ReactiveEnergyTotalExportT0Max_VARh,
          lhs.ActiveEnergyTotalImportT1Min_Wh >
            rhs.ActiveEnergyTotalImportT1Min_Wh
            ? rhs.ActiveEnergyTotalImportT1Min_Wh
            : lhs.ActiveEnergyTotalImportT1Min_Wh,
          lhs.ActiveEnergyTotalImportT1Max_Wh <
            rhs.ActiveEnergyTotalImportT1Max_Wh
            ? rhs.ActiveEnergyTotalImportT1Max_Wh
            : lhs.ActiveEnergyTotalImportT1Max_Wh,
          lhs.ActiveEnergyTotalImportT2Min_Wh >
            rhs.ActiveEnergyTotalImportT2Min_Wh
            ? rhs.ActiveEnergyTotalImportT2Min_Wh
            : lhs.ActiveEnergyTotalImportT2Min_Wh,
          lhs.ActiveEnergyTotalImportT2Max_Wh <
            rhs.ActiveEnergyTotalImportT2Max_Wh
            ? rhs.ActiveEnergyTotalImportT2Max_Wh
            : lhs.ActiveEnergyTotalImportT2Max_Wh
        )
      )
    );

  private static readonly Lazy<IUpsertAggregate.UpsertHolder> _upsert =
    new(() => new(_upsertExpression.Value.Value.Compile()));
}

public static class SchneideriEM3xxxAggregateModelExtensions
{
  public static SchneideriEM3xxxAggregateEntity ToEntity(
    this SchneideriEM3xxxAggregateModel model) =>
    new()
    {
      MeterId = model.MeterId,
      Timestamp = model.Timestamp,
      Interval = model.Interval.ToEntity(),
      Count = model.Count,
      VoltageL1AnyT0Avg_V = model.VoltageL1AnyT0Avg_V,
      VoltageL2AnyT0Avg_V = model.VoltageL2AnyT0Avg_V,
      VoltageL3AnyT0Avg_V = model.VoltageL3AnyT0Avg_V,
      CurrentL1AnyT0Avg_A = model.CurrentL1AnyT0Avg_A,
      CurrentL2AnyT0Avg_A = model.CurrentL2AnyT0Avg_A,
      CurrentL3AnyT0Avg_A = model.CurrentL3AnyT0Avg_A,
      ActivePowerL1NetT0Avg_W = model.ActivePowerL1NetT0Avg_W,
      ActivePowerL2NetT0Avg_W = model.ActivePowerL2NetT0Avg_W,
      ActivePowerL3NetT0Avg_W = model.ActivePowerL3NetT0Avg_W,
      ReactivePowerTotalNetT0Avg_VAR = model.ReactivePowerTotalNetT0Avg_VAR,
      ApparentPowerTotalNetT0Avg_VA = model.ApparentPowerTotalNetT0Avg_VA,
      ActiveEnergyTotalImportT0Min_Wh = model.ActiveEnergyTotalImportT0Min_Wh,
      ActiveEnergyTotalImportT0Max_Wh = model.ActiveEnergyTotalImportT0Max_Wh,
      ActiveEnergyTotalExportT0Min_Wh = model.ActiveEnergyTotalExportT0Min_Wh,
      ActiveEnergyTotalExportT0Max_Wh = model.ActiveEnergyTotalExportT0Max_Wh,
      ReactiveEnergyTotalImportT0Min_VARh = model.ReactiveEnergyTotalImportT0Min_VARh,
      ReactiveEnergyTotalImportT0Max_VARh = model.ReactiveEnergyTotalImportT0Max_VARh,
      ReactiveEnergyTotalExportT0Min_VARh = model.ReactiveEnergyTotalExportT0Min_VARh,
      ReactiveEnergyTotalExportT0Max_VARh = model.ReactiveEnergyTotalExportT0Max_VARh,
      ActiveEnergyTotalImportT1Min_Wh = model.ActiveEnergyTotalImportT1Min_Wh,
      ActiveEnergyTotalImportT1Max_Wh = model.ActiveEnergyTotalImportT1Max_Wh,
      ActiveEnergyTotalImportT2Min_Wh = model.ActiveEnergyTotalImportT2Min_Wh,
      ActiveEnergyTotalImportT2Max_Wh = model.ActiveEnergyTotalImportT2Max_Wh
    };

  public static SchneideriEM3xxxAggregateModel ToModel(
    this SchneideriEM3xxxAggregateEntity entity) =>
    new(
      entity.MeterId,
      entity.Timestamp,
      entity.Interval.ToModel(),
      entity.Count,
      entity.VoltageL1AnyT0Avg_V,
      entity.VoltageL2AnyT0Avg_V,
      entity.VoltageL3AnyT0Avg_V,
      entity.CurrentL1AnyT0Avg_A,
      entity.CurrentL2AnyT0Avg_A,
      entity.CurrentL3AnyT0Avg_A,
      entity.ActivePowerL1NetT0Avg_W,
      entity.ActivePowerL2NetT0Avg_W,
      entity.ActivePowerL3NetT0Avg_W,
      entity.ReactivePowerTotalNetT0Avg_VAR,
      entity.ApparentPowerTotalNetT0Avg_VA,
      entity.ActiveEnergyTotalImportT0Min_Wh,
      entity.ActiveEnergyTotalImportT0Max_Wh,
      entity.ActiveEnergyTotalExportT0Min_Wh,
      entity.ActiveEnergyTotalExportT0Max_Wh,
      entity.ReactiveEnergyTotalImportT0Min_VARh,
      entity.ReactiveEnergyTotalImportT0Max_VARh,
      entity.ReactiveEnergyTotalExportT0Min_VARh,
      entity.ReactiveEnergyTotalExportT0Max_VARh,
      entity.ActiveEnergyTotalImportT1Min_Wh,
      entity.ActiveEnergyTotalImportT1Max_Wh,
      entity.ActiveEnergyTotalImportT2Min_Wh,
      entity.ActiveEnergyTotalImportT2Max_Wh
    );
}

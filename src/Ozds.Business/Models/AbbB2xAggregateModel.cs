using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Abstractions;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

using IUpsertAggregate = IUpsertAggregate<AbbB2xAggregateModel>;

public record AbbB2xAggregateModel(
  string MeterId,
  DateTimeOffset Timestamp,
  TimeSpan Interval,
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
  float ReactivePowerL1NetT0Avg_VAR,
  float ReactivePowerL2NetT0Avg_VAR,
  float ReactivePowerL3NetT0Avg_VAR,
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
) : AggregateModel(MeterId, Timestamp, Interval, Count), IUpsertAggregate
{
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
      return new CompositeTariffMeasure(new() {
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
      return new CompositeTariffMeasure(new() {
        base.ReactivePower_VAR,
        new UnaryTariffMeasure(
          new NetDuplexMeasure(
            new TriPhasicMeasure(
              ReactivePowerL1NetT0Avg_VAR,
              ReactivePowerL2NetT0Avg_VAR,
              ReactivePowerL3NetT0Avg_VAR
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
      new((AbbB2xAggregateModel lhs, AbbB2xAggregateModel rhs) =>
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
          (lhs.ReactivePowerL1NetT0Avg_VAR * lhs.Count
            + rhs.ReactivePowerL1NetT0Avg_VAR * rhs.Count)
            / (lhs.Count + rhs.Count),
          (lhs.ReactivePowerL2NetT0Avg_VAR * lhs.Count
            + rhs.ReactivePowerL2NetT0Avg_VAR * rhs.Count)
            / (lhs.Count + rhs.Count),
          (lhs.ReactivePowerL3NetT0Avg_VAR * lhs.Count
            + rhs.ReactivePowerL3NetT0Avg_VAR * rhs.Count)
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

public static class AbbB2xAggregateModelExtensions
{
  public static AbbB2xAggregateEntity ToEntity(this AbbB2xAggregateModel model) =>
    new()
    {
      Meter = new() { Id = model.MeterId },
      Timestamp = model.Timestamp,
      Interval = model.Interval,
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
      ReactivePowerL1NetT0Avg_VAR = model.ReactivePowerL1NetT0Avg_VAR,
      ReactivePowerL2NetT0Avg_VAR = model.ReactivePowerL2NetT0Avg_VAR,
      ReactivePowerL3NetT0Avg_VAR = model.ReactivePowerL3NetT0Avg_VAR,
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

  public static AbbB2xAggregateModel ToModel(this AbbB2xAggregateEntity entity) =>
    new(
      entity.Meter.Id,
      entity.Timestamp,
      entity.Interval,
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
      entity.ReactivePowerL1NetT0Avg_VAR,
      entity.ReactivePowerL2NetT0Avg_VAR,
      entity.ReactivePowerL3NetT0Avg_VAR,
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

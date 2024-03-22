using Ozds.Business.Math;

namespace Ozds.Business.Models;

using IUpsertAggregate = IUpsertAggregate<AbbB2xAggregateModel>;

public record AbbB2xAggregateModel(
  string Source,
  DateTimeOffset Timestamp,
  TimeSpan TimeSpan,
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
) : IUpsertAggregate
{
  static IUpsertAggregate.UpsertExpressionHolder IUpsertAggregate.UpsertExpression => _upsertExpression.Value;

  static IUpsertAggregate.UpsertHolder IUpsertAggregate.Upsert => _upsert.Value;

  string IMeasurement.Source
  {
    get { return Source; }
  }

  DateTimeOffset IMeasurement.Timestamp
  {
    get { return Timestamp; }
  }

  TariffMeasure IMeasurement.Current_A
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

  TariffMeasure IMeasurement.Voltage_V
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

  TariffMeasure IMeasurement.ActivePower_W
  {
    get
    {
      return new UnaryTariffMeasure(
        new NetDuplexMeasure(
          new TriPhasicMeasure(
            ActivePowerL1NetT0Avg_W,
            ActivePowerL2NetT0Avg_W,
            ActivePowerL3NetT0Avg_W
          )
        )
      );
    }
  }

  TariffMeasure IMeasurement.ReactivePower_VAR
  {
    get
    {
      return new UnaryTariffMeasure(
        new NetDuplexMeasure(
          new TriPhasicMeasure(
            ReactivePowerL1NetT0Avg_VAR,
            ReactivePowerL2NetT0Avg_VAR,
            ReactivePowerL3NetT0Avg_VAR
          )
        )
      );
    }
  }

  TariffMeasure IMeasurement.ApparentPower_VA
  {
    get { return TariffMeasure.Null; }
  }

  SpanningMeasure IAggregate.ActiveEnergySpan_Wh
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

  SpanningMeasure IAggregate.ReactiveEnergySpan_VARh
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

  SpanningMeasure IAggregate.ApparentEnergySpan_VAh
  {
    get { return SpanningMeasure.Null; }
  }

  private static readonly Lazy<IUpsertAggregate.UpsertExpressionHolder> _upsertExpression =
    new(() =>
      new((AbbB2xAggregateModel lhs, AbbB2xAggregateModel rhs) =>
        new(
          lhs.Source,
          lhs.Timestamp,
          lhs.TimeSpan,
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

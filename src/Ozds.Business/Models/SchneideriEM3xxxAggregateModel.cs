using System.Linq.Expressions;
using Ozds.Business.Math;

namespace Ozds.Business.Models;

public record SchneideriEM3xxxAggregateModel(
  string Source,
  DateTimeOffset Timestamp,
  TimeSpan TimeSpan,
  long AggregateCount,
  float VoltageL1Avg_V,
  float VoltageL2Avg_V,
  float VoltageL3Avg_V,
  float CurrentL1Avg_A,
  float CurrentL2Avg_A,
  float CurrentL3Avg_A,
  float ActivePowerL1Avg_W,
  float ActivePowerL2Avg_W,
  float ActivePowerL3Avg_W,
  float ReactivePowerTotalAvg_VAR,
  float ApparentPowerTotalAvg_VA,
  float ActiveEnergyImportTotalMin_Wh,
  float ActiveEnergyImportTotalMax_Wh,
  float ActiveEnergyExportTotalMin_Wh,
  float ActiveEnergyExportTotalMax_Wh,
  float ReactiveEnergyImportTotalMin_VARh,
  float ReactiveEnergyImportTotalMax_VARh,
  float ReactiveEnergyExportTotalMin_VARh,
  float ReactiveEnergyExportTotalMax_VARh,
  float ActiveEnergyImportTotalT1Min_Wh,
  float ActiveEnergyImportTotalT1Max_Wh,
  float ActiveEnergyImportTotalT2Min_Wh,
  float ActiveEnergyImportTotalT2Max_Wh
) : IAggregate<SchneideriEM3xxxAggregateModel>
{
  static Expression<Func<SchneideriEM3xxxAggregateModel, SchneideriEM3xxxAggregateModel, SchneideriEM3xxxAggregateModel>> IAggregate<SchneideriEM3xxxAggregateModel>.UpsertExpression =>
    _upsertExpression.Value;

  string IMeasurement.Source
  {
    get { return Source; }
  }

  DateTimeOffset IMeasurement.Timestamp
  {
    get { return Timestamp; }
  }

  DuplexMeasure IMeasurement.Current_A
  {
    get
    {
      return new NetDuplexMeasure(
        new TriPhasicMeasure(CurrentL1Avg_A, CurrentL2Avg_A, CurrentL3Avg_A)
      );
    }
  }

  DuplexMeasure IMeasurement.Voltage_V
  {
    get
    {
      return new NetDuplexMeasure(
        new TriPhasicMeasure(VoltageL1Avg_V, VoltageL2Avg_V, VoltageL3Avg_V)
      );
    }
  }

  DuplexMeasure IMeasurement.ActivePower_W
  {
    get
    {
      return new NetDuplexMeasure(
        new TriPhasicMeasure(
          ActivePowerL1Avg_W,
          ActivePowerL2Avg_W,
          ActivePowerL3Avg_W
        )
      );
    }
  }

  DuplexMeasure IMeasurement.ReactivePower_VAR
  {
    get
    {
      return new NetDuplexMeasure(
        new SinglePhasicMeasure(
          ReactivePowerTotalAvg_VAR
        )
      );
    }
  }

  DuplexMeasure IMeasurement.ApparentPower_VA
  {
    get
    {
      return new NetDuplexMeasure(
        new SinglePhasicMeasure(
          ApparentPowerTotalAvg_VA
        )
      );
    }
  }

  SpanningMeasure<DuplexMeasure> IAggregate.ActiveEnergySpan_Wh
  {
    get
    {
      return new MinMaxSpanningMeasure<DuplexMeasure>(
        new ImportExportDuplexMeasure(
          new SinglePhasicMeasure(ActiveEnergyImportTotalMin_Wh),
          new SinglePhasicMeasure(ActiveEnergyExportTotalMin_Wh)
        ),
        new ImportExportDuplexMeasure(
          new SinglePhasicMeasure(ActiveEnergyImportTotalMax_Wh),
          new SinglePhasicMeasure(ActiveEnergyExportTotalMax_Wh)
        )
      );
    }
  }

  SpanningMeasure<DuplexMeasure> IAggregate.ReactiveEnergySpan_VARh
  {
    get
    {
      return new MinMaxSpanningMeasure<DuplexMeasure>(
        new ImportExportDuplexMeasure(
          new SinglePhasicMeasure(ReactiveEnergyImportTotalMin_VARh),
          new SinglePhasicMeasure(ReactiveEnergyExportTotalMin_VARh)
        ),
        new ImportExportDuplexMeasure(
          new SinglePhasicMeasure(ReactiveEnergyImportTotalMax_VARh),
          new SinglePhasicMeasure(ReactiveEnergyExportTotalMax_VARh)
        )
      );
    }
  }

  SpanningMeasure<DuplexMeasure> IAggregate.ApparentEnergySpan_VAh
  {
    get { return SpanningMeasure<DuplexMeasure>.Null; }
  }

  SpanningMeasure<TariffMeasure> IAggregate.TariffEnergySpan_Wh
  {
    get
    {
      return new MinMaxSpanningMeasure<TariffMeasure>(
        new BinaryTariffMeasure(
          new ImportExportDuplexMeasure(
            new SinglePhasicMeasure(ActiveEnergyImportTotalT1Min_Wh),
            PhasicMeasure.Null
          ),
          new ImportExportDuplexMeasure(
            new SinglePhasicMeasure(ActiveEnergyImportTotalT2Min_Wh),
            PhasicMeasure.Null
          )
        ),
        new BinaryTariffMeasure(
          new ImportExportDuplexMeasure(
            new SinglePhasicMeasure(ActiveEnergyImportTotalT1Max_Wh),
            PhasicMeasure.Null
          ),
          new ImportExportDuplexMeasure(
            new SinglePhasicMeasure(ActiveEnergyImportTotalT2Max_Wh),
            PhasicMeasure.Null
          )
        )
      );
    }
  }

  private static readonly Lazy<Expression<Func<SchneideriEM3xxxAggregateModel, SchneideriEM3xxxAggregateModel, SchneideriEM3xxxAggregateModel>>> _upsertExpression =
    new(() =>
      (SchneideriEM3xxxAggregateModel lhs, SchneideriEM3xxxAggregateModel rhs) =>
          new(
            lhs.Source,
            lhs.Timestamp,
            lhs.TimeSpan,
            lhs.AggregateCount + rhs.AggregateCount,
            (lhs.VoltageL1Avg_V * lhs.AggregateCount + rhs.VoltageL1Avg_V * rhs.AggregateCount) /
              (lhs.AggregateCount + rhs.AggregateCount),
            (lhs.VoltageL2Avg_V * lhs.AggregateCount + rhs.VoltageL2Avg_V * rhs.AggregateCount) /
              (lhs.AggregateCount + rhs.AggregateCount),
            (lhs.VoltageL3Avg_V * lhs.AggregateCount + rhs.VoltageL3Avg_V * rhs.AggregateCount) /
              (lhs.AggregateCount + rhs.AggregateCount),
            (lhs.CurrentL1Avg_A * lhs.AggregateCount + rhs.CurrentL1Avg_A * rhs.AggregateCount) /
              (lhs.AggregateCount + rhs.AggregateCount),
            (lhs.CurrentL2Avg_A * lhs.AggregateCount + rhs.CurrentL2Avg_A * rhs.AggregateCount) /
              (lhs.AggregateCount + rhs.AggregateCount),
            (lhs.CurrentL3Avg_A * lhs.AggregateCount + rhs.CurrentL3Avg_A * rhs.AggregateCount) /
              (lhs.AggregateCount + rhs.AggregateCount),
            (lhs.ActivePowerL1Avg_W * lhs.AggregateCount + rhs.ActivePowerL1Avg_W * rhs.AggregateCount) /
              (lhs.AggregateCount + rhs.AggregateCount),
            (lhs.ActivePowerL2Avg_W * lhs.AggregateCount + rhs.ActivePowerL2Avg_W * rhs.AggregateCount) /
              (lhs.AggregateCount + rhs.AggregateCount),
            (lhs.ActivePowerL3Avg_W * lhs.AggregateCount + rhs.ActivePowerL3Avg_W * rhs.AggregateCount) /
              (lhs.AggregateCount + rhs.AggregateCount),
            (lhs.ReactivePowerTotalAvg_VAR * lhs.AggregateCount + rhs.ReactivePowerTotalAvg_VAR * rhs.AggregateCount) /
              (lhs.AggregateCount + rhs.AggregateCount),
            (lhs.ApparentPowerTotalAvg_VA * lhs.AggregateCount + rhs.ApparentPowerTotalAvg_VA * rhs.AggregateCount) /
              (lhs.AggregateCount + rhs.AggregateCount),
            lhs.ActiveEnergyImportTotalMin_Wh > rhs.ActiveEnergyImportTotalMin_Wh
              ? rhs.ActiveEnergyImportTotalMin_Wh
              : lhs.ActiveEnergyImportTotalMin_Wh,
            lhs.ActiveEnergyImportTotalMax_Wh < rhs.ActiveEnergyImportTotalMax_Wh
              ? rhs.ActiveEnergyImportTotalMax_Wh
              : lhs.ActiveEnergyImportTotalMax_Wh,
            lhs.ActiveEnergyExportTotalMin_Wh > rhs.ActiveEnergyExportTotalMin_Wh
              ? rhs.ActiveEnergyExportTotalMin_Wh
              : lhs.ActiveEnergyExportTotalMin_Wh,
            lhs.ActiveEnergyExportTotalMax_Wh < rhs.ActiveEnergyExportTotalMax_Wh
              ? rhs.ActiveEnergyExportTotalMax_Wh
              : lhs.ActiveEnergyExportTotalMax_Wh,
            lhs.ReactiveEnergyImportTotalMin_VARh > rhs.ReactiveEnergyImportTotalMin_VARh
              ? rhs.ReactiveEnergyImportTotalMin_VARh
              : lhs.ReactiveEnergyImportTotalMin_VARh,
            lhs.ReactiveEnergyImportTotalMax_VARh < rhs.ReactiveEnergyImportTotalMax_VARh
              ? rhs.ReactiveEnergyImportTotalMax_VARh
              : lhs.ReactiveEnergyImportTotalMax_VARh,
            lhs.ReactiveEnergyExportTotalMin_VARh > rhs.ReactiveEnergyExportTotalMin_VARh
              ? rhs.ReactiveEnergyExportTotalMin_VARh
              : lhs.ReactiveEnergyExportTotalMin_VARh,
            lhs.ReactiveEnergyExportTotalMax_VARh < rhs.ReactiveEnergyExportTotalMax_VARh
              ? rhs.ReactiveEnergyExportTotalMax_VARh
              : lhs.ReactiveEnergyExportTotalMax_VARh,
            lhs.ActiveEnergyImportTotalT1Min_Wh > rhs.ActiveEnergyImportTotalT1Min_Wh
              ? rhs.ActiveEnergyImportTotalT1Min_Wh
              : lhs.ActiveEnergyImportTotalT1Min_Wh,
            lhs.ActiveEnergyImportTotalT1Max_Wh < rhs.ActiveEnergyImportTotalT1Max_Wh
              ? rhs.ActiveEnergyImportTotalT1Max_Wh
              : lhs.ActiveEnergyImportTotalT1Max_Wh,
            lhs.ActiveEnergyImportTotalT2Min_Wh > rhs.ActiveEnergyImportTotalT2Min_Wh
              ? rhs.ActiveEnergyImportTotalT2Min_Wh
              : lhs.ActiveEnergyImportTotalT2Min_Wh,
            lhs.ActiveEnergyImportTotalT2Max_Wh < rhs.ActiveEnergyImportTotalT2Max_Wh
              ? rhs.ActiveEnergyImportTotalT2Max_Wh
              : lhs.ActiveEnergyImportTotalT2Max_Wh
          ));
}

using Ozds.Business.Math;

namespace Ozds.Business.Models;

public record SchneiderAggregateModel(
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
) : IAggregate
{
  string IMeasurement.Source => Source;

  DateTimeOffset IMeasurement.Timestamp => Timestamp;

  DuplexMeasure IMeasurement.Current_A => new NetDuplexMeasure(
    new TriPhasicMeasure(CurrentL1Avg_A, CurrentL2Avg_A, CurrentL3Avg_A)
  );

  DuplexMeasure IMeasurement.Voltage_V => new NetDuplexMeasure(
    new TriPhasicMeasure(VoltageL1Avg_V, VoltageL2Avg_V, VoltageL3Avg_V)
  );

  DuplexMeasure IMeasurement.ActivePower_W => new NetDuplexMeasure(
    new TriPhasicMeasure(
      ActivePowerL1Avg_W,
      ActivePowerL2Avg_W,
      ActivePowerL3Avg_W
    )
  );

  DuplexMeasure IMeasurement.ReactivePower_VAR =>
    new NetDuplexMeasure(
      new SinglePhasicMeasure(
        ReactivePowerTotalAvg_VAR
      )
    );

  DuplexMeasure IMeasurement.ApparentPower_VA =>
    new NetDuplexMeasure(
      new SinglePhasicMeasure(
        ApparentPowerTotalAvg_VA
      )
    );

  SpanningMeasure<DuplexMeasure> IAggregate.ActiveEnergySpan_Wh =>
    new MinMaxSpanningMeasure<DuplexMeasure>(
      new ImportExportDuplexMeasure(
        new SinglePhasicMeasure(ActiveEnergyImportTotalMin_Wh),
        new SinglePhasicMeasure(ActiveEnergyExportTotalMin_Wh)
      ),
      new ImportExportDuplexMeasure(
        new SinglePhasicMeasure(ActiveEnergyImportTotalMax_Wh),
        new SinglePhasicMeasure(ActiveEnergyExportTotalMax_Wh)
      )
    );

  SpanningMeasure<DuplexMeasure> IAggregate.ReactiveEnergySpan_VARh =>
    new MinMaxSpanningMeasure<DuplexMeasure>(
      new ImportExportDuplexMeasure(
        new SinglePhasicMeasure(ReactiveEnergyImportTotalMin_VARh),
        new SinglePhasicMeasure(ReactiveEnergyExportTotalMin_VARh)
      ),
      new ImportExportDuplexMeasure(
        new SinglePhasicMeasure(ReactiveEnergyImportTotalMax_VARh),
        new SinglePhasicMeasure(ReactiveEnergyExportTotalMax_VARh)
      )
    );

  SpanningMeasure<DuplexMeasure> IAggregate.ApparentEnergySpan_VAh =>
    SpanningMeasure<DuplexMeasure>.Null;

  SpanningMeasure<TariffMeasure> IAggregate.TariffEnergySpan_Wh =>
    new MinMaxSpanningMeasure<TariffMeasure>(
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


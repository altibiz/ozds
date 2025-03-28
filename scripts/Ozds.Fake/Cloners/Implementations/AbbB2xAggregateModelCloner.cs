using Ozds.Business.Models;
using Ozds.Fake.Cloners.Base;
using Ozds.Fake.Extensions;

namespace Ozds.Fake.Cloners.Implementations;

public class AbbB2xAggregateModelCloner
  : MeasurementCloner<AbbB2xAggregateModel>
{
  protected override AbbB2xAggregateModel Clone(
    AbbB2xAggregateModel measurement
  )
  {
    return new AbbB2xAggregateModel
    {
      MeterId = measurement.MeterId,
      MeasurementLocationId = measurement.MeasurementLocationId,
      Timestamp = measurement.Timestamp,
      Count = measurement.Count,
      QuarterHourCount = measurement.QuarterHourCount,
      Interval = measurement.Interval,
      VoltageL1AnyT0_V = measurement.VoltageL1AnyT0_V.Clone(),
      VoltageL2AnyT0_V = measurement.VoltageL2AnyT0_V.Clone(),
      VoltageL3AnyT0_V = measurement.VoltageL3AnyT0_V.Clone(),
      CurrentL1AnyT0_A = measurement.CurrentL1AnyT0_A.Clone(),
      CurrentL2AnyT0_A = measurement.CurrentL2AnyT0_A.Clone(),
      CurrentL3AnyT0_A = measurement.CurrentL3AnyT0_A.Clone(),
      ActivePowerL1NetT0_W = measurement.ActivePowerL1NetT0_W.Clone(),
      ActivePowerL2NetT0_W = measurement.ActivePowerL2NetT0_W.Clone(),
      ActivePowerL3NetT0_W = measurement.ActivePowerL3NetT0_W.Clone(),
      ReactivePowerL1NetT0_VAR = measurement.ReactivePowerL1NetT0_VAR.Clone(),
      ReactivePowerL2NetT0_VAR = measurement.ReactivePowerL2NetT0_VAR.Clone(),
      ReactivePowerL3NetT0_VAR = measurement.ReactivePowerL3NetT0_VAR.Clone(),
      ActiveEnergyL1ImportT0_Wh = measurement.ActiveEnergyL1ImportT0_Wh.Clone(),

      DerivedActivePowerL1ImportT0_W =
        measurement.DerivedActivePowerL1ImportT0_W.Clone(),
      ActiveEnergyL2ImportT0_Wh = measurement.ActiveEnergyL2ImportT0_Wh
        .Clone(),

      DerivedActivePowerL2ImportT0_W =
        measurement.DerivedActivePowerL2ImportT0_W.Clone(),
      ActiveEnergyL3ImportT0_Wh = measurement.ActiveEnergyL3ImportT0_Wh
        .Clone(),

      DerivedActivePowerL3ImportT0_W =
        measurement.DerivedActivePowerL3ImportT0_W.Clone(),
      ActiveEnergyL1ExportT0_Wh = measurement.ActiveEnergyL1ExportT0_Wh
        .Clone(),

      DerivedActivePowerL1ExportT0_W =
        measurement.DerivedActivePowerL1ExportT0_W.Clone(),
      ActiveEnergyL2ExportT0_Wh = measurement.ActiveEnergyL2ExportT0_Wh
        .Clone(),

      DerivedActivePowerL2ExportT0_W =
        measurement.DerivedActivePowerL2ExportT0_W.Clone(),
      ActiveEnergyL3ExportT0_Wh = measurement.ActiveEnergyL3ExportT0_Wh
        .Clone(),

      DerivedActivePowerL3ExportT0_W =
        measurement.DerivedActivePowerL3ExportT0_W.Clone(),
      ReactiveEnergyL1ImportT0_VARh = measurement.ReactiveEnergyL1ImportT0_VARh
        .Clone(),

      DerivedReactivePowerL1ImportT0_VAR =
        measurement.DerivedReactivePowerL1ImportT0_VAR.Clone(),
      ReactiveEnergyL2ImportT0_VARh = measurement.ReactiveEnergyL2ImportT0_VARh
        .Clone(),

      DerivedReactivePowerL2ImportT0_VAR =
        measurement.DerivedReactivePowerL2ImportT0_VAR.Clone(),
      ReactiveEnergyL3ImportT0_VARh = measurement.ReactiveEnergyL3ImportT0_VARh
        .Clone(),

      DerivedReactivePowerL3ImportT0_VAR =
        measurement.DerivedReactivePowerL3ImportT0_VAR.Clone(),
      ReactiveEnergyL1ExportT0_VARh = measurement.ReactiveEnergyL1ExportT0_VARh
        .Clone(),

      DerivedReactivePowerL1ExportT0_VAR =
        measurement.DerivedReactivePowerL1ExportT0_VAR.Clone(),
      ReactiveEnergyL2ExportT0_VARh = measurement.ReactiveEnergyL2ExportT0_VARh
        .Clone(),

      DerivedReactivePowerL2ExportT0_VAR =
        measurement.DerivedReactivePowerL2ExportT0_VAR.Clone(),
      ReactiveEnergyL3ExportT0_VARh = measurement.ReactiveEnergyL3ExportT0_VARh
        .Clone(),

      DerivedReactivePowerL3ExportT0_VAR =
        measurement.DerivedReactivePowerL3ExportT0_VAR.Clone(),
      ActiveEnergyTotalImportT0_Wh = measurement.ActiveEnergyTotalImportT0_Wh
        .Clone(),

      DerivedActivePowerTotalImportT0_W =
        measurement.DerivedActivePowerTotalImportT0_W.Clone(),
      ActiveEnergyTotalExportT0_Wh = measurement.ActiveEnergyTotalExportT0_Wh
        .Clone(),

      DerivedActivePowerTotalExportT0_W =
        measurement.DerivedActivePowerTotalExportT0_W.Clone(),

      ReactiveEnergyTotalImportT0_VARh =
        measurement.ReactiveEnergyTotalImportT0_VARh.Clone(),

      DerivedReactivePowerTotalImportT0_VAR =
        measurement.DerivedReactivePowerTotalImportT0_VAR.Clone(),

      ReactiveEnergyTotalExportT0_VARh =
        measurement.ReactiveEnergyTotalExportT0_VARh.Clone(),

      DerivedReactivePowerTotalExportT0_VAR =
        measurement.DerivedReactivePowerTotalExportT0_VAR.Clone(),
      ActiveEnergyTotalImportT1_Wh = measurement.ActiveEnergyTotalImportT1_Wh
        .Clone(),

      DerivedActivePowerTotalImportT1_W =
        measurement.DerivedActivePowerTotalImportT1_W.Clone(),
      ActiveEnergyTotalImportT2_Wh = measurement.ActiveEnergyTotalImportT2_Wh
        .Clone(),

      DerivedActivePowerTotalImportT2_W =
        measurement.DerivedActivePowerTotalImportT2_W.Clone()
    };
  }
}

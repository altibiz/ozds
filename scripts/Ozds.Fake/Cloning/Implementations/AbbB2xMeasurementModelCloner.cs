using Ozds.Business.Models;
using Ozds.Fake.Cloning.Base;

namespace Ozds.Fake.Cloning.Implementations;

public class AbbB2xMeasurementModelCloner
  : MeasurementCloner<AbbB2xMeasurementModel>
{
  protected override AbbB2xMeasurementModel Clone(
    AbbB2xMeasurementModel measurement
  )
  {
    return new AbbB2xMeasurementModel
    {
      MeterId = measurement.MeterId,
      MeasurementLocationId = measurement.MeasurementLocationId,
      Timestamp = measurement.Timestamp,
      VoltageL1AnyT0_V = measurement.VoltageL1AnyT0_V,
      VoltageL2AnyT0_V = measurement.VoltageL2AnyT0_V,
      VoltageL3AnyT0_V = measurement.VoltageL3AnyT0_V,
      CurrentL1AnyT0_A = measurement.CurrentL1AnyT0_A,
      CurrentL2AnyT0_A = measurement.CurrentL2AnyT0_A,
      CurrentL3AnyT0_A = measurement.CurrentL3AnyT0_A,
      ActivePowerL1NetT0_W = measurement.ActivePowerL1NetT0_W,
      ActivePowerL2NetT0_W = measurement.ActivePowerL2NetT0_W,
      ActivePowerL3NetT0_W = measurement.ActivePowerL3NetT0_W,
      ReactivePowerL1NetT0_VAR = measurement.ReactivePowerL1NetT0_VAR,
      ReactivePowerL2NetT0_VAR = measurement.ReactivePowerL2NetT0_VAR,
      ReactivePowerL3NetT0_VAR = measurement.ReactivePowerL3NetT0_VAR,
      ActiveEnergyL1ImportT0_Wh = measurement.ActiveEnergyL1ImportT0_Wh,
      ActiveEnergyL2ImportT0_Wh = measurement.ActiveEnergyL2ImportT0_Wh,
      ActiveEnergyL3ImportT0_Wh = measurement.ActiveEnergyL3ImportT0_Wh,
      ActiveEnergyL1ExportT0_Wh = measurement.ActiveEnergyL1ExportT0_Wh,
      ActiveEnergyL2ExportT0_Wh = measurement.ActiveEnergyL2ExportT0_Wh,
      ActiveEnergyL3ExportT0_Wh = measurement.ActiveEnergyL3ExportT0_Wh,
      ReactiveEnergyL1ImportT0_VARh = measurement.ReactiveEnergyL1ImportT0_VARh,
      ReactiveEnergyL2ImportT0_VARh = measurement.ReactiveEnergyL2ImportT0_VARh,
      ReactiveEnergyL3ImportT0_VARh = measurement.ReactiveEnergyL3ImportT0_VARh,
      ReactiveEnergyL1ExportT0_VARh = measurement.ReactiveEnergyL1ExportT0_VARh,
      ReactiveEnergyL2ExportT0_VARh = measurement.ReactiveEnergyL2ExportT0_VARh,
      ReactiveEnergyL3ExportT0_VARh = measurement.ReactiveEnergyL3ExportT0_VARh,
      ActiveEnergyTotalImportT0_Wh = measurement.ActiveEnergyTotalImportT0_Wh,
      ActiveEnergyTotalExportT0_Wh = measurement.ActiveEnergyTotalExportT0_Wh,
      ReactiveEnergyTotalImportT0_VARh =
        measurement.ReactiveEnergyTotalImportT0_VARh,
      ReactiveEnergyTotalExportT0_VARh =
        measurement.ReactiveEnergyTotalExportT0_VARh,
      ActiveEnergyTotalImportT1_Wh = measurement.ActiveEnergyTotalImportT1_Wh,
      ActiveEnergyTotalImportT2_Wh = measurement.ActiveEnergyTotalImportT2_Wh,
    };
  }
}

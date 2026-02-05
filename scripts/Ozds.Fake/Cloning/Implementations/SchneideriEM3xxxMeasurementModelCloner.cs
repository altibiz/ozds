using Ozds.Business.Models;
using Ozds.Fake.Cloning.Base;

namespace Ozds.Fake.Cloning.Implementations;

public class SchneideriEM3xxxMeasurementModelCloner
  : MeasurementCloner<SchneideriEM3xxxMeasurementModel>
{
  protected override SchneideriEM3xxxMeasurementModel Clone(
    SchneideriEM3xxxMeasurementModel measurement
  )
  {
    return new SchneideriEM3xxxMeasurementModel
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
      ReactivePowerTotalNetT0_VAR = measurement.ReactivePowerTotalNetT0_VAR,
      ApparentPowerTotalNetT0_VA = measurement.ApparentPowerTotalNetT0_VA,
      ActiveEnergyL1ImportT0_Wh = measurement.ActiveEnergyL1ImportT0_Wh,
      ActiveEnergyL2ImportT0_Wh = measurement.ActiveEnergyL2ImportT0_Wh,
      ActiveEnergyL3ImportT0_Wh = measurement.ActiveEnergyL3ImportT0_Wh,
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

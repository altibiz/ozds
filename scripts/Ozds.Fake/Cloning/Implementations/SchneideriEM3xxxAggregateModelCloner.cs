using Ozds.Business.Models;
using Ozds.Fake.Cloning.Base;
using Ozds.Fake.Extensions;

namespace Ozds.Fake.Cloning.Implementations;

public class SchneideriEM3xxxAggregateModelCloner
  : MeasurementCloner<SchneideriEM3xxxAggregateModel>
{
  protected override SchneideriEM3xxxAggregateModel Clone(
    SchneideriEM3xxxAggregateModel measurement
  )
  {
    return new SchneideriEM3xxxAggregateModel
    {
      MeterId = measurement.MeterId,
      MeasurementLocationId = measurement.MeasurementLocationId,
      Timestamp = measurement.Timestamp,
      Count = measurement.Count,
      QuarterHourCount = measurement.QuarterHourCount,
      Interval = measurement.Interval,

      VoltageL1AnyT0_V = measurement.VoltageL1AnyT0_V
        .Clone(),
      VoltageL2AnyT0_V = measurement.VoltageL2AnyT0_V
        .Clone(),
      VoltageL3AnyT0_V = measurement.VoltageL3AnyT0_V
        .Clone(),
      CurrentL1AnyT0_A = measurement.CurrentL1AnyT0_A
        .Clone(),
      CurrentL2AnyT0_A = measurement.CurrentL2AnyT0_A
        .Clone(),
      CurrentL3AnyT0_A = measurement.CurrentL3AnyT0_A
        .Clone(),
      ActivePowerL1NetT0_W = measurement.ActivePowerL1NetT0_W
        .Clone(),
      ActivePowerL2NetT0_W = measurement.ActivePowerL2NetT0_W
        .Clone(),
      ActivePowerL3NetT0_W = measurement.ActivePowerL3NetT0_W
        .Clone(),
      ReactivePowerTotalNetT0_VAR = measurement.ReactivePowerTotalNetT0_VAR
        .Clone(),
      ApparentPowerTotalNetT0_VA = measurement.ApparentPowerTotalNetT0_VA
        .Clone(),
      ActiveEnergyL1ImportT0_Wh = measurement.ActiveEnergyL1ImportT0_Wh
        .Clone(),

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

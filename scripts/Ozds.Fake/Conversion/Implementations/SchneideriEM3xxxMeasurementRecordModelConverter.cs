using Ozds.Business.Models;
using Ozds.Fake.Conversion.Base;
using Ozds.Fake.Records;

namespace Ozds.Fake.Conversion.Implementations;

public class SchneideriEM3xxxMeasurementRecordModelConverter
  : MeasurementRecordModelConverter<
    SchneideriEM3xxxMeasurementRecord,
    SchneideriEM3xxxMeasurementModel>
{
  protected override SchneideriEM3xxxMeasurementModel ConvertToModel(
    SchneideriEM3xxxMeasurementRecord record)
  {
    return new SchneideriEM3xxxMeasurementModel
    {
      MeterId = record.MeterId,
      MeasurementLocationId = record.MeasurementLocationId,
      Timestamp = record.Timestamp,
      VoltageL1AnyT0_V = record.VoltageL1AnyT0_V,
      VoltageL2AnyT0_V = record.VoltageL2AnyT0_V,
      VoltageL3AnyT0_V = record.VoltageL3AnyT0_V,
      CurrentL1AnyT0_A = record.CurrentL1AnyT0_A,
      CurrentL2AnyT0_A = record.CurrentL2AnyT0_A,
      CurrentL3AnyT0_A = record.CurrentL3AnyT0_A,
      ActivePowerL1NetT0_W = record.ActivePowerL1NetT0_W,
      ActivePowerL2NetT0_W = record.ActivePowerL2NetT0_W,
      ActivePowerL3NetT0_W = record.ActivePowerL3NetT0_W,
      ReactivePowerTotalNetT0_VAR = record.ReactivePowerTotalNetT0_VAR,
      ApparentPowerTotalNetT0_VA = record.ApparentPowerTotalNetT0_VA,
      ActiveEnergyL1ImportT0_Wh = record.ActiveEnergyL1ImportT0_Wh,
      ActiveEnergyL2ImportT0_Wh = record.ActiveEnergyL2ImportT0_Wh,
      ActiveEnergyL3ImportT0_Wh = record.ActiveEnergyL3ImportT0_Wh,
      ActiveEnergyTotalImportT0_Wh = record.ActiveEnergyTotalImportT0_Wh,
      ActiveEnergyTotalExportT0_Wh = record.ActiveEnergyTotalExportT0_Wh,
      ReactiveEnergyTotalImportT0_VARh =
        record.ReactiveEnergyTotalImportT0_VARh,
      ReactiveEnergyTotalExportT0_VARh =
        record.ReactiveEnergyTotalExportT0_VARh,
      ActiveEnergyTotalImportT1_Wh = record.ActiveEnergyTotalImportT1_Wh,
      ActiveEnergyTotalImportT2_Wh = record.ActiveEnergyTotalImportT2_Wh
    };
  }
}

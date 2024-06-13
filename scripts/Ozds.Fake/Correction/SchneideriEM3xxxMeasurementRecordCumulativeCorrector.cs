using Ozds.Fake.Correction.Base;
using Ozds.Fake.Records;

namespace Ozds.Fake.Correction;

public class SchneideriEM3xxxMeasurementRecordCumulativeCorrector
  : CumulativeCorrector<SchneideriEM3xxxMeasurementRecord>
{
  protected override SchneideriEM3xxxMeasurementRecord CorrectCumulatives(
    DateTimeOffset timestamp,
    SchneideriEM3xxxMeasurementRecord measurementRecord,
    SchneideriEM3xxxMeasurementRecord firstMeasurementRecord,
    SchneideriEM3xxxMeasurementRecord lastMeasurementRecord
  )
  {
    var diffMultiplier = DiffMultiplier(
      timestamp,
      firstMeasurementRecord.Timestamp,
      lastMeasurementRecord.Timestamp
    );

    var activeEnergy = measurementRecord.ActiveEnergy_Wh
      .Add(
        lastMeasurementRecord.ActiveEnergy_Wh
          .Subtract(firstMeasurementRecord.ActiveEnergy_Wh)
          .Multiply(diffMultiplier)
      );

    var reactiveEnergy = measurementRecord.ReactiveEnergy_VARh
      .Add(
        lastMeasurementRecord.ReactiveEnergy_VARh
          .Subtract(firstMeasurementRecord.ReactiveEnergy_VARh)
          .Multiply(diffMultiplier)
      );

    var apparentEnergy = measurementRecord.ApparentEnergy_VAh
      .Add(
        lastMeasurementRecord.ApparentEnergy_VAh
          .Subtract(firstMeasurementRecord.ApparentEnergy_VAh)
          .Multiply(diffMultiplier)
      );

    measurementRecord.ActiveEnergyL1ImportT0_Wh =
      activeEnergy.TariffUnary.DuplexImport.PhaseSplit.ValueL1;
    measurementRecord.ActiveEnergyL2ImportT0_Wh =
      activeEnergy.TariffUnary.DuplexImport.PhaseSplit.ValueL2;
    measurementRecord.ActiveEnergyL3ImportT0_Wh =
      activeEnergy.TariffUnary.DuplexImport.PhaseSplit.ValueL3;
    measurementRecord.ActiveEnergyTotalImportT0_Wh =
      activeEnergy.TariffUnary.DuplexImport.PhaseSum;

    measurementRecord.ActiveEnergyTotalExportT0_Wh =
      activeEnergy.TariffUnary.DuplexExport.PhaseSum;

    measurementRecord.ReactiveEnergyTotalImportT0_VARh =
      reactiveEnergy.TariffUnary.DuplexImport.PhaseSum;

    measurementRecord.ReactiveEnergyTotalExportT0_VARh =
      reactiveEnergy.TariffUnary.DuplexExport.PhaseSum;

    measurementRecord.ApparentPowerTotalNetT0_VA =
      apparentEnergy.TariffUnary.DuplexNet.PhaseSum;

    measurementRecord.ActiveEnergyTotalImportT1_Wh =
      activeEnergy.TariffBinary.T1.DuplexImport.PhaseSum;
    measurementRecord.ActiveEnergyTotalImportT2_Wh =
      activeEnergy.TariffBinary.T2.DuplexExport.PhaseSum;

    return measurementRecord;
  }

  protected override SchneideriEM3xxxMeasurementRecord CopyRecord(
    SchneideriEM3xxxMeasurementRecord record)
  {
    return new SchneideriEM3xxxMeasurementRecord
    {
      MeterId = record.MeterId,
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

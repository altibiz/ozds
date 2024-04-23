using Ozds.Fake.Correction.Base;
using Ozds.Fake.Records;

namespace Ozds.Fake.Correction;

public class AbbB2xMeasurementRecordCumulativeCorrector
  : CumulativeCorrector<AbbB2xMeasurementRecord>
{
  protected override AbbB2xMeasurementRecord CorrectCumulatives(
    DateTimeOffset timestamp,
    AbbB2xMeasurementRecord measurementRecord,
    AbbB2xMeasurementRecord firstMeasurementRecord,
    AbbB2xMeasurementRecord lastMeasurementRecord
  )
  {
    var diffMultiplier = DiffMultiplier(
      timestamp,
      firstMeasurementRecord.Timestamp,
      lastMeasurementRecord.Timestamp
    );

    var activeEnergy = measurementRecord.ActiveEnergy_Wh +
                       (lastMeasurementRecord.ActiveEnergy_Wh
                        - firstMeasurementRecord.ActiveEnergy_Wh) *
                       diffMultiplier;

    var reactiveEnergy = measurementRecord.ReactiveEnergy_VARh +
                         (lastMeasurementRecord.ReactiveEnergy_VARh
                          - firstMeasurementRecord.ReactiveEnergy_VARh) *
                         diffMultiplier;

    measurementRecord.ActiveEnergyL1ImportT0_Wh =
      activeEnergy.TariffUnary.DuplexImport.PhaseSplit.ValueL1;
    measurementRecord.ActiveEnergyL2ImportT0_Wh =
      activeEnergy.TariffUnary.DuplexImport.PhaseSplit.ValueL2;
    measurementRecord.ActiveEnergyL3ImportT0_Wh =
      activeEnergy.TariffUnary.DuplexImport.PhaseSplit.ValueL3;
    measurementRecord.ActiveEnergyTotalImportT0_Wh =
      activeEnergy.TariffUnary.DuplexImport.PhaseSum;

    measurementRecord.ActiveEnergyL1ExportT0_Wh =
      activeEnergy.TariffUnary.DuplexExport.PhaseSplit.ValueL1;
    measurementRecord.ActiveEnergyL2ExportT0_Wh =
      activeEnergy.TariffUnary.DuplexExport.PhaseSplit.ValueL2;
    measurementRecord.ActiveEnergyL3ExportT0_Wh =
      activeEnergy.TariffUnary.DuplexExport.PhaseSplit.ValueL3;
    measurementRecord.ActiveEnergyTotalExportT0_Wh =
      activeEnergy.TariffUnary.DuplexExport.PhaseSum;

    measurementRecord.ReactiveEnergyL1ImportT0_VARh =
      reactiveEnergy.TariffUnary.DuplexImport.PhaseSplit.ValueL1;
    measurementRecord.ReactiveEnergyL2ImportT0_VARh =
      reactiveEnergy.TariffUnary.DuplexImport.PhaseSplit.ValueL2;
    measurementRecord.ReactiveEnergyL3ImportT0_VARh =
      reactiveEnergy.TariffUnary.DuplexImport.PhaseSplit.ValueL3;
    measurementRecord.ReactiveEnergyTotalImportT0_VARh =
      reactiveEnergy.TariffUnary.DuplexImport.PhaseSum;

    measurementRecord.ReactiveEnergyL1ExportT0_VARh =
      reactiveEnergy.TariffUnary.DuplexExport.PhaseSplit.ValueL1;
    measurementRecord.ReactiveEnergyL2ImportT0_VARh =
      reactiveEnergy.TariffUnary.DuplexExport.PhaseSplit.ValueL2;
    measurementRecord.ReactiveEnergyL3ImportT0_VARh =
      reactiveEnergy.TariffUnary.DuplexExport.PhaseSplit.ValueL3;
    measurementRecord.ReactiveEnergyTotalExportT0_VARh =
      reactiveEnergy.TariffUnary.DuplexExport.PhaseSum;

    measurementRecord.ActiveEnergyTotalImportT1_Wh =
      activeEnergy.TariffBinary.T1.DuplexImport.PhaseSum;
    measurementRecord.ActiveEnergyTotalImportT2_Wh =
      activeEnergy.TariffBinary.T2.DuplexExport.PhaseSum;

    return measurementRecord;
  }

  protected override AbbB2xMeasurementRecord CopyRecord(
    AbbB2xMeasurementRecord record)
  {
    return new AbbB2xMeasurementRecord
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
      ReactivePowerL1NetT0_VAR = record.ReactivePowerL1NetT0_VAR,
      ReactivePowerL2NetT0_VAR = record.ReactivePowerL2NetT0_VAR,
      ReactivePowerL3NetT0_VAR = record.ReactivePowerL3NetT0_VAR,
      ActiveEnergyL1ImportT0_Wh = record.ActiveEnergyL1ImportT0_Wh,
      ActiveEnergyL2ImportT0_Wh = record.ActiveEnergyL2ImportT0_Wh,
      ActiveEnergyL3ImportT0_Wh = record.ActiveEnergyL3ImportT0_Wh,
      ActiveEnergyL1ExportT0_Wh = record.ActiveEnergyL1ExportT0_Wh,
      ActiveEnergyL2ExportT0_Wh = record.ActiveEnergyL2ExportT0_Wh,
      ActiveEnergyL3ExportT0_Wh = record.ActiveEnergyL3ExportT0_Wh,
      ReactiveEnergyL1ImportT0_VARh = record.ReactiveEnergyL1ImportT0_VARh,
      ReactiveEnergyL2ImportT0_VARh = record.ReactiveEnergyL2ImportT0_VARh,
      ReactiveEnergyL3ImportT0_VARh = record.ReactiveEnergyL3ImportT0_VARh,
      ReactiveEnergyL1ExportT0_VARh = record.ReactiveEnergyL1ExportT0_VARh,
      ReactiveEnergyL2ExportT0_VARh = record.ReactiveEnergyL2ExportT0_VARh,
      ReactiveEnergyL3ExportT0_VARh = record.ReactiveEnergyL3ExportT0_VARh,
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

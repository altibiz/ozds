using Ozds.Business.Math;
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
      new MinMaxSpanningMeasure<float>(
        firstMeasurementRecord.ActiveEnergy_Wh,
        lastMeasurementRecord.ActiveEnergy_Wh
      ).SpanDiff * diffMultiplier;

    var reactiveEnergy = measurementRecord.ReactiveEnergy_VARh +
      new MinMaxSpanningMeasure<float>(
        firstMeasurementRecord.ReactiveEnergy_VARh,
        lastMeasurementRecord.ReactiveEnergy_VARh
      ).SpanDiff * diffMultiplier;

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

    return measurementRecord;
  }
}

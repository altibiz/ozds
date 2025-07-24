using Ozds.Business.Models;
using Ozds.Fake.Correction.Base;
using Ozds.Fake.Records;

namespace Ozds.Fake.Correction.Implementations;

public class SchneideriEM3xxxMeasurementRecordCorrector
  : ConcreteRecordCorrector<
    SchneideriEM3xxxMeasurementRecord,
    SchneideriEM3xxxMeasurementValidatorModel>
{
  protected override SchneideriEM3xxxMeasurementRecord CopyRecord(
    SchneideriEM3xxxMeasurementRecord record)
  {
    return new SchneideriEM3xxxMeasurementRecord
    {
      MeterId = record.MeterId,
      Timestamp = record.Timestamp,
      MeasurementLocationId = record.MeasurementLocationId,
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

  protected override SchneideriEM3xxxMeasurementRecord CorrectMeterId(
    SchneideriEM3xxxMeasurementRecord measurementRecord,
    string meterId
  )
  {
    measurementRecord.MeterId = meterId;
    return measurementRecord;
  }

  protected override SchneideriEM3xxxMeasurementRecord
    CorrectMeasurementLocationId(
      SchneideriEM3xxxMeasurementRecord measurementRecord,
      string measurementLocationId)
  {
    measurementRecord.MeasurementLocationId = measurementLocationId;
    return measurementRecord;
  }

  protected override SchneideriEM3xxxMeasurementRecord CorrectTimestamp(
    SchneideriEM3xxxMeasurementRecord measurementRecord,
    DateTimeOffset timestamp
  )
  {
    measurementRecord.Timestamp = timestamp;
    return measurementRecord;
  }

  protected override SchneideriEM3xxxMeasurementRecord CorrectCumulatives(
    SchneideriEM3xxxMeasurementRecord measurementRecord,
    SchneideriEM3xxxMeasurementRecord firstMeasurementRecord,
    SchneideriEM3xxxMeasurementRecord lastMeasurementRecord
  )
  {
    var diffMultiplier = DiffMultiplier(
      measurementRecord.Timestamp,
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
      activeEnergy.TariffUnary().DuplexImport().PhaseSplit().ValueL1;
    measurementRecord.ActiveEnergyL2ImportT0_Wh =
      activeEnergy.TariffUnary().DuplexImport().PhaseSplit().ValueL2;
    measurementRecord.ActiveEnergyL3ImportT0_Wh =
      activeEnergy.TariffUnary().DuplexImport().PhaseSplit().ValueL3;
    measurementRecord.ActiveEnergyTotalImportT0_Wh =
      activeEnergy.TariffUnary().DuplexImport().PhaseSum();

    measurementRecord.ActiveEnergyTotalExportT0_Wh =
      activeEnergy.TariffUnary().DuplexExport().PhaseSum();

    measurementRecord.ReactiveEnergyTotalImportT0_VARh =
      reactiveEnergy.TariffUnary().DuplexImport().PhaseSum();

    measurementRecord.ReactiveEnergyTotalExportT0_VARh =
      reactiveEnergy.TariffUnary().DuplexExport().PhaseSum();

    measurementRecord.ApparentPowerTotalNetT0_VA =
      apparentEnergy.TariffUnary().DuplexNet().PhaseSum();

    measurementRecord.ActiveEnergyTotalImportT1_Wh =
      activeEnergy.TariffBinary().T1.DuplexImport().PhaseSum();
    measurementRecord.ActiveEnergyTotalImportT2_Wh =
      activeEnergy.TariffBinary().T2.DuplexExport().PhaseSum();

    return measurementRecord;
  }

  protected override SchneideriEM3xxxMeasurementRecord CorrectValidation(
    SchneideriEM3xxxMeasurementRecord measurementRecord,
    SchneideriEM3xxxMeasurementValidatorModel validator
  )
  {
    measurementRecord.VoltageL1AnyT0_V = Clamp(
      measurementRecord.VoltageL1AnyT0_V,
      validator.MinVoltage_V,
      validator.MaxVoltage_V);
    measurementRecord.VoltageL2AnyT0_V = Clamp(
      measurementRecord.VoltageL2AnyT0_V,
      validator.MinVoltage_V,
      validator.MaxVoltage_V);
    measurementRecord.VoltageL3AnyT0_V = Clamp(
      measurementRecord.VoltageL3AnyT0_V,
      validator.MinVoltage_V,
      validator.MaxVoltage_V);
    measurementRecord.CurrentL1AnyT0_A = Clamp(
      measurementRecord.CurrentL1AnyT0_A,
      validator.MinCurrent_A,
      validator.MaxCurrent_A);
    measurementRecord.CurrentL2AnyT0_A = Clamp(
      measurementRecord.CurrentL2AnyT0_A,
      validator.MinCurrent_A,
      validator.MaxCurrent_A);
    measurementRecord.CurrentL3AnyT0_A = Clamp(
      measurementRecord.CurrentL3AnyT0_A,
      validator.MinCurrent_A,
      validator.MaxCurrent_A);
    measurementRecord.ActivePowerL1NetT0_W = Clamp(
      measurementRecord.ActivePowerL1NetT0_W,
      validator.MinActivePower_W,
      validator.MaxActivePower_W);
    measurementRecord.ActivePowerL2NetT0_W = Clamp(
      measurementRecord.ActivePowerL2NetT0_W,
      validator.MinActivePower_W,
      validator.MaxActivePower_W);
    measurementRecord.ActivePowerL3NetT0_W = Clamp(
      measurementRecord.ActivePowerL3NetT0_W,
      validator.MinActivePower_W,
      validator.MaxActivePower_W);
    measurementRecord.ReactivePowerTotalNetT0_VAR = Clamp(
      measurementRecord.ReactivePowerTotalNetT0_VAR,
      validator.MinReactivePower_VAR * 3,
      validator.MaxReactivePower_VAR * 3);
    measurementRecord.ApparentPowerTotalNetT0_VA = Clamp(
      measurementRecord.ApparentPowerTotalNetT0_VA,
      validator.MinApparentPower_VA * 3,
      validator.MaxApparentPower_VA * 3);
    return measurementRecord;
  }
}

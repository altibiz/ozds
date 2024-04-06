using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class AbbB2xMeasurementModelEntityConverter : ModelEntityConverter<
  AbbB2xMeasurementModel, AbbB2xMeasurementEntity>
{
  protected override AbbB2xMeasurementEntity ToEntity(
    AbbB2xMeasurementModel model)
  {
    return model.ToEntity();
  }

  protected override AbbB2xMeasurementModel ToModel(
    AbbB2xMeasurementEntity entity)
  {
    return entity.ToModel();
  }
}

public static class AbbB2xMeasurementModelEntityConverterExtensions
{
  public static AbbB2xMeasurementModel ToModel(
    this AbbB2xMeasurementEntity entity)
  {
    return new AbbB2xMeasurementModel
    {
      MeterId = entity.MeterId,
      Timestamp = entity.Timestamp,
      VoltageL1AnyT0_V = entity.VoltageL1AnyT0_V,
      VoltageL2AnyT0_V = entity.VoltageL2AnyT0_V,
      VoltageL3AnyT0_V = entity.VoltageL3AnyT0_V,
      CurrentL1AnyT0_A = entity.CurrentL1AnyT0_A,
      CurrentL2AnyT0_A = entity.CurrentL2AnyT0_A,
      CurrentL3AnyT0_A = entity.CurrentL3AnyT0_A,
      ActivePowerL1NetT0_W = entity.ActivePowerL1NetT0_W,
      ActivePowerL2NetT0_W = entity.ActivePowerL2NetT0_W,
      ActivePowerL3NetT0_W = entity.ActivePowerL3NetT0_W,
      ReactivePowerL1NetT0_VAR = entity.ReactivePowerL1NetT0_VAR,
      ReactivePowerL2NetT0_VAR = entity.ReactivePowerL2NetT0_VAR,
      ReactivePowerL3NetT0_VAR = entity.ReactivePowerL3NetT0_VAR,
      ActiveEnergyL1ImportT0_Wh = entity.ActiveEnergyL1ImportT0_Wh,
      ActiveEnergyL2ImportT0_Wh = entity.ActiveEnergyL2ImportT0_Wh,
      ActiveEnergyL3ImportT0_Wh = entity.ActiveEnergyL3ImportT0_Wh,
      ActiveEnergyL1ExportT0_Wh = entity.ActiveEnergyL1ExportT0_Wh,
      ActiveEnergyL2ExportT0_Wh = entity.ActiveEnergyL2ExportT0_Wh,
      ActiveEnergyL3ExportT0_Wh = entity.ActiveEnergyL3ExportT0_Wh,
      ReactiveEnergyL1ImportT0_VARh = entity.ReactiveEnergyL1ImportT0_VARh,
      ReactiveEnergyL2ImportT0_VARh = entity.ReactiveEnergyL2ImportT0_VARh,
      ReactiveEnergyL3ImportT0_VARh = entity.ReactiveEnergyL3ImportT0_VARh,
      ReactiveEnergyL1ExportT0_VARh = entity.ReactiveEnergyL1ExportT0_VARh,
      ReactiveEnergyL2ExportT0_VARh = entity.ReactiveEnergyL2ExportT0_VARh,
      ReactiveEnergyL3ExportT0_VARh = entity.ReactiveEnergyL3ExportT0_VARh,
      ActiveEnergyTotalImportT0_Wh = entity.ActiveEnergyTotalImportT0_Wh,
      ActiveEnergyTotalExportT0_Wh = entity.ActiveEnergyTotalExportT0_Wh,
      ReactiveEnergyTotalImportT0_VARh =
        entity.ReactiveEnergyTotalImportT0_VARh,
      ReactiveEnergyTotalExportT0_VARh =
        entity.ReactiveEnergyTotalExportT0_VARh,
      ActiveEnergyTotalImportT1_Wh = entity.ActiveEnergyTotalImportT1_Wh,
      ActiveEnergyTotalImportT2_Wh = entity.ActiveEnergyTotalImportT2_Wh
    };
  }

  public static AbbB2xMeasurementEntity ToEntity(
    this AbbB2xMeasurementModel model)
  {
    return new AbbB2xMeasurementEntity
    {
      MeterId = model.MeterId,
      Timestamp = model.Timestamp,
      VoltageL1AnyT0_V = model.VoltageL1AnyT0_V,
      VoltageL2AnyT0_V = model.VoltageL2AnyT0_V,
      VoltageL3AnyT0_V = model.VoltageL3AnyT0_V,
      CurrentL1AnyT0_A = model.CurrentL1AnyT0_A,
      CurrentL2AnyT0_A = model.CurrentL2AnyT0_A,
      CurrentL3AnyT0_A = model.CurrentL3AnyT0_A,
      ActivePowerL1NetT0_W = model.ActivePowerL1NetT0_W,
      ActivePowerL2NetT0_W = model.ActivePowerL2NetT0_W,
      ActivePowerL3NetT0_W = model.ActivePowerL3NetT0_W,
      ReactivePowerL1NetT0_VAR = model.ReactivePowerL1NetT0_VAR,
      ReactivePowerL2NetT0_VAR = model.ReactivePowerL2NetT0_VAR,
      ReactivePowerL3NetT0_VAR = model.ReactivePowerL3NetT0_VAR,
      ActiveEnergyL1ImportT0_Wh = model.ActiveEnergyL1ImportT0_Wh,
      ActiveEnergyL2ImportT0_Wh = model.ActiveEnergyL2ImportT0_Wh,
      ActiveEnergyL3ImportT0_Wh = model.ActiveEnergyL3ImportT0_Wh,
      ActiveEnergyL1ExportT0_Wh = model.ActiveEnergyL1ExportT0_Wh,
      ActiveEnergyL2ExportT0_Wh = model.ActiveEnergyL2ExportT0_Wh,
      ActiveEnergyL3ExportT0_Wh = model.ActiveEnergyL3ExportT0_Wh,
      ReactiveEnergyL1ImportT0_VARh = model.ReactiveEnergyL1ImportT0_VARh,
      ReactiveEnergyL2ImportT0_VARh = model.ReactiveEnergyL2ImportT0_VARh,
      ReactiveEnergyL3ImportT0_VARh = model.ReactiveEnergyL3ImportT0_VARh,
      ReactiveEnergyL1ExportT0_VARh = model.ReactiveEnergyL1ExportT0_VARh,
      ReactiveEnergyL2ExportT0_VARh = model.ReactiveEnergyL2ExportT0_VARh,
      ReactiveEnergyL3ExportT0_VARh = model.ReactiveEnergyL3ExportT0_VARh,
      ActiveEnergyTotalImportT0_Wh = model.ActiveEnergyTotalImportT0_Wh,
      ActiveEnergyTotalExportT0_Wh = model.ActiveEnergyTotalExportT0_Wh,
      ReactiveEnergyTotalImportT0_VARh = model.ReactiveEnergyTotalImportT0_VARh,
      ReactiveEnergyTotalExportT0_VARh = model.ReactiveEnergyTotalExportT0_VARh,
      ActiveEnergyTotalImportT1_Wh = model.ActiveEnergyTotalImportT1_Wh,
      ActiveEnergyTotalImportT2_Wh = model.ActiveEnergyTotalImportT2_Wh
    };
  }
}

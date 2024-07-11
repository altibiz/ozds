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
      VoltageL1AnyT0_V = (decimal)entity.VoltageL1AnyT0_V,
      VoltageL2AnyT0_V = (decimal)entity.VoltageL2AnyT0_V,
      VoltageL3AnyT0_V = (decimal)entity.VoltageL3AnyT0_V,
      CurrentL1AnyT0_A = (decimal)entity.CurrentL1AnyT0_A,
      CurrentL2AnyT0_A = (decimal)entity.CurrentL2AnyT0_A,
      CurrentL3AnyT0_A = (decimal)entity.CurrentL3AnyT0_A,
      ActivePowerL1NetT0_W = (decimal)entity.ActivePowerL1NetT0_W,
      ActivePowerL2NetT0_W = (decimal)entity.ActivePowerL2NetT0_W,
      ActivePowerL3NetT0_W = (decimal)entity.ActivePowerL3NetT0_W,
      ReactivePowerL1NetT0_VAR = (decimal)entity.ReactivePowerL1NetT0_VAR,
      ReactivePowerL2NetT0_VAR = (decimal)entity.ReactivePowerL2NetT0_VAR,
      ReactivePowerL3NetT0_VAR = (decimal)entity.ReactivePowerL3NetT0_VAR,
      ActiveEnergyL1ImportT0_Wh = (decimal)entity.ActiveEnergyL1ImportT0_Wh,
      ActiveEnergyL2ImportT0_Wh = (decimal)entity.ActiveEnergyL2ImportT0_Wh,
      ActiveEnergyL3ImportT0_Wh = (decimal)entity.ActiveEnergyL3ImportT0_Wh,
      ActiveEnergyL1ExportT0_Wh = (decimal)entity.ActiveEnergyL1ExportT0_Wh,
      ActiveEnergyL2ExportT0_Wh = (decimal)entity.ActiveEnergyL2ExportT0_Wh,
      ActiveEnergyL3ExportT0_Wh = (decimal)entity.ActiveEnergyL3ExportT0_Wh,
      ReactiveEnergyL1ImportT0_VARh =
        (decimal)entity.ReactiveEnergyL1ImportT0_VARh,
      ReactiveEnergyL2ImportT0_VARh =
        (decimal)entity.ReactiveEnergyL2ImportT0_VARh,
      ReactiveEnergyL3ImportT0_VARh =
        (decimal)entity.ReactiveEnergyL3ImportT0_VARh,
      ReactiveEnergyL1ExportT0_VARh =
        (decimal)entity.ReactiveEnergyL1ExportT0_VARh,
      ReactiveEnergyL2ExportT0_VARh =
        (decimal)entity.ReactiveEnergyL2ExportT0_VARh,
      ReactiveEnergyL3ExportT0_VARh =
        (decimal)entity.ReactiveEnergyL3ExportT0_VARh,
      ActiveEnergyTotalImportT0_Wh =
        (decimal)entity.ActiveEnergyTotalImportT0_Wh,
      ActiveEnergyTotalExportT0_Wh =
        (decimal)entity.ActiveEnergyTotalExportT0_Wh,
      ReactiveEnergyTotalImportT0_VARh =
        (decimal)entity.ReactiveEnergyTotalImportT0_VARh,
      ReactiveEnergyTotalExportT0_VARh =
        (decimal)entity.ReactiveEnergyTotalExportT0_VARh,
      ActiveEnergyTotalImportT1_Wh =
        (decimal)entity.ActiveEnergyTotalImportT1_Wh,
      ActiveEnergyTotalImportT2_Wh =
        (decimal)entity.ActiveEnergyTotalImportT2_Wh
    };
  }

  public static AbbB2xMeasurementEntity ToEntity(
    this AbbB2xMeasurementModel model)
  {
    return new AbbB2xMeasurementEntity
    {
      MeterId = model.MeterId,
      Timestamp = model.Timestamp,
      VoltageL1AnyT0_V = (float)model.VoltageL1AnyT0_V,
      VoltageL2AnyT0_V = (float)model.VoltageL2AnyT0_V,
      VoltageL3AnyT0_V = (float)model.VoltageL3AnyT0_V,
      CurrentL1AnyT0_A = (float)model.CurrentL1AnyT0_A,
      CurrentL2AnyT0_A = (float)model.CurrentL2AnyT0_A,
      CurrentL3AnyT0_A = (float)model.CurrentL3AnyT0_A,
      ActivePowerL1NetT0_W = (float)model.ActivePowerL1NetT0_W,
      ActivePowerL2NetT0_W = (float)model.ActivePowerL2NetT0_W,
      ActivePowerL3NetT0_W = (float)model.ActivePowerL3NetT0_W,
      ReactivePowerL1NetT0_VAR = (float)model.ReactivePowerL1NetT0_VAR,
      ReactivePowerL2NetT0_VAR = (float)model.ReactivePowerL2NetT0_VAR,
      ReactivePowerL3NetT0_VAR = (float)model.ReactivePowerL3NetT0_VAR,
      ActiveEnergyL1ImportT0_Wh = (float)model.ActiveEnergyL1ImportT0_Wh,
      ActiveEnergyL2ImportT0_Wh = (float)model.ActiveEnergyL2ImportT0_Wh,
      ActiveEnergyL3ImportT0_Wh = (float)model.ActiveEnergyL3ImportT0_Wh,
      ActiveEnergyL1ExportT0_Wh = (float)model.ActiveEnergyL1ExportT0_Wh,
      ActiveEnergyL2ExportT0_Wh = (float)model.ActiveEnergyL2ExportT0_Wh,
      ActiveEnergyL3ExportT0_Wh = (float)model.ActiveEnergyL3ExportT0_Wh,
      ReactiveEnergyL1ImportT0_VARh =
        (float)model.ReactiveEnergyL1ImportT0_VARh,
      ReactiveEnergyL2ImportT0_VARh =
        (float)model.ReactiveEnergyL2ImportT0_VARh,
      ReactiveEnergyL3ImportT0_VARh =
        (float)model.ReactiveEnergyL3ImportT0_VARh,
      ReactiveEnergyL1ExportT0_VARh =
        (float)model.ReactiveEnergyL1ExportT0_VARh,
      ReactiveEnergyL2ExportT0_VARh =
        (float)model.ReactiveEnergyL2ExportT0_VARh,
      ReactiveEnergyL3ExportT0_VARh =
        (float)model.ReactiveEnergyL3ExportT0_VARh,
      ActiveEnergyTotalImportT0_Wh = (float)model.ActiveEnergyTotalImportT0_Wh,
      ActiveEnergyTotalExportT0_Wh = (float)model.ActiveEnergyTotalExportT0_Wh,
      ReactiveEnergyTotalImportT0_VARh =
        (float)model.ReactiveEnergyTotalImportT0_VARh,
      ReactiveEnergyTotalExportT0_VARh =
        (float)model.ReactiveEnergyTotalExportT0_VARh,
      ActiveEnergyTotalImportT1_Wh = (float)model.ActiveEnergyTotalImportT1_Wh,
      ActiveEnergyTotalImportT2_Wh = (float)model.ActiveEnergyTotalImportT2_Wh
    };
  }
}

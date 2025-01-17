using Ozds.Business.Conversion.Base;
using Ozds.Business.Conversion.Primitive;
using Ozds.Business.Models;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion.Implementations.Measurements;

public class AbbB2xMeasurementModelEntityConverter : ConcreteModelEntityConverter<
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
      MeasurementLocationId = entity.MeasurementLocationId,
      Timestamp = entity.Timestamp,
      VoltageL1AnyT0_V = entity.VoltageL1AnyT0_V.ToDecimal(),
      VoltageL2AnyT0_V = entity.VoltageL2AnyT0_V.ToDecimal(),
      VoltageL3AnyT0_V = entity.VoltageL3AnyT0_V.ToDecimal(),
      CurrentL1AnyT0_A = entity.CurrentL1AnyT0_A.ToDecimal(),
      CurrentL2AnyT0_A = entity.CurrentL2AnyT0_A.ToDecimal(),
      CurrentL3AnyT0_A = entity.CurrentL3AnyT0_A.ToDecimal(),
      ActivePowerL1NetT0_W = entity.ActivePowerL1NetT0_W.ToDecimal(),
      ActivePowerL2NetT0_W = entity.ActivePowerL2NetT0_W.ToDecimal(),
      ActivePowerL3NetT0_W = entity.ActivePowerL3NetT0_W.ToDecimal(),
      ReactivePowerL1NetT0_VAR = entity.ReactivePowerL1NetT0_VAR.ToDecimal(),
      ReactivePowerL2NetT0_VAR = entity.ReactivePowerL2NetT0_VAR.ToDecimal(),
      ReactivePowerL3NetT0_VAR = entity.ReactivePowerL3NetT0_VAR.ToDecimal(),
      ActiveEnergyL1ImportT0_Wh = entity.ActiveEnergyL1ImportT0_Wh.ToDecimal(),
      ActiveEnergyL2ImportT0_Wh = entity.ActiveEnergyL2ImportT0_Wh.ToDecimal(),
      ActiveEnergyL3ImportT0_Wh = entity.ActiveEnergyL3ImportT0_Wh.ToDecimal(),
      ActiveEnergyL1ExportT0_Wh = entity.ActiveEnergyL1ExportT0_Wh.ToDecimal(),
      ActiveEnergyL2ExportT0_Wh = entity.ActiveEnergyL2ExportT0_Wh.ToDecimal(),
      ActiveEnergyL3ExportT0_Wh = entity.ActiveEnergyL3ExportT0_Wh.ToDecimal(),
      ReactiveEnergyL1ImportT0_VARh =
        entity.ReactiveEnergyL1ImportT0_VARh.ToDecimal(),
      ReactiveEnergyL2ImportT0_VARh =
        entity.ReactiveEnergyL2ImportT0_VARh.ToDecimal(),
      ReactiveEnergyL3ImportT0_VARh =
        entity.ReactiveEnergyL3ImportT0_VARh.ToDecimal(),
      ReactiveEnergyL1ExportT0_VARh =
        entity.ReactiveEnergyL1ExportT0_VARh.ToDecimal(),
      ReactiveEnergyL2ExportT0_VARh =
        entity.ReactiveEnergyL2ExportT0_VARh.ToDecimal(),
      ReactiveEnergyL3ExportT0_VARh =
        entity.ReactiveEnergyL3ExportT0_VARh.ToDecimal(),
      ActiveEnergyTotalImportT0_Wh =
        entity.ActiveEnergyTotalImportT0_Wh.ToDecimal(),
      ActiveEnergyTotalExportT0_Wh =
        entity.ActiveEnergyTotalExportT0_Wh.ToDecimal(),
      ReactiveEnergyTotalImportT0_VARh =
        entity.ReactiveEnergyTotalImportT0_VARh.ToDecimal(),
      ReactiveEnergyTotalExportT0_VARh =
        entity.ReactiveEnergyTotalExportT0_VARh.ToDecimal(),
      ActiveEnergyTotalImportT1_Wh =
        entity.ActiveEnergyTotalImportT1_Wh.ToDecimal(),
      ActiveEnergyTotalImportT2_Wh =
        entity.ActiveEnergyTotalImportT2_Wh.ToDecimal()
    };
  }

  public static AbbB2xMeasurementEntity ToEntity(
    this AbbB2xMeasurementModel model)
  {
    return new AbbB2xMeasurementEntity
    {
      MeterId = model.MeterId,
      MeasurementLocationId = model.MeasurementLocationId,
      Timestamp = model.Timestamp,
      VoltageL1AnyT0_V = model.VoltageL1AnyT0_V.ToFloat(),
      VoltageL2AnyT0_V = model.VoltageL2AnyT0_V.ToFloat(),
      VoltageL3AnyT0_V = model.VoltageL3AnyT0_V.ToFloat(),
      CurrentL1AnyT0_A = model.CurrentL1AnyT0_A.ToFloat(),
      CurrentL2AnyT0_A = model.CurrentL2AnyT0_A.ToFloat(),
      CurrentL3AnyT0_A = model.CurrentL3AnyT0_A.ToFloat(),
      ActivePowerL1NetT0_W = model.ActivePowerL1NetT0_W.ToFloat(),
      ActivePowerL2NetT0_W = model.ActivePowerL2NetT0_W.ToFloat(),
      ActivePowerL3NetT0_W = model.ActivePowerL3NetT0_W.ToFloat(),
      ReactivePowerL1NetT0_VAR = model.ReactivePowerL1NetT0_VAR.ToFloat(),
      ReactivePowerL2NetT0_VAR = model.ReactivePowerL2NetT0_VAR.ToFloat(),
      ReactivePowerL3NetT0_VAR = model.ReactivePowerL3NetT0_VAR.ToFloat(),
      ActiveEnergyL1ImportT0_Wh = model.ActiveEnergyL1ImportT0_Wh.ToFloat(),
      ActiveEnergyL2ImportT0_Wh = model.ActiveEnergyL2ImportT0_Wh.ToFloat(),
      ActiveEnergyL3ImportT0_Wh = model.ActiveEnergyL3ImportT0_Wh.ToFloat(),
      ActiveEnergyL1ExportT0_Wh = model.ActiveEnergyL1ExportT0_Wh.ToFloat(),
      ActiveEnergyL2ExportT0_Wh = model.ActiveEnergyL2ExportT0_Wh.ToFloat(),
      ActiveEnergyL3ExportT0_Wh = model.ActiveEnergyL3ExportT0_Wh.ToFloat(),
      ReactiveEnergyL1ImportT0_VARh =
        model.ReactiveEnergyL1ImportT0_VARh.ToFloat(),
      ReactiveEnergyL2ImportT0_VARh =
        model.ReactiveEnergyL2ImportT0_VARh.ToFloat(),
      ReactiveEnergyL3ImportT0_VARh =
        model.ReactiveEnergyL3ImportT0_VARh.ToFloat(),
      ReactiveEnergyL1ExportT0_VARh =
        model.ReactiveEnergyL1ExportT0_VARh.ToFloat(),
      ReactiveEnergyL2ExportT0_VARh =
        model.ReactiveEnergyL2ExportT0_VARh.ToFloat(),
      ReactiveEnergyL3ExportT0_VARh =
        model.ReactiveEnergyL3ExportT0_VARh.ToFloat(),
      ActiveEnergyTotalImportT0_Wh = model.ActiveEnergyTotalImportT0_Wh.ToFloat(),
      ActiveEnergyTotalExportT0_Wh = model.ActiveEnergyTotalExportT0_Wh.ToFloat(),
      ReactiveEnergyTotalImportT0_VARh =
        model.ReactiveEnergyTotalImportT0_VARh.ToFloat(),
      ReactiveEnergyTotalExportT0_VARh =
        model.ReactiveEnergyTotalExportT0_VARh.ToFloat(),
      ActiveEnergyTotalImportT1_Wh = model.ActiveEnergyTotalImportT1_Wh.ToFloat(),
      ActiveEnergyTotalImportT2_Wh = model.ActiveEnergyTotalImportT2_Wh.ToFloat()
    };
  }
}

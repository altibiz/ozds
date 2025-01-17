using Ozds.Business.Conversion.Base;
using Ozds.Business.Conversion.Primitive;
using Ozds.Business.Models;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class SchneideriEM3xxxMeasurementModelEntityConverter :
  ConcreteModelEntityConverter<
    SchneideriEM3xxxMeasurementModel,
    SchneideriEM3xxxMeasurementEntity>
{
  protected override SchneideriEM3xxxMeasurementEntity ToEntity(
    SchneideriEM3xxxMeasurementModel model)
  {
    return model.ToEntity();
  }

  protected override SchneideriEM3xxxMeasurementModel ToModel(
    SchneideriEM3xxxMeasurementEntity entity)
  {
    return entity.ToModel();
  }
}

public static class SchneideriEM3xxxMeasurementModelEntityConverterExtensions
{
  public static SchneideriEM3xxxMeasurementModel ToModel(
    this SchneideriEM3xxxMeasurementEntity entity)
  {
    return new SchneideriEM3xxxMeasurementModel
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
      ReactivePowerTotalNetT0_VAR = entity.ReactivePowerTotalNetT0_VAR.ToDecimal(),
      ApparentPowerTotalNetT0_VA = entity.ApparentPowerTotalNetT0_VA.ToDecimal(),
      ActiveEnergyL1ImportT0_Wh = entity.ActiveEnergyL1ImportT0_Wh.ToDecimal(),
      ActiveEnergyL2ImportT0_Wh = entity.ActiveEnergyL2ImportT0_Wh.ToDecimal(),
      ActiveEnergyL3ImportT0_Wh = entity.ActiveEnergyL3ImportT0_Wh.ToDecimal(),
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

  public static SchneideriEM3xxxMeasurementEntity ToEntity(
    this SchneideriEM3xxxMeasurementModel model)
  {
    return new SchneideriEM3xxxMeasurementEntity
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
      ReactivePowerTotalNetT0_VAR = model.ReactivePowerTotalNetT0_VAR.ToFloat(),
      ApparentPowerTotalNetT0_VA = model.ApparentPowerTotalNetT0_VA.ToFloat(),
      ActiveEnergyL1ImportT0_Wh = model.ActiveEnergyL1ImportT0_Wh.ToFloat(),
      ActiveEnergyL2ImportT0_Wh = model.ActiveEnergyL2ImportT0_Wh.ToFloat(),
      ActiveEnergyL3ImportT0_Wh = model.ActiveEnergyL3ImportT0_Wh.ToFloat(),
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

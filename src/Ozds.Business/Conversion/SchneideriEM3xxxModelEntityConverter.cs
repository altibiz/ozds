using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class SchneideriEM3xxxModelEntityConverter : ModelEntityConverter<
  SchneideriEM3xxxMeasurementModel, SchneideriEM3xxxMeasurementEntity>
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
      ReactivePowerTotalNetT0_VAR = entity.ReactivePowerTotalNetT0_VAR,
      ApparentPowerTotalNetT0_VA = entity.ApparentPowerTotalNetT0_VA,
      ActiveEnergyL1ImportT0_Wh = entity.ActiveEnergyL1ImportT0_Wh,
      ActiveEnergyL2ImportT0_Wh = entity.ActiveEnergyL2ImportT0_Wh,
      ActiveEnergyL3ImportT0_Wh = entity.ActiveEnergyL3ImportT0_Wh,
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

  public static SchneideriEM3xxxMeasurementEntity ToEntity(
    this SchneideriEM3xxxMeasurementModel model)
  {
    return new SchneideriEM3xxxMeasurementEntity
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
      ReactivePowerTotalNetT0_VAR = model.ReactivePowerTotalNetT0_VAR,
      ApparentPowerTotalNetT0_VA = model.ApparentPowerTotalNetT0_VA,
      ActiveEnergyL1ImportT0_Wh = model.ActiveEnergyL1ImportT0_Wh,
      ActiveEnergyL2ImportT0_Wh = model.ActiveEnergyL2ImportT0_Wh,
      ActiveEnergyL3ImportT0_Wh = model.ActiveEnergyL3ImportT0_Wh,
      ActiveEnergyTotalImportT0_Wh = model.ActiveEnergyTotalImportT0_Wh,
      ActiveEnergyTotalExportT0_Wh = model.ActiveEnergyTotalExportT0_Wh,
      ReactiveEnergyTotalImportT0_VARh = model.ReactiveEnergyTotalImportT0_VARh,
      ReactiveEnergyTotalExportT0_VARh = model.ReactiveEnergyTotalExportT0_VARh,
      ActiveEnergyTotalImportT1_Wh = model.ActiveEnergyTotalImportT1_Wh,
      ActiveEnergyTotalImportT2_Wh = model.ActiveEnergyTotalImportT2_Wh
    };
  }
}

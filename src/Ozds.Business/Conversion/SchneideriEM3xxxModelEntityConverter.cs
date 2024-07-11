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
      VoltageL1AnyT0_V = (decimal)entity.VoltageL1AnyT0_V,
      VoltageL2AnyT0_V = (decimal)entity.VoltageL2AnyT0_V,
      VoltageL3AnyT0_V = (decimal)entity.VoltageL3AnyT0_V,
      CurrentL1AnyT0_A = (decimal)entity.CurrentL1AnyT0_A,
      CurrentL2AnyT0_A = (decimal)entity.CurrentL2AnyT0_A,
      CurrentL3AnyT0_A = (decimal)entity.CurrentL3AnyT0_A,
      ActivePowerL1NetT0_W = (decimal)entity.ActivePowerL1NetT0_W,
      ActivePowerL2NetT0_W = (decimal)entity.ActivePowerL2NetT0_W,
      ActivePowerL3NetT0_W = (decimal)entity.ActivePowerL3NetT0_W,
      ReactivePowerTotalNetT0_VAR = (decimal)entity.ReactivePowerTotalNetT0_VAR,
      ApparentPowerTotalNetT0_VA = (decimal)entity.ApparentPowerTotalNetT0_VA,
      ActiveEnergyL1ImportT0_Wh = (decimal)entity.ActiveEnergyL1ImportT0_Wh,
      ActiveEnergyL2ImportT0_Wh = (decimal)entity.ActiveEnergyL2ImportT0_Wh,
      ActiveEnergyL3ImportT0_Wh = (decimal)entity.ActiveEnergyL3ImportT0_Wh,
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

  public static SchneideriEM3xxxMeasurementEntity ToEntity(
    this SchneideriEM3xxxMeasurementModel model)
  {
    return new SchneideriEM3xxxMeasurementEntity
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
      ReactivePowerTotalNetT0_VAR = (float)model.ReactivePowerTotalNetT0_VAR,
      ApparentPowerTotalNetT0_VA = (float)model.ApparentPowerTotalNetT0_VA,
      ActiveEnergyL1ImportT0_Wh = (float)model.ActiveEnergyL1ImportT0_Wh,
      ActiveEnergyL2ImportT0_Wh = (float)model.ActiveEnergyL2ImportT0_Wh,
      ActiveEnergyL3ImportT0_Wh = (float)model.ActiveEnergyL3ImportT0_Wh,
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

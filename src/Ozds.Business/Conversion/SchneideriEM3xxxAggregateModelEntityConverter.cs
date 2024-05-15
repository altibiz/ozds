using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class SchneideriEM3xxxAggregateModelEntityConverter :
  ModelEntityConverter<SchneideriEM3xxxAggregateModel,
    SchneideriEM3xxxAggregateEntity>
{
  protected override SchneideriEM3xxxAggregateEntity ToEntity(
    SchneideriEM3xxxAggregateModel model)
  {
    return model.ToEntity();
  }

  protected override SchneideriEM3xxxAggregateModel ToModel(
    SchneideriEM3xxxAggregateEntity entity)
  {
    return entity.ToModel();
  }
}

public static class SchneideriEM3xxxAggregateModelEntityConverterExtensions
{
  public static SchneideriEM3xxxAggregateEntity ToEntity(
    this SchneideriEM3xxxAggregateModel model)
  {
    return new SchneideriEM3xxxAggregateEntity
    {
      MeterId = model.MeterId,
      Timestamp = model.Timestamp,
      Interval = model.Interval.ToEntity(),
      Count = model.Count,
      VoltageL1AnyT0Avg_V = (float)model.VoltageL1AnyT0Avg_V,
      VoltageL2AnyT0Avg_V = (float)model.VoltageL2AnyT0Avg_V,
      VoltageL3AnyT0Avg_V = (float)model.VoltageL3AnyT0Avg_V,
      CurrentL1AnyT0Avg_A = (float)model.CurrentL1AnyT0Avg_A,
      CurrentL2AnyT0Avg_A = (float)model.CurrentL2AnyT0Avg_A,
      CurrentL3AnyT0Avg_A = (float)model.CurrentL3AnyT0Avg_A,
      ActivePowerL1NetT0Avg_W = (float)model.ActivePowerL1NetT0Avg_W,
      ActivePowerL2NetT0Avg_W = (float)model.ActivePowerL2NetT0Avg_W,
      ActivePowerL3NetT0Avg_W = (float)model.ActivePowerL3NetT0Avg_W,
      ReactivePowerTotalNetT0Avg_VAR =
        (float)model.ReactivePowerTotalNetT0Avg_VAR,
      ApparentPowerTotalNetT0Avg_VA =
        (float)model.ApparentPowerTotalNetT0Avg_VA,
      ActiveEnergyTotalImportT0Min_Wh =
        (float)model.ActiveEnergyTotalImportT0Min_Wh,
      ActiveEnergyTotalImportT0Max_Wh =
        (float)model.ActiveEnergyTotalImportT0Max_Wh,
      ActiveEnergyTotalExportT0Min_Wh =
        (float)model.ActiveEnergyTotalExportT0Min_Wh,
      ActiveEnergyTotalExportT0Max_Wh =
        (float)model.ActiveEnergyTotalExportT0Max_Wh,
      ReactiveEnergyTotalImportT0Min_VARh =
        (float)model.ReactiveEnergyTotalImportT0Min_VARh,
      ReactiveEnergyTotalImportT0Max_VARh =
        (float)model.ReactiveEnergyTotalImportT0Max_VARh,
      ReactiveEnergyTotalExportT0Min_VARh =
        (float)model.ReactiveEnergyTotalExportT0Min_VARh,
      ReactiveEnergyTotalExportT0Max_VARh =
        (float)model.ReactiveEnergyTotalExportT0Max_VARh,
      ActiveEnergyTotalImportT1Min_Wh =
        (float)model.ActiveEnergyTotalImportT1Min_Wh,
      ActiveEnergyTotalImportT1Max_Wh =
        (float)model.ActiveEnergyTotalImportT1Max_Wh,
      ActiveEnergyTotalImportT2Min_Wh =
        (float)model.ActiveEnergyTotalImportT2Min_Wh,
      ActiveEnergyTotalImportT2Max_Wh =
        (float)model.ActiveEnergyTotalImportT2Max_Wh
    };
  }

  public static SchneideriEM3xxxAggregateModel ToModel(
    this SchneideriEM3xxxAggregateEntity entity)
  {
    return new SchneideriEM3xxxAggregateModel
    {
      MeterId = entity.MeterId,
      Timestamp = entity.Timestamp,
      Interval = entity.Interval.ToModel(),
      Count = entity.Count,
      VoltageL1AnyT0Avg_V = (decimal)entity.VoltageL1AnyT0Avg_V,
      VoltageL2AnyT0Avg_V = (decimal)entity.VoltageL2AnyT0Avg_V,
      VoltageL3AnyT0Avg_V = (decimal)entity.VoltageL3AnyT0Avg_V,
      CurrentL1AnyT0Avg_A = (decimal)entity.CurrentL1AnyT0Avg_A,
      CurrentL2AnyT0Avg_A = (decimal)entity.CurrentL2AnyT0Avg_A,
      CurrentL3AnyT0Avg_A = (decimal)entity.CurrentL3AnyT0Avg_A,
      ActivePowerL1NetT0Avg_W = (decimal)entity.ActivePowerL1NetT0Avg_W,
      ActivePowerL2NetT0Avg_W = (decimal)entity.ActivePowerL2NetT0Avg_W,
      ActivePowerL3NetT0Avg_W = (decimal)entity.ActivePowerL3NetT0Avg_W,
      ReactivePowerTotalNetT0Avg_VAR =
        (decimal)entity.ReactivePowerTotalNetT0Avg_VAR,
      ApparentPowerTotalNetT0Avg_VA =
        (decimal)entity.ApparentPowerTotalNetT0Avg_VA,
      ActiveEnergyTotalImportT0Min_Wh =
        (decimal)entity.ActiveEnergyTotalImportT0Min_Wh,
      ActiveEnergyTotalImportT0Max_Wh =
        (decimal)entity.ActiveEnergyTotalImportT0Max_Wh,
      ActiveEnergyTotalExportT0Min_Wh =
        (decimal)entity.ActiveEnergyTotalExportT0Min_Wh,
      ActiveEnergyTotalExportT0Max_Wh =
        (decimal)entity.ActiveEnergyTotalExportT0Max_Wh,
      ReactiveEnergyTotalImportT0Min_VARh =
        (decimal)entity.ReactiveEnergyTotalImportT0Min_VARh,
      ReactiveEnergyTotalImportT0Max_VARh =
        (decimal)entity.ReactiveEnergyTotalImportT0Max_VARh,
      ReactiveEnergyTotalExportT0Min_VARh =
        (decimal)entity.ReactiveEnergyTotalExportT0Min_VARh,
      ReactiveEnergyTotalExportT0Max_VARh =
        (decimal)entity.ReactiveEnergyTotalExportT0Max_VARh,
      ActiveEnergyTotalImportT1Min_Wh =
        (decimal)entity.ActiveEnergyTotalImportT1Min_Wh,
      ActiveEnergyTotalImportT1Max_Wh =
        (decimal)entity.ActiveEnergyTotalImportT1Max_Wh,
      ActiveEnergyTotalImportT2Min_Wh =
        (decimal)entity.ActiveEnergyTotalImportT2Min_Wh,
      ActiveEnergyTotalImportT2Max_Wh =
        (decimal)entity.ActiveEnergyTotalImportT2Max_Wh
    };
  }
}

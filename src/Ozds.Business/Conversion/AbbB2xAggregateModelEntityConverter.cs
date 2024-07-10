using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class AbbB2xAggregateModelEntityConverter : ModelEntityConverter<
  AbbB2xAggregateModel, AbbB2xAggregateEntity>
{
  protected override AbbB2xAggregateEntity ToEntity(
    AbbB2xAggregateModel model)
  {
    return model.ToEntity();
  }

  protected override AbbB2xAggregateModel ToModel(
    AbbB2xAggregateEntity entity)
  {
    return entity.ToModel();
  }
}

public static class AbbB2xAggregateModelEntityConverterExtensions
{
  public static AbbB2xAggregateEntity ToEntity(this AbbB2xAggregateModel model)
  {
    return new AbbB2xAggregateEntity
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
      ReactivePowerL1NetT0Avg_VAR = (float)model.ReactivePowerL1NetT0Avg_VAR,
      ReactivePowerL2NetT0Avg_VAR = (float)model.ReactivePowerL2NetT0Avg_VAR,
      ReactivePowerL3NetT0Avg_VAR = (float)model.ReactivePowerL3NetT0Avg_VAR,
      ActiveEnergyTotalImportT0Min_Wh = (float)model.ActiveEnergyTotalImportT0Min_Wh,
      ActiveEnergyTotalImportT0Max_Wh = (float)model.ActiveEnergyTotalImportT0Max_Wh,
      ActiveEnergyTotalExportT0Min_Wh = (float)model.ActiveEnergyTotalExportT0Min_Wh,
      ActiveEnergyTotalExportT0Max_Wh = (float)model.ActiveEnergyTotalExportT0Max_Wh,
      ReactiveEnergyTotalImportT0Min_VARh = (float)model.ReactiveEnergyTotalImportT0Min_VARh,
      ReactiveEnergyTotalImportT0Max_VARh = (float)model.ReactiveEnergyTotalImportT0Max_VARh,
      ReactiveEnergyTotalExportT0Min_VARh = (float)model.ReactiveEnergyTotalExportT0Min_VARh,
      ReactiveEnergyTotalExportT0Max_VARh = (float)model.ReactiveEnergyTotalExportT0Max_VARh,
      ActiveEnergyTotalImportT1Min_Wh = (float)model.ActiveEnergyTotalImportT1Min_Wh,
      ActiveEnergyTotalImportT1Max_Wh = (float)model.ActiveEnergyTotalImportT1Max_Wh,
      ActiveEnergyTotalImportT2Min_Wh = (float)model.ActiveEnergyTotalImportT2Min_Wh,
      ActiveEnergyTotalImportT2Max_Wh = (float)model.ActiveEnergyTotalImportT2Max_Wh
    };
  }

  public static AbbB2xAggregateModel ToModel(this AbbB2xAggregateEntity entity)
  {
    return new AbbB2xAggregateModel
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
      ReactivePowerL1NetT0Avg_VAR = (decimal)entity.ReactivePowerL1NetT0Avg_VAR,
      ReactivePowerL2NetT0Avg_VAR = (decimal)entity.ReactivePowerL2NetT0Avg_VAR,
      ReactivePowerL3NetT0Avg_VAR = (decimal)entity.ReactivePowerL3NetT0Avg_VAR,
      ActiveEnergyTotalImportT0Min_Wh = (decimal)entity.ActiveEnergyTotalImportT0Min_Wh,
      ActiveEnergyTotalImportT0Max_Wh = (decimal)entity.ActiveEnergyTotalImportT0Max_Wh,
      ActiveEnergyTotalExportT0Min_Wh = (decimal)entity.ActiveEnergyTotalExportT0Min_Wh,
      ActiveEnergyTotalExportT0Max_Wh = (decimal)entity.ActiveEnergyTotalExportT0Max_Wh,
      ReactiveEnergyTotalImportT0Min_VARh = (decimal)entity.ReactiveEnergyTotalImportT0Min_VARh,
      ReactiveEnergyTotalImportT0Max_VARh = (decimal)entity.ReactiveEnergyTotalImportT0Max_VARh,
      ReactiveEnergyTotalExportT0Min_VARh = (decimal)entity.ReactiveEnergyTotalExportT0Min_VARh,
      ReactiveEnergyTotalExportT0Max_VARh = (decimal)entity.ReactiveEnergyTotalExportT0Max_VARh,
      ActiveEnergyTotalImportT1Min_Wh = (decimal)entity.ActiveEnergyTotalImportT1Min_Wh,
      ActiveEnergyTotalImportT1Max_Wh = (decimal)entity.ActiveEnergyTotalImportT1Max_Wh,
      ActiveEnergyTotalImportT2Min_Wh = (decimal)entity.ActiveEnergyTotalImportT2Min_Wh,
      ActiveEnergyTotalImportT2Max_Wh = (decimal)entity.ActiveEnergyTotalImportT2Max_Wh
    };
  }
}

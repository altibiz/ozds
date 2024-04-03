using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;
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
      VoltageL1AnyT0Avg_V = model.VoltageL1AnyT0Avg_V,
      VoltageL2AnyT0Avg_V = model.VoltageL2AnyT0Avg_V,
      VoltageL3AnyT0Avg_V = model.VoltageL3AnyT0Avg_V,
      CurrentL1AnyT0Avg_A = model.CurrentL1AnyT0Avg_A,
      CurrentL2AnyT0Avg_A = model.CurrentL2AnyT0Avg_A,
      CurrentL3AnyT0Avg_A = model.CurrentL3AnyT0Avg_A,
      ActivePowerL1NetT0Avg_W = model.ActivePowerL1NetT0Avg_W,
      ActivePowerL2NetT0Avg_W = model.ActivePowerL2NetT0Avg_W,
      ActivePowerL3NetT0Avg_W = model.ActivePowerL3NetT0Avg_W,
      ReactivePowerL1NetT0Avg_VAR = model.ReactivePowerL1NetT0Avg_VAR,
      ReactivePowerL2NetT0Avg_VAR = model.ReactivePowerL2NetT0Avg_VAR,
      ReactivePowerL3NetT0Avg_VAR = model.ReactivePowerL3NetT0Avg_VAR,
      ActiveEnergyTotalImportT0Min_Wh = model.ActiveEnergyTotalImportT0Min_Wh,
      ActiveEnergyTotalImportT0Max_Wh = model.ActiveEnergyTotalImportT0Max_Wh,
      ActiveEnergyTotalExportT0Min_Wh = model.ActiveEnergyTotalExportT0Min_Wh,
      ActiveEnergyTotalExportT0Max_Wh = model.ActiveEnergyTotalExportT0Max_Wh,
      ReactiveEnergyTotalImportT0Min_VARh =
        model.ReactiveEnergyTotalImportT0Min_VARh,
      ReactiveEnergyTotalImportT0Max_VARh =
        model.ReactiveEnergyTotalImportT0Max_VARh,
      ReactiveEnergyTotalExportT0Min_VARh =
        model.ReactiveEnergyTotalExportT0Min_VARh,
      ReactiveEnergyTotalExportT0Max_VARh =
        model.ReactiveEnergyTotalExportT0Max_VARh,
      ActiveEnergyTotalImportT1Min_Wh = model.ActiveEnergyTotalImportT1Min_Wh,
      ActiveEnergyTotalImportT1Max_Wh = model.ActiveEnergyTotalImportT1Max_Wh,
      ActiveEnergyTotalImportT2Min_Wh = model.ActiveEnergyTotalImportT2Min_Wh,
      ActiveEnergyTotalImportT2Max_Wh = model.ActiveEnergyTotalImportT2Max_Wh
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
      VoltageL1AnyT0Avg_V = entity.VoltageL1AnyT0Avg_V,
      VoltageL2AnyT0Avg_V = entity.VoltageL2AnyT0Avg_V,
      VoltageL3AnyT0Avg_V = entity.VoltageL3AnyT0Avg_V,
      CurrentL1AnyT0Avg_A = entity.CurrentL1AnyT0Avg_A,
      CurrentL2AnyT0Avg_A = entity.CurrentL2AnyT0Avg_A,
      CurrentL3AnyT0Avg_A = entity.CurrentL3AnyT0Avg_A,
      ActivePowerL1NetT0Avg_W = entity.ActivePowerL1NetT0Avg_W,
      ActivePowerL2NetT0Avg_W = entity.ActivePowerL2NetT0Avg_W,
      ActivePowerL3NetT0Avg_W = entity.ActivePowerL3NetT0Avg_W,
      ReactivePowerL1NetT0Avg_VAR = entity.ReactivePowerL1NetT0Avg_VAR,
      ReactivePowerL2NetT0Avg_VAR = entity.ReactivePowerL2NetT0Avg_VAR,
      ReactivePowerL3NetT0Avg_VAR = entity.ReactivePowerL3NetT0Avg_VAR,
      ActiveEnergyTotalImportT0Min_Wh = entity.ActiveEnergyTotalImportT0Min_Wh,
      ActiveEnergyTotalImportT0Max_Wh = entity.ActiveEnergyTotalImportT0Max_Wh,
      ActiveEnergyTotalExportT0Min_Wh = entity.ActiveEnergyTotalExportT0Min_Wh,
      ActiveEnergyTotalExportT0Max_Wh = entity.ActiveEnergyTotalExportT0Max_Wh,
      ReactiveEnergyTotalImportT0Min_VARh =
        entity.ReactiveEnergyTotalImportT0Min_VARh,
      ReactiveEnergyTotalImportT0Max_VARh =
        entity.ReactiveEnergyTotalImportT0Max_VARh,
      ReactiveEnergyTotalExportT0Min_VARh =
        entity.ReactiveEnergyTotalExportT0Min_VARh,
      ReactiveEnergyTotalExportT0Max_VARh =
        entity.ReactiveEnergyTotalExportT0Max_VARh,
      ActiveEnergyTotalImportT1Min_Wh = entity.ActiveEnergyTotalImportT1Min_Wh,
      ActiveEnergyTotalImportT1Max_Wh = entity.ActiveEnergyTotalImportT1Max_Wh,
      ActiveEnergyTotalImportT2Min_Wh = entity.ActiveEnergyTotalImportT2Min_Wh,
      ActiveEnergyTotalImportT2Max_Wh = entity.ActiveEnergyTotalImportT2Max_Wh
    };
  }
}

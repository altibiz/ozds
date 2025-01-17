using Ozds.Business.Conversion.Base;
using Ozds.Business.Conversion.Complex;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion.Implementations.Measurements;

public class AbbB2xAggregateModelEntityConverter : ConcreteModelEntityConverter<
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
      MeasurementLocationId = model.MeasurementLocationId,
      Timestamp = model.Timestamp,
      Interval = model.Interval.ToEntity(),
      Count = model.Count,
      QuarterHourCount = model.QuarterHourCount,
      VoltageL1AnyT0_V = model.VoltageL1AnyT0_V.ToEntity(),
      VoltageL2AnyT0_V = model.VoltageL2AnyT0_V.ToEntity(),
      VoltageL3AnyT0_V = model.VoltageL3AnyT0_V.ToEntity(),
      CurrentL1AnyT0_A = model.CurrentL1AnyT0_A.ToEntity(),
      CurrentL2AnyT0_A = model.CurrentL2AnyT0_A.ToEntity(),
      CurrentL3AnyT0_A = model.CurrentL3AnyT0_A.ToEntity(),
      ActivePowerL1NetT0_W = model.ActivePowerL1NetT0_W.ToEntity(),
      ActivePowerL2NetT0_W = model.ActivePowerL2NetT0_W.ToEntity(),
      ActivePowerL3NetT0_W = model.ActivePowerL3NetT0_W.ToEntity(),
      ReactivePowerL1NetT0_VAR = model.ReactivePowerL1NetT0_VAR.ToEntity(),
      ReactivePowerL2NetT0_VAR = model.ReactivePowerL2NetT0_VAR.ToEntity(),
      ReactivePowerL3NetT0_VAR = model.ReactivePowerL3NetT0_VAR.ToEntity(),
      ActiveEnergyL1ImportT0_Wh =
        model.ActiveEnergyL1ImportT0_Wh.ToEntity(),
      DerivedActivePowerL1ImportT0_W =
        model.DerivedActivePowerL1ImportT0_W.ToEntity(),
      ActiveEnergyL2ImportT0_Wh =
        model.ActiveEnergyL2ImportT0_Wh.ToEntity(),
      DerivedActivePowerL2ImportT0_W =
        model.DerivedActivePowerL2ImportT0_W.ToEntity(),
      ActiveEnergyL3ImportT0_Wh =
        model.ActiveEnergyL3ImportT0_Wh.ToEntity(),
      DerivedActivePowerL3ImportT0_W =
        model.DerivedActivePowerL3ImportT0_W.ToEntity(),
      ActiveEnergyL1ExportT0_Wh =
        model.ActiveEnergyL1ExportT0_Wh.ToEntity(),
      DerivedActivePowerL1ExportT0_W =
        model.DerivedActivePowerL1ExportT0_W.ToEntity(),
      ActiveEnergyL2ExportT0_Wh =
        model.ActiveEnergyL2ExportT0_Wh.ToEntity(),
      DerivedActivePowerL2ExportT0_W =
        model.DerivedActivePowerL2ExportT0_W.ToEntity(),
      ActiveEnergyL3ExportT0_Wh =
        model.ActiveEnergyL3ExportT0_Wh.ToEntity(),
      DerivedActivePowerL3ExportT0_W =
        model.DerivedActivePowerL3ExportT0_W.ToEntity(),
      ReactiveEnergyL1ImportT0_VARh =
        model.ReactiveEnergyL1ImportT0_VARh.ToEntity(),
      DerivedReactivePowerL1ImportT0_VAR =
        model.DerivedReactivePowerL1ImportT0_VAR.ToEntity(),
      ReactiveEnergyL2ImportT0_VARh =
        model.ReactiveEnergyL2ImportT0_VARh.ToEntity(),
      DerivedReactivePowerL2ImportT0_VAR =
        model.DerivedReactivePowerL2ImportT0_VAR.ToEntity(),
      ReactiveEnergyL3ImportT0_VARh =
        model.ReactiveEnergyL3ImportT0_VARh.ToEntity(),
      DerivedReactivePowerL3ImportT0_VAR =
        model.DerivedReactivePowerL3ImportT0_VAR.ToEntity(),
      ReactiveEnergyL1ExportT0_VARh =
        model.ReactiveEnergyL1ExportT0_VARh.ToEntity(),
      DerivedReactivePowerL1ExportT0_VAR =
        model.DerivedReactivePowerL1ExportT0_VAR.ToEntity(),
      ReactiveEnergyL2ExportT0_VARh =
        model.ReactiveEnergyL2ExportT0_VARh.ToEntity(),
      DerivedReactivePowerL2ExportT0_VAR =
        model.DerivedReactivePowerL2ExportT0_VAR.ToEntity(),
      ReactiveEnergyL3ExportT0_VARh =
        model.ReactiveEnergyL3ExportT0_VARh.ToEntity(),
      DerivedReactivePowerL3ExportT0_VAR =
        model.DerivedReactivePowerL3ExportT0_VAR.ToEntity(),
      ActiveEnergyTotalImportT0_Wh =
        model.ActiveEnergyTotalImportT0_Wh.ToEntity(),
      DerivedActivePowerTotalImportT0_W =
        model.DerivedActivePowerTotalImportT0_W.ToEntity(),
      ActiveEnergyTotalExportT0_Wh =
        model.ActiveEnergyTotalExportT0_Wh.ToEntity(),
      DerivedActivePowerTotalExportT0_W =
        model.DerivedActivePowerTotalExportT0_W.ToEntity(),
      ReactiveEnergyTotalImportT0_VARh =
        model.ReactiveEnergyTotalImportT0_VARh.ToEntity(),
      DerivedReactivePowerTotalImportT0_VAR =
        model.DerivedReactivePowerTotalImportT0_VAR.ToEntity(),
      ReactiveEnergyTotalExportT0_VARh =
        model.ReactiveEnergyTotalExportT0_VARh.ToEntity(),
      DerivedReactivePowerTotalExportT0_VAR =
        model.DerivedReactivePowerTotalExportT0_VAR.ToEntity(),
      ActiveEnergyTotalImportT1_Wh =
        model.ActiveEnergyTotalImportT1_Wh.ToEntity(),
      DerivedActivePowerTotalImportT1_W =
        model.DerivedActivePowerTotalImportT1_W.ToEntity(),
      ActiveEnergyTotalImportT2_Wh =
        model.ActiveEnergyTotalImportT2_Wh.ToEntity(),
      DerivedActivePowerTotalImportT2_W =
        model.DerivedActivePowerTotalImportT2_W.ToEntity(),
    };
  }

  public static AbbB2xAggregateModel ToModel(this AbbB2xAggregateEntity entity)
  {
    return new AbbB2xAggregateModel
    {
      MeterId = entity.MeterId,
      MeasurementLocationId = entity.MeasurementLocationId,
      Timestamp = entity.Timestamp,
      Interval = entity.Interval.ToModel(),
      Count = entity.Count,
      QuarterHourCount = entity.QuarterHourCount,
      VoltageL1AnyT0_V = entity.VoltageL1AnyT0_V.ToModel(),
      VoltageL2AnyT0_V = entity.VoltageL2AnyT0_V.ToModel(),
      VoltageL3AnyT0_V = entity.VoltageL3AnyT0_V.ToModel(),
      CurrentL1AnyT0_A = entity.CurrentL1AnyT0_A.ToModel(),
      CurrentL2AnyT0_A = entity.CurrentL2AnyT0_A.ToModel(),
      CurrentL3AnyT0_A = entity.CurrentL3AnyT0_A.ToModel(),
      ActivePowerL1NetT0_W = entity.ActivePowerL1NetT0_W.ToModel(),
      ActivePowerL2NetT0_W = entity.ActivePowerL2NetT0_W.ToModel(),
      ActivePowerL3NetT0_W = entity.ActivePowerL3NetT0_W.ToModel(),
      ReactivePowerL1NetT0_VAR = entity.ReactivePowerL1NetT0_VAR.ToModel(),
      ReactivePowerL2NetT0_VAR = entity.ReactivePowerL2NetT0_VAR.ToModel(),
      ReactivePowerL3NetT0_VAR = entity.ReactivePowerL3NetT0_VAR.ToModel(),
      ActiveEnergyL1ImportT0_Wh =
        entity.ActiveEnergyL1ImportT0_Wh.ToModel(),
      DerivedActivePowerL1ImportT0_W =
        entity.DerivedActivePowerL1ImportT0_W.ToModel(),
      ActiveEnergyL2ImportT0_Wh =
        entity.ActiveEnergyL2ImportT0_Wh.ToModel(),
      DerivedActivePowerL2ImportT0_W =
        entity.DerivedActivePowerL2ImportT0_W.ToModel(),
      ActiveEnergyL3ImportT0_Wh =
        entity.ActiveEnergyL3ImportT0_Wh.ToModel(),
      DerivedActivePowerL3ImportT0_W =
        entity.DerivedActivePowerL3ImportT0_W.ToModel(),
      ActiveEnergyL1ExportT0_Wh =
        entity.ActiveEnergyL1ExportT0_Wh.ToModel(),
      DerivedActivePowerL1ExportT0_W =
        entity.DerivedActivePowerL1ExportT0_W.ToModel(),
      ActiveEnergyL2ExportT0_Wh =
        entity.ActiveEnergyL2ExportT0_Wh.ToModel(),
      DerivedActivePowerL2ExportT0_W =
        entity.DerivedActivePowerL2ExportT0_W.ToModel(),
      ActiveEnergyL3ExportT0_Wh =
        entity.ActiveEnergyL3ExportT0_Wh.ToModel(),
      DerivedActivePowerL3ExportT0_W =
        entity.DerivedActivePowerL3ExportT0_W.ToModel(),
      ReactiveEnergyL1ImportT0_VARh =
        entity.ReactiveEnergyL1ImportT0_VARh.ToModel(),
      DerivedReactivePowerL1ImportT0_VAR =
        entity.DerivedReactivePowerL1ImportT0_VAR.ToModel(),
      ReactiveEnergyL2ImportT0_VARh =
        entity.ReactiveEnergyL2ImportT0_VARh.ToModel(),
      DerivedReactivePowerL2ImportT0_VAR =
        entity.DerivedReactivePowerL2ImportT0_VAR.ToModel(),
      ReactiveEnergyL3ImportT0_VARh =
        entity.ReactiveEnergyL3ImportT0_VARh.ToModel(),
      DerivedReactivePowerL3ImportT0_VAR =
        entity.DerivedReactivePowerL3ImportT0_VAR.ToModel(),
      ReactiveEnergyL1ExportT0_VARh =
        entity.ReactiveEnergyL1ExportT0_VARh.ToModel(),
      DerivedReactivePowerL1ExportT0_VAR =
        entity.DerivedReactivePowerL1ExportT0_VAR.ToModel(),
      ReactiveEnergyL2ExportT0_VARh =
        entity.ReactiveEnergyL2ExportT0_VARh.ToModel(),
      DerivedReactivePowerL2ExportT0_VAR =
        entity.DerivedReactivePowerL2ExportT0_VAR.ToModel(),
      ReactiveEnergyL3ExportT0_VARh =
        entity.ReactiveEnergyL3ExportT0_VARh.ToModel(),
      DerivedReactivePowerL3ExportT0_VAR =
        entity.DerivedReactivePowerL3ExportT0_VAR.ToModel(),
      ActiveEnergyTotalImportT0_Wh =
        entity.ActiveEnergyTotalImportT0_Wh.ToModel(),
      DerivedActivePowerTotalImportT0_W =
        entity.DerivedActivePowerTotalImportT0_W.ToModel(),
      ActiveEnergyTotalExportT0_Wh =
        entity.ActiveEnergyTotalExportT0_Wh.ToModel(),
      DerivedActivePowerTotalExportT0_W =
        entity.DerivedActivePowerTotalExportT0_W.ToModel(),
      ReactiveEnergyTotalImportT0_VARh =
        entity.ReactiveEnergyTotalImportT0_VARh.ToModel(),
      DerivedReactivePowerTotalImportT0_VAR =
        entity.DerivedReactivePowerTotalImportT0_VAR.ToModel(),
      ReactiveEnergyTotalExportT0_VARh =
        entity.ReactiveEnergyTotalExportT0_VARh.ToModel(),
      DerivedReactivePowerTotalExportT0_VAR =
        entity.DerivedReactivePowerTotalExportT0_VAR.ToModel(),
      ActiveEnergyTotalImportT1_Wh =
        entity.ActiveEnergyTotalImportT1_Wh.ToModel(),
      DerivedActivePowerTotalImportT1_W =
        entity.DerivedActivePowerTotalImportT1_W.ToModel(),
      ActiveEnergyTotalImportT2_Wh =
        entity.ActiveEnergyTotalImportT2_Wh.ToModel(),
      DerivedActivePowerTotalImportT2_W =
        entity.DerivedActivePowerTotalImportT2_W.ToModel(),
    };
  }
}

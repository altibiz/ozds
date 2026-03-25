using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Report.Entities;

namespace Ozds.Business.Conversion.Implementations.Report;

public class ShortenedEnergyCardModelReportEntityConverter
  : ConcreteModelReportEntityConverter<
    ShortenedEnergyCardModel,
    ShortenedEnergyCardEntity>
{
  public override void InitializeEntity(
    ShortenedEnergyCardModel model,
    ShortenedEnergyCardEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.MeasurementLocationTitle = model.MeasurementLocationTitle;
    entity.MeterId = model.MeterId;
    entity.ActiveEnergyTotalImportT1_kWh =
      model.ActiveEnergyTotalImportT1_kWh;
    entity.ActiveEnergyTotalImportT2_kWh =
      model.ActiveEnergyTotalImportT2_kWh;
  }

  public override void InitializeModel(
    ShortenedEnergyCardEntity entity,
    ShortenedEnergyCardModel model
  )
  {
    base.InitializeModel(entity, model);
    model.MeasurementLocationTitle = entity.MeasurementLocationTitle;
    model.MeterId = entity.MeterId;
    model.ActiveEnergyTotalImportT1_kWh =
      entity.ActiveEnergyTotalImportT1_kWh;
    model.ActiveEnergyTotalImportT2_kWh =
      entity.ActiveEnergyTotalImportT2_kWh;
  }
}

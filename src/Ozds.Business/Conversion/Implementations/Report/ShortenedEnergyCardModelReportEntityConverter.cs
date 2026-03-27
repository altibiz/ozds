using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Report.Entities;

namespace Ozds.Business.Conversion.Implementations.Report;

public class ShortenedEnergyCardModelReportEntityConverter
  : ConcreteModelReportEntityConverter<
    ShortenedEnergyCardReportModel,
    ShortenedEnergyCardReportEntity
  >
{
  public override void InitializeEntity(
    ShortenedEnergyCardReportModel model,
    ShortenedEnergyCardReportEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.MeasurementLocationTitle = model.MeasurementLocationTitle;
    entity.MeterId = model.MeterId.Substring(
      model.MeterId.LastIndexOf('-') + 1
    ); // NOTE: requested by client to display only serial of meter
    entity.ActiveEnergyTotalImportT1_kWh = model.ActiveEnergyTotalImportT1_kWh;
    entity.ActiveEnergyTotalImportT2_kWh = model.ActiveEnergyTotalImportT2_kWh;
  }

  public override void InitializeModel(
    ShortenedEnergyCardReportEntity entity,
    ShortenedEnergyCardReportModel model
  )
  {
    base.InitializeModel(entity, model);
    model.MeasurementLocationTitle = entity.MeasurementLocationTitle;
    model.MeterId = entity.MeterId;
    model.ActiveEnergyTotalImportT1_kWh = entity.ActiveEnergyTotalImportT1_kWh;
    model.ActiveEnergyTotalImportT2_kWh = entity.ActiveEnergyTotalImportT2_kWh;
  }
}

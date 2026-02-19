using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Report.Entities;

namespace Ozds.Business.Conversion.Implementations.Report;

public class LoadCurveModelReportEntityConverter
  : ConcreteModelReportEntityConverter<LoadCurveReportModel, LoadCurveEntity>
{
  public override void InitializeEntity(
    LoadCurveReportModel model,
    LoadCurveEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.MeasurementLocationCode = model.MeasurementLocationCode;
    entity.Timestamp = model.Timestamp;
    entity.ObisCode = model.ObisCode;
    entity.MeterId = model.MeterId;
    entity.Energy_kx = model.Energy_kx;
    entity.Power_kx = model.Power_kx;
  }

  public override void InitializeModel(
    LoadCurveEntity entity,
    LoadCurveReportModel model
  )
  {
    base.InitializeModel(entity, model);
    model.MeasurementLocationCode = entity.MeasurementLocationCode;
    model.Timestamp = entity.Timestamp;
    model.ObisCode = entity.ObisCode;
    model.MeterId = entity.MeterId;
    model.Energy_kx = entity.Energy_kx;
    model.Power_kx = entity.Power_kx;
  }
}

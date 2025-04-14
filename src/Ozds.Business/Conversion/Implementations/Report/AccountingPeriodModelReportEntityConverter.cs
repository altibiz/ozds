using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Report.Entities;

namespace Ozds.Business.Conversion.Implementations.Report;

public class AccountingPeriodModelReportEntityConverter :
  ConcreteModelReportEntityConverter<
    AccountingPeriodReportModel,
    AccountingPeriodEntity>
{
  public override void InitializeEntity(
    AccountingPeriodReportModel model,
    AccountingPeriodEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.MeasurementLocationCode = model.MeasurementLocationCode;
    entity.ObisCode = model.ObisCode;
    entity.Timestamp = model.Timestamp;
    entity.Value = model.Value;
    entity.Unit = model.Unit;
  }

  public override void InitializeModel(
    AccountingPeriodEntity entity,
    AccountingPeriodReportModel model)
  {
    base.InitializeModel(entity, model);
    model.MeasurementLocationCode = entity.MeasurementLocationCode;
    model.ObisCode = entity.ObisCode;
    model.Timestamp = entity.Timestamp;
    model.Value = entity.Value;
    model.Unit = entity.Unit;
  }
}

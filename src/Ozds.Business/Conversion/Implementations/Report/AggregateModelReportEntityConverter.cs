using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;
using Ozds.Report.Entities;

namespace Ozds.Business.Conversion.Implementations.Report;

public class AggregateModelReportEntityConverter(
  IServiceProvider serviceProvider
) : ConcreteModelReportEntityConverter<AggregateModel, AggregateEntity>
{
  private readonly ModelReportEntityConverter converter =
    serviceProvider.GetRequiredService<ModelReportEntityConverter>();

  public override void InitializeEntity(
    AggregateModel model,
    AggregateEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.MeterId = model.MeterId;
    entity.MeasurementLocationId = model.MeasurementLocationId;
    entity.Timestamp = model.Timestamp;
    entity.Interval = converter.ToEntity<IntervalEntity>(model.Interval);
  }

  public override void InitializeModel(
    AggregateEntity entity,
    AggregateModel model)
  {
    base.InitializeModel(entity, model);
    model.MeterId = entity.MeterId;
    model.MeasurementLocationId = entity.MeasurementLocationId;
    model.Timestamp = entity.Timestamp;
    model.Interval = converter.ToModel<IntervalModel>(entity.Interval);
  }
}

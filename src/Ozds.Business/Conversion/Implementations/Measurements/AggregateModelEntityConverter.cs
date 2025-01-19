using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Enums;

namespace Ozds.Business.Conversion.Implementations.Measurements;

public class AggregateModelEntityConverter(IServiceProvider serviceProvider)
  : ConcreteModelEntityConverter<AggregateModel, AggregateEntity>
{
  private readonly ModelEntityConverter modelEntityConverter =
    serviceProvider.GetRequiredService<ModelEntityConverter>();

  public override void InitializeEntity(
    AggregateModel model,
    AggregateEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.Count = model.Count;
    entity.QuarterHourCount = model.QuarterHourCount;
    entity.Interval = modelEntityConverter.ToEntity<IntervalEntity>(
      model.Interval);
    entity.Timestamp = model.Timestamp;
    entity.MeterId = model.MeterId;
    entity.MeasurementLocationId = model.MeasurementLocationId;
  }

  public override void InitializeModel(
    AggregateEntity entity,
    AggregateModel model
  )
  {
    base.InitializeModel(entity, model);
    model.Count = entity.Count;
    model.QuarterHourCount = entity.QuarterHourCount;
    model.Interval = modelEntityConverter.ToModel<IntervalModel>(
      entity.Interval);
    model.Timestamp = entity.Timestamp;
    model.MeterId = entity.MeterId;
    model.MeasurementLocationId = entity.MeasurementLocationId;
  }
}

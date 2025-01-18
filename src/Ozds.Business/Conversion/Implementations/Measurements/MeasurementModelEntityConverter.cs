using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.Measurements;

public class MeasurementModelEntityConverter
  : ConcreteModelEntityConverter<MeasurementModel, MeasurementEntity>
{
  public override void InitializeEntity(
    MeasurementModel model,
    MeasurementEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.MeterId = model.MeterId;
    entity.MeasurementLocationId = model.MeasurementLocationId;
    entity.Timestamp = model.Timestamp;
  }

  public override void InitializeModel(
    MeasurementEntity entity,
    MeasurementModel model
  )
  {
    base.InitializeModel(entity, model);
    model.MeterId = entity.MeterId;
    model.MeasurementLocationId = entity.MeasurementLocationId;
    model.Timestamp = entity.Timestamp;
  }
}

using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.Measurements;

public class MeasurementLocationEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
      MeasurementLocationModel,
      AuditableModel,
      MeasurementLocationEntity,
      AuditableEntity>(serviceProvider)
{
  public override void InitializeEntity(
    MeasurementLocationModel model,
    MeasurementLocationEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.MeterId = model.MeterId;
  }

  public override void InitializeModel(
    MeasurementLocationEntity entity,
    MeasurementLocationModel model
  )
  {
    base.InitializeModel(entity, model);
    model.MeterId = entity.MeterId;
  }
}

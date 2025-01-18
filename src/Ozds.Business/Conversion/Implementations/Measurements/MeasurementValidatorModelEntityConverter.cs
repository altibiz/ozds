using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.Measurements;

public class MeasurementValidatorModelEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
      MeasurementValidatorModel,
      AuditableModel,
      MeasurementValidatorEntity,
      AuditableEntity>(serviceProvider)
{
  public override void InitializeEntity(
    MeasurementValidatorModel model,
    MeasurementValidatorEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.Kind = model.Kind;
  }

  public override void InitializeModel(
    MeasurementValidatorEntity entity,
    MeasurementValidatorModel model
  )
  {
    base.InitializeModel(entity, model);
    model.Kind = entity.Kind;
  }
}

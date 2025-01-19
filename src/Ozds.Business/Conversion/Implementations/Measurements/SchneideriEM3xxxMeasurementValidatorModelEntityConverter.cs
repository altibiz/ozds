using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.Measurements;

public class SchneideriEM3xxxMeasurementValidatorModelEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
      SchneideriEM3xxxMeasurementValidatorModel,
      MeasurementValidatorModel,
      SchneideriEM3xxxMeasurementValidatorEntity,
      MeasurementValidatorEntity>(serviceProvider)
{
  public override void InitializeEntity(
    SchneideriEM3xxxMeasurementValidatorModel model,
    SchneideriEM3xxxMeasurementValidatorEntity entity)
  {
    base.InitializeEntity(model, entity);
    entity.Kind = model.Kind;
  }

  public override void InitializeModel(
    SchneideriEM3xxxMeasurementValidatorEntity entity,
    SchneideriEM3xxxMeasurementValidatorModel model)
  {
    base.InitializeModel(entity, model);
    model.Kind = entity.Kind;
  }
}

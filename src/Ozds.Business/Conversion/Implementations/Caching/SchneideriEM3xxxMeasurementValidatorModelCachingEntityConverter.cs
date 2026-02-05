using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Caching.Entities;
using Ozds.Caching.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.Caching;

public class SchneideriEM3xxxMeasurementValidatorModelCachingEntityConverter(
  IServiceProvider serviceProvider
)
  : InheritingModelCachingEntityConverter<
    SchneideriEM3xxxMeasurementValidatorModel,
    MeasurementValidatorModel,
    SchneideriEM3xxxMeasurementValidatorEntity,
    MeasurementValidatorEntity
  >(serviceProvider)
{
  public override void InitializeEntity(
    SchneideriEM3xxxMeasurementValidatorModel model,
    SchneideriEM3xxxMeasurementValidatorEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.MinVoltage_V = model.MinVoltage_V;
    entity.MaxVoltage_V = model.MaxVoltage_V;
    entity.MinCurrent_A = model.MinCurrent_A;
    entity.MaxCurrent_A = model.MaxCurrent_A;
    entity.MinActivePower_W = model.MinActivePower_W;
    entity.MaxActivePower_W = model.MaxActivePower_W;
    entity.MinReactivePower_VAR = model.MinReactivePower_VAR;
    entity.MaxReactivePower_VAR = model.MaxReactivePower_VAR;
    entity.MinApparentPower_VA = model.MinApparentPower_VA;
    entity.MaxApparentPower_VA = model.MaxApparentPower_VA;
  }

  public override void InitializeModel(
    SchneideriEM3xxxMeasurementValidatorEntity entity,
    SchneideriEM3xxxMeasurementValidatorModel model
  )
  {
    base.InitializeModel(entity, model);
    model.MinVoltage_V = entity.MinVoltage_V;
    model.MaxVoltage_V = entity.MaxVoltage_V;
    model.MinCurrent_A = entity.MinCurrent_A;
    model.MaxCurrent_A = entity.MaxCurrent_A;
    model.MinActivePower_W = entity.MinActivePower_W;
    model.MaxActivePower_W = entity.MaxActivePower_W;
    model.MinReactivePower_VAR = entity.MinReactivePower_VAR;
    model.MaxReactivePower_VAR = entity.MaxReactivePower_VAR;
    model.MinApparentPower_VA = entity.MinApparentPower_VA;
    model.MaxApparentPower_VA = entity.MaxApparentPower_VA;
  }
}

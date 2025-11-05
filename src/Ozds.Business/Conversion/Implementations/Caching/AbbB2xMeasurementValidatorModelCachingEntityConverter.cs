using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Caching.Entities;
using Ozds.Caching.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.Caching;

public class AbbB2xMeasurementValidatorModelCachingEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelCachingEntityConverter<
  AbbB2xMeasurementValidatorModel,
  TrackableModel,
  AbbB2xMeasurementValidatorEntity,
  TrackableEntity>(serviceProvider)
{
  public override void InitializeEntity(
    AbbB2xMeasurementValidatorModel model,
    AbbB2xMeasurementValidatorEntity entity
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
  }

  public override void InitializeModel(
    AbbB2xMeasurementValidatorEntity entity,
    AbbB2xMeasurementValidatorModel model
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
  }
}

using Ozds.Business.Conversion.Base;
using Ozds.Business.Extensions;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.Measurements;

public class AbbB2xMeasurementValidatorModelEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
      AbbB2xMeasurementValidatorModel,
      AuditableModel,
      AbbB2xMeasurementValidatorEntity,
      AuditableEntity>(serviceProvider)
{
  public override void InitializeEntity(
    AbbB2xMeasurementValidatorModel model,
    AbbB2xMeasurementValidatorEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.MinVoltage_V = model.MinVoltage_V.ToFloat();
    entity.MaxVoltage_V = model.MaxVoltage_V.ToFloat();
    entity.MinCurrent_A = model.MinCurrent_A.ToFloat();
    entity.MaxCurrent_A = model.MaxCurrent_A.ToFloat();
    entity.MinActivePower_W = model.MinActivePower_W.ToFloat();
    entity.MaxActivePower_W = model.MaxActivePower_W.ToFloat();
    entity.MinReactivePower_VAR = model.MinReactivePower_VAR.ToFloat();
    entity.MaxReactivePower_VAR = model.MaxReactivePower_VAR.ToFloat();
  }

  public override void InitializeModel(
    AbbB2xMeasurementValidatorEntity entity,
    AbbB2xMeasurementValidatorModel model
  )
  {
    base.InitializeModel(entity, model);
    model.MinVoltage_V = entity.MinVoltage_V.ToDecimal();
    model.MaxVoltage_V = entity.MaxVoltage_V.ToDecimal();
    model.MinCurrent_A = entity.MinCurrent_A.ToDecimal();
    model.MaxCurrent_A = entity.MaxCurrent_A.ToDecimal();
    model.MinActivePower_W = entity.MinActivePower_W.ToDecimal();
    model.MaxActivePower_W = entity.MaxActivePower_W.ToDecimal();
    model.MinReactivePower_VAR = entity.MinReactivePower_VAR.ToDecimal();
    model.MaxReactivePower_VAR = entity.MaxReactivePower_VAR.ToDecimal();
  }
}

using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Enums;
using Ozds.Caching.Entities.Base;
using Ozds.Caching.Entities.Complex;
using Ozds.Caching.Entities.Enums;

namespace Ozds.Business.Conversion.Implementations.Caching;

public class MeterModelCachingEntityConverter(IServiceProvider serviceProvider)
  : InheritingModelCachingEntityConverter<
    MeterModel,
    TrackableModel,
    MeterEntity,
    TrackableEntity
  >(serviceProvider)
{
  private readonly ModelCachingEntityConverter modelEntityConverter =
    serviceProvider.GetRequiredService<ModelCachingEntityConverter>();

  public override void InitializeEntity(MeterModel model, MeterEntity entity)
  {
    base.InitializeEntity(model, entity);
    entity.ConnectionPower_W = model.ConnectionPower_W;
    entity.MessengerId = model.MessengerId;
    entity.Phases = model
      .Phases.Select(phase => modelEntityConverter.ToEntity<PhaseEntity>(phase))
      .ToHashSet();
    entity.MeasurementValidatorId = model.MeasurementValidatorId;
    entity.MaxInactivityPeriod = modelEntityConverter.ToEntity<PeriodEntity>(
      model.MaxInactivityPeriod
    );
  }

  public override void InitializeModel(MeterEntity entity, MeterModel model)
  {
    base.InitializeModel(entity, model);
    model.ConnectionPower_W = entity.ConnectionPower_W;
    model.MessengerId = entity.MessengerId;
    model.Phases = entity
      .Phases.Select(phase => modelEntityConverter.ToModel<PhaseModel>(phase))
      .ToHashSet();
    model.MeasurementValidatorId = entity.MeasurementValidatorId;
    model.MaxInactivityPeriod = modelEntityConverter.ToModel<PeriodModel>(
      entity.MaxInactivityPeriod
    );
  }
}

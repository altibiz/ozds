using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;
using Ozds.Caching.Entities;
using Ozds.Caching.Entities.Base;
using Ozds.Caching.Entities.Enums;

namespace Ozds.Business.Conversion.Implementations.Caching;

public class ScopeModelCachingEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelCachingEntityConverter<
  ScopeModel,
  TrackableModel,
  ScopeEntity,
  TrackableEntity>(serviceProvider)
{
  private readonly ModelCachingEntityConverter modelEntityConverter = serviceProvider
    .GetRequiredService<ModelCachingEntityConverter>();

  public override void InitializeEntity(
    ScopeModel model,
    ScopeEntity entity
  )
  {
    base.InitializeEntity(model, entity);

    entity.ScopeModelId = model.ScopeModelId;
    entity.ScopeModelType = model.ScopeModelType;
    entity.ScopeAction = modelEntityConverter
      .ToEntity<ActionEntity>(model.ScopeAction);
  }

  public override void InitializeModel(
    ScopeEntity entity,
    ScopeModel model
  )
  {
    base.InitializeModel(entity, model);

    model.ScopeModelId = entity.ScopeModelId;
    model.ScopeModelType = entity.ScopeModelType;
    model.ScopeAction = modelEntityConverter
      .ToModel<ActionModel>(entity.ScopeAction);
  }
}

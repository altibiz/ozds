using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Caching.Entities;
using Ozds.Caching.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.Caching;

public class ApiKeyModelCachingEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelCachingEntityConverter<
  ApiKeyModel,
  TrackableModel,
  ApiKeyEntity,
  TrackableEntity>(serviceProvider)
{
  public override void InitializeEntity(
    ApiKeyModel model,
    ApiKeyEntity entity
  )
  {
    base.InitializeEntity(model, entity);

    entity.PrincipalModelId = model.PrincipalModelId;
    entity.PrincipalModelType = model.PrincipalModelType;
    entity.Hash = model.Hash;
    entity.ExpiresOn = model.ExpiresOn;
  }

  public override void InitializeModel(
    ApiKeyEntity entity,
    ApiKeyModel model
  )
  {
    base.InitializeModel(entity, model);

    model.PrincipalModelId = entity.PrincipalModelId;
    model.PrincipalModelType = entity.PrincipalModelType;
    model.Hash = entity.Hash;
    model.ExpiresOn = entity.ExpiresOn;
  }
}

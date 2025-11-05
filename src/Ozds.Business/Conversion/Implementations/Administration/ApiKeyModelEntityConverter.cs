using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Reflection;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;
using Ozds.Data.Reflection;

namespace Ozds.Business.Conversion.Implementations.Administration;

public class ApiKeyModelEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
  ApiKeyModel,
  TrackableModel,
  ApiKeyEntity,
  TrackableEntity>(serviceProvider)
{
  private readonly Data.Reflection.EntityReflector entityReflector =
    serviceProvider.GetRequiredService<Data.Reflection.EntityReflector>();

  private readonly ModelEntityConverter modelEntityConverter =
    serviceProvider.GetRequiredService<ModelEntityConverter>();

  private readonly ModelReflector modelReflector =
    serviceProvider.GetRequiredService<ModelReflector>();

  public override void InitializeEntity(
    ApiKeyModel model,
    ApiKeyEntity entity
  )
  {
    base.InitializeEntity(model, entity);

    var principalModelType =
      modelReflector.ResolveModelType(model.PrincipalModelType);

    var principalEntityType =
      modelEntityConverter.EntityType(principalModelType);

    var principalEntityTypeName =
      entityReflector.ResolveEntityName(principalEntityType);

    var principalEntityTable =
      entityReflector.ResolveEntityTable(principalEntityType);

    entity.PrincipalEntityId = model.PrincipalModelId;
    entity.PrincipalEntityType = principalEntityTypeName;
    entity.PrincipalEntityTable = principalEntityTable;
    entity.Hash = model.Hash;
    entity.ExpiresOn = model.ExpiresOn;
  }

  public override void InitializeModel(
    ApiKeyEntity entity,
    ApiKeyModel model
  )
  {
    base.InitializeModel(entity, model);

    var principalEntityType = entityReflector
      .ResolveEntityTypeFromName(entity.PrincipalEntityType);

    var principalModelType =
      modelEntityConverter.ModelType(principalEntityType);

    var principalModelTypeName =
      modelReflector.ResolveModelName(principalModelType);

    model.PrincipalModelId = entity.PrincipalEntityId;
    model.PrincipalModelType = principalModelTypeName;
    model.Hash = entity.Hash;
    model.ExpiresOn = entity.ExpiresOn;
    model.PrincipalModelId = entity.PrincipalEntityId;
  }
}

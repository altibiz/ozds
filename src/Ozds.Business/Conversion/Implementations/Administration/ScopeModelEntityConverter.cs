using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;
using Ozds.Business.Reflection;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Reflection;

namespace Ozds.Business.Conversion.Implementations.Administration;

public class ScopeModelEntityConverter(IServiceProvider serviceProvider)
  : InheritingModelEntityConverter<
    ScopeModel,
    TrackableModel,
    ScopeEntity,
    TrackableEntity
  >(serviceProvider)
{
  private readonly EntityReflector entityReflector =
    serviceProvider.GetRequiredService<EntityReflector>();

  private readonly ModelEntityConverter modelEntityConverter =
    serviceProvider.GetRequiredService<ModelEntityConverter>();

  private readonly ModelReflector modelReflector =
    serviceProvider.GetRequiredService<ModelReflector>();

  public override void InitializeEntity(ScopeModel model, ScopeEntity entity)
  {
    base.InitializeEntity(model, entity);

    var scopeModelType = model.ScopeModelType is null
      ? null
      : modelReflector.ResolveModelType(model.ScopeModelType);

    var scopeEntityType = scopeModelType is null
      ? null
      : modelEntityConverter.EntityType(scopeModelType);

    var scopeEntityTypeName = scopeEntityType is null
      ? null
      : entityReflector.ResolveEntityName(scopeEntityType);

    var scopeEntityTable = scopeEntityType is null
      ? null
      : entityReflector.ResolveEntityTable(scopeEntityType);

    entity.ScopeEntityId = model.ScopeModelId;
    entity.ScopeEntityType = scopeEntityTypeName;
    entity.ScopeEntityTable = scopeEntityTable;
    entity.ScopeAction = modelEntityConverter.ToEntity<ActionEntity>(
      model.ScopeAction
    );
  }

  public override void InitializeModel(ScopeEntity entity, ScopeModel model)
  {
    base.InitializeModel(entity, model);

    var scopeEntityType = entity.ScopeEntityType is null
      ? null
      : entityReflector.ResolveEntityTypeFromName(entity.ScopeEntityType);

    var scopeModelType = scopeEntityType is null
      ? null
      : modelEntityConverter.ModelType(scopeEntityType);

    var scopeModelTypeName = scopeModelType is null
      ? null
      : modelReflector.ResolveModelName(scopeModelType);

    model.ScopeModelId = entity.ScopeEntityId;
    model.ScopeModelType = scopeModelTypeName;
    model.ScopeAction = modelEntityConverter.ToModel<ActionModel>(
      entity.ScopeAction
    );
  }
}

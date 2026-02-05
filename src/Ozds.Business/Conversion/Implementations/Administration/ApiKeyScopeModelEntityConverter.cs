using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Joins;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Joins;

namespace Ozds.Business.Conversion.Implementations.Administration;

public class ApiKeyScopeModelEntityConverter(IServiceProvider serviceProvider)
  : InheritingModelEntityConverter<
    ApiKeyScopeModel,
    AuditableJoinModel,
    ApiKeyScopeEntity,
    AuditableJoinEntity
  >(serviceProvider)
{
  public override void InitializeEntity(
    ApiKeyScopeModel model,
    ApiKeyScopeEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.ApiKeyId = model.ApiKeyId;
    entity.ScopeId = model.ScopeId;
  }

  public override void InitializeModel(
    ApiKeyScopeEntity entity,
    ApiKeyScopeModel model
  )
  {
    base.InitializeModel(entity, model);
    model.ApiKeyId = entity.ApiKeyId;
    model.ScopeId = entity.ScopeId;
  }
}

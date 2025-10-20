using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Joins;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Joins;

namespace Ozds.Business.Conversion.Implementations.Administration;

public class NetworkUserRepresentativeEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
  NetworkUserRepresentativeModel,
  AuditableJoinModel,
  NetworkUserRepresentativeEntity,
  AuditableJoinEntity>(serviceProvider)
{
  public override void InitializeEntity(
    NetworkUserRepresentativeModel model,
    NetworkUserRepresentativeEntity entity)
  {
    base.InitializeEntity(model, entity);
    entity.NetworkUserId = model.NetworkUserId;
    entity.RepresentativeId = model.RepresentativeId;
  }

  public override void InitializeModel(
    NetworkUserRepresentativeEntity entity,
    NetworkUserRepresentativeModel model)
  {
    base.InitializeModel(entity, model);
    model.NetworkUserId = entity.NetworkUserId;
    model.RepresentativeId = entity.RepresentativeId;
  }
}

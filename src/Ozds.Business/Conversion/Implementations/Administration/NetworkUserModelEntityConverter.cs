using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Complex;

namespace Ozds.Business.Conversion.Implementations.Administration;

public class NetworkUserModelEntityConverter(IServiceProvider serviceProvider)
  : InheritingModelEntityConverter<
      NetworkUserModel,
      AuditableModel,
      NetworkUserEntity,
      AuditableEntity>(serviceProvider)
{
  private readonly ModelEntityConverter modelEntityConverter =
    serviceProvider.GetRequiredService<ModelEntityConverter>();

  public override void InitializeEntity(
    NetworkUserModel model,
    NetworkUserEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.LocationId = model.LocationId;
    entity.LegalPerson = model.LegalPerson is null
      ? null!
      : modelEntityConverter.ToEntity<LegalPersonEntity>(model.LegalPerson);
    entity.AltiBizSubProjectCode = model.AltiBizSubProjectCode;
  }

  public override void InitializeModel(
    NetworkUserEntity entity,
    NetworkUserModel model
  )
  {
    base.InitializeModel(entity, model);
    model.LocationId = entity.LocationId;
    model.LegalPerson = entity.LegalPerson is null
      ? null!
      : modelEntityConverter.ToModel<LegalPersonModel>(entity.LegalPerson);
    model.AltiBizSubProjectCode = entity.AltiBizSubProjectCode;
  }
}

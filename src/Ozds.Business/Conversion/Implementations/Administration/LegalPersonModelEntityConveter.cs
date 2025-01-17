using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Complex;
using Ozds.Data.Entities.Complex;

namespace Ozds.Business.Conversion.Implementations.Administration;

public class LegalPersonModelEntityConverter
  : ConcreteModelEntityConverter<LegalPersonModel, LegalPersonEntity>
{
  public override void InitializeEntity(
    LegalPersonModel model,
    LegalPersonEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.Name = model.Name;
    entity.Email = model.Email;
    entity.PhoneNumber = model.PhoneNumber;
    entity.Address = model.Address;
    entity.City = model.City;
    entity.PostalCode = model.PostalCode;
    entity.SocialSecurityNumber = model.SocialSecurityNumber;
  }

  public override void InitializeModel(
    LegalPersonEntity entity,
    LegalPersonModel model
  )
  {
    base.InitializeModel(entity, model);
    model.Name = entity.Name;
    model.Email = entity.Email;
    model.PhoneNumber = entity.PhoneNumber;
    model.Address = entity.Address;
    model.City = entity.City;
    model.PostalCode = entity.PostalCode;
    model.SocialSecurityNumber = entity.SocialSecurityNumber;
  }
}

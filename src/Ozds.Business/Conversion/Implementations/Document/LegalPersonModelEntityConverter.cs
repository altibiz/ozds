using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Complex;
using Ozds.Document.Entities;

namespace Ozds.Business.Conversion.Implementations.Document;

public class LegalPersonModelDocumentEntityConverter
  : ConcreteModelDocumentEntityConverter<
      LegalPersonModel,
      LegalPersonEntity>
{
  public override void InitializeEntity(
    LegalPersonModel model,
    LegalPersonEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.Name = model.Name;
    entity.SocialSecurityNumber = model.SocialSecurityNumber;
    entity.Address = model.Address;
    entity.PostalCode = model.PostalCode;
    entity.City = model.City;
    entity.Email = model.Email;
    entity.PhoneNumber = model.PhoneNumber;
  }
}

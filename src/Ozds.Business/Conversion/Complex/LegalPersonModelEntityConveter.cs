using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Complex;
using Ozds.Data.Entities.Complex;

namespace Ozds.Business.Conversion.Complex;

public class
  LegalPersonModelEntityConverter : ModelEntityConverter<LegalPersonModel,
  LegalPersonEntity>
{
  protected override LegalPersonEntity ToEntity(LegalPersonModel model)
  {
    return model.ToEntity();
  }

  protected override LegalPersonModel ToModel(LegalPersonEntity entity)
  {
    return entity.ToModel();
  }
}

public static class LegalPersonModelEntityConverterExtensions
{
  public static LegalPersonModel ToModel(this LegalPersonEntity entity)
  {
    return new LegalPersonModel
    {
      Name = entity.Name,
      Email = entity.Email,
      PhoneNumber = entity.PhoneNumber,
      Address = entity.Address,
      City = entity.City,
      PostalCode = entity.PostalCode,
      SocialSecurityNumber = entity.SocialSecurityNumber
    };
  }

  public static LegalPersonEntity ToEntity(this LegalPersonModel model)
  {
    return new LegalPersonEntity
    {
      Name = model.Name,
      Email = model.Email,
      PhoneNumber = model.PhoneNumber,
      Address = model.Address,
      City = model.City,
      PostalCode = model.PostalCode,
      SocialSecurityNumber = model.SocialSecurityNumber
    };
  }
}

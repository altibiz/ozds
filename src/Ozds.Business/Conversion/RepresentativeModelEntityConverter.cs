using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class RepresentativeModelEntityConverter : ModelEntityConverter<RepresentativeModel, RepresentativeEntity>
{
  protected override RepresentativeEntity ToEntity(RepresentativeModel model) =>
    model.ToEntity();

  protected override RepresentativeModel ToModel(RepresentativeEntity entity) =>
    entity.ToModel();
}

public static class RepresentativeModelEntityConverterExtensions
{
  public static RepresentativeEntity ToEntity(this RepresentativeModel model)
  {
    return new RepresentativeEntity
    {
      Id = model.Id,
      Title = model.Title,
      CreatedOn = model.CreatedOn,
      CreatedById = model.CreatedById,
      LastUpdatedOn = model.LastUpdatedOn,
      LastUpdatedById = model.LastUpdatedById,
      IsDeleted = model.IsDeleted,
      DeletedOn = model.DeletedOn,
      DeletedById = model.DeletedById,
      Role = model.Role.ToEntity(),
      Name = model.Name,
      SocialSecurityNumber = model.SocialSecurityNumber,
      Address = model.Address,
      City = model.City,
      PostalCode = model.PostalCode,
      Email = model.Email,
      PhoneNumber = model.PhoneNumber
    };
  }

  public static RepresentativeModel ToModel(this RepresentativeEntity entity)
  {
    return new RepresentativeModel
    {
      Id = entity.Id,
      Title = entity.Title,
      CreatedOn = entity.CreatedOn,
      CreatedById = entity.CreatedById,
      LastUpdatedOn = entity.LastUpdatedOn,
      LastUpdatedById = entity.LastUpdatedById,
      IsDeleted = entity.IsDeleted,
      DeletedOn = entity.DeletedOn,
      DeletedById = entity.DeletedById,
      Role = entity.Role.ToModel(),
      Name = entity.Name,
      SocialSecurityNumber = entity.SocialSecurityNumber,
      Address = entity.Address,
      City = entity.City,
      PostalCode = entity.PostalCode,
      Email = entity.Email,
      PhoneNumber = entity.PhoneNumber
    };
  }
}

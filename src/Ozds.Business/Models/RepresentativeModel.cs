using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public record RepresentativeModel(
  string Id,
  string Title,
  DateTimeOffset CreatedOn,
  string? CreatedById,
  DateTimeOffset? LastUpdatedOn,
  string? LastUpdatedById,
  bool IsDeleted,
  DateTimeOffset? DeletedOn,
  string? DeletedById,
  RoleModel Role,
  string Name,
  string SocialSecurityNumber,
  string Address,
  string City,
  string PostalCode,
  string Email,
  string PhoneNumber
) : AuditableModel(
  Id,
  Title,
  CreatedOn,
  CreatedById,
  LastUpdatedOn,
  LastUpdatedById,
  IsDeleted,
  DeletedOn,
  DeletedById
)
{
  public override IEnumerable<ValidationResult> Validate(
    ValidationContext validationContext)
  {
    throw new NotImplementedException();
  }
}

public static class RepresentativeModelExtensions
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
    return new RepresentativeModel(
      entity.Id,
      entity.Title,
      entity.CreatedOn,
      entity.CreatedById,
      entity.LastUpdatedOn,
      entity.LastUpdatedById,
      entity.IsDeleted,
      entity.DeletedOn,
      entity.DeletedById,
      entity.Role.ToModel(),
      entity.Name,
      entity.SocialSecurityNumber,
      entity.Address,
      entity.City,
      entity.PostalCode,
      entity.Email,
      entity.PhoneNumber
    );
  }
}

using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public record RepresentativeModel(
  string Id,
  string Title,
  DateTimeOffset CreationDate,
  string? CreatedById,
  DateTimeOffset? LastUpdateDate,
  string? LastUpdatedById,
  bool IsDeleted,
  DateTimeOffset? DeletionDate,
  string? DeletedById,
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
  CreationDate,
  CreatedById,
  LastUpdateDate,
  LastUpdatedById,
  IsDeleted,
  DeletionDate,
  DeletedById
)
{
  public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
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
      CreatedOn = model.CreationDate,
      CreatedById = model.CreatedById,
      LastUpdatedOn = model.LastUpdateDate,
      LastUpdatedById = model.LastUpdatedById,
      IsDeleted = model.IsDeleted,
      DeletedOn = model.DeletionDate,
      DeletedById = model.DeletedById,
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

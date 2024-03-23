using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public record RepresentativeModel(
  string Id,
  bool IsDeleted,
  string UserId,
  string Name,
  string SocialSecurityNumber,
  string Address,
  string City,
  string PostalCode,
  string Email,
  string PhoneNumber
) : SoftDeletableModel(Id, IsDeleted)
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
      IsDeleted = model.IsDeleted,
      UserId = model.UserId,
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
      entity.IsDeleted,
      entity.UserId,
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

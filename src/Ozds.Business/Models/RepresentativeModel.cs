using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public class RepresentativeModel : AuditableModel
{
  [Required] public required RoleModel Role { get; set; }

  [Required] public required string Name { get; set; }

  [Required] public required string SocialSecurityNumber { get; set; }

  [Required] public required string Address { get; set; }

  [Required] public required string City { get; set; }

  [Required] public required string PostalCode { get; set; }

  [EmailAddress] public required string Email { get; set; }

  [Phone] public required string PhoneNumber { get; set; }

  public static RepresentativeModel New(UserModel user)
  {
    return new RepresentativeModel
    {
      Id = user.Id,
      Title = user.UserName,
      CreatedOn = DateTimeOffset.UtcNow,
      CreatedById = null,
      LastUpdatedOn = null,
      LastUpdatedById = null,
      IsDeleted = false,
      DeletedOn = null,
      DeletedById = null,
      Role = RoleModel.NetworkUserRepresentative,
      Name = user.UserName,
      SocialSecurityNumber = string.Empty,
      Address = string.Empty,
      City = string.Empty,
      PostalCode = string.Empty,
      Email = user.Email,
      PhoneNumber = string.Empty
    };
  }

  public override IEnumerable<ValidationResult> Validate(
    ValidationContext validationContext)
  {
    foreach (var result in base.Validate(validationContext))
    {
      yield return result;
    }

    if (
      validationContext.MemberName is null or nameof(SocialSecurityNumber) &&
      SocialSecurityNumber.All(char.IsDigit) == false
    )
    {
      yield return new ValidationResult(
        "Social security number must contain only digits.",
        new[] { nameof(SocialSecurityNumber) }
      );
    }

    if (
      validationContext.MemberName is null or nameof(SocialSecurityNumber) &&
      SocialSecurityNumber.Length != 11
    )
    {
      yield return new ValidationResult(
        "Social security number must contain only digits.",
        new[] { nameof(SocialSecurityNumber) }
      );
    }

    if (
      validationContext.MemberName is null or nameof(PostalCode) &&
      PostalCode.All(char.IsDigit) == false
    )
    {
      yield return new ValidationResult(
        "Postal code must contain only digits.",
        new[] { nameof(PostalCode) }
      );
    }

    if (
      validationContext.MemberName is null or nameof(PostalCode) &&
      PostalCode.Length != 5
    )
    {
      yield return new ValidationResult(
        "Postal code must contain exactly 5 digits.",
        new[] { nameof(PostalCode) }
      );
    }
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

using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Complex;

public class LegalPersonModel : IModel
{
  [Required]
  public required string Name { get; set; } = default!;

  [Required]
  public required string SocialSecurityNumber { get; set; } = default!;

  [Required]
  public required string Address { get; set; } = default!;

  [Required]
  public required string PostalCode { get; set; } = default!;

  [Required]
  public required string City { get; set; } = default!;

  [EmailAddress]
  [Required]
  public required string Email { get; set; } = default!;

  [Phone]
  [Required]
  public required string PhoneNumber { get; set; } = default!;

  public IEnumerable<ValidationResult> Validate(
    ValidationContext validationContext)
  {
    if (
      validationContext.MemberName is null or nameof(SocialSecurityNumber) &&
      !SocialSecurityNumber.All(char.IsDigit)
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
        "Social security number must contain only 11 digits.",
        new[] { nameof(SocialSecurityNumber) }
      );
    }

    if (
      validationContext.MemberName is null or nameof(PostalCode) &&
      !PostalCode.All(char.IsDigit)
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

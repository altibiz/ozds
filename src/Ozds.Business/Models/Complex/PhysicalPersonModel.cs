using System.ComponentModel.DataAnnotations;

namespace Ozds.Business.Models.Complex;

public class PhysicalPersonModel : IValidatableObject
{
  [Required]
  public required string Name { get; set; } = default!;

  [EmailAddress]
  [Required]
  public required string Email { get; set; } = default!;

  [Phone]
  [Required]
  public required string PhoneNumber { get; set; } = default!;

  public IEnumerable<ValidationResult> Validate(
    ValidationContext validationContext)
  {
    yield break;
  }

  public static PhysicalPersonModel New()
  {
    return new PhysicalPersonModel
    {
      Name = "",
      Email = "",
      PhoneNumber = ""
    };
  }
}

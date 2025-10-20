using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Complex;

public class PhysicalPersonModel : Model
{
  [Required]
  public required string Name { get; set; } = default!;

  [EmailAddress]
  [Required]
  public required string Email { get; set; } = default!;

  [Phone]
  [Required]
  public required string PhoneNumber { get; set; } = default!;
}

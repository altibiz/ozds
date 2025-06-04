using System.ComponentModel.DataAnnotations;

namespace Ozds.Business.Models.Composite;

public class UserWithPasswordModel
{
  [Required]
  public required UserModel User { get; set; }

  [Required]
  public required string OldPassword { get; set; }

  [Required]
  public required string NewPassword { get; set; }
}

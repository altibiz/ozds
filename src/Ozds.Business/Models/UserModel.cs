using System.ComponentModel.DataAnnotations;

namespace Ozds.Business.Models;

public class UserModel : Base.UserModel
{
  [Required]
  [RegularExpression("^[a-zA-Z0-9_\\-\\.]+$")]
  public required string Id { get; set; }

  [Required]
  [RegularExpression("^[a-zA-Z0-9_\\-\\.]+$")]
  public required string Name { get; set; }

  [Required]
  [EmailAddress]
  public required string Email { get; set; }
}

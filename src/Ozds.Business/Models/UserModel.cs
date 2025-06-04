using System.ComponentModel.DataAnnotations;

namespace Ozds.Business.Models;

public class UserModel
{
  [Required]
  public required string Id { get; set; }

  [Required]
  public required string Name { get; set; }

  [Required]
  public required string Email { get; set; }
}

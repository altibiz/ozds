namespace Ozds.Users.Entities;

public class PasswordEntity
{
  public string UserId { get; set; } = default!;

  public string OldPassword { get; set; } = default!;

  public string NewPassword { get; set; } = default!;
}

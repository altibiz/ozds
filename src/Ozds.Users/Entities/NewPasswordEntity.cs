namespace Ozds.Users.Entities;

public class NewPasswordEntity
{
  public string UserId { get; set; } = default!;

  public string NewPassword { get; set; } = default!;
}

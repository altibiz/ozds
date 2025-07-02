namespace Ozds.Users.Entities;

public class UserEntity
{
  public string Id { get; set; } = default!;

  public string Name { get; set; } = default!;

  public string Email { get; set; } = default!;
}

public class UserWithPasswordEntity
{
  public UserEntity User { get; set; } = default!;

  public string OldPassword { get; set; } = default!;

  public string NewPassword { get; set; } = default!;
}

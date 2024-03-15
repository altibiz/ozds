namespace Ozds.Data.Models;

public record UserModel(
  string Id,
  string UserName,
  string Email,
  List<string> Roles
);

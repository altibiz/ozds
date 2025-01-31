using Ozds.Business.Models;

namespace Ozds.Client.State;

public record UserState(
  string LogoutToken,
  UserModel User
);

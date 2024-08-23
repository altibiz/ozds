using Ozds.Users.Models;

// TODO: remove connection to Ozds.Users

namespace Ozds.Client.State;

public record UserState(
  UserModel User
);

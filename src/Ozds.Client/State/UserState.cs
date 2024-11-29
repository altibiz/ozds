using System.Security.Claims;
using Ozds.Business.Models;

namespace Ozds.Client.State;

public record UserState(
  ClaimsPrincipal ClaimsPrincipal,
  string LogoutToken,
  UserModel User
);

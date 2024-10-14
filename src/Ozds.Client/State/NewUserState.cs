using System.Security.Claims;
using Ozds.Business.Models.Composite;

namespace Ozds.Client.State;

public record NewUserState(
  ClaimsPrincipal ClaimsPrincipal,
  MaybeRepresentingUserModel User
);

using System.Security.Claims;
using Ozds.Business.Models;
using Ozds.Business.Models.Composite;

namespace Ozds.Client.State;

public record UserState(
  ClaimsPrincipal ClaimsPrincipal,
  MaybeRepresentingUserModel RepresentingUser
);

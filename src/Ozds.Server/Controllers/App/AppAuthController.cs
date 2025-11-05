using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersHostExtensions = Ozds.Users.Extensions.HostExtensions;

namespace Ozds.Server.Controllers.App;

[Route("app/auth")]
public class AppAuthController : Controller
{
  [HttpGet]
  [Route("login")]
  public IActionResult Login(
    [FromQuery] string returnUrl = "/"
  )
  {
    if (!Url.IsLocalUrl(returnUrl))
    {
      returnUrl = "/";
    }

    return Challenge(
      new AuthenticationProperties { RedirectUri = returnUrl },
      UsersHostExtensions.ChallengeScheme);
  }

  [HttpPost]
  [Route("logout")]
  [Authorize]
  public IActionResult Logout(
    [FromQuery] string returnUrl = "/"
  )
  {
    if (!Url.IsLocalUrl(returnUrl))
    {
      returnUrl = "/";
    }

    return SignOut(
      new AuthenticationProperties { RedirectUri = returnUrl },
      UsersHostExtensions.AuthenticationScheme,
      UsersHostExtensions.ChallengeScheme);
  }
}

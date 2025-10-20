using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ozds.Users.Extensions;

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
      HostExtensions.AuthenticationScheme);
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
      CookieAuthenticationDefaults.AuthenticationScheme,
      HostExtensions.AuthenticationScheme);
  }
}

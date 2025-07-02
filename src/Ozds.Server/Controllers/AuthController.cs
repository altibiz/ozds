using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;

namespace Ozds.Server.Controllers;

public class AuthController : Controller
{
  public IActionResult Login(string returnUrl = "/")
  {
    return Challenge(
      new AuthenticationProperties { RedirectUri = returnUrl },
      OpenIdConnectDefaults.AuthenticationScheme);
  }

  public IActionResult Logout(string returnUrl = "/")
  {
    return SignOut(
      new AuthenticationProperties { RedirectUri = returnUrl },
      CookieAuthenticationDefaults.AuthenticationScheme,
      OpenIdConnectDefaults.AuthenticationScheme);
  }
}

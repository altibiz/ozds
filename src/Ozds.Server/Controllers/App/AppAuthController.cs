using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ozds.Users.Entities;

namespace Ozds.Server.Controllers.App;

[Route("app/auth")]
public class AppAuthController(
  SignInManager<OzdsUser> signInManager
) : Controller
{
  [HttpGet]
  [Route("login")]
  public IActionResult Login([FromQuery] string returnUrl = "/")
  {
    if (!Url.IsLocalUrl(returnUrl))
    {
      returnUrl = "/";
    }

    if (User.Identity?.IsAuthenticated == true)
    {
      return LocalRedirect(returnUrl);
    }

    ViewData["ReturnUrl"] = returnUrl;
    return View();
  }

  [HttpPost]
  [Route("login")]
  public async Task<IActionResult> Login(
    [FromForm] string username,
    [FromForm] string password,
    [FromForm] bool rememberMe = false,
    [FromQuery] string returnUrl = "/"
  )
  {
    if (!Url.IsLocalUrl(returnUrl))
    {
      returnUrl = "/";
    }

    var result = await signInManager.PasswordSignInAsync(
      username,
      password,
      isPersistent: rememberMe,
      lockoutOnFailure: true
    );

    if (result.Succeeded)
    {
      return LocalRedirect(returnUrl);
    }

    if (result.IsLockedOut)
    {
      ViewData["Error"] = "Account locked out. Please try again later.";
    }
    else
    {
      ViewData["Error"] = "Invalid login attempt.";
    }

    ViewData["ReturnUrl"] = returnUrl;
    return View();
  }

  [HttpPost]
  [Route("logout")]
  [Authorize]
  public async Task<IActionResult> Logout(
    [FromQuery] string returnUrl = "/"
  )
  {
    if (!Url.IsLocalUrl(returnUrl))
    {
      returnUrl = "/";
    }

    await signInManager.SignOutAsync();

    return LocalRedirect(returnUrl);
  }
}

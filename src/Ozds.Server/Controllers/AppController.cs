using System.Globalization;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Ozds.Client.ViewModels;

namespace Ozds.Server.Controllers;

public class AppController(IAntiforgery antiforgery) : Controller
{
  public IActionResult Catchall([FromRoute] string culture)
  {
    CultureInfo? cultureInfo;
    try
    {
      cultureInfo = new CultureInfo(culture);
    }
    catch (CultureNotFoundException ex)
    {
      return BadRequest(ex);
    }

    string? logoutToken;
    try
    {
      logoutToken = antiforgery
        .GetAndStoreTokens(Request.HttpContext).RequestToken;
    }
    catch (Exception ex)
    {
      return BadRequest(ex);
    }

    return View(new AppViewModel
    {
      Culture = cultureInfo,
      LogoutToken = logoutToken!,
    });
  }
}

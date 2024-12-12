using System.Globalization;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Ozds.Client.ViewModels;

namespace Ozds.Server.Controllers;

public class AppController(IAntiforgery antiforgery) : Controller
{
  public IActionResult Catchall([FromRoute] string? culture)
  {
    CultureInfo? cultureInfo = null;
    if (culture is not null)
    {
      try
      {
        cultureInfo = new CultureInfo(culture);
      }
      catch (Exception)
      {
        // NOTE: let local storage handle the culture
      }
    }

    if (culture is { } && cultureInfo is { }
      && cultureInfo.TwoLetterISOLanguageName != culture)
    {
      return Redirect($"/app/{cultureInfo.TwoLetterISOLanguageName}");
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

    return View(
      new AppViewModel
      {
        Culture = cultureInfo,
        LogoutToken = logoutToken!,
      });
  }
}

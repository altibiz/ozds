using System.Globalization;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Ozds.Server.ViewModels;

namespace Ozds.Server.Controllers;

public class AppController(IAntiforgery antiforgery) : Controller
{
  public const string LocalStorageCulture = "_";

  public IActionResult Uncultured()
  {
    return Redirect($"/app/{LocalStorageCulture}");
  }

  public IActionResult Cultured(string culture)
  {
    CultureInfo? cultureInfo = null;
    if (culture is { })
    {
      try
      {
        cultureInfo = new CultureInfo(culture);
      }
      catch (Exception)
      {
        if (culture != LocalStorageCulture)
        {
          return Redirect($"/app/{LocalStorageCulture}");
        }
      }
    }

    if (cultureInfo is { } && cultureInfo.TwoLetterISOLanguageName != culture)
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

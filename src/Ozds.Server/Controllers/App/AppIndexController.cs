using System.Globalization;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ozds.Server.ViewModels;

namespace Ozds.Server.Controllers.App;

[Route("app")]
public class AppIndexController(IAntiforgery antiforgery) : Controller
{
  public const string LocalStorageCulture = "_";

  [HttpGet]
  [Route("")]
  public IActionResult Index()
  {
    return Redirect($"/app/{LocalStorageCulture}");
  }

  [HttpGet]
  [Route("{culture}/{**catchall}")]
  [Authorize]
  public IActionResult Index(
    string culture,
    string? catchall
  )
  {
    if (catchall?.StartsWith("_content") ?? false)
    {
      return Redirect($"/{catchall}");
    }

    CultureInfo? cultureInfo = null;
    if (culture is not null)
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

    if (cultureInfo is not null
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
      "App",
      new AppViewModel
      {
        Culture = cultureInfo,
        LogoutToken = logoutToken!
      });
  }
}

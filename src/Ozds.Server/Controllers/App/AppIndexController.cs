using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ozds.Business.Queries;
using Ozds.Server.ViewModels;

namespace Ozds.Server.Controllers.App;

[Route("app")]
public class AppIndexController(
  IAntiforgery antiforgery,
  LocalizationQueries localizationQueries
) : Controller
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

    var cultureInfo = localizationQueries.IdToCulture(culture);
    if (cultureInfo is null && culture != LocalStorageCulture)
    {
      return Redirect($"/app/{LocalStorageCulture}");
    }

    var cultureInfoId =
      cultureInfo is null
        ? null
        : localizationQueries.CultureToId(cultureInfo);
    if (cultureInfoId is not null && culture != cultureInfoId)
    {
      return Redirect($"/app/{cultureInfoId}");
    }

    string? logoutToken;
    try
    {
      logoutToken = antiforgery
          .GetAndStoreTokens(Request.HttpContext).RequestToken
        ?? throw new InvalidOperationException("Antiforgery token is null.");
    }
    catch (Exception ex)
    {
      return BadRequest(ex);
    }

    return View(
      "App",
      new AppViewModel
      {
        CultureId = cultureInfoId,
        LogoutToken = logoutToken
      });
  }
}

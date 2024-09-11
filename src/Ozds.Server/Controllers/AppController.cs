using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Ozds.Client.ViewModels;

namespace Ozds.Server.Controllers;

public class AppController : Controller
{
  public IActionResult Catchall([FromRoute] string culture)
  {
    try
    {
      return View(new AppViewModel { Culture = new CultureInfo(culture) });
    }
    catch (CultureNotFoundException ex)
    {
      return BadRequest(ex);
    }
  }
}

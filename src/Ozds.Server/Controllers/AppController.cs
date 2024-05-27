using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Ozds.Server.ViewModels;

namespace Ozds.Server.Controllers;

public class AppController : Controller
{
  public IActionResult Index([FromRoute] string culture)
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

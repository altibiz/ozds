using Microsoft.AspNetCore.Mvc;

namespace Ozds.Server.Controllers;

public class AppController : Controller
{
  public IActionResult Index()
  {
    return View();
  }
}

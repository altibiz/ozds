using Microsoft.AspNetCore.Mvc;

namespace Ozds.Server.Controllers;

public class ClientController : Controller
{
  public IActionResult Index()
  {
    return View("Client");
  }
}

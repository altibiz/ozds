using Microsoft.AspNetCore.Mvc;
using Ozds.Server.Controllers.App;

namespace Ozds.Server.Controllers;

[Route("")]
public class IndexController : Controller
{
  [HttpGet]
  [Route("")]
  public IActionResult Index()
  {
    return Redirect($"/app/{AppIndexController.LocalStorageCulture}");
  }
}

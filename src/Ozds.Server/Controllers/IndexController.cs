using Microsoft.AspNetCore.Mvc;

namespace Ozds.Server.Controllers;

public class IndexController : Controller
{
  public IActionResult Index()
  {
    return Redirect("/app/hr");
  }
}

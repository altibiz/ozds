using Microsoft.AspNetCore.Mvc;
using Ozds.Business.Iot;

namespace Ozds.Server.Controllers;

[IgnoreAntiforgeryToken]
public class IotController : Controller
{
  private readonly OzdsIotHandler _iotHandler;

  public IotController(OzdsIotHandler iotHandler)
  {
    _iotHandler = iotHandler;
  }

  [HttpPost]
  public async Task<IActionResult> Push(string id)
  {
    using var reader = new StreamReader(Request.Body);

    var message = await reader.ReadToEndAsync();

    if (!await _iotHandler.Authorize(id, message))
    {
      return Unauthorized();
    }

    await _iotHandler.OnPush(id, message);

    return Ok();
  }

  public async Task<IActionResult> Poll([FromRoute] string id,
    [FromBody] string message)
  {
    if (!await _iotHandler.Authorize(id, message))
    {
      return Unauthorized();
    }

    await _iotHandler.OnPoll(id, message);

    return Ok();
  }
}

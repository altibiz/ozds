using Microsoft.AspNetCore.Mvc;
using Ozds.Business.Iot;

namespace Ozds.Server.Controllers;

[IgnoreAntiforgeryToken]
public class IotController(OzdsIotHandler iotHandler) : Controller
{
  private readonly OzdsIotHandler _iotHandler = iotHandler;

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

  public async Task<IActionResult> Poll(string id)
  {
    using var reader = new StreamReader(Request.Body);
    var message = await reader.ReadToEndAsync();

    if (!await _iotHandler.Authorize(id, message))
    {
      return Unauthorized();
    }

    await _iotHandler.OnPoll(id, message);

    return Ok();
  }

  [HttpPost]
  public async Task<IActionResult> Update(string id)
  {
    using var reader = new StreamReader(Request.Body);
    var message = await reader.ReadToEndAsync();

    if (!await _iotHandler.Authorize(id, message))
    {
      return Unauthorized();
    }

    await _iotHandler.OnUpdate(id, message);

    return Ok();
  }
}

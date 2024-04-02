using Microsoft.AspNetCore.Mvc;
using Ozds.Business.Iot;

namespace Ozds.Server.Controllers;

public class IotController : ControllerBase
{
  private readonly OzdsIotHandler _iotHandler;

  public IotController(OzdsIotHandler iotHandler)
  {
    _iotHandler = iotHandler;
  }

  public async Task<IActionResult> Push([FromRoute] string id, [FromBody] string message)
  {
    if (!await _iotHandler.Authorize(id, message))
    {
      return Unauthorized();
    }

    await _iotHandler.OnPush(id, message);

    return Ok();
  }

  public async Task<IActionResult> Poll([FromRoute] string id, [FromBody] string message)
  {
    if (!await _iotHandler.Authorize(id, message))
    {
      return Unauthorized();
    }

    await _iotHandler.OnPoll(id, message);

    return Ok();
  }
}

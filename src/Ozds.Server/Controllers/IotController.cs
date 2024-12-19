using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Ozds.Iot.Entities.Abstractions;
using Ozds.Iot.Observers.Abstractions;
using Ozds.Iot.Observers.EventArgs;

namespace Ozds.Server.Controllers;

[IgnoreAntiforgeryToken]
public class IotController(IPushPublisher publisher) : Controller
{
  public static readonly JsonSerializerOptions Options = new()
  {
    PropertyNameCaseInsensitive = true,
    NumberHandling = JsonNumberHandling.AllowReadingFromString
  };

  [HttpPost]
  public async Task<IActionResult> Push(string id)
  {
    IMessengerPushRequestEntity? request;
    try
    {
      request = await JsonSerializer
        .DeserializeAsync<IMessengerPushRequestEntity>(
          Request.Body,
          Options
        );
    }
    catch (JsonException ex)
    {
      return BadRequest(ex.Message);
    }

    if (request is null)
    {
      return BadRequest("No request found");
    }

    if (request.Measurements.Count is 0)
    {
      return BadRequest("No measurements found");
    }

    publisher.PublishPush(new PushEventArgs(id, request));

    return Ok();
  }
}

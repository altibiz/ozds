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
  public async Task<IActionResult> Push(
    string id,
    [FromHeader(Name = "X-Buffer-Behavior")]
    string? bufferBehavior = "buffer"
  )
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

    var eventArgs = new PushEventArgs
    {
      MessengerId = id,
      BufferBehavior = bufferBehavior switch
      {
        "realtime" => PushEventBufferBehavior.Realtime,
        "buffer" => PushEventBufferBehavior.Buffer,
        _ => throw new ArgumentOutOfRangeException(nameof(bufferBehavior))
      },
      Request = request
    };

    publisher.Publish(eventArgs);

    return Ok();
  }
}

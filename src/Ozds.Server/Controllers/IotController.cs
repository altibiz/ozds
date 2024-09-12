using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Ozds.Iot.Entities;
using Ozds.Iot.Entities.Abstractions;
using Ozds.Iot.Observers.Abstractions;
using Ozds.Iot.Observers.EventArgs;

namespace Ozds.Server.Controllers;

[IgnoreAntiforgeryToken]
public class IotController(IPushPublisher publisher) : Controller
{
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

    publisher.PublishPush(new PushEventArgs(id, request));

    return Ok();
  }

  public static readonly JsonSerializerOptions Options = new()
  {
    PropertyNameCaseInsensitive = true
  };
}

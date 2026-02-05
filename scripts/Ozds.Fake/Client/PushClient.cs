using System.Net.Http.Headers;
using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Fake.Client;

public enum PushClientBufferBehavior
{
  Realtime,
  Buffer,
  Aggregate,
}

public static class PushClientBufferBehaviorExtensions
{
  public static string ToValue(this PushClientBufferBehavior bufferBehavior)
  {
    return bufferBehavior switch
    {
      PushClientBufferBehavior.Realtime => "realtime",
      PushClientBufferBehavior.Buffer => "buffer",
      PushClientBufferBehavior.Aggregate => "aggregate",
      _ => throw new InvalidOperationException(
        $"Unknown buffer behavior {bufferBehavior}"
      ),
    };
  }
}

public class PushClient(
  IHttpClientFactory httpClientFactory,
  ILogger<PushClient> logger
)
{
  public const string Name = "Ozds.Fake";

  public async Task Push(
    string messengerId,
    string messengerApiKey,
    PushClientBufferBehavior bufferBehavior,
    IMessengerPushRequestEntity request,
    CancellationToken cancellationToken
  )
  {
    var client = httpClientFactory.CreateClient(Name);
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
      "Bearer",
      messengerApiKey
    );
    client.DefaultRequestHeaders.Add(
      "X-Buffer-Behavior",
      bufferBehavior.ToValue()
    );

    logger.LogInformation(
      "Pushing {Count} measurements for messenger {MessengerId}",
      request.Measurements.Count,
      messengerId
    );

    var content = JsonContent.Create(request);

    var success = false;
    var retries = 0;

    while (!success && retries < 3)
    {
      retries++;
      try
      {
        var response = await client.PostAsync(
          $"iot/push/{messengerId}",
          content,
          cancellationToken
        );
        success = response.IsSuccessStatusCode;
        if (!success)
        {
          logger.LogWarning(
            "Failed to push measurements with {StatusCode} because {Message}, retrying...",
            response.StatusCode,
            await response.Content.ReadAsStringAsync(cancellationToken)
          );
        }
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "Failed to push measurements");
        await Task.Delay(1000, cancellationToken);
      }
    }
  }
}

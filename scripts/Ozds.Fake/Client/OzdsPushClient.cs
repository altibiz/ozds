using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Fake.Client;

public class OzdsPushClient(
  IHttpClientFactory httpClientFactory,
  ILogger<OzdsPushClient> logger,
  IServiceProvider serviceProvider
)
{
  public async Task Push(
    string messengerId,
    string apiKey,
    bool realtime,
    IMessengerPushRequestEntity request,
    CancellationToken cancellationToken = default
  )
  {
    var client = httpClientFactory.CreateClient();
    client.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
    client.DefaultRequestHeaders.Add(
      "X-Buffer-Behavior",
      realtime ? "realtime" : "buffer");

    client.BaseAddress = new Uri(
      serviceProvider.GetService<OzdsFakePushArguments>()?.BaseUrl
      ?? serviceProvider.GetService<OzdsFakeSeedArguments>()?.BaseUrl
      ?? throw new InvalidOperationException("No base URL found"));

    logger.LogInformation(
      "Pushing measurements to {BaseUrl} for messenger {MessengerId}",
      client.BaseAddress,
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
        var response =
          await client.PostAsync(
            $"iot/push/{messengerId}",
            content,
            cancellationToken
          );
        success = response.IsSuccessStatusCode;
        if (!success)
        {
          logger.LogWarning(
            "Failed to push measurements with {StatusCode}, retrying...",
            response.StatusCode
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

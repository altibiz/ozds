using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Fake.Client;

// TODO: put some options into appsettings.json

public class OzdsPushClient(
  IHttpClientFactory httpClientFactory,
  ILogger<OzdsPushClient> logger
)
{
  public async Task Push(
    string messengerId,
    string apiKey,
    IMessengerPushRequestEntity request,
    CancellationToken cancellationToken = default
  )
  {
    var client = httpClientFactory.CreateClient();
    client.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
    Console.WriteLine(client.BaseAddress);

    // FIXME: this should be configurable
#pragma warning disable S1075
    client.BaseAddress = new Uri("http://localhost:5000/");
#pragma warning restore S1075

    logger.LogInformation(
      "Pushing measurements to {BaseUrl} for messenger {MessengerId}",
      client.BaseAddress,
      messengerId
    );

    var content = JsonContent.Create(request);

    var success = false;

    while (!success)
    {
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
      }
      finally
      {
        await Task.Delay(1000, cancellationToken);
      }
    }
  }
}

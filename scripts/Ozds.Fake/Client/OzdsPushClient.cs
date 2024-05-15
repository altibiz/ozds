using Ozds.Business.Iot;

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
    MessengerPushRequest request,
    CancellationToken cancellationToken = default
  )
  {
    var client = httpClientFactory.CreateClient();
    client.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
    Console.WriteLine(client.BaseAddress);
    // FIXME: this should be configurable
    client.BaseAddress = new Uri("http://localhost:5000/");

    logger.LogInformation(
      "Pushing measurements to {baseUrl} for messenger {messengerId}",
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
            "Failed to push measurements with {statusCode}, retrying...",
            response.StatusCode
          );
        }

        await Task.Delay(1000, cancellationToken);
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "Failed to push measurements");
      }
    }
  }
}

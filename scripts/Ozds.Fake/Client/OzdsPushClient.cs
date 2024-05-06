using Ozds.Business.Iot;

namespace Ozds.Fake.Client;

public class OzdsPushClient(
  IHttpClientFactory httpClientFactory,
  ILogger<OzdsPushClient> logger)
{
  private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
  private readonly ILogger _logger = logger;

  public async Task Push(
    string messengerId,
    string apiKey,
    MessengerPushRequest request
  )
  {
    var client = _httpClientFactory.CreateClient();
    client.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
    Console.WriteLine(client.BaseAddress);
    // FIXME: this should be configurable
    client.BaseAddress = new Uri("http://localhost:5000/");

    _logger.LogInformation(
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
          await client.PostAsync($"iot/push/{messengerId}", content);
        success = response.IsSuccessStatusCode;
        if (!success)
        {
          _logger.LogWarning(
            "Failed to push measurements with {statusCode}, retrying...",
            response.StatusCode
          );
        }

        await Task.Delay(1000);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Failed to push measurements");
      }
    }
  }
}

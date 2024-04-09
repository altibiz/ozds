using Ozds.Business.Iot;

namespace Ozds.Fake.Client;

public class OzdsPushClient
{
  private readonly IHttpClientFactory _httpClientFactory;

  public OzdsPushClient(IHttpClientFactory httpClientFactory)
  {
    _httpClientFactory = httpClientFactory;
  }

  public async Task Push(
    string baseUrl,
    string messengerId,
    string apiKey,
    MessengerPushRequest request
  )
  {
    var client = _httpClientFactory.CreateClient();
    client.DefaultRequestHeaders.Add("X-Api-Key", apiKey);

    var content = JsonContent.Create(request);

    await client.PostAsync($"{baseUrl}/iot/push/{messengerId}", content);
  }
}

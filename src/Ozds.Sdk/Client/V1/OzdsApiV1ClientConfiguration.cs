using System.Net.Http.Headers;

namespace Ozds.Sdk.Client.V1;

internal partial class OzdsApiV1Client
{
  private string? _apiKey;
  private string? _baseUrl;

  public string? BaseUrl
  {
    get { return _baseUrl ??= _httpClient.BaseAddress?.ToString(); }
    set
    {
      _baseUrl ??= value;
      _httpClient.BaseAddress = _baseUrl is { } baseUrl
        ? new Uri(baseUrl)
        : null;
    }
  }

  public string? ApiKey
  {
    get
    {
      return _apiKey ??= _httpClient
        .DefaultRequestHeaders
        .Authorization
        ?.Parameter;
    }
    set
    {
      _apiKey ??= value;
      _httpClient.DefaultRequestHeaders.Authorization = _apiKey is { } apiKey
        ? new AuthenticationHeaderValue("Bearer", apiKey)
        : null;
    }
  }
}

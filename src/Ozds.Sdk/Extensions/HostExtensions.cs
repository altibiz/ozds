using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using Ozds.Sdk.Client.V1;
using Ozds.Sdk.Contracts.V1;
using Ozds.Sdk.Options;

namespace Ozds.Sdk.Extensions;

public static class HostExtensions
{
  public static IHostApplicationBuilder AddOzdsSdk(
    this IHostApplicationBuilder builder,
    Action<Configurator>? configure = null
  )
  {
    var configurator = new Configurator();
    if (configure is not null)
    {
      configure(configurator);
    }

    builder.AddHttp();
    builder.AddOptions();
    builder.AddContracts(configurator);
    return builder;
  }

  private static IHostApplicationBuilder AddHttp(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddHttpClient();
    return builder;
  }

  private static IHostApplicationBuilder AddOptions(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.ConfigureOptions<ConfigureOzdsSdkOptions>();
    return builder;
  }

  private static IHostApplicationBuilder AddContracts(
    this IHostApplicationBuilder builder,
    Configurator configurator
  )
  {
    builder.Services.AddScoped<IOzdsApiV1Client>(
      services =>
      {
        var factory = services.GetRequiredService<IHttpClientFactory>();
        var client = factory.CreateClient();

        var options = services.GetRequiredService<IOptions<OzdsSdkOptions>>();
        client.BaseAddress = new Uri(options.Value.BaseUrl);
        client.DefaultRequestHeaders.Authorization =
          new AuthenticationHeaderValue("Bearer", options.Value.ApiKey);

        client = configurator.ConfigureClient(client);

        return new OzdsApiV1Client(client);
      });
    return builder;
  }

  public class Configurator
  {
    public Func<HttpClient, HttpClient> ConfigureClient { get; private set; } =
      httpClient => httpClient;

    public Configurator WithClient(
      Func<HttpClient, HttpClient> configureClient
    )
    {
      var prior = ConfigureClient;
      ConfigureClient = httpClient =>
      {
        return configureClient(prior(httpClient));
      };
      return this;
    }
  }
}

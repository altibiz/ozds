using System.ClientModel;
using Altibiz.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using OpenAI;
using Ozds.Translation.Arguments;
using Ozds.Translation.Client;
using Ozds.Translation.Options;
using Ozds.Translation.Services;
using Ozds.Translation.Workers.Abstractions;

namespace Ozds.Translation.Extensions;

public static class HostExtensions
{
  public static IHostApplicationBuilder AddOzdsTranslation(
    this IHostApplicationBuilder builder,
    IOzdsTranslationArguments arguments
  )
  {
    builder.Services.AddSingleton(arguments.GetType(), arguments);
    builder
      .AddOptions()
      .AddWorkers()
      .AddClient();

    if (arguments is OzdsTranslationRegexArguments)
    {
      builder.Services.AddHostedService<RegexService>();
    }

    if (arguments is OzdsTranslationTypeArguments)
    {
      builder.Services.AddHostedService<TypeService>();
    }

    return builder;
  }

  private static IHostApplicationBuilder AddOptions(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.ConfigureOptions<ConfigureOzdsTranslationOptions>();
    var relativeServerSettings = builder.Configuration
      .GetSection("Ozds:Translation:ServerSettings")
      .Get<string>();
    if (relativeServerSettings is not null)
    {
      var serverSettings = Path.Combine(
        builder.Environment.ContentRootPath,
        relativeServerSettings);
      builder.Configuration.AddJsonFile(serverSettings);
    }

    return builder;
  }

  private static IHostApplicationBuilder AddWorkers(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddScopedAssignableTo<IWorker>();
    return builder;
  }

  private static IHostApplicationBuilder AddClient(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddScoped(
      services =>
      {
        var options = services
          .GetRequiredService<IOptions<OzdsTranslationOptions>>().Value;

        var clientOptions = new OpenAIClientOptions
        {
          Endpoint = new Uri(options.OpenAiApi.BaseUrl)
        };

        var clientCredential = new ApiKeyCredential(options.OpenAiApi.ApiKey);

        var client = new OpenAIClient(clientCredential, clientOptions);

        return client;
      });
    builder.Services.AddScoped<TranslateClient>();
    return builder;
  }
}

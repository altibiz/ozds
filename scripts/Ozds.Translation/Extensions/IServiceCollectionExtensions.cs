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

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddOzdsTranslation(
    this IServiceCollection services,
    object arguments
  )
  {
    services.AddSingleton(arguments.GetType(), arguments);
    services
      .AddOptions()
      .AddWorkers()
      .AddClient();

    return arguments switch
    {
      OzdsTranslationRegexArguments => services
        .AddHostedService<RegexService>(),
      OzdsTranslationTypeArguments => services
        .AddHostedService<TypeService>(),
      _ => throw new InvalidOperationException($"Unknown options: {arguments}")
    };
  }

  private static IServiceCollection AddOptions(
    this IServiceCollection services
  )
  {
    services.ConfigureOptions<ConfigureOzdsTranslationOptions>();
    return services;
  }

  private static IServiceCollection AddWorkers(
    this IServiceCollection services
  )
  {
    services.AddScopedAssignableTo<IWorker>();
    return services;
  }

  private static IServiceCollection AddClient(
    this IServiceCollection services
  )
  {
    services.AddScoped(
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
    services.AddScoped<TranslateClient>();
    return services;
  }
}

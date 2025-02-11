using Ozds.Document.Loaders.Abstractions;
using Ozds.Document.Loaders.Implementations;
using Ozds.Document.Queries;
using Ozds.Document.Renderers.Abstractions;
using Ozds.Document.Renderers.Implementations;

namespace Ozds.Document.Extensions;

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddOzdsDocument(
    this IServiceCollection services,
    IHostApplicationBuilder builder
  )
  {
    services.AddDocumentRenderer();
    services.AddDocumentServices();
    services.AddDocumentQueries();
    return services;
  }

  public static IServiceCollection AddDocumentRenderer(
    this IServiceCollection services
  )
  {
    services.AddSingleton<IHtmlToPdfRenderer, PlaywrightHtmlToPdfRenderer>();
    services.AddSingleton<IComponentToHtmlRenderer, AspNetCoreComponentsComponentToHtmlRenderer>();
    services.AddSingleton<DocumentRenderer>();

    return services;
  }

  public static IServiceCollection AddDocumentServices(
    this IServiceCollection services
  )
  {
    services.AddSingleton<IDocumentAssetLoader, DocumentAssetLoader>();
    services.AddSingleton<IDocumentLocalizer, DocumentLocalizer>();
    return services;
  }

  public static IServiceCollection AddDocumentQueries(
    this IServiceCollection services
  )
  {
    services.AddScoped<DocumentQueries>();
    return services;
  }
}

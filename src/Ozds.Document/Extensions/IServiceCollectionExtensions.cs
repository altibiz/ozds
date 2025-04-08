using Ozds.Document.Queries;
using Ozds.Document.Renderers.Abstractions;
using Ozds.Document.Renderers.Implementations;

namespace Ozds.Document.Extensions;

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddOzdsDocument(
    this IServiceCollection services
  )
  {
    services.AddDocumentRenderer();
    services.AddDocumentQueries();
    return services;
  }

  public static IServiceCollection AddDocumentRenderer(
    this IServiceCollection services
  )
  {
    services.AddSingleton<IHtmlToPdfRenderer, PlaywrightHtmlToPdfRenderer>();
    services.AddScoped<
      IComponentToHtmlRenderer,
      AspNetCoreComponentsComponentToHtmlRenderer>();
    services.AddScoped<DocumentRenderer>();

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

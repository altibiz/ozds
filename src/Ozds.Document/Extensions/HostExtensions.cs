using Ozds.Document.Queries;
using Ozds.Document.Renderers.Abstractions;
using Ozds.Document.Renderers.Implementations;

namespace Ozds.Document.Extensions;

public static class HostExtensions
{
  public static IHostApplicationBuilder AddOzdsDocument(
    this IHostApplicationBuilder builder
  )
  {
    builder.AddDocumentRenderer();
    builder.AddDocumentQueries();
    return builder;
  }

  public static IHostApplicationBuilder AddDocumentRenderer(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddSingleton<
      IHtmlToPdfRenderer,
      PlaywrightHtmlToPdfRenderer
    >();
    builder.Services.AddScoped<
      IComponentToHtmlRenderer,
      AspNetCoreComponentsComponentToHtmlRenderer
    >();
    builder.Services.AddScoped<DocumentRenderer>();

    return builder;
  }

  public static IHostApplicationBuilder AddDocumentQueries(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddScoped<DocumentQueries>();
    return builder;
  }
}

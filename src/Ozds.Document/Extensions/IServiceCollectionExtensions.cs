using DinkToPdf;
using DinkToPdf.Contracts;
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
    services.AddHtmlToPdfConverter();
    services.AddDocumentRenderer();
    return services;
  }

  public static IServiceCollection AddHtmlToPdfConverter(
    this IServiceCollection services
  )
  {
    services.AddSingleton<IConverter>(
      _ => new SynchronizedConverter(new PdfTools()));

    return services;
  }

  public static IServiceCollection AddDocumentRenderer(
    this IServiceCollection services
  )
  {
    services.AddSingleton<IHtmlToPdfRenderer, LibwkhtmltoxHtmlToPdfRenderer>();
    services.AddSingleton<IComponentToHtmlRenderer, AspNetCoreComponentsComponentToHtmlRenderer>();
    services.AddSingleton<DocumentRenderer>();

    return services;
  }
}

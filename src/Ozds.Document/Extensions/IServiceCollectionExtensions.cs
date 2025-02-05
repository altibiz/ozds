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
    services.AddLibwkhtmltoxRenderer();
    services.AddPdfSharpRenderer();
    services.AddDocumentRenderer();
    return services;
  }

  public static IServiceCollection AddPdfSharpRenderer(
    this IServiceCollection services
  )
  {
    return services;
  }

  public static IServiceCollection AddLibwkhtmltoxRenderer(
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
    services.AddSingleton<IDocumentRenderer, LibwkhtmltoxDocumentRenderer>();
#pragma warning disable S125 // Sections of code should not be commented out
    // services.AddSingleton<IDocumentRenderer, PdfSharpDocumentRenderer>();
#pragma warning restore S125 // Sections of code should not be commented out

    services.AddSingleton<DocumentRenderer>();

    return services;
  }
}

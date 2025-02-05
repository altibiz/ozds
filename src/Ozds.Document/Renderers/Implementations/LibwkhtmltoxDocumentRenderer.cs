using DinkToPdf;
using DinkToPdf.Contracts;
using Ozds.Document.Renderers.Abstractions;

namespace Ozds.Document.Renderers.Implementations;

public class LibwkhtmltoxDocumentRenderer(
  IConverter pdfToHtmlConverter,
  ILogger<LibwkhtmltoxDocumentRenderer> logger
) : IDocumentRenderer
{
#pragma warning disable SA1011 // Closing square brackets should be spaced correctly
  public Task<byte[]?> RenderHtmlToPdf(string html)
#pragma warning restore SA1011 // Closing square brackets should be spaced correctly
  {
    return Task.Run(() =>
    {
      try
      {
        var doc = new HtmlToPdfDocument()
        {
          GlobalSettings =
          {
            ColorMode = ColorMode.Color,
            Orientation = Orientation.Portrait,
            PaperSize = PaperKind.A4Plus,
          },
          Objects =
          {
            new ObjectSettings()
            {
              PagesCount = true,
              HtmlContent = html,
              WebSettings =
              {
                DefaultEncoding = "utf-8"
              },
              HeaderSettings =
              {
                FontSize = 9,
                Right = "Page [page] of [toPage]",
                Line = true,
                Spacing = 2.812
              }
            }
          }
        };

        var bytes = pdfToHtmlConverter.Convert(doc);

        return bytes;
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "Error while rendering html to pdf");
        return null;
      }
    });
  }
}

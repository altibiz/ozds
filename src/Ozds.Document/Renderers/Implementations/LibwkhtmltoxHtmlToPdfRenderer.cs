using DinkToPdf;
using DinkToPdf.Contracts;
using Ozds.Document.Renderers.Abstractions;

namespace Ozds.Document.Renderers.Implementations;

public class LibwkhtmltoxHtmlToPdfRenderer(
  IConverter pdfToHtmlConverter,
  ILogger<LibwkhtmltoxHtmlToPdfRenderer> logger
) : IHtmlToPdfRenderer
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
              HtmlContent = html,
              WebSettings =
              {
                DefaultEncoding = "utf-8",
                LoadImages = true
              },
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

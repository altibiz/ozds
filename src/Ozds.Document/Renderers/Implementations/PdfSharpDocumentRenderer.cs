using Ozds.Document.Renderers.Abstractions;
using PdfSharp;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace Ozds.Document.Renderers.Implementations;

public class PdfSharpDocumentRenderer(
  ILogger<PdfSharpDocumentRenderer> logger
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
        using var doc = PdfGenerator.GeneratePdf(html, PageSize.A4, 1);
        using var outputStream = new MemoryStream();
        var pdf = PdfGenerator.GeneratePdf(html, PageSize.A4);
        pdf.Save(outputStream);
        var result = outputStream.ToArray();
        pdf.Dispose();
        return result;
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "Error while rendering html to pdf");
        return null;
      }
    });
  }
}

using Microsoft.Playwright;
using Ozds.Document.Renderers.Abstractions;

namespace Ozds.Document.Renderers.Implementations;

public class PlaywrightHtmlToPdfRenderer : IHtmlToPdfRenderer
{
#pragma warning disable SA1011 // Closing square brackets should be spaced correctly
  public async Task<byte[]?> RenderHtmlToPdf(string html)
#pragma warning restore SA1011 // Closing square brackets should be spaced correctly
  {
    using var playwright = await Playwright.CreateAsync();

    await using var browser = await playwright.Chromium.LaunchAsync();

    var context = await browser.NewContextAsync();
    var page = await context.NewPageAsync();
    await page.SetContentAsync(html, new PageSetContentOptions
    {
      WaitUntil = WaitUntilState.DOMContentLoaded
    });

    var pdfBytes = await page.PdfAsync(new PagePdfOptions
    {
      Format = "A4",
      PrintBackground = true,
    });

    return pdfBytes;
  }
}

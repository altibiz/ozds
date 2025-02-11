using Microsoft.Playwright;
using Ozds.Document.Renderers.Abstractions;

namespace Ozds.Document.Renderers.Implementations;

public sealed class PlaywrightHtmlToPdfRenderer
  : IHtmlToPdfRenderer, IAsyncDisposable
{
  private readonly SemaphoreSlim _lock = new(1, 1);

  private readonly Lazy<Task<BrowserContext>> _browserContext = new(async () =>
  {
    var playwright = await Playwright.CreateAsync();
    var browser = await playwright.Chromium.LaunchAsync(
      new BrowserTypeLaunchOptions
      {
        Headless = true,
        ChromiumSandbox = false
      });

    var context = await browser.NewContextAsync();

    return new BrowserContext(playwright, browser, context);
  });

#pragma warning disable SA1011 // Closing square brackets should be spaced correctly
  public async Task<byte[]?> RenderHtmlToPdf(string html)
#pragma warning restore SA1011 // Closing square brackets should be spaced correctly
  {
    await _lock.WaitAsync();

    try
    {
      var context = await _browserContext.Value;
      var page = await context.Context.NewPageAsync();
      await page.SetContentAsync(html, new PageSetContentOptions
      {
        WaitUntil = WaitUntilState.DOMContentLoaded
      });
      var pdfBytes = await page.PdfAsync(new PagePdfOptions
      {
        Format = "A4",
        PrintBackground = true,
      });
      await page.CloseAsync();

      return pdfBytes;
    }
    finally
    {
      _lock.Release();
    }
  }

  public async ValueTask DisposeAsync()
  {
    if (!_browserContext.IsValueCreated)
    {
      return;
    }

    var context = await _browserContext.Value;
    await context.Context.CloseAsync();
    await context.Context.DisposeAsync();

    await context.Browser.CloseAsync();
    await context.Browser.DisposeAsync();

    context.Playwright.Dispose();
  }

  private record struct BrowserContext(
    IPlaywright Playwright,
    IBrowser Browser,
    IBrowserContext Context
  );
}

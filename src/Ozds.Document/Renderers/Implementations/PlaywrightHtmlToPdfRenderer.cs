using Microsoft.Playwright;
using Ozds.Document.Renderers.Abstractions;

namespace Ozds.Document.Renderers.Implementations;

public sealed class PlaywrightHtmlToPdfRenderer(
  ILogger<PlaywrightHtmlToPdfRenderer> logger
) : IHtmlToPdfRenderer, IAsyncDisposable
{
  private readonly Lazy<Task<BrowserContext>> _browserContext = new(
    async () =>
    {
      var playwright = await Playwright.CreateAsync();
      var browser = await playwright.Chromium.LaunchAsync(
        new BrowserTypeLaunchOptions
        {
          // NOTE: the Headless option makes it use the old headless mode
          // which is not present anymore in chromium since 132
          Args = ["--headless"]
        });

      var context = await browser.NewContextAsync();

      return new BrowserContext(playwright, browser, context);
    });

  private readonly SemaphoreSlim _lock = new(1, 1);

  public async ValueTask DisposeAsync()
  {
    if (!_browserContext.IsValueCreated)
    {
      return;
    }

    BrowserContext? context;
    try
    {
      context = await _browserContext.Value;
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "Failed to dispose context.");
      return;
    }

    try
    {
      await context.Context.DisposeAsync();
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "Failed to dispose context.");
    }

    try
    {
      await context.Browser.DisposeAsync();
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "Failed to dispose browser.");
    }

    try
    {
      context.Playwright.Dispose();
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "Failed to dispose playwright.");
    }
  }

#pragma warning disable SA1011 // Closing square brackets should be spaced correctly
  public async Task<byte[]?> RenderHtmlToPdf(
    string html,
    CancellationToken cancellationToken
  )
#pragma warning restore SA1011 // Closing square brackets should be spaced correctly
  {
    await _lock.WaitAsync();

    try
    {
      var context = await _browserContext.Value;
      if (cancellationToken.IsCancellationRequested)
      {
        return null;
      }

      var page = await context.Context.NewPageAsync();
      if (cancellationToken.IsCancellationRequested)
      {
        await page.CloseAsync();
        return null;
      }

      await page.SetContentAsync(
        html, new PageSetContentOptions
        {
          WaitUntil = WaitUntilState.DOMContentLoaded
        });
      if (cancellationToken.IsCancellationRequested)
      {
        await page.CloseAsync();
        return null;
      }

      var pdfBytes = await page.PdfAsync(
        new PagePdfOptions
        {
          Format = "A4",
          PrintBackground = true
        });
      if (cancellationToken.IsCancellationRequested)
      {
        await page.CloseAsync();
        return null;
      }

      await page.CloseAsync();
      return pdfBytes;
    }
    finally
    {
      _lock.Release();
    }
  }

  private sealed record class BrowserContext(
    IPlaywright Playwright,
    IBrowser Browser,
    IBrowserContext Context
  );
}

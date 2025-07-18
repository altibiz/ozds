using Microsoft.Playwright;

namespace Ozds.Server.Test.Containers;

public sealed class PlaywrightBrowser : IComposableService<PlaywrightBrowser>
{
  private readonly IBrowser browser;
  private readonly IPlaywright playwright;

  private IBrowserContext? context;

  private IPage? page;

  private PlaywrightBrowser(
    IPlaywright playwright,
    IBrowser browser
  )
  {
    this.playwright = playwright;
    this.browser = browser;
  }

  public IPage Page
  {
    get
    {
      return page
        ?? throw new InvalidOperationException(
          "Playwright browser not configured"
        );
    }
  }

  public static async Task<PlaywrightBrowser> Create(
    ContainerNetwork network,
    CancellationToken cancellationToken
  )
  {
    var playwright = await Playwright.CreateAsync();
    var browser = await playwright.Chromium.LaunchAsync();

    return new PlaywrightBrowser(playwright, browser);
  }

  public async Task Configure(
    ServiceComposition composition,
    CancellationToken cancellationToken
  )
  {
    context = await browser
      .NewContextAsync(
        new BrowserNewContextOptions
        {
          BaseURL = composition.Ozds.HttpBaseUrl
        });
    page = await context.NewPageAsync();
  }

  public Task Start(CancellationToken cancellationToken)
  {
    return Task.CompletedTask;
  }

  public Task Stop(CancellationToken cancellationToken)
  {
    return Task.CompletedTask;
  }

  public async ValueTask DisposeAsync()
  {
    if (context is not null)
    {
      await context.DisposeAsync();
    }

    await browser.DisposeAsync();
    playwright.Dispose();
  }
}

using Microsoft.Playwright;
using Ozds.Client.Test.Containers;
using TUnit.Core.Enums;

namespace Ozds.Client.Test.Base;

public class OzdsClientTestBase
{
  private ServiceComposition? composition;

  private ServiceComposition Composition
  {
    get
    {
      return composition ?? throw new InvalidOperationException(
        "Container composition not initialized"
      );
    }
  }

  public IPage Page
  {
    get { return Composition.Playwright.Page; }
  }

  public TestUserFixture User
  {
    get { return new TestUserFixture(Composition); }
  }

  public TestLocationFixture Location
  {
    get { return new TestLocationFixture(Composition); }
  }

  public DebugFixture Debug
  {
    get { return new DebugFixture(); }
  }

  [Before(HookType.Test)]
  public async Task SetUp(CancellationToken cancellationToken)
  {
    composition = await ServiceComposition.Create(cancellationToken);
    await composition.Start(cancellationToken);
  }

  [After(HookType.Test)]
  public async Task TearDown(TestContext context, CancellationToken _)
  {
    if (composition is null)
    {
      return;
    }

    if (context.Result?.Status is Status.Failed)
    {
      var testName = context.TestDetails.TestName;
      var testBinDir = Directory.GetCurrentDirectory();
      var screenshotDir = Path.GetFullPath(
        Path.Combine(
          testBinDir, "..", "..", "..", "..", "..",
          "artifacts", "screenshots"));
      var date = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
      var path = Path.Combine(
        screenshotDir,
        $"ozds-client-test-{testName}-failed-at-{date}.png"
      );

      try
      {
        await composition.Playwright.Page.ScreenshotAsync(
          new PageScreenshotOptions { Path = path });
      }
      catch (Exception ex)
      {
        await Console.Error.WriteLineAsync(
          "Failed to take screenshot after failure"
          + $" of {testName} at {path}:{Environment.NewLine}{ex}");
      }
    }

    await composition.Stop(CancellationToken.None);
    await composition.DisposeAsync();
    composition = null;
  }
}

using Microsoft.Playwright;
using Ozds.Server.Test.Containers;
using TUnit.Core.Enums;

namespace Ozds.Server.Test.Base;

public class OzdsServerTestBase
{
  private ServiceComposition? composition;
  private Action<ServiceComposition>? configure;
  private AsyncServiceScope? scope;

  public OzdsServerTestBase(
    Action<ServiceComposition>? configure = null
  )
  {
    this.configure = configure;
  }

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

  public TestNetworkUserFixture NetworkUser
  {
    get { return new TestNetworkUserFixture(Composition); }
  }

  public TestMeasurementLocationFixture MeasurementLocation
  {
    get { return new TestMeasurementLocationFixture(Composition); }
  }

  public TestMeterFixture Meter
  {
    get { return new TestMeterFixture(Composition); }
  }

  public TestMeasurementFixture Measurement
  {
    get { return new TestMeasurementFixture(Composition); }
  }

  public DebugFixture Debug
  {
    get { return new DebugFixture(); }
  }

  public IServiceProvider Services
  {
    get
    {
      scope ??= Composition.Ozds.Services.CreateAsyncScope();
      return scope.Value.ServiceProvider;
    }
  }

  public void Configure(Action<ServiceComposition> configure)
  {
    this.configure = configure;
  }

  [Before(HookType.Test)]
  public async Task SetUp(CancellationToken cancellationToken)
  {
    composition = await ServiceComposition.Create(cancellationToken, configure);
    await composition.Start(cancellationToken);
  }

  [After(HookType.Test)]
  public async Task TearDown(TestContext context, CancellationToken _)
  {
    if (scope is { } scopeValue)
    {
      await scopeValue.DisposeAsync();
      scope = null;
    }

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

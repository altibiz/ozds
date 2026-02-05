using Ozds.Assets.Extensions;
using Ozds.Caching.Extensions;
using Ozds.Time.Extensions;

namespace Ozds.Caching.Test.Base;

public partial class OzdsCachingTestBase
{
  private IHost? host;

  protected async Task Start(CancellationToken cancellationToken)
  {
    if (host is null)
    {
      throw new InvalidOperationException("Host not initialized");
    }

    await host.StartAsync(cancellationToken);
  }

  protected async Task Stop(CancellationToken cancellationToken)
  {
    if (host is null)
    {
      throw new InvalidOperationException("Host not initialized");
    }

    await host.StopAsync(cancellationToken);
  }

  [Before(HookType.Test)]
  public async Task SetUp(CancellationToken cancellationToken)
  {
    var builder = Host.CreateApplicationBuilder();
    builder.Configuration.AddInMemoryCollection(
      new Dictionary<string, string?>
      {
        { "Ozds:Caching:ConnectionString", "memory://" },
      }
    );
    builder.AddOzdsCaching();
    builder.AddOzdsAssets();
    builder.AddOzdsTime();
    host = builder.Build();
    await host.StartAsync(cancellationToken);
  }

  [After(HookType.Test)]
  public async Task TearDown(CancellationToken cancellationToken)
  {
    if (host != null)
    {
      await host.StopAsync(cancellationToken);
      host.Dispose();
      host = null;
    }
  }
}

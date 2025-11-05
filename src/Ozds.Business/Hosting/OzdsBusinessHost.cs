using Ozds.Assets.Extensions;
using Ozds.Business.Extensions;
using Ozds.Caching.Extensions;
using Ozds.Data.Extensions;
using Ozds.Document.Extensions;
using Ozds.Email.Extensions;
using Ozds.Iot.Extensions;
using Ozds.Jobs.Extensions;
using Ozds.Messaging.Extensions;
using Ozds.Report.Extensions;
using Ozds.Time.Extensions;
using Ozds.Users.Extensions;

namespace Ozds.Business.Hosting;

public class OzdsBusinessHost<THostApplicationBuilder, THost> : IHost
  where THostApplicationBuilder : IHostApplicationBuilder
  where THost : IHost
{
  private readonly IHost inner;

  protected bool IsDisposed { get; set; }

  public OzdsBusinessHost(
    THostApplicationBuilder builder,
    Action<THostApplicationBuilder> configure,
    Func<THostApplicationBuilder, THost> build
  )
  {
    configure(builder);

    builder
      .AddOzdsTime()
      .AddOzdsAssets()
      .AddOzdsDocument()
      .AddOzdsReport()
      .AddOzdsUsers()
      .AddOzdsData()
      .AddOzdsMessaging()
      .AddOzdsJobs()
      .AddOzdsEmail()
      .AddOzdsBusiness()
      .AddOzdsIot()
      .AddOzdsCaching();

    inner = build(builder);
  }

  public IServiceProvider Services => inner.Services;

  public Task StartAsync(CancellationToken cancellationToken = default) =>
    inner.StartAsync(cancellationToken);

  public Task StopAsync(CancellationToken cancellationToken = default) =>
    inner.StopAsync(cancellationToken);

  public void Dispose()
  {
    Dispose(true);
    GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing)
  {
    if (IsDisposed)
    {
      return;
    }

    if (disposing)
    {
      inner.Dispose();
    }

    IsDisposed = true;
  }
}

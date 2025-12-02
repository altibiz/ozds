using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using Ozds.Assets.Extensions;
using Ozds.Data.Context;
using Ozds.Data.Extensions;
using Ozds.Data.Options;
using Ozds.Time.Extensions;

namespace Ozds.Data.Test.Containers;

public sealed class OzdsData : IAsyncDisposable
{
  private readonly IHost host;

  private readonly AsyncServiceScope scope;

  public IServiceProvider ServiceProvider => scope.ServiceProvider;

  private OzdsData(
    IHost host,
    AsyncServiceScope scope
  )
  {
    this.host = host;
    this.scope = scope;
  }

  public static async Task<OzdsData> Create(
    PostgresContainer postgresContainer,
    CancellationToken cancellationToken
  )
  {
    var builder = Host.CreateApplicationBuilder();

    builder.Services.AddLogging();
    builder.Services.AddSingleton<IConfiguration>(
      new ConfigurationBuilder().Build());
    builder.Services.AddSingleton<IHostEnvironment>(
      new HostingEnvironment
      {
        EnvironmentName = "Production"
      });

    builder.AddOzdsTime();
    builder.AddOzdsAssets();
    builder.AddOzdsData();
    builder.Services.Configure<OzdsDataOptions>(
      options =>
      {
        options.ConnectionString = postgresContainer.ConnectionString;
        options.UseProxies = false;
        options.WithServices = false;
        options.LogSql = false;
      });

    builder.Services.AddSingleton<MeasurementEntityFactory>();

    var host = builder.Build();

    var scope = host.Services.CreateAsyncScope();

    await using var migrationContext = await scope.ServiceProvider
      .GetRequiredService<IDbContextFactory<DataDbContext>>()
      .CreateDbContextAsync(cancellationToken);
    await migrationContext.Database.MigrateAsync(cancellationToken);

    return new(host, scope);
  }

  public async ValueTask DisposeAsync()
  {
    await scope.DisposeAsync();
    host.Dispose();
  }
}

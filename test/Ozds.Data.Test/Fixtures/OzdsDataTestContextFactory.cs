using System.Runtime.InteropServices;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using Npgsql;
using Ozds.Business.Extensions;
using Ozds.Data.Context;
using Ozds.Data.Extensions;
using Ozds.Data.Options;
using Ozds.Time.Extensions;

namespace Ozds.Data.Test.Fixtures;

public sealed record class OzdsDataTestContext(
  IContainer PostgresContainer,
  IServiceProvider ServiceProvider,
  AsyncServiceScope ServiceScope
) : IAsyncDisposable
{
  public async ValueTask DisposeAsync()
  {
    await PostgresContainer.DisposeAsync();
    await ServiceScope.DisposeAsync();
  }
}

public static class OzdsDataTestContextFactory
{
  private const ushort PostgresqlPort = 5432;

  private const string PostgresDb = "ozds";

  private const string PostgresUser = "ozds";

  private const string PostgresPassword = "ozds";

  private const string PostgresReady =
    ".*listening on IPv4.*";

  public static async Task<OzdsDataTestContext> CreateOzdsDataTestContext(
    CancellationToken cancellationToken
  )
  {
    var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    var wait = isWindows
      ? Wait
        .ForWindowsContainer()
        .UntilMessageIsLogged(PostgresReady)
      : Wait
        .ForUnixContainer()
        .UntilMessageIsLogged(PostgresReady);

    var container = new ContainerBuilder()
      .WithImage("timescale/timescaledb-ha:pg14-latest")
      .WithPortBinding(PostgresqlPort, assignRandomHostPort: true)
      .WithEnvironment("POSTGRES_DB", PostgresDb)
      .WithEnvironment("POSTGRES_USER", PostgresUser)
      .WithEnvironment("POSTGRES_PASSWORD", PostgresPassword)
      .WithWaitStrategy(wait)
      .Build();

    await container.StartAsync(cancellationToken);

    var builder = Host.CreateApplicationBuilder();
    builder.Services.AddLogging();
    builder.Services.AddSingleton<IConfiguration>(
      new ConfigurationBuilder().Build());
    builder.Services.AddSingleton<IHostEnvironment>(
      new HostingEnvironment
      {
        EnvironmentName = "Production"
      });
    builder.Services.AddSingleton<MeasurementEntityFactory>();
    builder.Services.Configure<OzdsDataOptions>(
      options =>
      {
        var connectionString = new NpgsqlConnectionStringBuilder
        {
          Host = container.Hostname,
          Port = container.GetMappedPublicPort(PostgresqlPort),
          Database = PostgresDb,
          Username = PostgresUser,
          Password = PostgresPassword
        };

        options.ConnectionString = connectionString.ToString();
        options.UseProxies = false;
        options.WithServices = false;
        options.LogSql = false;
      });
    builder.AddOzdsTime();
    builder.AddOzdsData();
    builder.AddOzdsBusinessPure();

    var host = builder.Build();

    var scope = host.Services.CreateAsyncScope();

    await using var migrationContext = await scope.ServiceProvider
      .GetRequiredService<IDbContextFactory<DataDbContext>>()
      .CreateDbContextAsync(cancellationToken);
    await migrationContext.Database.MigrateAsync(cancellationToken);

    return new OzdsDataTestContext(container, host.Services, scope);
  }
}

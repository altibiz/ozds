using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.Metrics;
using Microsoft.Extensions.Primitives;
using Ozds.Data.Context;
using Ozds.Data.Extensions;
using Ozds.Data.Test.Fixtures;
using Xunit.DependencyInjection.Logging;

namespace Ozds.Data.Test;

public class Startup
{
  public void ConfigureHost(IHostBuilder hostBuilder)
  {
    hostBuilder.ConfigureAppConfiguration((context, builder) =>
    {
      var root = Directory
        .GetParent(context.HostingEnvironment.ContentRootPath)
        ?.Parent?.Parent?.Parent?.Parent?.Parent?.FullName
        ?? throw new InvalidOperationException("Root is null");
      var server = Path.Join(root, "src", "Ozds.Server");
      var appsettings = Path.Join(server, "appsettings.json");
      var appsettingsDevelopment =
        Path.Join(server, "appsettings.Development.json");

      builder.AddJsonFile(appsettings);
      builder.AddJsonFile(appsettingsDevelopment);
    });
  }

  public void ConfigureServices(
    IServiceCollection services,
    HostBuilderContext context
  )
  {
    var logLevel = context.Configuration
      .GetValue<LogLevel>("Logging:LogLevel:Default");

    services.AddLogging(
      builder =>
        builder.AddXunitOutput(builder =>
        {
          builder.Filter = (_, level) => level >= logLevel;
        })
    );

    var builderProxy = new HostApplicationBuilderProxy(context, services);
    services.AddOzdsData(builderProxy);

    services.AddScoped<DataDbContextFixture>();
  }

  public void Configure(IServiceProvider serviceProvider)
  {
    var factory = serviceProvider
      .GetRequiredService<IDbContextFactory<DataDbContext>>();
    using var context = factory.CreateDbContext();
    context.Database.Migrate();
  }

  private class HostApplicationBuilderProxy(
    HostBuilderContext context,
    IServiceCollection services
  ) : IHostApplicationBuilder
  {
    public IConfigurationManager Configuration { get; } =
      new ConfigurationManagerProxy(context.Configuration);

    public IHostEnvironment Environment => context.HostingEnvironment;

    public ILoggingBuilder Logging => default!;

    public IMetricsBuilder Metrics => default!;

    public IDictionary<object, object> Properties => context.Properties;

    public IServiceCollection Services => services;

    public void ConfigureContainer<TContainerBuilder>(
      IServiceProviderFactory<TContainerBuilder> factory,
      Action<TContainerBuilder>? configure = null
    )
      where TContainerBuilder : notnull
    {
    }
  }

  private class ConfigurationManagerProxy(
    IConfiguration configuration
  ) : IConfigurationManager
  {
    public string? this[string key]
    {
      get => configuration[key];
      set => configuration[key] = value;
    }

    public IDictionary<string, object> Properties => default!;

    public IList<IConfigurationSource> Sources => default!;

    public IConfigurationBuilder Add(IConfigurationSource source)
    {
      return this;
    }

    public IConfigurationRoot Build()
    {
      throw new NotImplementedException();
    }

    public IEnumerable<IConfigurationSection> GetChildren()
    {
      return configuration.GetChildren();
    }

    public IChangeToken GetReloadToken()
    {
      return configuration.GetReloadToken();
    }

    public IConfigurationSection GetSection(string key)
    {
      return configuration.GetSection(key);
    }
  }
}

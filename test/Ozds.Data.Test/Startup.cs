using Microsoft.EntityFrameworkCore;
using Ozds.Business.Extensions;
using Ozds.Data.Context;
using Ozds.Data.Extensions;
using Ozds.Data.Test.Context;
using Xunit.DependencyInjection.Logging;

namespace Ozds.Data.Test;

public class Startup
{
  public void ConfigureHost(IHostBuilder hostBuilder)
  {
    hostBuilder.ConfigureAppConfiguration(
      (context, builder) => { builder.AddJsonFile("appsettings.json"); });
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
        builder.AddXunitOutput(
          builder => { builder.Filter = (_, level) => level >= logLevel; })
    );

    services.AddOzdsData();
    services.AddOzdsBusinessPure();

    services.AddScoped<EphemeralDataDbContextManager>();
    services.AddScoped<DataDbContextManager>();
  }

  public void Configure(IServiceProvider serviceProvider)
  {
    var factory = serviceProvider
      .GetRequiredService<IDbContextFactory<DataDbContext>>();
    using var context = factory.CreateDbContext();
    context.Database.Migrate();
  }
}

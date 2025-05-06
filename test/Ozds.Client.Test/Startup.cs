using Altibiz.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.Playwright;
using Ozds.Client.Test.Options;
using Ozds.Client.Test.Server;
using Xunit.DependencyInjection.Logging;
using IFixture = Ozds.Client.Test.Fixtures.Abstractions.IFixture;

namespace Ozds.Client.Test;

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

    services.AddSingleton(Playwright.CreateAsync().GetAwaiter().GetResult());
    services.AddScoped(
      services => services
        .GetRequiredService<IPlaywright>().Chromium
        .LaunchAsync(new BrowserTypeLaunchOptions { Headless = true })
        .GetAwaiter()
        .GetResult());
    services.AddScoped(
      services =>
      {
        var options = services
          .GetRequiredService<IOptions<OzdsClientTestOptions>>().Value;
        var baseUrl = options.Browser.BaseUri;
        return services.GetRequiredService<IBrowser>()
          .NewContextAsync(
            new BrowserNewContextOptions
            {
              BaseURL = baseUrl
            })
          .GetAwaiter()
          .GetResult();
      });
    services.AddScoped(
      services => services
        .GetRequiredService<IBrowserContext>()
        .NewPageAsync()
        .GetAwaiter()
        .GetResult());
    services.ConfigureOptions<OzdsClientTestConfigureOptions>();
    services.AddHostedService<ServerManager>();
    services.AddHttpClient();

    services.AddScopedAssignableTo<IFixture>();
  }
}

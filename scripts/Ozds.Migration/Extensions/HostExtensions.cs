using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.FileProviders;
using Ozds.Migration.Arguments;
using Ozds.Migration.Options;
using Ozds.Migration.Services;

namespace Ozds.Migration.Extensions;

public static class HostExtensions
{
  public static IHostApplicationBuilder AddOzdsMigration(
    this IHostApplicationBuilder builder,
    IOzdsMigrationArguments arguments
  )
  {
    builder.Services.AddSingleton(arguments.GetType(), arguments);
    builder.AddOptions();

    if (arguments is OzdsMigrationMigrateArguments)
    {
      builder.Services.AddHostedService<MigrateHostedService>();
    }

    if (arguments is OzdsMigrationGenerateArguments)
    {
      builder.Services.AddHostedService<GenerateHostedService>();
    }

    return builder;
  }

  private static IHostApplicationBuilder AddOptions(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.ConfigureOptions<ConfigureOzdsMigrationOptions>();
    var relativeServerSettings = builder.Configuration
      .GetSection("Ozds:Migration:ServerSettings")
      .Get<string>();
    if (relativeServerSettings is not null)
    {
      var serverSettings = Path.GetFullPath(
        relativeServerSettings,
        builder.Environment.ContentRootPath);
      var directory = Path.GetDirectoryName(serverSettings)
        ?? throw new InvalidOperationException(
          "ServerSettings must be a file path");
      var file = Path.GetFileName(serverSettings);
      var source = new JsonConfigurationSource
      {
        FileProvider = new PhysicalFileProvider(directory),
        Path = file
      };
      builder.Configuration.Sources.Insert(0, source);
    }

    return builder;
  }
}

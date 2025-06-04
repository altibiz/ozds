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
      var serverSettings = Path.Combine(
        builder.Environment.ContentRootPath,
        relativeServerSettings);
      builder.Configuration.AddJsonFile(serverSettings);
    }

    return builder;
  }
}

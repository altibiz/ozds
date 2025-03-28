using Ozds.Migration.Arguments;
using Ozds.Migration.Options;
using Ozds.Migration.Services;

namespace Ozds.Migration.Extensions;

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddOzdsMigration(
    this IServiceCollection services,
    object arguments
  )
  {
    services.AddSingleton(arguments.GetType(), arguments);
    services.AddOptions();

    return arguments switch
    {
      OzdsMigrationMigrateArguments => services
        .AddHostedService<MigrateHostedService>(),
      OzdsMigrationGenerateArguments => services
        .AddHostedService<GenerateHostedService>(),
      _ => throw new InvalidOperationException($"Unknown options: {arguments}")
    };
  }

  private static IServiceCollection AddOptions(
    this IServiceCollection services
  )
  {
    services.ConfigureOptions<ConfigureOzdsMigrationOptions>();
    services.ConfigureOptions<ConfigureOzdsMessagingOptions>();
    services.ConfigureOptions<ConfigureOzdsDataOptions>();
    services.ConfigureOptions<ConfigureOzdsJobsOptions>();
    return services;
  }
}

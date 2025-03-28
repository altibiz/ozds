using Altibiz.DependencyInjection.Extensions;
using MassTransit;
using Microsoft.Extensions.Options;
using Ozds.Fake.Arguments;
using Ozds.Fake.Client;
using Ozds.Fake.Cloners;
using Ozds.Fake.Cloners.Abstractions;
using Ozds.Fake.Conversion;
using Ozds.Fake.Conversion.Abstractions;
using Ozds.Fake.Correction;
using Ozds.Fake.Correction.Abstractions;
using Ozds.Fake.Generators;
using Ozds.Fake.Generators.Abstractions;
using Ozds.Fake.Loaders;
using Ozds.Fake.Loaders.Abstractions;
using Ozds.Fake.Options;
using Ozds.Fake.Packing;
using Ozds.Fake.Packing.Abstractions;
using Ozds.Fake.Services;
using Ozds.Fake.Workers.Abstractions;
using Ozds.Messaging.Context;

// TODO: remove dependency on Ozds.Messaging here

namespace Ozds.Fake.Extensions;

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddOzdsFake(
    this IServiceCollection services,
    object arguments
  )
  {
    services = services
      .AddSingleton(arguments.GetType(), arguments)
      .AddRecords()
      .AddLoaders()
      .AddGenerators()
      .AddCloners()
      .AddPackers()
      .AddWorkers()
      .AddOptions();

    return arguments switch
    {
      OzdsFakePushArguments push => services
        .AddClient(push.Timeout_s)
        .AddHostedService<PushService>(),
      OzdsFakeSeedArguments seed => services
        .AddClient(seed.Timeout_s)
        .AddHostedService<SeedService>(),
      OzdsFakeInsertArguments insert => services
        .AddClient(insert.Timeout_s)
        .AddHostedService<InsertService>(),
      OzdsFakeAltibizArguments => services
        .AddMessaging(),
      _ => throw new InvalidOperationException($"Unknown options: {arguments}")
    };
  }

  private static IServiceCollection AddOptions(
    this IServiceCollection services
  )
  {
    services.ConfigureOptions<ConfigureOzdsFakeOptions>();
    return services;
  }

  private static IServiceCollection AddGenerators(
    this IServiceCollection services
  )
  {
    services.AddTransientAssignableTo(typeof(IMeasurementRecordGenerator));
    services.AddSingleton(typeof(MeasurementRecordGenerator));

    return services;
  }

  private static IServiceCollection AddCloners(
    this IServiceCollection services
  )
  {
    services.AddTransientAssignableTo(typeof(IMeasurementCloner));
    services.AddSingleton(typeof(MeasurementCloner));
    return services;
  }

  private static IServiceCollection AddLoaders(
    this IServiceCollection services
  )
  {
    services.AddTransientAssignableTo(typeof(ILoader));
    services.AddSingleton(typeof(ResourceCache));
    return services;
  }

  private static IServiceCollection AddRecords(
    this IServiceCollection services
  )
  {
    services.AddTransientAssignableTo(
      typeof(IMeasurementRecordPushRequestConverter));
    services.AddTransientAssignableTo(
      typeof(IMeasurementRecordModelConverter));
    services.AddSingleton(
      typeof(MeasurementRecordConverter));
    services.AddTransientAssignableTo(
      typeof(IRecordCorrector));
    services.AddSingleton(typeof(RecordCorrector));
    return services;
  }

  private static IServiceCollection AddPackers(
    this IServiceCollection services
  )
  {
    services.AddTransientAssignableTo(typeof(IMessengerPushRequestPacker));
    services.AddSingleton(typeof(MessengerPushRequestPacker));
    return services;
  }

  private static IServiceCollection AddWorkers(
    this IServiceCollection services
  )
  {
    services.AddScopedAssignableTo(typeof(IWorker));
    return services;
  }

  private static IServiceCollection AddClient(
    this IServiceCollection services,
    int timeout_s
  )
  {
    services.AddHttpClient(
      PushClient.Name,
      (services, options) =>
      {
        var clientOptions = services
          .GetRequiredService<IOptions<OzdsFakeOptions>>().Value.Client;

        options.Timeout = TimeSpan.FromSeconds(timeout_s);
        options.BaseAddress = new Uri(clientOptions.BaseUrl);
        options.DefaultRequestHeaders.Add(
          "X-Api-Key", clientOptions.ApiKey);
      });
    services.AddScoped(typeof(PushClient));
    services.AddScoped(typeof(InsertClient));
    return services;
  }

  private static IServiceCollection AddMessaging(
    this IServiceCollection services
  )
  {
    services.AddMassTransit(
      x =>
      {
        var fakeAssembly = typeof(IServiceCollectionExtensions).Assembly;
        var messagingAssembly = typeof(MessagingDbContext).Assembly;

        x.SetKebabCaseEndpointNameFormatter();

        x.AddConsumers(fakeAssembly);
        x.AddSagaStateMachines(fakeAssembly);
        x.AddActivities(fakeAssembly);

        x.AddSagas(messagingAssembly);
        x.SetInMemorySagaRepositoryProvider();

        x.UsingRabbitMq(
          (context, cfg) =>
          {
            var messagingOptions = context
              .GetRequiredService<IOptions<OzdsFakeOptions>>().Value.Messaging;

            var connectionStringDictionary = messagingOptions.ConnectionString
              .Split(';')
              .ToDictionary(x => x.Split('=')[0], x => x.Split('=')[1]);
            var host = connectionStringDictionary["Host"];
            var virtualHost = connectionStringDictionary["VirtualHost"];
            var username = connectionStringDictionary["Username"];
            var password = connectionStringDictionary["Password"];

            cfg.Host(
              host, virtualHost, cfg =>
              {
                cfg.Username(username);
                cfg.Password(password);
              });
            cfg.ConfigureEndpoints(context);
          });
      });

    return services;
  }
}

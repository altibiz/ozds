using Altibiz.DependencyInjection.Extensions;
using MassTransit;
using Ozds.Business.Conversion;
using Ozds.Business.Conversion.Abstractions;
using Ozds.Fake.Client;
using Ozds.Fake.Conversion.Abstractions;
using Ozds.Fake.Conversion.Agnostic;
using Ozds.Fake.Correction.Abstractions;
using Ozds.Fake.Correction.Agnostic;
using Ozds.Fake.Generators.Abstractions;
using Ozds.Fake.Generators.Agnostic;
using Ozds.Fake.Loaders;
using Ozds.Fake.Loaders.Abstractions;
using Ozds.Fake.Packing.Abstractions;
using Ozds.Fake.Packing.Agnostic;
using Ozds.Messaging.Context;

namespace Ozds.Fake.Extensions;

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddGenerators(
    this IServiceCollection services)
  {
    services.AddTransientAssignableTo(typeof(IMeasurementGenerator));
    services.AddSingleton(typeof(AgnosticMeasurementGenerator));

    services.AddTransientAssignableTo(typeof(IPushRequestMeasurementConverter));
    services.AddSingleton(typeof(PushRequestMeasurementConverter));

    services.AddTransientAssignableTo(typeof(IModelEntityConverter));
    services.AddSingleton(typeof(ModelEntityConverter));

    return services;
  }

  public static IServiceCollection AddLoaders(this IServiceCollection services)
  {
    services.AddTransientAssignableTo(typeof(ILoader));
    services.AddSingleton(typeof(ResourceCache));
    return services;
  }

  public static IServiceCollection AddRecords(this IServiceCollection services)
  {
    services.AddTransientAssignableTo(
      typeof(IMeasurementRecordPushRequestConverter));
    services.AddSingleton(
      typeof(AgnosticMeasurementRecordPushRequestConverter));
    services.AddTransientAssignableTo(
      typeof(ICorrector));
    services.AddSingleton(typeof(AgnosticCorrector));
    return services;
  }

  public static IServiceCollection AddClient(
    this IServiceCollection services,
    int timeout,
    string baseUrl
  )
  {
    services.AddHttpClient(
      "Ozds.Fake", options =>
      {
        options.Timeout = TimeSpan.FromSeconds(timeout);
        options.BaseAddress = new Uri(baseUrl);
      });
    services.AddScoped(typeof(OzdsPushClient));
    return services;
  }

  public static IServiceCollection AddPackers(this IServiceCollection services)
  {
    services.AddTransientAssignableTo(typeof(IMessengerPushRequestPacker));
    services.AddSingleton(typeof(AgnosticMessengerPushRequestPacker));
    return services;
  }

  public static IServiceCollection AddMessaging(
    this IServiceCollection services,
    string host,
    string virtualHost,
    string username,
    string password
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

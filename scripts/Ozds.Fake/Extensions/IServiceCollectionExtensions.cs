using System.Reflection;
using MassTransit;
using Ozds.Business.Conversion.Abstractions;
using Ozds.Business.Conversion.Agnostic;
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

    services.AddTransientAssignableTo(
      typeof(IPushRequestMeasurementConverter),
      typeof(IPushRequestMeasurementConverter).Assembly
    );
    services.AddSingleton(typeof(AgnosticPushRequestMeasurementConverter));

    services.AddTransientAssignableTo(
      typeof(IModelEntityConverter),
      typeof(IModelEntityConverter).Assembly
    );
    services.AddSingleton(typeof(AgnosticModelEntityConverter));

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

  private static void AddTransientAssignableTo(
    this IServiceCollection services,
    Type assignableTo,
    Assembly? assembly = null
  )
  {
    var conversionTypes =
      (assembly ?? typeof(IServiceCollectionExtensions).Assembly)
      .GetTypes()
      .Where(
        type =>
          !type.IsAbstract &&
          type.IsClass &&
          type.IsAssignableTo(assignableTo));

    foreach (var conversionType in conversionTypes)
    {
      services.AddTransient(conversionType);
      foreach (var interfaceType in conversionType.GetAllInterfaces())
      {
        services.AddTransient(
          interfaceType, services =>
            services.GetRequiredService(conversionType));
      }
    }
  }

  private static Type[] GetAllInterfaces(this Type type)
  {
    return type.GetInterfaces()
      .Concat(type.GetInterfaces().SelectMany(GetAllInterfaces))
      .Concat(type.BaseType?.GetAllInterfaces() ?? Array.Empty<Type>())
      .Distinct()
      .ToArray();
  }
}

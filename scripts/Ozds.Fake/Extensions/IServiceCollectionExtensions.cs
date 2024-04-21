using System.Reflection;
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

// TODO: figure out how to discover generic types nicely

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
    services.AddTransient(typeof(CsvLoader<>));
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
      typeof(ICumulativeCorrector));
    services.AddSingleton(typeof(AgnosticCumulativeCorrector));
    return services;
  }

  public static IServiceCollection AddClient(
    this IServiceCollection services,
    int timeout,
    string baseUrl
  )
  {
    services.AddHttpClient("Ozds.Fake", options =>
    {
      options.Timeout = TimeSpan.FromSeconds(timeout);
      options.BaseAddress = new Uri(baseUrl);
    });
    services.AddScoped(typeof(OzdsPushClient));
    return services;
  }

  private static void AddTransientAssignableTo(
    this IServiceCollection services,
    Type assignableTo,
    Assembly? assembly = null
  )
  {
    assembly ??= typeof(IServiceCollectionExtensions).Assembly;
    var conversionTypes = assembly
      .GetTypes()
      .Where(type =>
        !type.IsAbstract &&
        !type.IsGenericType &&
        type.IsClass &&
        type.IsAssignableTo(assignableTo));

    foreach (var conversionType in conversionTypes)
    {
      services.AddTransient(assignableTo, conversionType);
      services.AddTransient(conversionType);
    }
  }
}

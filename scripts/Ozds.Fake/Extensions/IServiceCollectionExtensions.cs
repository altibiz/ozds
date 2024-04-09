using System.Reflection;
using Ozds.Business.Conversion.Abstractions;
using Ozds.Data.Timescale;
using Ozds.Fake.Client;
using Ozds.Fake.Generators.Abstractions;
using Ozds.Fake.Loaders;

// TODO: figure out how to discover generic types

namespace Ozds.Fake.Extensions;

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddGenerators(
    this IServiceCollection services)
  {
    services.AddScopedAssignableTo(typeof(IMeasurementGenerator));
    services.AddTransientAssignableTo(
      typeof(IPushRequestMeasurementConverter),
      typeof(IPushRequestMeasurementConverter).Assembly
    );
    services.AddTransientAssignableTo(
      typeof(IModelEntityConverter),
      typeof(IModelEntityConverter).Assembly
    );
    return services;
  }

  public static IServiceCollection AddLoaders(this IServiceCollection services)
  {
    services.AddTransient(typeof(CsvLoader<>));
    services.AddSingleton(typeof(ResourceCache));
    return services;
  }

  public static IServiceCollection AddClient(
    this IServiceCollection services,
    int timeout = 3
  )
  {
    services.AddHttpClient("Ozds.Fake", options => options.Timeout = TimeSpan.FromSeconds(timeout));
    services.AddScoped(typeof(OzdsPushClient));
    return services;
  }

  private static void AddScopedAssignableTo(
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
      services.AddScoped(assignableTo, conversionType);
      services.AddScoped(conversionType);
    }
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

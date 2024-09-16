using Ozds.Iot.Observers.Abstractions;

namespace Ozds.Iot.Extensions;

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddOzdsIot(
    this IServiceCollection services,
    IHostApplicationBuilder builder
  )
  {
    // Observers
    services.AddSingletonAssignableTo(typeof(IPublisher));
    services.AddSingletonAssignableTo(typeof(ISubscriber));

    return services;
  }

  private static void AddSingletonAssignableTo(
    this IServiceCollection services,
    Type assignableTo
  )
  {
    var conversionTypes = typeof(IServiceCollectionExtensions).Assembly
      .GetTypes()
      .Where(
        type =>
          !type.IsAbstract &&
          type.IsClass &&
          type.IsAssignableTo(assignableTo));

    foreach (var conversionType in conversionTypes)
    {
      services.AddSingleton(conversionType);
      foreach (var interfaceType in conversionType.GetAllInterfaces())
      {
        services.AddSingleton(
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

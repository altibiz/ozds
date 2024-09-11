using Ozds.Business.Activation.Abstractions;
using Ozds.Business.Activation.Agnostic;
using Ozds.Business.Aggregation;
using Ozds.Business.Aggregation.Abstractions;
using Ozds.Business.Aggregation.Agnostic;
using Ozds.Business.Conversion.Abstractions;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Finance;
using Ozds.Business.Finance.Abstractions;
using Ozds.Business.Finance.Agnostic;
using Ozds.Business.Iot;
using Ozds.Business.Localization;
using Ozds.Business.Localization.Abstractions;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Business.Naming.Abstractions;
using Ozds.Business.Naming.Agnostic;
using Ozds.Business.Queries.Abstractions;

namespace Ozds.Business.Extensions;

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddOzdsBusiness(
    this IServiceCollection services,
    IHostApplicationBuilder builder
  )
  {
    // Activation
    services.AddTransientAssignableTo(typeof(IModelActivator));
    services.AddSingleton(typeof(AgnosticModelActivator));

    services.AddScopedAssignableTo(typeof(IOzdsQueries));
    services.AddScopedAssignableTo(typeof(IOzdsMutations));

    services.AddTransientAssignableTo(typeof(IAggregateUpserter));
    services.AddSingleton(typeof(AgnosticAggregateUpserter));

    services.AddTransientAssignableTo(typeof(IModelEntityConverter));
    services.AddSingleton(typeof(AgnosticModelEntityConverter));
    services.AddTransientAssignableTo(typeof(IMeasurementAggregateConverter));
    services.AddSingleton(typeof(AgnosticMeasurementAggregateConverter));
    services.AddTransientAssignableTo(typeof(IPushRequestMeasurementConverter));
    services.AddSingleton(typeof(AgnosticPushRequestMeasurementConverter));
    services.AddTransientAssignableTo(typeof(IMeterNamingConvention));
    services.AddSingleton(typeof(AgnosticMeterNamingConvention));

    services.AddTransientAssignableTo(
      typeof(INetworkUserCalculationCalculator));
    services.AddSingleton(typeof(AgnosticNetworkUserCalculationCalculator));
    services.AddTransientAssignableTo(typeof(ICalculationItemCalculator));
    services.AddSingleton(typeof(AgnosticCalculationItemCalculator));
    services.AddTransient(
      typeof(INetworkUserInvoiceCalculator),
      typeof(NetworkUserInvoiceCalculator));
    services.AddScoped(
      typeof(INetworkUserInvoiceIssuer), typeof(NetworkUserInvoiceIssuer));

    services.AddScoped(typeof(OzdsIotHandler));
    services.AddScoped(typeof(BatchAggregatedMeasurementUpserter));

    services.AddSingleton(typeof(IOzdsLocalizer), typeof(OzdsLocalizer));

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
          !type.IsGenericType &&
          type.IsClass &&
          type.IsAssignableTo(assignableTo));

    foreach (var conversionType in conversionTypes)
    {
      services.AddSingleton(conversionType);
      foreach (var interfaceType in conversionType.GetInterfaces())
      {
        services.AddSingleton(
          interfaceType, services =>
            services.GetRequiredService(conversionType));
      }
    }
  }

  private static void AddScopedAssignableTo(
    this IServiceCollection services,
    Type assignableTo
  )
  {
    var conversionTypes = typeof(IServiceCollectionExtensions).Assembly
      .GetTypes()
      .Where(
        type =>
          !type.IsAbstract &&
          !type.IsGenericType &&
          type.IsClass &&
          type.IsAssignableTo(assignableTo));

    foreach (var conversionType in conversionTypes)
    {
      services.AddScoped(conversionType);
      foreach (var interfaceType in conversionType.GetAllInterfaces())
      {
        services.AddScoped(
          interfaceType, services =>
            services.GetRequiredService(conversionType));
      }
    }
  }

  private static void AddTransientAssignableTo(
    this IServiceCollection services,
    Type assignableTo
  )
  {
    var conversionTypes = typeof(IServiceCollectionExtensions).Assembly
      .GetTypes()
      .Where(
        type =>
          !type.IsAbstract &&
          !type.IsGenericType &&
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
      .ToHashSet()
      .ToArray();
  }
}

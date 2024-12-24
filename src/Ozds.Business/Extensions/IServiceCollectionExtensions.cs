using Ozds.Business.Activation.Abstractions;
using Ozds.Business.Activation.Agnostic;
using Ozds.Business.Aggregation.Abstractions;
using Ozds.Business.Aggregation.Agnostic;
using Ozds.Business.Caching.Abstractions;
using Ozds.Business.Conversion.Abstractions;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Finance;
using Ozds.Business.Finance.Abstractions;
using Ozds.Business.Finance.Agnostic;
using Ozds.Business.Localization;
using Ozds.Business.Localization.Abstractions;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Business.Naming.Abstractions;
using Ozds.Business.Naming.Agnostic;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Queries.Abstractions;
using Ozds.Business.Reactors.Abstractions;
using Ozds.Business.Validation.Abstractions;
using Ozds.Business.Validation.Agnostic;

namespace Ozds.Business.Extensions;

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddOzdsBusiness(
    this IServiceCollection services,
    IHostApplicationBuilder builder
  )
  {
    services.AddActivation();
    services.AddAggregation();
    services.AddConversion();
    services.AddFinance();
    services.AddLocalization();
    services.AddMutations();
    services.AddNaming();
    services.AddObservers();
    services.AddQueries();
    services.AddValidation();
    services.AddReactors();
    services.AddCaching();
    return services;
  }

  private static IServiceCollection AddActivation(
    this IServiceCollection services
  )
  {
    services.AddTransientAssignableTo(typeof(IModelActivator));
    services.AddSingleton(typeof(AgnosticModelActivator));
    return services;
  }

  private static IServiceCollection AddAggregation(
    this IServiceCollection services
  )
  {
    services.AddTransientAssignableTo(typeof(IAggregateUpserter));
    services.AddSingleton(typeof(AgnosticAggregateUpserter));
    return services;
  }

  private static IServiceCollection AddConversion(
    this IServiceCollection services
  )
  {
    services.AddTransientAssignableTo(typeof(IModelEntityConverter));
    services.AddSingleton(typeof(AgnosticModelEntityConverter));
    services.AddTransientAssignableTo(typeof(IMeasurementAggregateConverter));
    services.AddSingleton(typeof(AgnosticMeasurementAggregateConverter));
    services.AddTransientAssignableTo(typeof(IPushRequestMeasurementConverter));
    services.AddSingleton(typeof(AgnosticPushRequestMeasurementConverter));
    return services;
  }

  private static IServiceCollection AddFinance(
    this IServiceCollection services
  )
  {
    services.AddTransientAssignableTo(
      typeof(INetworkUserCalculationCalculator));
    services.AddSingleton(typeof(AgnosticNetworkUserCalculationCalculator));
    services.AddTransientAssignableTo(typeof(ICalculationItemCalculator));
    services.AddSingleton(typeof(AgnosticCalculationItemCalculator));
    services.AddTransient(
      typeof(INetworkUserInvoiceCalculator),
      typeof(NetworkUserInvoiceCalculator));
    services.AddTransient(
      typeof(INetworkUserInvoiceIssuer), typeof(NetworkUserInvoiceIssuer));
    return services;
  }

  private static IServiceCollection AddLocalization(
    this IServiceCollection services
  )
  {
    services.AddSingleton(typeof(ILocalizer), typeof(Localizer));
    return services;
  }

  private static IServiceCollection AddMutations(
    this IServiceCollection services
  )
  {
    services.AddScopedAssignableTo(typeof(IMutations));
    return services;
  }

  private static IServiceCollection AddNaming(
    this IServiceCollection services
  )
  {
    services.AddTransientAssignableTo(typeof(IMeterNamingConvention));
    services.AddSingleton(typeof(AgnosticMeterNamingConvention));
    return services;
  }

  private static IServiceCollection AddObservers(
    this IServiceCollection services
  )
  {
    services.AddSingletonAssignableTo(typeof(IPublisher));
    services.AddSingletonAssignableTo(typeof(ISubscriber));
    return services;
  }

  private static IServiceCollection AddQueries(
    this IServiceCollection services
  )
  {
    services.AddScopedAssignableTo(typeof(IQueries));
    return services;
  }

  private static IServiceCollection AddValidation(
    this IServiceCollection services
  )
  {
    services.AddTransientAssignableTo(typeof(IValidator));
    services.AddSingleton(typeof(AgnosticValidator));
    return services;
  }

  private static IServiceCollection AddReactors(
    this IServiceCollection services
  )
  {
    services.AddSingletonAssignableTo(typeof(IReactor));
    return services;
  }

  private static IServiceCollection AddCaching(
    this IServiceCollection services
  )
  {
    services.AddSingletonAssignableTo(typeof(ICache));
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

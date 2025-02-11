using Altibiz.DependencyInjection.Extensions;
using Ozds.Business.Activation;
using Ozds.Business.Activation.Abstractions;
using Ozds.Business.Aggregation;
using Ozds.Business.Aggregation.Abstractions;
using Ozds.Business.Buffers.Abstractions;
using Ozds.Business.Caching.Abstractions;
using Ozds.Business.Conversion;
using Ozds.Business.Conversion.Abstractions;
using Ozds.Business.Finance;
using Ozds.Business.Finance.Abstractions;
using Ozds.Business.Finance.Implementations;
using Ozds.Business.Localization.Abstractions;
using Ozds.Business.Localization.Implementations;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Business.Naming;
using Ozds.Business.Naming.Abstractions;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Queries.Abstractions;
using Ozds.Business.Reactors.Abstractions;
using Ozds.Business.Validation;
using Ozds.Business.Validation.Abstractions;

namespace Ozds.Business.Extensions;

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddOzdsBusiness(
    this IServiceCollection services,
    IHostApplicationBuilder builder
  )
  {
    services.AddOzdsActivation();
    services.AddAggregation();
    services.AddOzdsConversion();
    services.AddFinance();
    services.AddLocalization();
    services.AddMutations();
    services.AddNaming();
    services.AddObservers();
    services.AddQueries();
    services.AddValidation();
    services.AddReactors();
    services.AddCaching();
    services.AddBuffers();
    return services;
  }

  public static IServiceCollection AddOzdsActivation(
    this IServiceCollection services
  )
  {
    services.AddTransientAssignableTo(typeof(IModelActivator));
    services.AddSingleton(typeof(ModelActivator));
    return services;
  }

  public static IServiceCollection AddOzdsConversion(
    this IServiceCollection services
  )
  {
    services.AddTransientAssignableTo(typeof(IModelEntityConverter));
    services.AddSingleton(typeof(ModelEntityConverter));
    services.AddTransientAssignableTo(typeof(IMeasurementAggregateConverter));
    services.AddSingleton(typeof(MeasurementAggregateConverter));
    services.AddTransientAssignableTo(typeof(IPushRequestMeasurementConverter));
    services.AddSingleton(typeof(PushRequestMeasurementConverter));
    return services;
  }

  private static IServiceCollection AddAggregation(
    this IServiceCollection services
  )
  {
    services.AddTransientAssignableTo(typeof(IAggregateUpserter));
    services.AddSingleton(typeof(AggregateUpserter));
    return services;
  }

  private static IServiceCollection AddFinance(
    this IServiceCollection services
  )
  {
    services.AddTransientAssignableTo(
      typeof(INetworkUserCalculationCalculator));
    services.AddSingleton(typeof(NetworkUserCalculationCalculator));
    services.AddTransientAssignableTo(typeof(ICalculationItemCalculator));
    services.AddSingleton(typeof(CalculationItemCalculator));
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
    services.AddSingleton(typeof(MeterNamingConvention));
    return services;
  }

  private static IServiceCollection AddObservers(
    this IServiceCollection services
  )
  {
    services.AddSingletonAssignableTo(typeof(IPublisher));
    services.AddSingletonAssignableTo(typeof(ISubscriber));
    services.AddSingletonAssignableTo(typeof(IPipe));
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
    services.AddSingleton(typeof(ModelValidator));
    return services;
  }

  private static IServiceCollection AddReactors(
    this IServiceCollection services
  )
  {
    services.AddSingletonAssignableTo(typeof(IReactor));
    services.AddScopedAssignableTo(typeof(IHandler));
    return services;
  }

  private static IServiceCollection AddCaching(
    this IServiceCollection services
  )
  {
    services.AddSingletonAssignableTo(typeof(ICache));
    return services;
  }

  private static IServiceCollection AddBuffers(
    this IServiceCollection services
  )
  {
    services.AddSingletonAssignableTo(typeof(IBuffer));
    return services;
  }
}

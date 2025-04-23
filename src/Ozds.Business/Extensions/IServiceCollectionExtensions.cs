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
using Ozds.Business.Mutations.Abstractions;
using Ozds.Business.Naming;
using Ozds.Business.Naming.Abstractions;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Queries.Abstractions;
using Ozds.Business.Reactors.Abstractions;
using Ozds.Business.Services;
using Ozds.Business.Validation;
using Ozds.Business.Validation.Abstractions;

namespace Ozds.Business.Extensions;

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddOzdsBusiness(
    this IServiceCollection services
  )
  {
    services.AddOzdsBusinessPure();
    services.AddObservers();
    services.AddReactors();
    services.AddServices();
    services.AddCaching();
    services.AddBuffers();
    services.AddMutations();
    services.AddQueries();
    services.AddValidation();
    return services;
  }

  public static IServiceCollection AddOzdsBusinessPure(
    this IServiceCollection services
  )
  {
    services.AddActivation();
    services.AddAggregation();
    services.AddConversion();
    services.AddFinance();
    services.AddLocalization();
    services.AddNaming();
    return services;
  }

  private static IServiceCollection AddActivation(
    this IServiceCollection services
  )
  {
    services.AddTransientAssignableTo(typeof(IModelActivator));
    services.AddSingleton(typeof(ModelActivator));
    return services;
  }

  private static IServiceCollection AddConversion(
    this IServiceCollection services
  )
  {
    services.AddTransientAssignableTo(typeof(IModelEntityConverter));
    services.AddSingleton(typeof(ModelEntityConverter));
    services.AddTransientAssignableTo(typeof(IModelDocumentEntityConverter));
    services.AddSingleton(typeof(ModelDocumentEntityConverter));
    services.AddTransientAssignableTo(typeof(IModelReportEntityConverter));
    services.AddSingleton(typeof(ModelReportEntityConverter));
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
    return services;
  }

  private static IServiceCollection AddMutations(
    this IServiceCollection services
  )
  {
    services.AddScopedAssignableTo(typeof(IMutations));
    return services;
  }

  private static IServiceCollection AddServices(
    this IServiceCollection services
  )
  {
    services.AddHostedService<MigrationService>();
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
    services.AddSingleton(typeof(HtmlSanitizer));
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

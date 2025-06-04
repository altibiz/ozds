using Altibiz.DependencyInjection.Extensions;
using Ozds.Business.Activation;
using Ozds.Business.Activation.Abstractions;
using Ozds.Business.Aggregation;
using Ozds.Business.Aggregation.Abstractions;
using Ozds.Business.Analysis;
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
using Ozds.Business.Validation;
using Ozds.Business.Validation.Abstractions;

namespace Ozds.Business.Extensions;

public static class HostExtensions
{
  public static IHostApplicationBuilder AddOzdsBusiness(
    this IHostApplicationBuilder builder
  )
  {
    builder.AddOzdsBusinessPure();
    builder.AddAnalysis();
    builder.AddObservers();
    builder.AddReactors();
    builder.AddCaching();
    builder.AddBuffers();
    builder.AddMutations();
    builder.AddQueries();
    builder.AddValidation();
    return builder;
  }

  public static IHostApplicationBuilder AddOzdsBusinessPure(
    this IHostApplicationBuilder builder
  )
  {
    builder.AddActivation();
    builder.AddAggregation();
    builder.AddConversion();
    builder.AddFinance();
    builder.AddNaming();
    return builder;
  }

  private static IHostApplicationBuilder AddAnalysis(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddTransient<Analyzer>();
    return builder;
  }

  private static IHostApplicationBuilder AddActivation(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddTransientAssignableTo(typeof(IModelActivator));
    builder.Services.AddSingleton(typeof(ModelActivator));
    return builder;
  }

  private static IHostApplicationBuilder AddConversion(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddTransientAssignableTo(typeof(IModelEntityConverter));
    builder.Services.AddSingleton(typeof(ModelEntityConverter));
    builder.Services.AddTransientAssignableTo(
      typeof(IModelDocumentEntityConverter));
    builder.Services.AddSingleton(typeof(ModelDocumentEntityConverter));
    builder.Services.AddTransientAssignableTo(
      typeof(IModelReportEntityConverter));
    builder.Services.AddSingleton(typeof(ModelReportEntityConverter));
    builder.Services.AddTransientAssignableTo(
      typeof(IMeasurementAggregateConverter));
    builder.Services.AddSingleton(typeof(MeasurementAggregateConverter));
    builder.Services.AddTransientAssignableTo(
      typeof(IPushRequestMeasurementConverter));
    builder.Services.AddSingleton(typeof(PushRequestMeasurementConverter));
    return builder;
  }

  private static IHostApplicationBuilder AddAggregation(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddTransientAssignableTo(typeof(IAggregateUpserter));
    builder.Services.AddSingleton(typeof(AggregateUpserter));
    return builder;
  }

  private static IHostApplicationBuilder AddFinance(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddTransientAssignableTo(
      typeof(INetworkUserCalculationCalculator));
    builder.Services.AddSingleton(typeof(NetworkUserCalculationCalculator));
    builder.Services.AddTransientAssignableTo(
      typeof(ICalculationItemCalculator));
    builder.Services.AddSingleton(typeof(CalculationItemCalculator));
    builder.Services.AddTransient(
      typeof(INetworkUserInvoiceCalculator),
      typeof(NetworkUserInvoiceCalculator));
    return builder;
  }

  private static IHostApplicationBuilder AddMutations(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddScopedAssignableTo(typeof(IMutations));
    return builder;
  }

  private static IHostApplicationBuilder AddNaming(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddTransientAssignableTo(typeof(IMeterNamingConvention));
    builder.Services.AddSingleton(typeof(MeterNamingConvention));
    return builder;
  }

  private static IHostApplicationBuilder AddObservers(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddSingletonAssignableTo(typeof(IPublisher));
    builder.Services.AddSingletonAssignableTo(typeof(ISubscriber));
    builder.Services.AddSingletonAssignableTo(typeof(IPipe));
    return builder;
  }

  private static IHostApplicationBuilder AddQueries(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddScopedAssignableTo(typeof(IQueries));
    builder.Services.AddSingletonAssignableTo(typeof(ISingletonQueries));
    return builder;
  }

  private static IHostApplicationBuilder AddValidation(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddTransientAssignableTo(typeof(IValidator));
    builder.Services.AddSingleton(typeof(ModelValidator));
    builder.Services.AddSingleton(typeof(HtmlSanitizer));
    return builder;
  }

  private static IHostApplicationBuilder AddReactors(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddSingletonAssignableTo(typeof(IReactor));
    builder.Services.AddScopedAssignableTo(typeof(IHandler));
    return builder;
  }

  private static IHostApplicationBuilder AddCaching(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddSingletonAssignableTo(typeof(ICache));
    return builder;
  }

  private static IHostApplicationBuilder AddBuffers(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddSingletonAssignableTo(typeof(IBuffer));
    return builder;
  }
}

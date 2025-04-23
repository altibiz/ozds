using Altibiz.DependencyInjection.Extensions;
using Ozds.Report.Mutations.Abstractions;
using Ozds.Report.Queries.Abstractions;
using Ozds.Report.Serialization.Abstractions;

namespace Ozds.Report.Extensions;

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddOzdsReport(
    this IServiceCollection services
  )
  {
    services.AddQueries();
    services.AddMutations();
    services.AddSerialization();
    return services;
  }

  public static IServiceCollection AddQueries(
    this IServiceCollection services
  )
  {
    services.AddScopedAssignableTo(typeof(IQueries));
    return services;
  }

  public static IServiceCollection AddMutations(
    this IServiceCollection services
  )
  {
    services.AddScopedAssignableTo(typeof(IMutations));
    return services;
  }

  public static IServiceCollection AddSerialization(
    this IServiceCollection services
  )
  {
    services.AddTransientAssignableTo(typeof(IImporter));
    services.AddTransientAssignableTo(typeof(IExporter));
    return services;
  }
}

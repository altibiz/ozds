using Altibiz.DependencyInjection.Extensions;
using Ozds.Report.Mutations.Abstractions;
using Ozds.Report.Queries.Abstractions;
using Ozds.Report.Serialization.Abstractions;

namespace Ozds.Report.Extensions;

public static class HostExtensions
{
  public static IHostApplicationBuilder AddOzdsReport(
    this IHostApplicationBuilder builder
  )
  {
    builder.AddQueries();
    builder.AddMutations();
    builder.AddSerialization();
    return builder;
  }

  public static IHostApplicationBuilder AddQueries(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddScopedAssignableTo(typeof(IQueries));
    return builder;
  }

  public static IHostApplicationBuilder AddMutations(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddScopedAssignableTo(typeof(IMutations));
    return builder;
  }

  public static IHostApplicationBuilder AddSerialization(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddTransientAssignableTo(typeof(IImporter));
    builder.Services.AddTransientAssignableTo(typeof(IExporter));
    return builder;
  }
}

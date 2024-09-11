using System.Reflection;
using Npgsql;

namespace Ozds.Data.Extensions;

public interface INpgsqlDataSourceConfiguration
{
  void Configure(NpgsqlDataSourceBuilder builder);
}

public static class NpgsqlDataSourceBuilderExtensions
{
  public static NpgsqlDataSourceBuilder ApplyConfigurationsFromAssembly(
    this NpgsqlDataSourceBuilder builder,
    Assembly assembly
  )
  {
    return assembly
      .GetTypes()
      .Where(
        type =>
          !type.IsAbstract
          && !type.IsGenericType
          && typeof(INpgsqlDataSourceConfiguration).IsAssignableFrom(type))
      .Aggregate(
        builder,
        (builder, type) =>
        {
          var config =
            Activator.CreateInstance(type) as INpgsqlDataSourceConfiguration;
          config?.Configure(builder);
          return builder;
        }
      );
  }
}

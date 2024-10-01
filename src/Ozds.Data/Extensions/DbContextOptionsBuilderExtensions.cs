using System.Data.Common;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using Ozds.Data.Interceptors;
using Ozds.Data.Timescale;

namespace Ozds.Data.Extensions;

public static class DbContextOptionsBuilderExtensions
{
  public static DbContextOptionsBuilder UseTimescale(
    this DbContextOptionsBuilder builder,
    DbDataSource dataSource,
    Action<NpgsqlDbContextOptionsBuilder>? npgsqlOptionsAction = null
  )
  {
    return builder
      .UseNpgsql(dataSource, npgsqlOptionsAction)
      .ReplaceService<IMigrationsSqlGenerator, TimescaleMigrationSqlGenerator>()
      .ReplaceService<IRelationalAnnotationProvider,
        TimescaleAnnotationProvider>();
  }

  public static DbContextOptionsBuilder
    AddServedSaveChangesInterceptorsFromAssembly(
      this DbContextOptionsBuilder builder,
      Assembly assembly,
      IServiceProvider serviceProvider
    )
  {
    return builder.AddInterceptors(
      assembly
        .GetTypes()
        .Where(type => type.IsSubclassOf(typeof(ServedSaveChangesInterceptor)))
        .Select(
          type =>
          {
            try
            {
              return (IInterceptor?)Activator.CreateInstance(
                type,
                serviceProvider);
            }
            catch (Exception)
            {
              return null;
            }
          })
        .Where(interceptor => interceptor is not null)
        .OfType<ServedSaveChangesInterceptor>()
        .OrderBy(interceptor => interceptor.Order)
        .OfType<IInterceptor>()
        .ToArray());
  }
}

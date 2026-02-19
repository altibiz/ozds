using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Ozds.Data.Context;
using Ozds.Data.Interceptors;

namespace Ozds.Data.Extensions;

public static class DbContextOptionsBuilderExtensions
{
  public static DbContextOptionsBuilder UseTimescale(
    this DbContextOptionsBuilder builder
  )
  {
    return builder
      .ReplaceService<IMigrationsSqlGenerator, TimescaleMigrationSqlGenerator>()
      .ReplaceService<
        IRelationalAnnotationProvider,
        TimescaleAnnotationProvider
      >();
  }

  public static DbContextOptionsBuilder AddServedSaveChangesInterceptorsFromAssembly(
    this DbContextOptionsBuilder builder,
    Assembly assembly,
    IServiceProvider serviceProvider
  )
  {
    return builder.AddInterceptors(
      assembly
        .GetTypes()
        .Where(type => type.IsAssignableTo(typeof(ServedInterceptor)))
        .Where(type => !type.IsAbstract && !type.IsGenericType)
        .Select(type =>
        {
          try
          {
            return (IInterceptor?)
              Activator.CreateInstance(type, serviceProvider);
          }
          catch (Exception)
          {
            return null;
          }
        })
        .Where(interceptor => interceptor is not null)
        .OfType<ServedInterceptor>()
        .OrderBy(interceptor => interceptor.Order)
        .OfType<IInterceptor>()
        .ToArray()
    );
  }
}

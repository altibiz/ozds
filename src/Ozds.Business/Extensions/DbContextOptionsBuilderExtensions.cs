using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ozds.Business.Interceptors;

namespace Ozds.Business.Extensions;

public static class DbContextOptionsBuilderExtensions
{
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
        .Select(type =>
        {
          try
          {
            return (IInterceptor?)Activator.CreateInstance(type,
              serviceProvider);
          }
          catch (Exception)
          {
            return null;
          }
        })
        .Where(interceptor => interceptor is not null)
        .OfType<IInterceptor>()
        .ToArray());
  }
}

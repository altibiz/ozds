using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Ozds.Data.Test.Extensions;

public static class SpecimenBuilderExtensions
{
  private static readonly Lazy<MethodInfo> GenericCreateMethodLazy = new(() =>
    typeof(SpecimenFactory)
      .GetMethods(BindingFlags.Static | BindingFlags.Public)
      .First(method =>
        method.Name == "Create"
        && method.IsGenericMethod
        && method.GetGenericArguments().Length == 1
        && method.GetParameters().Length == 1
        && method.GetParameters()[0].ParameterType == typeof(ISpecimenBuilder)
      )
  );

  private static readonly Lazy<MethodInfo> GenericCreateManyMethodLazy = new(
    () =>
      typeof(SpecimenFactory)
        .GetMethods(BindingFlags.Static | BindingFlags.Public)
        .First(method =>
          method.Name == "CreateMany"
          && method.IsGenericMethod
          && method.GetGenericArguments().Length == 1
          && method.GetParameters().Length == 2
          && method.GetParameters()[0].ParameterType == typeof(ISpecimenBuilder)
          && method.GetParameters()[1].ParameterType == typeof(int)
        )
  );

  private static MethodInfo GenericCreateMethod
  {
    get { return GenericCreateMethodLazy.Value; }
  }

  private static MethodInfo GenericCreateManyMethod
  {
    get { return GenericCreateManyMethodLazy.Value; }
  }

  public static T Create<T>(this ISpecimenBuilder builder, Type type)
  {
    var createMethod = GenericCreateMethod.MakeGenericMethod(type);
    var entity =
      createMethod.Invoke(null, [builder])
      ?? throw new InvalidOperationException("Entity not created");
    return (T)entity;
  }

  public static IEnumerable<T> CreateMany<T>(
    this ISpecimenBuilder builder,
    Type type,
    int count
  )
  {
    var createManyMethod = GenericCreateManyMethod.MakeGenericMethod(type);
    var entity =
      createManyMethod.Invoke(null, [builder, count])
      ?? throw new InvalidOperationException("Entities not created");
    return (IEnumerable<T>)entity;
  }

  public static async Task<T> CreateInDb<T>(
    this ISpecimenBuilder builder,
    DbContext context,
    CancellationToken cancellationToken,
    Action<T>? action = null
  )
  {
    var entity = builder.Create<T>()!;
    action?.Invoke(entity);
    context.Add(entity);
    await context.SaveChangesAsync(cancellationToken);
    return entity;
  }

  public static async Task<T> CreateInDb<T>(
    this ISpecimenBuilder builder,
    DbContext context,
    Type type,
    CancellationToken cancellationToken,
    Action<T>? action = null
  )
  {
    var createMethod = GenericCreateMethod.MakeGenericMethod(type);
    var entity =
      createMethod.Invoke(null, [builder])
      ?? throw new InvalidOperationException("Entity not created");
    action?.Invoke((T)entity);
    context.Add(entity);
    await context.SaveChangesAsync(cancellationToken);
    return (T)entity;
  }
}

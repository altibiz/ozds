using System.Collections;

namespace Ozds.Client.Extensions;

public static class TypeExtensions
{
  public static Type? CommonType(this IEnumerable<Type> types)
  {
    using var enumerator = types.GetEnumerator();
    if (!enumerator.MoveNext())
    {
      return null;
    }

    var common = enumerator.Current;

    while (enumerator.MoveNext())
    {
      common = GetCommonSuperType(common, enumerator.Current);

      if (common == typeof(object))
      {
        return typeof(object);
      }
    }

    return common;
  }

  private static Type GetCommonSuperType(Type lhs, Type rhs)
  {
    if (lhs == rhs)
    {
      return lhs;
    }

    if (lhs.IsAssignableFrom(rhs))
    {
      return lhs;
    }

    if (rhs.IsAssignableFrom(lhs))
    {
      return rhs;
    }

    var current = lhs.BaseType;
    while (current != null)
    {
      if (current.IsAssignableFrom(rhs))
      {
        return current;
      }

      current = current.BaseType;
    }

    return typeof(object);
  }

  public static IEnumerable GetNullableEnumValues(this Type type)
  {
    var isNullable = type.IsGenericType
      && type.GetGenericTypeDefinition() == typeof(Nullable<>);

    var enumType = isNullable
      ? type.GetGenericArguments().First()
      : type;

    foreach (var item in Enum.GetValues(enumType))
    {
      yield return item;
    }

    if (isNullable)
    {
      yield return null;
    }
  }
}

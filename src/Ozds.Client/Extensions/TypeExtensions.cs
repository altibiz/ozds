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

  private static Type GetCommonSuperType(Type a, Type b)
  {
    if (a == b)
    {
      return a;
    }

    if (a.IsAssignableFrom(b))
    {
      return a;
    }

    if (b.IsAssignableFrom(a))
    {
      return b;
    }

    var current = a.BaseType;
    while (current != null)
    {
      if (current.IsAssignableFrom(b))
      {
        return current;
      }

      current = current.BaseType;
    }

    return typeof(object);
  }
}

using System.Linq.Expressions;
using System.Reflection;
using Ozds.Assets.Queries.Abstractions;

namespace Ozds.Assets.Queries.Implementations;

public class TranslationQueries : ITranslationQueries
{
  public string GeneralKey(Type type, bool trimmed = true, bool plural = false)
  {
    return AddPluralFn(plural)(CleanTypeName(type, trimmed));
  }

  public string GeneralKey(Type type, string member)
  {
    return member;
  }

  public string GeneralKey(MemberExpression member)
  {
    return member.Member.Name;
  }

  public string Key(Type type, bool plural = false)
  {
    return AddPluralFn(plural)(AddNamespace(type, CleanTypeName(type)));
  }

  public string Key(Type type, string member)
  {
    return $"{AddNamespace(type, CleanTypeName(type))}.{member}";
  }

  public string Key(MemberExpression member)
  {
    var order = MemberOrder(member);
    var type = order.First().Type;
    var suffix = string.Join(
      ".",
      order.Select(x => x.Property));
    return AddNamespace(type, $"{CleanTypeName(type)}.{suffix}");
  }

  public string[] KeyOverrides(Type type, bool plural = false)
  {
    var order = VirtualizationOrder(type);
    return order
      .Select(x => Key(x))
      .Append(GeneralKey(type, false))
      .Append(GeneralKey(type))
      .Select(AddPluralFn(plural))
      .ToArray();
  }

  public string[] KeyOverrides(Type type, string member)
  {
    var order = VirtualizationOrder(type, member);
    return order
      .Select(x => Key(x, member))
      .Append(GeneralKey(type, member))
      .ToArray();
  }

  public string[] KeyOverrides(MemberExpression member)
  {
    var memberOrder = MemberOrder(member);
    var overrides = new List<string>();
    foreach (var ((type, property), index) in memberOrder
      .Select((x, i) => (x, i)))
    {
      var suffix = string.Join(
        ".",
        memberOrder.Skip(index + 1).Select(x => x.Property));
      suffix = suffix == string.Empty ? "" : $".{suffix}";
      var virtualizationOrder = VirtualizationOrder(type, property);
      overrides.AddRange(
        virtualizationOrder.Select(x => $"{Key(x, property)}{suffix}"));
    }

    return overrides
      .Append(GeneralKey(member))
      .ToArray();
  }

  public string ShortKey(Type type, bool plural = false)
  {
    return AddPluralFn(plural)(CleanTypeName(type));
  }

  public string ShortKey(Type type, string member)
  {
    return $"{CleanTypeName(type)}.{member}";
  }

  public string ShortKey(MemberExpression member)
  {
    var order = MemberOrder(member);
    var type = order.First().Type;
    var suffix = string.Join(
      ".",
      order.Select(x => x.Property));
    return $"{CleanTypeName(type)}.{suffix}";
  }

  public string[] ShortKeyOverrides(Type type, bool plural = false)
  {
    var order = VirtualizationOrder(type);
    return order
      .Select(x => ShortKey(x))
      .Append(GeneralKey(type))
      .Select(AddPluralFn(plural))
      .ToArray();
  }

  public string[] ShortKeyOverrides(Type type, string member)
  {
    var order = VirtualizationOrder(type, member);
    return order
      .Select(x => ShortKey(x, member))
      .Append(GeneralKey(type, member))
      .ToArray();
  }

  public string[] ShortKeyOverrides(MemberExpression member)
  {
    var memberOrder = MemberOrder(member);
    var overrides = new List<string>();
    foreach (var ((type, property), index) in memberOrder
      .Select((x, i) => (x, i)))
    {
      var suffix = string.Join(
        ".",
        memberOrder.Skip(index + 1).Select(x => x.Property));
      suffix = suffix == string.Empty ? "" : $".{suffix}";
      var virtualizationOrder = VirtualizationOrder(type, property);
      overrides.AddRange(
        virtualizationOrder.Select(x => $"{ShortKey(x, property)}{suffix}"));
    }

    return overrides
      .Append(member.Member.Name)
      .Append(GeneralKey(member))
      .ToArray();
  }

  private static Func<string, string> AddPluralFn(bool plural)
  {
    return plural ? AddPlural : x => x;
  }

  private static string AddPlural(string key)
  {
    return $"~{key}";
  }

  private static string AddNamespace(Type type, string name)
  {
    return string.IsNullOrEmpty(type.Namespace)
      ? name
      : $"{type.Namespace}.{name}";
  }

  private static string CleanTypeName(
    Type type,
    bool trim = false
  )
  {
    var baseName = type.Name;

    if (!type.IsGenericType)
    {
      if (trim)
      {
        if (type.IsInterface && baseName.StartsWith('I'))
        {
          baseName = baseName[1..];
        }

        if (baseName.LastIndexOf("Model") is > 0 and var modelIndex)
        {
          baseName = baseName[..modelIndex];
        }

        if (baseName.LastIndexOf("Entity") is > 0 and var entityIndex)
        {
          baseName = baseName[..entityIndex];
        }
      }

      return baseName;
    }

    var backtickIndex = baseName.IndexOf('`');
    if (backtickIndex > 0)
    {
      baseName = baseName[..backtickIndex];
    }

    var genericArgs = string.Join(
      ", ",
      type.GetGenericArguments().Select(x => x.Name));
    return $"{baseName}<{genericArgs}>";
  }

  private static List<MemberExpressionItem> MemberOrder(
    MemberExpression member
  )
  {
    var expression = member as Expression;
    var order = new List<MemberExpressionItem>();
    while (expression is MemberExpression memberExpression)
    {
      expression = memberExpression.Expression
        ?? throw new InvalidOperationException(
          $"Expression of {memberExpression} is null");
      order.Add(
        new MemberExpressionItem(
          memberExpression.Expression.Type,
          memberExpression.Member.Name));
    }

    order.Reverse();
    return order;
  }

  private static List<Type> VirtualizationOrder(Type type)
  {
    var overrides = new List<Type>();
    var currentType = type;
    while (currentType != null)
    {
      overrides.Add(currentType);

      foreach (var @interface in GetAllInterfacesRecursively(currentType))
      {
        overrides.Add(@interface);
      }

      currentType = currentType.BaseType;
    }

    return overrides;
  }

  private static List<Type> VirtualizationOrder(Type type, string property)
  {
    var overrides = new List<Type>();
    var currentType = type;
    while (currentType != null)
    {
      if (DeclaresProperty(currentType, property))
      {
        if (!overrides.Contains(currentType))
        {
          overrides.Add(currentType);
        }

#pragma warning disable S3267 // Loops should be simplified with "LINQ" expressions
        foreach (var @interface in GetAllInterfacesRecursively(currentType))
        {
          if (DeclaresProperty(@interface, property)
            && !overrides.Contains(@interface))
          {
            overrides.Add(@interface);
          }
        }
#pragma warning restore S3267 // Loops should be simplified with "LINQ" expressions
      }
      else
      {
        break;
      }

      currentType = currentType.BaseType;
    }

    return overrides;
  }

  private static bool DeclaresProperty(Type type, string property)
  {
    var info = type
      .GetProperty(property, BindingFlags.Instance | BindingFlags.Public);
    return info is not null;
  }

  private static HashSet<Type> GetAllInterfacesRecursively(Type type)
  {
    var interfaces = new HashSet<Type>();

    foreach (var @interface in type
      .GetInterfaces()
      .Except(type.BaseType?.GetInterfaces() ?? Enumerable.Empty<Type>()))
    {
      interfaces.Add(@interface);

      foreach (var nestedInterface in GetAllInterfacesRecursively(@interface))
      {
        interfaces.Add(nestedInterface);
      }
    }

    return interfaces;
  }

  private sealed record MemberExpressionItem(
    Type Type,
    string Property
  );
}

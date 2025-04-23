using System.Globalization;
using System.Linq.Expressions;
using System.Text;
using Ozds.Assets.Queries.Abstractions;

namespace Ozds.Assets.Queries.Implementations;

public class LocalizationQueries(
  IAssetQueries assetQueries
) : ILocalizationQueries
{
  public string Translate(CultureInfo culture, string notLocalized)
  {
    var translations = assetQueries.LoadTranslations(culture);
    if (translations.TryGetValue(notLocalized, out var value))
    {
      return value;
    }

    return notLocalized;
  }

  public string Key(Type type)
  {
    return AddNamespace(type, CleanTypeName(type));
  }

  public string ShortKey(Type type)
  {
    return CleanTypeName(type);
  }

  public string Key(Type type, string member)
  {
    return $"{AddNamespace(type, CleanTypeName(type))}.{member}";
  }

  public string ShortKey(Type type, string member)
  {
    return $"{CleanTypeName(type)}.{member}";
  }

  public string Key(MemberExpression member)
  {
    var (type, suffix) = UnwrapMember(member);
    return AddNamespace(type, $"{CleanTypeName(type)}{suffix}");
  }

  public string ShortKey(MemberExpression member)
  {
    var (type, suffix) = UnwrapMember(member);
    return $"{CleanTypeName(type)}{suffix}";
  }

  private static (Type, string) UnwrapMember(MemberExpression member)
  {
    var expression = member as Expression;
    var suffix = new StringBuilder();
    while (expression is MemberExpression memberExpression)
    {
      suffix.Insert(0, $".{memberExpression.Member.Name}");
      expression = memberExpression.Expression
        ?? throw new InvalidOperationException(
          $"Expression of {memberExpression} is null");
    }

    var type = expression.Type;
    return (type, suffix.ToString());
  }

  private static string AddNamespace(Type type, string name)
  {
    return string.IsNullOrEmpty(type.Namespace)
      ? name
      : $"{type.Namespace}.{name}";
  }

  private static string CleanTypeName(Type type)
  {
    if (!type.IsGenericType)
    {
      return type.Name;
    }

    var baseName = type.Name;
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
}

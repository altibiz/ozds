using System.Globalization;
using System.Linq.Expressions;
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
    return CleanTypeName(type);
  }

  public string Key(Type type, string member)
  {
    return $"{CleanTypeName(type)}.{member}";
  }

  public string Key(MemberExpression member)
  {
    static string Recurse(Expression expression)
    {
      if (expression is MemberExpression memberExpression)
      {
        var container = memberExpression.Expression
          ?? throw new InvalidOperationException(
            $"Member expression {expression} has no container.");
        return Recurse(container)
          + $".{memberExpression.Member.Name}";
      }

      return CleanTypeName(expression.Type);
    }

    return Recurse(member);
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

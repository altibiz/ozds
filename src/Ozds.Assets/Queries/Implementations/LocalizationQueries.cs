using System.Globalization;
using System.Linq.Expressions;
using System.Text;
using Ozds.Assets.Queries.Abstractions;
using Ozds.Assets.Time;

namespace Ozds.Assets.Queries.Implementations;

public class LocalizationQueries(
  IAssetQueries assetQueries
) : ILocalizationQueries
{
  public CultureInfo CroatianCulture
  {
    get { return AssetConstants.CroatianCulture; }
  }

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

  public string NumericString(decimal? number, int places = 2)
  {
    if (number is null)
    {
      return "";
    }

    var cultureInfo = CroatianCulture;

    var numberFormatInfo = (NumberFormatInfo)cultureInfo.NumberFormat.Clone();
    numberFormatInfo.NumberGroupSeparator = ".";
    numberFormatInfo.NumberDecimalDigits = places;

    var roundedNumber = Math.Round(number.Value, places);
    return roundedNumber.ToString("N", numberFormatInfo);
  }

  public string NumericString(float? number, int places = 2)
  {
    if (number is null)
    {
      return "";
    }

    var cultureInfo = CroatianCulture;

    var numberFormatInfo = (NumberFormatInfo)cultureInfo.NumberFormat.Clone();
    numberFormatInfo.NumberGroupSeparator = ".";
    numberFormatInfo.NumberDecimalDigits = places;

    var roundedNumber = Math.Round(number.Value, places);
    return roundedNumber.ToString("N", numberFormatInfo);
  }

  public string DateString(DateTimeOffset? dateTimeOffset)
  {
    if (dateTimeOffset is null)
    {
      return "";
    }

    var cultureInfo = CroatianCulture;

    var withTimezone = dateTimeOffset
      .Value
      .ToOffset(DateTimeOffsetTooling.GetOffset(dateTimeOffset.Value));

    return withTimezone.ToString("dd. MM. yyyy.", cultureInfo);
  }

  public string DateTimeString(DateTimeOffset? dateTimeOffset)
  {
    if (dateTimeOffset is null)
    {
      return "";
    }

    var cultureInfo = CroatianCulture;

    var withTimezone = dateTimeOffset
      .Value
      .ToOffset(DateTimeOffsetTooling.GetOffset(dateTimeOffset.Value));

    return withTimezone.ToString("dd. MM. yyyy. HH:mm", cultureInfo);
  }

  public DateTimeOffset DateTimeApplyOffset(
    DateTimeOffset dateTimeOffset)
  {
    var a = dateTimeOffset.UtcDateTime.Add(
      DateTimeOffsetTooling.GetOffset(dateTimeOffset));
    return a;
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

using System.Globalization;
using System.Linq.Expressions;
using Ozds.Assets;
using Ozds.Business.Queries.Abstractions;
using AssetLocalizationQueries =
  Ozds.Assets.Queries.Abstractions.ILocalizationQueries;

namespace Ozds.Business.Queries;

public class LocalizationQueries(
  AssetLocalizationQueries localizer
) : IQueries
{
  public CultureInfo CroatianCulture
  {
    get { return AssetConstants.CroatianCulture; }
  }

  public CultureInfo EnglishCulture
  {
    get { return AssetConstants.EnglishCulture; }
  }

  public string Translate(CultureInfo culture, string notLocalized)
  {
    return localizer.Translate(culture, notLocalized);
  }

  public string Translate(CultureInfo culture, Type type)
  {
    var key = localizer.Key(type);
    var value = localizer.Translate(culture, key);
    if (key != value)
    {
      return value;
    }

    key = localizer.ShortKey(type);
    value = localizer.Translate(culture, key);
    return value;
  }

  public string Translate(CultureInfo culture, Type type, string member)
  {
    var key = localizer.Key(type, member);
    var value = localizer.Translate(culture, key);
    if (key != value)
    {
      return value;
    }

    key = localizer.ShortKey(type, member);
    value = localizer.Translate(culture, key);
    return value;
  }

  public string Translate(CultureInfo culture, MemberExpression member)
  {
    var key = localizer.Key(member);
    var value = localizer.Translate(culture, key);
    if (key != value)
    {
      return value;
    }

    key = localizer.ShortKey(member);
    value = localizer.Translate(culture, key);
    return value;
  }
}

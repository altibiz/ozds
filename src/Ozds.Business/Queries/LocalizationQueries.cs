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
    return localizer.Translate(culture, type);
  }

  public string Translate(CultureInfo culture, Type type, string member)
  {
    return localizer.Translate(culture, type, member);
  }

  public string Translate(CultureInfo culture, MemberExpression member)
  {
    return localizer.Translate(culture, member);
  }

  public string NumericString(decimal? number, int places = 2)
  {
    return localizer.NumericString(number, places);
  }

  public string NumericString(float? number, int places = 2)
  {
    return localizer.NumericString(number, places);
  }

  public string DateString(DateTimeOffset? dateTimeOffset)
  {
    return localizer.DateString(dateTimeOffset);
  }

  public string DateTimeString(DateTimeOffset? dateTimeOffset)
  {
    return localizer.DateTimeString(dateTimeOffset);
  }

  public DateTimeOffset DateTimeApplyOffset(
    DateTimeOffset dateTimeOffset)
  {
    return localizer.DateTimeApplyOffset(dateTimeOffset);
  }
}

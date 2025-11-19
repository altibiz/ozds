using System.Globalization;
using System.Linq.Expressions;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Enums;
using Ozds.Business.Queries.Abstractions;
using AssetCultureQueries =
  Ozds.Assets.Queries.Abstractions.ICultureQueries;
using AssetLocalizationQueries =
  Ozds.Assets.Queries.Abstractions.ILocalizationQueries;

namespace Ozds.Business.Queries;

public class LocalizationQueries(
  AssetLocalizationQueries localizationQueries,
  AssetCultureQueries cultureQueries
) : IQueries
{
  public CultureInfo DefaultCulture
  {
    get { return cultureQueries.DefaultCulture; }
  }

  public CultureInfo CroatianCulture
  {
    get { return cultureQueries.CroatianCulture; }
  }

  public CultureInfo EnglishCulture
  {
    get { return cultureQueries.EnglishCulture; }
  }

  public CultureInfo? IdToCulture(string culture)
  {
    return cultureQueries.IdToCulture(culture);
  }

  public string CultureToId(CultureInfo culture)
  {
    return cultureQueries.CultureToId(culture);
  }

  public string CultureToName(CultureInfo culture)
  {
    return cultureQueries.CultureToName(culture);
  }

  public string Translate(CultureInfo culture, string notLocalized)
  {
    return localizationQueries.Translate(culture, notLocalized);
  }

  public string Translate(CultureInfo culture, Type type, bool plural = false)
  {
    return localizationQueries.Translate(culture, type, plural);
  }

  public string Translate(CultureInfo culture, Type type, string member)
  {
    return localizationQueries.Translate(culture, type, member);
  }

  public string Translate(CultureInfo culture, MemberExpression member)
  {
    return localizationQueries.Translate(culture, member);
  }

  public string NumericString(decimal? number, int places = 2)
  {
    return localizationQueries.NumericString(number, places);
  }

  public string NumericString(float? number, int places = 2)
  {
    return localizationQueries.NumericString(number, places);
  }

  public string DateString(DateTimeOffset? dateTimeOffset)
  {
    return localizationQueries.DateString(dateTimeOffset);
  }

  public string DateTimeString(DateTimeOffset? dateTimeOffset)
  {
    return localizationQueries.DateTimeString(dateTimeOffset);
  }

  public DateTimeOffset DateTimeApplyOffset(
    DateTimeOffset dateTimeOffset)
  {
    return localizationQueries.DateTimeApplyOffset(dateTimeOffset);
  }

  // NOTE: hack to get the translator to translate these
  // Translate("seconds")
  // Translate("minutes")
  // Translate("hours")
  // Translate("days")
  // Translate("weeks")
  // Translate("months")
  // Translate("years")
  // Translate("a second")
  // Translate("a minute")
  // Translate("an hour")
  // Translate("a day")
  // Translate("a week")
  // Translate("a month")
  // Translate("a year")
  public string TranslateDuration(
    CultureInfo culture,
    DurationModel duration,
    bool plural = false
  )
  {
    string nonLocalized;
    if (plural)
    {
      nonLocalized = duration switch
      {
        DurationModel.Second => "seconds",
        DurationModel.Minute => "minutes",
        DurationModel.Hour => "hours",
        DurationModel.Day => "days",
        DurationModel.Week => "weeks",
        DurationModel.Month => "months",
        DurationModel.Year => "years",
        _ => throw new NotImplementedException()
      };
    }
    else
    {
      nonLocalized = duration switch
      {
        DurationModel.Second => "a second",
        DurationModel.Minute => "a minute",
        DurationModel.Hour => "an hour",
        DurationModel.Day => "a day",
        DurationModel.Week => "a week",
        DurationModel.Month => "a month",
        DurationModel.Year => "a year",
        _ => throw new NotImplementedException()
      };
    }

    return localizationQueries.Translate(culture, nonLocalized);
  }

  public string TranslatePeriod(
    CultureInfo culture,
    PeriodModel duration
  )
  {
    var translatedDuration = TranslateDuration(
      culture,
      duration.Duration,
      duration.Multiplier != 1);

    return $"{duration.Multiplier} {translatedDuration}";
  }

  public string DateFormat(CultureInfo cultureInfo)
  {
    return localizationQueries.DateFormat(cultureInfo);
  }

  public string DateTimeFormat(CultureInfo cultureInfo)
  {
    return localizationQueries.DateTimeFormat(cultureInfo);
  }
}

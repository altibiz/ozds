using System.Globalization;
using System.Linq.Expressions;
using Ozds.Assets;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Enums;
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

    return localizer.Translate(culture, nonLocalized);
  }

  public string TranslatePeriod(
    CultureInfo culture,
    PeriodModel duration
  )
  {
    var translatedDuration = TranslateDuration(
      culture,
      duration.Duration,
      duration.Multiplier > 1);

    return $"{duration.Multiplier} {translatedDuration}";
  }
}

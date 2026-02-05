using System.Collections.Concurrent;
using System.Globalization;
using System.Linq.Expressions;
using Ozds.Assets.Queries.Abstractions;
using Ozds.Time.Queries.Abstractions;

namespace Ozds.Assets.Queries.Implementations;

public class LocalizationQueries(
  IAssetQueries assetQueries,
  ITimeQueries timeQueries,
  ITranslationQueries translationQueries,
  ICultureQueries cultureQueries
) : ILocalizationQueries
{
  private readonly ConcurrentDictionary<
    TranslationKey,
    string
  > translationCache = new();

  public string Translate(CultureInfo culture, Type type, bool plural = false)
  {
    var cacheKey = new TranslationTypeKey(culture, type, plural);

    return translationCache.GetOrAdd(
      cacheKey,
      _ =>
      {
        var translations = assetQueries.LoadTranslations(
          cultureQueries.CultureToId(culture)
        );

        var overrides = translationQueries.KeyOverrides(type, plural);
        foreach (var key in overrides)
        {
          if (translations.TryGetValue(key, out var translation))
          {
            return translation;
          }
        }

        return overrides.First();
      }
    );
  }

  public string Translate(CultureInfo culture, Type type, string member)
  {
    var cacheKey = new TranslationMemberKey(culture, type, member);

    return translationCache.GetOrAdd(
      cacheKey,
      _ =>
      {
        var translations = assetQueries.LoadTranslations(
          cultureQueries.CultureToId(culture)
        );

        var overrides = translationQueries.KeyOverrides(type, member);
        foreach (var key in overrides)
        {
          if (translations.TryGetValue(key, out var translation))
          {
            return translation;
          }
        }

        return overrides.First();
      }
    );
  }

  public string Translate(CultureInfo culture, MemberExpression member)
  {
    var cacheKey = new TranslationExpressionKey(culture, member);

    return translationCache.GetOrAdd(
      cacheKey,
      _ =>
      {
        var translations = assetQueries.LoadTranslations(
          cultureQueries.CultureToId(culture)
        );

        var overrides = translationQueries.KeyOverrides(member);
        foreach (var key in overrides)
        {
          if (translations.TryGetValue(key, out var translation))
          {
            return translation;
          }
        }

        return overrides.First();
      }
    );
  }

  public string Translate(CultureInfo culture, string notLocalized)
  {
    var cacheKey = new TranslationStringKey(culture, notLocalized);

    return translationCache.GetOrAdd(
      cacheKey,
      _ =>
      {
        var translations = assetQueries.LoadTranslations(
          cultureQueries.CultureToId(culture)
        );

        if (translations.TryGetValue(notLocalized, out var value))
        {
          return value;
        }

        return notLocalized;
      }
    );
  }

  public string NumericString(decimal? number, int places = 2)
  {
    if (number is null)
    {
      return "";
    }

    var cultureInfo = cultureQueries.CroatianCulture;

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

    var cultureInfo = cultureQueries.CroatianCulture;

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

    var cultureInfo = cultureQueries.CroatianCulture;

    var withTimezone = dateTimeOffset.Value.ToOffset(
      timeQueries.GetCroatianOffset(dateTimeOffset.Value)
    );

    return withTimezone.ToString(DateFormat(cultureInfo), cultureInfo);
  }

  public string DateTimeString(DateTimeOffset? dateTimeOffset)
  {
    if (dateTimeOffset is null)
    {
      return "";
    }

    var cultureInfo = cultureQueries.CroatianCulture;

    var withTimezone = dateTimeOffset.Value.ToOffset(
      timeQueries.GetCroatianOffset(dateTimeOffset.Value)
    );

    return withTimezone.ToString(DateTimeFormat(cultureInfo), cultureInfo);
  }

  public DateTimeOffset DateTimeApplyOffset(DateTimeOffset dateTimeOffset)
  {
    var a = dateTimeOffset.UtcDateTime.Add(
      timeQueries.GetCroatianOffset(dateTimeOffset)
    );
    return a;
  }

  public string DocumentDate(DateTimeOffset date)
  {
    return date.ToString("dd. MM. yyyy.");
  }

  public string DocumentDateTime(DateTimeOffset date)
  {
    return date.ToString("dd. MM. yyyy. HH:mm");
  }

  public string DocumentNumber(decimal number, int precision = 2)
  {
    var cultureInfo = cultureQueries.CroatianCulture;
    var nfi = (NumberFormatInfo)cultureInfo.NumberFormat.Clone();

    nfi.NumberGroupSeparator = ".";
    var format = "#,##0." + new string('#', precision);
    return number.ToString(format, nfi);
  }

  public string DateFormat(CultureInfo cultureInfo)
  {
    return "dd. MM. yyyy.";
  }

  public string DateTimeFormat(CultureInfo cultureInfo)
  {
    return "dd. MM. yyyy. HH:mm";
  }

  private record TranslationKey(CultureInfo Culture);

  private sealed record TranslationTypeKey(
    CultureInfo Culture,
    Type Type,
    bool Plural
  ) : TranslationKey(Culture);

  private sealed record TranslationMemberKey(
    CultureInfo Culture,
    Type Type,
    string Member
  ) : TranslationKey(Culture);

  private sealed record TranslationExpressionKey(
    CultureInfo Culture,
    MemberExpression Expression
  ) : TranslationKey(Culture);

  private sealed record TranslationStringKey(
    CultureInfo Culture,
    string NotLocalized
  ) : TranslationKey(Culture);
}

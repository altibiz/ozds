using System.Globalization;
using System.Linq.Expressions;
using System.Text.Json;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Enums;
using Ozds.Business.Queries;

namespace Ozds.Client.Components.Base;

public abstract partial class OzdsComponentBase : DisposableComponentBase
{
  private static readonly JsonSerializerOptions JsonSerializerOptions = new()
  {
    WriteIndented = true
  };

  private LocalizationQueries? localizationQueries;

  private LocalizationQueries LocalizationQueries =>
    localizationQueries ??= ScopedServices
      .GetRequiredService<LocalizationQueries>();

  protected CultureInfo CroatianCulture
  {
    get
    {
      return LocalizationQueries.CroatianCulture;
    }
  }

  protected CultureInfo EnglishCulture
  {
    get
    {
      return LocalizationQueries.EnglishCulture;
    }
  }

  protected string NumericString(decimal? number, int places = 2)
  {
    if (number is null)
    {
      return "";
    }

    return LocalizationQueries.NumericString(number, places);
  }

  protected string NumericString(float? number, int places = 2)
  {
    if (number is null)
    {
      return "";
    }

    return LocalizationQueries.NumericString(number, places);
  }

  protected string DateString(DateTimeOffset? dateTimeOffset)
  {
    if (dateTimeOffset is null)
    {
      return "";
    }

    return LocalizationQueries.DateString(dateTimeOffset);
  }

  protected string DateTimeString(DateTimeOffset? dateTimeOffset)
  {
    if (dateTimeOffset is null)
    {
      return "";
    }

    return LocalizationQueries.DateTimeString(dateTimeOffset);
  }

  protected DateTimeOffset DateTimeApplyOffset(
    DateTimeOffset dateTimeOffset)
  {
    return LocalizationQueries.DateTimeApplyOffset(dateTimeOffset);
  }

  protected string Translate(string notLocalized)
  {
    var culture = GetCulture();
    return LocalizationQueries.Translate(culture, notLocalized);
  }

  protected string Translate(Type type, bool plural = false)
  {
    var culture = GetCulture();
    return LocalizationQueries.Translate(culture, type, plural);
  }

  protected string Translate(Type type, string member)
  {
    var culture = GetCulture();
    return LocalizationQueries.Translate(culture, type, member);
  }

  protected string Translate(MemberExpression member)
  {
    var culture = GetCulture();
    return LocalizationQueries.Translate(culture, member);
  }

  protected string TranslateDuration(
    DurationModel duration,
    bool plural = false
  )
  {
    var culture = GetCulture();
    return LocalizationQueries.TranslateDuration(
      culture,
      duration,
      plural);
  }

  protected string TranslatePeriod(
    PeriodModel period
  )
  {
    var culture = GetCulture();
    return LocalizationQueries.TranslatePeriod(
      culture,
      period);
  }

  protected string DateFormat()
  {
    var culture = GetCulture();
    return LocalizationQueries.DateFormat(culture);
  }

  protected string DateTimeFormat()
  {
    var culture = GetCulture();
    return LocalizationQueries.DateTimeFormat(culture);
  }

  protected static string JsonString(object? jsonDocument)
  {
    return JsonSerializer.Serialize(jsonDocument, JsonSerializerOptions);
  }
}

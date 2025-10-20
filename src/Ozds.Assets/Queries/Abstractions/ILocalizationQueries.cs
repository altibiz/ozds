using System.Globalization;
using System.Linq.Expressions;

namespace Ozds.Assets.Queries.Abstractions;

public interface ILocalizationQueries : IQueries
{
  public string Translate(CultureInfo culture, Type type, bool plural = false);

  public string Translate(CultureInfo culture, Type type, string member);

  public string Translate(CultureInfo culture, MemberExpression member);

  public string Translate(CultureInfo culture, string notLocalized);

  public string NumericString(decimal? number, int places = 2);

  public string NumericString(float? number, int places = 2);

  public string DateString(DateTimeOffset? dateTimeOffset);

  public string DateTimeString(DateTimeOffset? dateTimeOffset);

  public DateTimeOffset DateTimeApplyOffset(
    DateTimeOffset dateTimeOffset);

  public string DocumentDate(DateTimeOffset date);

  public string DocumentNumber(decimal number, int precision = 2);

  public string DateFormat(CultureInfo cultureInfo);

  public string DateTimeFormat(CultureInfo cultureInfo);
}

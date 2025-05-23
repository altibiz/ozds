using System.Globalization;
using System.Linq.Expressions;

namespace Ozds.Assets.Queries.Abstractions;

public interface ILocalizationQueries : IQueries
{
  public string Translate(CultureInfo culture, string notLocalized);

  public string Key(Type type);

  public string Key(Type type, string member);

  public string Key(MemberExpression member);

  public string ShortKey(Type type);

  public string ShortKey(Type type, string member);

  public string ShortKey(MemberExpression member);

  public string NumericString(decimal? number, int places = 2);

  public string NumericString(float? number, int places = 2);

  public string DateString(DateTimeOffset? dateTimeOffset);

  public string DateTimeString(DateTimeOffset? dateTimeOffset);

  public DateTimeOffset DateTimeApplyOffset(
    DateTimeOffset dateTimeOffset);
}

using System.Globalization;
using System.Linq.Expressions;

namespace Ozds.Assets.Queries.Abstractions;

public interface ILocalizationQueries : IQueries
{
  public string Translate(CultureInfo culture, string notLocalized);

  public string Key(Type type);

  public string Key(Type type, string member);

  public string Key(MemberExpression member);
}

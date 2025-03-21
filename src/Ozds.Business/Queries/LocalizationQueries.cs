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

  public string Key(Type type)
  {
    return localizer.Key(type);
  }

  public string Key(Type type, string member)
  {
    return localizer.Key(type, member);
  }

  public string Key(MemberExpression member)
  {
    return localizer.Key(member);
  }
}

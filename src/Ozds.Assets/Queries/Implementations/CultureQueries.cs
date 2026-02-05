using System.Globalization;
using Ozds.Assets.Queries.Abstractions;

namespace Ozds.Assets.Queries.Implementations;

public class CultureQueries : ICultureQueries
{
  private static readonly CultureInfo CroatianCultureValue = new("hr-HR");

  private static readonly CultureInfo EnglishCultureValue = new("en-US");

  public CultureInfo DefaultCulture
  {
    get { return EnglishCultureValue; }
  }

  public CultureInfo CroatianCulture
  {
    get { return CroatianCultureValue; }
  }

  public CultureInfo EnglishCulture
  {
    get { return EnglishCultureValue; }
  }

  public CultureInfo? IdToCulture(string culture)
  {
    try
    {
      var cultureInfo = CultureInfo.CreateSpecificCulture(culture);

      // NOTE: any nonsensical culture that passes parsing gets set to invariant
      if (
        cultureInfo.TwoLetterISOLanguageName
        == CultureInfo.InvariantCulture.TwoLetterISOLanguageName
      )
      {
        return null;
      }

      return cultureInfo;
    }
    catch (Exception)
    {
      return null;
    }
  }

  public string CultureToId(CultureInfo culture)
  {
    return culture.TwoLetterISOLanguageName;
  }

  public string CultureToName(CultureInfo culture)
  {
    return culture.NativeName;
  }
}

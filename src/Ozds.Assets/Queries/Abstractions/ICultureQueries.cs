using System.Globalization;

namespace Ozds.Assets.Queries.Abstractions;

public interface ICultureQueries : IQueries
{
  public CultureInfo DefaultCulture { get; }

  public CultureInfo CroatianCulture { get; }

  public CultureInfo EnglishCulture { get; }

  public CultureInfo? IdToCulture(string culture);

  public string CultureToId(CultureInfo culture);

  public string CultureToName(CultureInfo culture);
}

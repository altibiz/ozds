using System.Globalization;

namespace Ozds.Business.Localization.Abstractions;

public interface IOzdsLocalizer
{
  public string this[string notLocalized] { get; }

  public string ForCulture(CultureInfo culture, string notLocalized);
}

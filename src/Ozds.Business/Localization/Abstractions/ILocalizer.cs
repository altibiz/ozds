using System.Globalization;

namespace Ozds.Business.Localization.Abstractions;

public interface ILocalizer
{
  public string this[string notLocalized] { get; }

  public string ForCulture(CultureInfo culture, string notLocalized);
}

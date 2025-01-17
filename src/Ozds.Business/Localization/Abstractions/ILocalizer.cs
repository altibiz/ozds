using System.Globalization;

namespace Ozds.Business.Localization.Abstractions;

public interface ILocalizer
{
  public string TranslateForCulture(CultureInfo culture, string notLocalized);

  public string Translate(string notLocalized);
}

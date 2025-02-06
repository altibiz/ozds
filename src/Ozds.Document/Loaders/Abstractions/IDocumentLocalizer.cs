using System.Globalization;

namespace Ozds.Document.Loaders.Abstractions;

public interface IDocumentLocalizer
{
  public string TranslateForCulture(CultureInfo culture, string notLocalized);

  public string Translate(string notLocalized);
}

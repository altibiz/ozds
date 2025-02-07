using System.Globalization;
using Ozds.Document.Loaders.Abstractions;

namespace Ozds.Document.Loaders.Implementations;

public class DocumentLocalizer(
  IDocumentAssetLoader assetLoader
) : IDocumentLocalizer
{
  public string Translate(string notLocalized)
  {
    return TranslateForCulture(CultureInfo.CurrentCulture, notLocalized);
  }

  public string TranslateForCulture(CultureInfo culture, string notLocalized)
  {
    if (assetLoader
      .LoadTranslations(culture)
      .TryGetValue(notLocalized, out var localized))
    {
      return localized;
    }

    return notLocalized;
  }
}

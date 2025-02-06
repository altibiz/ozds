using System.Globalization;
using Ozds.Document.Loaders.Abstractions;

namespace Ozds.Document.Loaders;

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
    return assetLoader.LoadTranslations(culture)[notLocalized] ?? notLocalized;
  }
}

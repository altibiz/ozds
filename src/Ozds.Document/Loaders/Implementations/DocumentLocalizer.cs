using System.Globalization;
using Ozds.Document.Loaders.Abstractions;

namespace Ozds.Document.Loaders.Implementations;

public class DocumentLocalizer(
  IDocumentAssetLoader assetLoader
) : IDocumentLocalizer
{
  public string Translate(string notLocalized)
  {
    if (assetLoader
      .LoadTranslations(new CultureInfo("hr"))
      .TryGetValue(notLocalized, out var localized))
    {
      return localized;
    }

    return notLocalized;
  }
}

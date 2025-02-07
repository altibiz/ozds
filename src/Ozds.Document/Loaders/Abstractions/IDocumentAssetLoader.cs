using System.Globalization;

namespace Ozds.Document.Loaders.Abstractions;

public interface IDocumentAssetLoader
{
  public Dictionary<string, string> LoadTranslations(CultureInfo culture);

  public string LoadSvg(string name);

  public string LoadTtfBase64(string name);
}

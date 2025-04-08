using System.Globalization;

namespace Ozds.Assets.Queries.Abstractions;

public interface IAssetQueries : IQueries
{
  public Dictionary<string, string> LoadTranslations(CultureInfo culture);

  public string LoadSvg(string name);

  public string LoadTtfBase64(string name);
}

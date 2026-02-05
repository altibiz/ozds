using System.Collections.Concurrent;
using Ozds.Assets.Entities;
using Ozds.Assets.Queries.Abstractions;

namespace Ozds.Assets.Queries.Implementations;

public class AssetQueries : IAssetQueries
{
  private readonly ConcurrentDictionary<string, string> fontCache = new();

  private readonly ConcurrentDictionary<string, string> svgCache = new();

  private readonly ConcurrentDictionary<
    string,
    Dictionary<string, string>
  > translationCache = new();

  public Dictionary<string, string> LoadTranslations(string culture)
  {
    return translationCache.GetOrAdd(culture, LoadTranslationsUncached);
  }

  public string LoadSvg(string name)
  {
    return svgCache.GetOrAdd(name, LoadSvgUncached);
  }

  public string LoadTtfBase64(string name)
  {
    return fontCache.GetOrAdd(name, LoadTtfBase64Uncached);
  }

  private Dictionary<string, string> LoadTranslationsUncached(string culture)
  {
    var fileName = $"Translations.{culture}.xml";
    var stream = Load(fileName);
    using var streamReader = new StreamReader(stream);
    var text = streamReader.ReadToEnd();
    var dictionary = TranslationDictionaryEntity.FromString(text, fileName);
    return dictionary.ToDictionary();
  }

  private string LoadSvgUncached(string name)
  {
    var stream = Load($"Images.{name}.svg");
    using var reader = new StreamReader(stream);
    return reader.ReadToEnd();
  }

  private string LoadTtfBase64Uncached(string name)
  {
    var stream = Load($"Fonts.{name}.ttf");
    using var memoryStream = new MemoryStream();
    stream.CopyTo(memoryStream);
    var bytes = memoryStream.ToArray();
    var base64 = Convert.ToBase64String(bytes);
    return base64;
  }

  private static Stream Load(string name)
  {
    var assembly = typeof(AssetQueries).Assembly;
    var fullName = $"{assembly.GetName().Name}.Assets.{name}";

    var stream =
      assembly.GetManifestResourceStream(fullName)
      ?? throw new InvalidOperationException(
        $"Resource {fullName} does not exist."
          + $" Here are the available resources for the given assembly"
          + $" '{assembly.GetName().Name}':{Environment.NewLine}"
          + string.Join(
            Environment.NewLine,
            assembly.GetManifestResourceNames()
          )
      );
    return stream;
  }
}

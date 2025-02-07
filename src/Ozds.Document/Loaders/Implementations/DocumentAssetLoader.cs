using System.Collections.Concurrent;
using System.Globalization;
using System.Text.Json;
using Ozds.Document.Loaders.Abstractions;

namespace Ozds.Document.Loaders.Implementations;

public class DocumentAssetLoader : IDocumentAssetLoader
{
  public Dictionary<string, string> LoadTranslations(CultureInfo culture)
  {
    return translationCache.GetOrAdd(culture, LoadTranslationsUncached);
  }

  public string LoadSvg(string name)
  {
    return svgCache.GetOrAdd(name, LoadSvgUncached);
  }

  private Dictionary<string, string> LoadTranslationsUncached(
    CultureInfo culture
  )
  {
    var cultureString = culture.TwoLetterISOLanguageName;
    var jsonStream = Load($"Translations.{cultureString}.json");
    return JsonSerializer.Deserialize<Dictionary<string, string>>(jsonStream)!;
  }

  private string LoadSvgUncached(string name)
  {
    var jsonStream = Load($"Images.{name}.svg");
    using var jsonStreamReader = new StreamReader(jsonStream);
    return jsonStreamReader.ReadToEnd();
  }

  private static Stream Load(string name)
  {
    var assembly = typeof(DocumentAssetLoader).Assembly;
    var fullName = $"{assembly.GetName().Name}.Assets.{name}";

    var stream = assembly.GetManifestResourceStream(fullName) ??
      throw new InvalidOperationException(
        $"Resource {fullName} does not exist."
        + $" Here are the available resources for the given assembly"
        + $" '{assembly.GetName().Name}':\n"
        + string.Join("\n", assembly.GetManifestResourceNames())
      );
    return stream;
  }

  private readonly ConcurrentDictionary<string, string> svgCache =
    new();

  private readonly
    ConcurrentDictionary<CultureInfo, Dictionary<string, string>>
    translationCache = new();
}

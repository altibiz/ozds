using System.Collections.Concurrent;
using System.Globalization;
using System.Text.Json;
using Ozds.Document.Loaders.Abstractions;

namespace Ozds.Document.Loaders.Implementations;

public class DocumentAssetLoader : IDocumentAssetLoader
{
  private readonly ConcurrentDictionary<string, string> fontCache =
    new();

  private readonly ConcurrentDictionary<string, string> svgCache =
    new();

  private readonly
    ConcurrentDictionary<CultureInfo, Dictionary<string, string>>
    translationCache = new();

  public Dictionary<string, string> LoadTranslations(CultureInfo culture)
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

  private Dictionary<string, string> LoadTranslationsUncached(
    CultureInfo culture
  )
  {
    var cultureString = culture.TwoLetterISOLanguageName;
    var stream = Load($"Translations.{cultureString}.json");
    return JsonSerializer.Deserialize<Dictionary<string, string>>(stream)!;
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
}

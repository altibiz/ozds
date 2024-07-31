using System.Globalization;
using System.Text.Json;
using Ozds.Business.Localization.Abstractions;

namespace Ozds.Business.Localization;

public class OzdsLocalizer : IOzdsLocalizer
{
  private static readonly Lazy<Dictionary<string, string>> _translations =
    new(() => LoadTranslations("translations-en.json"));

  private static readonly Lazy<Dictionary<string, string>> _translationsHR =
    new(() => LoadTranslations("translations-hr.json"));

  public string ForCulture(CultureInfo culture, string notLocalized)
  {
    Dictionary<string, string> activeTranslation = [];
    if (culture.Equals(CultureInfo.CreateSpecificCulture("en-US")))
    {
      activeTranslation = _translations.Value;
    }
    else if (culture.Equals(CultureInfo.CreateSpecificCulture("hr-HR")))
    {
      activeTranslation = _translationsHR.Value;
    }

    if (activeTranslation.Count > 0 &&
      activeTranslation.TryGetValue(notLocalized, out var value))
    {
      return value;
    }

    return notLocalized;
  }

  public string this[string notLocalized]
  {
    get { return ForCulture(CultureInfo.CurrentCulture, notLocalized); }
  }

  private static Dictionary<string, string> LoadTranslations(string fileName)
  {
    var jsonStream = Load(fileName);
    using var jsonStreamReader = new StreamReader(jsonStream);
    var jsonText = jsonStreamReader.ReadToEnd();
    return JsonSerializer.Deserialize<Dictionary<string, string>>(jsonText)!;
  }

  private static Stream Load(string name)
  {
    var assembly = typeof(OzdsLocalizer).Assembly;
    var fullName = $"{assembly.GetName().Name}.Assets.{name}";

    var stream = assembly.GetManifestResourceStream(fullName) ??
      throw new InvalidOperationException(
        $"Resource {fullName} does not exist. "
        + $"Here are the available resources for the given assembly '{
          assembly.GetName().Name
        }':\n"
        + string.Join("\n", assembly.GetManifestResourceNames())
      );
    return stream;
  }
}

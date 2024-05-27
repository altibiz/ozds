using System.Globalization;
using System.Text.Json;

namespace Ozds.Client;

public interface IOzdsComponentLocalizer
{
  string this[string notLocalized] { get; }
}

public class OzdsComponentLocalizer : IOzdsComponentLocalizer
{
  private static readonly Lazy<Dictionary<string, string>> _translations =
    new(() => LoadTranslations("translations-en.json"));

  private static readonly Lazy<Dictionary<string, string>> _translationsHR =
    new(() => LoadTranslations("translations-hr.json"));

  public string this[string notLocalized]
  {
    get
    {
      Dictionary<string, string> _activeTranslation = [];
      var culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
      var hrCulture = CultureInfo.CreateSpecificCulture("hr-HR").TwoLetterISOLanguageName;
      var enCulture = CultureInfo.CreateSpecificCulture("en-US").TwoLetterISOLanguageName;
      if (culture == enCulture)
      {
        _activeTranslation = _translations.Value;
      }
      else if (culture == hrCulture)
      {
        _activeTranslation = _translationsHR.Value;
      }

      if (_activeTranslation.Count > 0 &&
          _activeTranslation.TryGetValue(notLocalized, out var value))
      {
        return value;
      }

      return
        notLocalized;
    }
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
    var assembly = typeof(OzdsComponentLocalizer).Assembly;
    var fullName = $"{assembly.GetName().Name}.Assets.{name}";

    var stream = assembly.GetManifestResourceStream(fullName) ??
                 throw new InvalidOperationException(
                   $"Resource {fullName} does not exist. "
                   + $"Here are the available resources for the given assembly '{assembly.GetName().Name}':\n"
                   + string.Join("\n", assembly.GetManifestResourceNames())
                 );
    return stream;
  }
}

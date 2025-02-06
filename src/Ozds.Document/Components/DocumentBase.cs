using Microsoft.AspNetCore.Components;
using Ozds.Document.Loaders.Abstractions;

namespace Ozds.Document.Components;

public abstract class DocumentBase : ComponentBase
{
  [Inject]
  private IDocumentLocalizer Localizer { get; set; } = default!;

  [Inject]
  private IDocumentAssetLoader AssetLoader { get; set; } = default!;

  public string Translate(string notLocalized)
  {
    return Localizer.Translate(notLocalized);
  }

  public string Svg(string name)
  {
    return AssetLoader.LoadSvg(name);
  }

  public string DateString(DateTimeOffset date)
  {
    return date.ToString("dd.MM.yyyy.");
  }

  public string NumericString(decimal number, int precision = 2)
  {
    return number.ToString($"0.{new string('#', precision)}");
  }

  public string NumericString(double number, int precision = 2)
  {
    return number.ToString($"0.{new string('#', precision)}");
  }
}

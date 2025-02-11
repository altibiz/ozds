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

  public MarkupString Svg(string name)
  {
    var svg = AssetLoader.LoadSvg(name);
    return new MarkupString(svg);
  }

  public MarkupString TtfBase64(string name)
  {
    var ttf = AssetLoader.LoadTtfBase64(name);
    return new MarkupString(ttf);
  }

  public string Date(DateTimeOffset date)
  {
    return date.ToString("dd.MM.yyyy.");
  }

  public string Number(decimal number, int precision = 2)
  {
    return number.ToString($"0.{new string('#', precision)}");
  }
}

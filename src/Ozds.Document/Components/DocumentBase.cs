using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
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
    return new MarkupString(AssetLoader.LoadSvg(name));
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

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Ozds.Document.Loaders.Abstractions;

namespace Ozds.Document.Components;

public abstract class DocumentBase : ComponentBase
{
  [Inject]
  private IDocumentLocalizer Localizer { get; set; } = default!;

  [Inject]
  private IDocumentAssetLoader AssetLoader { get; set; } = default!;

  [Inject]
  private IHtmlHelper Html { get; set; } = default!;

  public IHtmlContent Translate(string notLocalized)
  {
    return new StringHtmlContent(Localizer.Translate(notLocalized));
  }

  public IHtmlContent Svg(string name)
  {
    return Html.Raw(AssetLoader.LoadSvg(name));
  }

  public IHtmlContent Date(DateTimeOffset date)
  {
    return new StringHtmlContent(date.ToString("dd.MM.yyyy."));
  }

  public IHtmlContent Number(decimal number, int precision = 2)
  {
    return new StringHtmlContent(
      number.ToString($"0.{new string('#', precision)}")
    );
  }
}

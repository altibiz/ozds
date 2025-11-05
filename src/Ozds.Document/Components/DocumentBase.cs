using Microsoft.AspNetCore.Components;
using Ozds.Assets;
using Ozds.Assets.Queries.Abstractions;

namespace Ozds.Document.Components;

public abstract class DocumentBase : ComponentBase
{
  [Inject]
  private ILocalizationQueries LocalizationQueries { get; set; } = default!;

  [Inject]
  private ICultureQueries CultureQueries { get; set; } = default!;

  [Inject]
  private IAssetQueries AssetLoader { get; set; } = default!;

  public string Translate(string notLocalized)
  {
    return LocalizationQueries.Translate(
      CultureQueries.CroatianCulture,
      notLocalized
    );
  }

  public string Translate(Type type)
  {
    return LocalizationQueries.Translate(
      CultureQueries.CroatianCulture,
      type
    );
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
    return LocalizationQueries.DocumentDate(date);
  }

  public string Number(decimal number, int precision = 2)
  {
    return LocalizationQueries.DocumentNumber(number, precision);
  }
}

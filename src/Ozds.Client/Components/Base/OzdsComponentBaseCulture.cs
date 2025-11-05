using System.Globalization;
using Microsoft.AspNetCore.Components;
using Ozds.Business.Queries;
using Ozds.Client.State;

namespace Ozds.Client.Components.Base;

public abstract partial class OzdsComponentBase : DisposableComponentBase
{
  [CascadingParameter]
  private CultureState CultureState { get; set; } = default!;

  protected string GetCultureString()
  {
    return LocalizationQueries.CultureToId(GetCulture());
  }

  protected CultureInfo GetCulture()
  {
    if (CultureState?.Culture is { } culture)
    {
      return culture;
    }

    return LocalizationQueries.EnglishCulture;
  }

  protected Task SetCulture(CultureInfo culture)
  {
    if (CultureState is { } cultureState)
    {
      return cultureState.SetCulture(culture);
    }

    return Task.CompletedTask;
  }
}

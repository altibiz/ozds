using Microsoft.AspNetCore.Components;
using Ozds.Business.Models.Abstractions;
using Ozds.Client.Components.Base;

namespace Ozds.Client.Components.Tables;

public partial class ResponsiveTable<T> : OzdsComponentBase
{
  private string? _searchString;

  [Parameter]
  public List<T> Model { get; set; } = default!;

  [Parameter]
  public RenderFragment<T>? Summary { get; set; } = default!;

  [Parameter]
  public RenderFragment<T>? Details { get; set; } = default!;

  [Parameter]
  public RenderFragment? Columns { get; set; } = default!;

  [Inject]
  private NavigationManager NavigationManager { get; set; } = default!;

  private bool Filter(T value)
  {
    if (string.IsNullOrWhiteSpace(_searchString))
    {
      return true;
    }

    if (value is IIdentifiable identifiable
      && identifiable.Title.Contains(
        _searchString,
        StringComparison.OrdinalIgnoreCase))
    {
      return true;
    }

    return false;
  }
}

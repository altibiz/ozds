using Microsoft.AspNetCore.Components;
using Ozds.Business.Models.Abstractions;
using Ozds.Client.Components.Base;

namespace Ozds.Client.Components.Tables;

public partial class ResponsiveTable<T> : OzdsComponentBase
{
  [Parameter]
  public List<T> Model { get; set; } = default!;

  [Parameter]
  public RenderFragment<T> Summary { get; set; } = default!;

  [Parameter]
  public RenderFragment<T> Details { get; set; } = default!;

  [Parameter]
  public RenderFragment Columns { get; set; } = default!;

  [Parameter]
  public Func<T, string> Link { get; set; } = default!;

  [Parameter]
  public RenderFragment<T>? Title { get; set; } = default!;

  private string? _searchString;

  private List<TableItemMetadata> _metadata = new();

  protected override void OnParametersSet()
  {
    _metadata = Model.Select(x => new TableItemMetadata(false)).ToList();
  }

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

  private sealed record TableItemMetadata(bool Expanded);
}

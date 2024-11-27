using Microsoft.AspNetCore.Components;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Client.Components.Streaming;

public partial class Table<T>
{
  [Parameter]
  public List<T> Model { get; set; } = default!;

  [Parameter]
  public RenderFragment Columns { get; set; } = default!;

  [Parameter]
  public RenderFragment<T> Summary { get; set; } = default!;

  [Parameter]
  public RenderFragment<T> Details { get; set; } = default!;

  [Parameter]
  public string Link { get; set; } = default!;

  private string? _searchString;

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

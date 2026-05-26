using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ozds.Client.Components.Base;

namespace Ozds.Client.Components.Fields;

public partial class ScrollablePreField : OzdsComponentBase
{
  private bool _expanded;

  [Parameter]
  public string? Content { get; set; }

  [Parameter]
  public int MaxHeight { get; set; } = 600;

  [Parameter]
  public bool ShowLineNumbers { get; set; }

  [Parameter]
  public EventCallback<bool> ExpandedChanged { get; set; }

  private string PaperStyle
  {
    get
    {
      return _expanded
        ? "overflow-y: auto; overscroll-behavior: none;"
        : $"max-height: {MaxHeight}px; overflow-y: auto; overscroll-behavior: none;";
    }
  }

  private string ExpandIcon
  {
    get
    {
      return _expanded
        ? Icons.Material.Filled.UnfoldLess
        : Icons.Material.Filled.UnfoldMore;
    }
  }

  private string ExpandTitle
  {
    get
    {
      return _expanded ? Translate("Collapse") : Translate("Expand");
    }
  }

  private string LineNumbers
  {
    get
    {
      if (string.IsNullOrEmpty(Content))
      {
        return string.Empty;
      }

      var count = 1;
      foreach (var c in Content)
      {
        if (c == '\n')
        {
          count++;
        }
      }

      return string.Join('\n', Enumerable.Range(1, count));
    }
  }

  private async Task ToggleExpanded()
  {
    _expanded = !_expanded;
    await ExpandedChanged.InvokeAsync(_expanded);
  }
}

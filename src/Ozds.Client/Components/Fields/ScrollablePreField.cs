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
  public bool WrapText { get; set; }

  [Parameter]
  public bool ShowLineNumbers { get; set; } = true;

  [Parameter]
  public EventCallback<bool> ExpandedChanged { get; set; }

  private string[] _lines = [];

  private string[] Lines => _lines;

  protected override void OnParametersSet()
  {
    _lines = Content is null
      ? []
      : Content.ReplaceLineEndings("\n").Split('\n');
  }

  private string ContainerVars
  {
    get
    {
      var digits = Lines.Length.ToString().Length;
      return $"overflow: hidden; --spf-max-height: {MaxHeight}px;"
        + $" --spf-gutter: {digits}ch;";
    }
  }

  private string LineCountLabel
  {
    get
    {
      var count = Lines.Length;
      var unit = count == 1 ? Translate("line") : Translate("lines");
      return $"{count} {unit}";
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
    get { return _expanded ? Translate("Collapse") : Translate("Expand"); }
  }

  private async Task ToggleExpanded()
  {
    _expanded = !_expanded;
    await ExpandedChanged.InvokeAsync(_expanded);
  }
}

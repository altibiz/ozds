using Microsoft.AspNetCore.Components;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Client.Components.Charts;

public partial class ChartControls
{
  [Parameter]
  public List<IMeter> Meters { get; set; } = default!;

  [Parameter]
  public RenderFragment<ChartParameters> ChildContent { get; set; } = default!;

  private ChartParameters _parameters = new();
}

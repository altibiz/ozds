using Microsoft.AspNetCore.Components;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Client.Components.Charts;

public partial class MeasurementChartControls
{
  [Parameter]
  public List<IMeter> Meters { get; set; } = default!;

  [Parameter]
  public HashSet<MeasurementChartProfile> Profiles { get; set; } = new();

  [Parameter]
  public RenderFragment<MeasurementChartParameters> ChildContent { get; set; } = default!;

  private MeasurementChartParameters _parameters = new();
}

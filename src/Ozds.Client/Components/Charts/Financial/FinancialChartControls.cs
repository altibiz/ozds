using Microsoft.AspNetCore.Components;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Client.Components.Charts;

public partial class FinancialChartControls
{
  [Parameter]
  public List<IMeter> Meters { get; set; } = default!;

  [Parameter]
  public HashSet<FinancialChartProfile> Profiles { get; set; } = new();

  [Parameter]
  public RenderFragment<FinancialChartParameters> ChildContent { get; set; } = default!;

  private FinancialChartParameters _parameters = new();
}

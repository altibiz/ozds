using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ozds.Business.Models.Abstractions;
using Ozds.Client.Components.Base;

namespace Ozds.Client.Components.Charts;

public partial class MeasurementHeatMapChart : OzdsComponentBase
{
  [Parameter]
  public MeasurementChartParameters Parameters { get; set; } = default!;

  private List<ChartSeries> ChartSeries =>
  [
    new()
    {
      Name = Translate("Measurement count"),
      Data = Parameters.Measurements.Items
        .OfType<IAggregate>()
        .Select(x => (double)x.Count)
        .ToArray()
    }
  ];

  private ChartOptions ChartOptions => new()
  {
    ShowLabels = false,
  };
}

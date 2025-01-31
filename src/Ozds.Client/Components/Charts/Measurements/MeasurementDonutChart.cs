using ApexCharts;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Client.Components.Base;
using Ozds.Client.Extensions;
using Ozds.Client.State;

namespace Ozds.Client.Components.Charts;

public partial class MeasurementDonutChart : OzdsComponentBase
{
  private readonly string _id = Guid.NewGuid().ToString();

  private ApexChart<IMeasurement>? _chart;

  private ApexChartOptions<IMeasurement> _options =
    new ApexChartOptions<IMeasurement>()
      .WithFixedScriptPath();

  [CascadingParameter]
  public Breakpoint Breakpoint { get; set; }

  [CascadingParameter]
  public ThemeState ThemeState { get; set; } = default!;

  [Parameter]
  public MeasurementChartParameters Parameters { get; set; } = default!;

  [Parameter]
  public bool Area { get; set; }

  [Parameter]
  public bool Brush { get; set; }

  protected override void OnInitialized()
  {
    _options = CreateGraphOptions();
  }

  protected override async Task OnInitializedAsync()
  {
    await OnParametersSetAsync();
  }

  protected override async Task OnParametersSetAsync()
  {
    _options = CreateGraphOptions();
    if (_chart is { } chart)
    {
      await chart.UpdateSeriesAsync();
      await chart.UpdateOptionsAsync(false, true, false);
    }
  }

  private ApexChartOptions<IMeasurement> CreateGraphOptions()
  {
    var options = _options;
    options.Chart.Id = _id;

    var maxPower = Parameters.Measurements.Items
      .Select(x => x.ActivePower_W.TariffUnary().DuplexImport().PhaseSum())
      .OrderByDescending(x => x)
      .Cast<decimal?>()
      .FirstOrDefault();
    if (Parameters.Measure == MeasureModel.ActivePower
      && Parameters.Meters.Count == 1)
    {
      options = _options.WithActivePower(
        $"{Parameters.Meters.First().Id} {Translate("CONNECTION POWER")}",
        Parameters.Meters.First().ConnectionPower_W,
        maxPower
      );
    }

    var measure =
      $"{Translate(Parameters.Measure.ToTitle())}"
      + $" ({Parameters.Measure.ToUnit()})";
    options = Breakpoint <= Breakpoint.Sm
      ? options.WithSmAndDown(measure)
      : options.WithMdAndUp(measure);

    var timeSpan = Parameters.Resolution
      .ToTimeSpan(
        Parameters.Multiplier,
        DateTimeOffset.UtcNow);
    if (timeSpan.TotalDays > 1)
    {
      options = options.WithShortDate();
    }
    else
    {
      options = options.WithLongDate();
    }

    options = options.WithColorMode(ThemeState.IsDarkMode);

    return options;
  }
}

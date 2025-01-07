using ApexCharts;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Client.Components.Base;
using Ozds.Client.Extensions;

namespace Ozds.Client.Components.Charts;

public partial class MeasurementLineChart : OzdsComponentBase
{
  [CascadingParameter]
  public Breakpoint Breakpoint { get; set; } = default!;

  [Parameter]
  public MeasurementChartParameters Parameters { get; set; } = default!;

  [Parameter]
  public bool Area { get; set; } = false;

  [Parameter]
  public bool Brush { get; set; } = false;

  private readonly string _id = Guid.NewGuid().ToString();

  private ApexChart<IMeasurement>? _chart = default!;

  private ApexChart<IMeasurement>? _brushChart = default!;

  private ApexChartOptions<IMeasurement> _options =
    new ApexChartOptions<IMeasurement>()
      .WithFixedScriptPath();

  private ApexChartOptions<IMeasurement> _brushOptions =
    new ApexChartOptions<IMeasurement>()
      .WithFixedScriptPath();

  protected override void OnInitialized()
  {
    _options = CreateGraphOptions();
    _brushOptions = CreateBrushOptions();
  }

  protected override async Task OnParametersSetAsync()
  {
    _options = CreateGraphOptions();
    if (_chart is { } chart)
    {
      await chart.UpdateSeriesAsync(animate: true);
      await chart.UpdateOptionsAsync(false, true, false);
    }

    _brushOptions = CreateBrushOptions();
    if (_brushChart is { } brushChart)
    {
      await brushChart.UpdateSeriesAsync(animate: true);
      await brushChart.UpdateOptionsAsync(false, true, false);
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

    if (Area)
    {
      options = options.WithArea();
    }

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

    return options;
  }

  private ApexChartOptions<IMeasurement> CreateBrushOptions()
  {
    var options = _brushOptions;

    options.WithBrush(_id);

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

    return options;
  }
}

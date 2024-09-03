using ApexCharts;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Pushing.Abstractions;
using Ozds.Business.Queries.Abstractions;
using Ozds.Business.Queries.Agnostic;
using Ozds.Client.Base;

namespace Ozds.Client.Shared.Charts;

public partial class NewMeterGraph : OzdsOwningComponentBase
{
  [Parameter]
  public IMeter Model { get; set; } = default!;

  [Parameter]
  public DateTimeOffset Timestamp { get; set; } = default!;

  [Parameter]
  public ChartMeasure Measure { get; set; } = ChartMeasure.ActivePower;

  [Parameter]
  public ChartResolution Resolution { get; set; } = ChartResolution.Minute;

  [Parameter]
  public HashSet<PhaseModel> Phases { get; set; } =
    Enum.GetValues<PhaseModel>().ToHashSet();

  [Parameter]
  public int Multiplier { get; set; } = 1;

  [Parameter]
  public bool Refresh { get; set; } = true;

  [CascadingParameter]
  public Breakpoint Breakpoint { get; set; } = default!;

  [Inject]
  public IMeasurementSubscriber MeasurementSubscriber { get; set; } = default!;

  private ApexChart<IMeasurement>? _chart = default!;

  private PaginatedList<IMeasurement> _measurements = new(new(), 0);

  private ApexChartOptions<IMeasurement> _options =
    NewApexChartOptions<IMeasurement>();

  protected override void OnInitialized()
  {
    MeasurementSubscriber.OnAfterPublish += OnAfterMeasurementsPublished;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      MeasurementSubscriber.OnAfterPublish -= OnAfterMeasurementsPublished;
    }
  }

  protected override void OnParametersSet()
  {
    var now = DateTimeOffset.UtcNow;
    if (Timestamp == default)
    {
      Timestamp = now.Subtract(Resolution.ToTimeSpan(Multiplier, now));
    }
    _options = CreateGraphOptions();
  }

  protected override async Task OnParametersSetAsync()
  {
    _measurements = await LoadAsync();
  }

  protected override async Task OnAfterRenderAsync(bool firstRender)
  {
    if (firstRender)
    {
      return;
    }

    _measurements = await LoadAsync();

    if (_chart is not null)
    {
      await _chart.UpdateSeriesAsync(true);
    }
  }

  private void OnAfterMeasurementsPublished(object? _sender, MeasurementPublishEventArgs args)
  {
    if (!Refresh)
    {
      return;
    }

    var now = DateTimeOffset.UtcNow;
    Timestamp = now.Subtract(Resolution.ToTimeSpan(Multiplier, now));

    _options = CreateGraphOptions();
    InvokeAsync(StateHasChanged);
  }

  private async Task<PaginatedList<IMeasurement>> LoadAsync()
  {
    var timeSpan = Resolution.ToTimeSpan(Multiplier, Timestamp);
    var appropriateInterval = QueryConstants
      .AppropriateInterval(timeSpan, Timestamp);

    if (appropriateInterval is null)
    {
      var measurementQueries = ScopedServices
        .GetRequiredService<OzdsMeasurementQueries>();
      var measurements = await measurementQueries.ReadDynamic(
        Timestamp,
        Timestamp.Add(timeSpan),
        meterId: Model.Id
      );
      return measurements;
    }

    var aggregateQueries = ScopedServices
      .GetRequiredService<OzdsAggregateQueries>();
    var aggregates = await aggregateQueries.ReadDynamic(
      Timestamp,
      Timestamp.Add(timeSpan),
      interval: appropriateInterval,
      meterId: Model.Id
    );
    var casted = new PaginatedList<IMeasurement>(
      aggregates.Items.Cast<IMeasurement>().ToList(),
      aggregates.TotalCount
    );
    return casted;
  }

  private ApexChartOptions<IMeasurement> CreateGraphOptions()
  {
    if (Breakpoint <= Breakpoint.Sm)
    {
      var options = CreateSmAndDownGraphOptions(
        Resolution,
        Timestamp,
        Multiplier
      );
      options = SetPowerAnnotationGraphOptions(
        options,
        T["CONNECTION POWER"],
        Model.ConnectionPower_W,
        _measurements.Items
          .OrderByDescending(m => m.ActivePower_W.TariffUnary().DuplexImport().PhaseSum())
          .FirstOrDefault()
          ?.ActivePower_W.TariffUnary().DuplexImport().PhaseSum());
      options = SetSmAndDownTimeRangeGraphOptions(
        options,
        Resolution,
        Timestamp,
        Multiplier
      );
      return options;
    }
    else
    {
      var options = CreateMdAndUpGraphOptions(
        Resolution,
        Timestamp,
        Multiplier
      );
      options = SetPowerAnnotationGraphOptions(
        options,
        T["CONNECTION POWER"],
        Model.ConnectionPower_W,
        _measurements.Items
          .OrderByDescending(m => m.ActivePower_W.TariffUnary().DuplexImport().PhaseSum())
          .FirstOrDefault()
          ?.ActivePower_W.TariffUnary().DuplexImport().PhaseSum());
      options = SetMdAndUpTimeRangeGraphOptions(
        options,
        Resolution,
        Timestamp,
        Multiplier
      );
      return options;
    }
  }

  private static ApexChartOptions<IMeasurement> SetSmAndDownTimeRangeGraphOptions(
    ApexChartOptions<IMeasurement> options,
    ChartResolution resolution,
    DateTimeOffset timestamp,
    int multiplier
  )
  {
    options.Xaxis = new XAxis
    {
      Labels = new XAxisLabels { Show = false },
      Range = resolution.ToTimeSpan(multiplier, timestamp).TotalMilliseconds
    };

    return options;
  }

  private static ApexChartOptions<IMeasurement> SetMdAndUpTimeRangeGraphOptions(
    ApexChartOptions<IMeasurement> options,
    ChartResolution resolution,
    DateTimeOffset timestamp,
    int multiplier
  )
  {
    options.Xaxis = new XAxis
    {
      Type = XAxisType.Datetime,
      AxisTicks = new AxisTicks(),
      Range = resolution.ToTimeSpan(multiplier, timestamp).TotalMilliseconds
    };

    return options;
  }

  private static ApexChartOptions<IMeasurement> SetPowerAnnotationGraphOptions(
    ApexChartOptions<IMeasurement> options,
    string label,
    decimal connectionPower,
    decimal? maxPower)
  {
    var annotation = CreateYAxisAnnotations(label, connectionPower);

    if (maxPower is null)
    {
      options.Yaxis.Clear();
      options.Yaxis.Add(
        new YAxis
        {
          Max = maxPower * 1.5M,
          Labels = new YAxisLabels
          {
            Formatter = "function(val, index) { return (val ?? 0).toFixed(0); }"
          }
        });
      options.Annotations = new Annotations
      {
        Yaxis = new List<AnnotationsYAxis> { annotation }
      };
    }
    else
    {
      options.Annotations = new Annotations();
      options.Yaxis.Clear();
      options.Yaxis.Add(
        new YAxis
        {
          Labels = new YAxisLabels
          {
            Formatter = "function(val, index) { return (val ?? 0).toFixed(0); }"
          }
        });
    }

    return options;
  }

  private static ApexChartOptions<IMeasurement> CreateSmAndDownGraphOptions(
    ChartResolution resolution,
    DateTimeOffset timestamp,
    int multiplier
  )
  {
    var options = NewApexChartOptions<IMeasurement>();
    options.Grid = new Grid
    {
      BorderColor = "#e7e7e7",
      Row = new GridRow
      {
        Colors = new List<string> { "#f3f3f3", "transparent" },
        Opacity = 0.5d
      }
    };
    options.Tooltip = new Tooltip
    {
      X = new TooltipX { Format = @"HH:mm:ss" }
    };
    options.Yaxis = new List<YAxis>();
    options.Yaxis.Add(
      new YAxis
      {
        Labels = new YAxisLabels
        {
          Formatter = "function(val, index) { return (val ?? 0).toFixed(0); }"
        }
      });
    options.Xaxis = new XAxis();
    options.Xaxis = new XAxis
    {
      Labels = new XAxisLabels { Show = false },
      Range = resolution.ToTimeSpan(multiplier, timestamp).TotalMilliseconds
    };
    options.Chart = new Chart
    {
      Toolbar = new Toolbar
      {
        Tools = new Tools
        {
          Zoomin = false,
          Zoomout = false,
          Download = false,
          Pan = false,
          Selection = false
        }
      }
    };

    return options;
  }

  private static ApexChartOptions<IMeasurement> CreateMdAndUpGraphOptions(
    ChartResolution resolution,
    DateTimeOffset timestamp,
    int multiplier
  )
  {
    var options = NewApexChartOptions<IMeasurement>();
    options.Grid = new Grid
    {
      BorderColor = "#e7e7e7",
      Row = new GridRow
      {
        Colors = new List<string> { "#f3f3f3", "transparent" },
        Opacity = 0.5d
      }
    };
    options.Chart = new Chart
    {
      Toolbar = new Toolbar
      {
        Tools = new Tools
        {
          Zoomin = false,
          Zoomout = false,
          Zoom = false,
          Download = false,
          Pan = true,
          Selection = false
        }
      }
    };
    options.Tooltip = new Tooltip
    {
      X = new TooltipX { Format = @"HH:mm:ss" }
    };
    options.Yaxis = new List<YAxis>();
    options.Yaxis.Add(
      new YAxis
      {
        Labels = new YAxisLabels
        {
          Formatter = "function(val, index) { return (val ?? 0).toFixed(0); }"
        }
      });
    options.Xaxis = new XAxis();
    options.Xaxis = new XAxis
    {
      Type = XAxisType.Datetime,
      AxisTicks = new AxisTicks(),
      Range = resolution.ToTimeSpan(multiplier, timestamp).TotalMilliseconds
    };

    return options;
  }

  private static AnnotationsYAxis CreateYAxisAnnotations(
    string label,
    decimal connectionPower
  )
  {
    var annotations = new AnnotationsYAxis
    {
      Label = new Label
      {
        Text = label,
        Style = new Style
        {
          Background = "red",
          Color = "white",
          FontSize = "12px"
        }
      },
      Y = connectionPower * 3,
      BorderColor = "red",
      StrokeDashArray = 0
    };

    return annotations;
  }
}

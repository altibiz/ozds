using ApexCharts;

namespace Ozds.Client.Extensions;

// TODO: multi power annotation

public static class ApexChartOptionsExtensions
{
  public static ApexChartOptions<T> WithFixedScriptPath<T>(
    this ApexChartOptions<T> options
  )
    where T : class
  {
    options.Blazor = new ApexChartsBlazorOptions
    {
      JavascriptPath = "/_content/Blazor-ApexCharts/js/blazor-apexcharts.js"
    };
    return options;
  }

  public static ApexChartOptions<T> WithArea<T>(
    this ApexChartOptions<T> options
  )
    where T : class
  {
    options.Chart.Type = ChartType.Area;
    return options;
  }

  public static ApexChartOptions<T> WithShortDate<T>(
    this ApexChartOptions<T> options
  )
    where T : class
  {
    options.Tooltip = new Tooltip { X = new TooltipX { Format = @"dd.MM." } };
    return options;
  }

  public static ApexChartOptions<T> WithLongDate<T>(
    this ApexChartOptions<T> options
  )
    where T : class
  {
    options.Tooltip = new Tooltip
    {
      X = new TooltipX { Format = @"dd.MM. HH:mm" }
    };
    return options;
  }

  public static ApexChartOptions<T> WithSmAndDown<T>(
    this ApexChartOptions<T> options,
    string measure
  )
    where T : class
  {
    options.Chart.Toolbar = new Toolbar
    {
      Tools = new Tools
      {
        Zoomin = false,
        Zoomout = false,
        Zoom = false,
        Download = false,
        Pan = false,
        Selection = false,
        Reset = false
      }
    };

    options.Grid = new Grid
    {
      BorderColor = "#e7e7e7",
      Row = new GridRow
      {
        Colors = new List<string> { "#ddeeff", "transparent" },
        Opacity = 0.5d
      }
    };

    options.Tooltip = new Tooltip
    {
      X = new TooltipX { Format = @"HH:mm:ss" },
      Y = new TooltipY
      {
        Title = new TooltipYTitle
        {
          Formatter = $"function(name) {{ return '{measure} ' + name; }}"
        }
      }
    };

    options.Yaxis =
    [
      new YAxis
      {
        Labels = new YAxisLabels
        {
          Formatter = "function(val, index) { return (val ?? 0).toFixed(0); }"
        }
      }
    ];

    options.Xaxis = new XAxis
    {
      Labels = new XAxisLabels { Show = false },
    };

    return options;
  }

  public static ApexChartOptions<T> WithMdAndUp<T>(
    this ApexChartOptions<T> options,
    string measure
  )
    where T : class
  {
    options.Chart.Toolbar = new Toolbar
    {
      Tools = new Tools
      {
        Zoomin = false,
        Zoomout = false,
        Zoom = false,
        Download = false,
        Pan = false,
        Selection = false,
        Reset = false
      }
    };

    options.Grid = new Grid
    {
      BorderColor = "#e7e7e7",
      Row = new GridRow
      {
        Colors = new List<string> { "#ddeeff", "transparent" },
        Opacity = 0.5d
      }
    };

    options.Tooltip = new Tooltip
    {
      X = new TooltipX { Format = @"HH:mm:ss" },
      Y = new TooltipY
      {
        Title = new TooltipYTitle
        {
          Formatter = $"function(name) {{ return '{measure} ' + name; }}"
        }
      }
    };

    options.Yaxis =
    [
      new YAxis
      {
        Labels = new YAxisLabels
        {
          Formatter = "function(val, index) { return (val ?? 0).toFixed(0); }"
        }
      }
    ];

    options.Xaxis = new XAxis
    {
      Type = XAxisType.Datetime,
      AxisTicks = new AxisTicks(),
    };

    return options;
  }

  public static ApexChartOptions<T> ForBarSum<T>(
    this ApexChartOptions<T> options
  )
    where T : class
  {
    options.Yaxis[0].Title = new AxisTitle { Text = "kW" };
    options.Yaxis.Add(new YAxis
    {
      Title = new AxisTitle { Text = "kWh" },
      SeriesName = "Active Power",
      DecimalsInFloat = 0,
      Opposite = true
    });
    return options;
  }

  public static ApexChartOptions<T> WithActivePower<T>(
    this ApexChartOptions<T> options,
    string label,
    decimal connectionPower,
    decimal? maxPower
  )
    where T : class
  {
    if (maxPower is null)
    {
      options.Yaxis =
      [
        new YAxis
        {
          Max = maxPower * 1.5M,
          Labels = new YAxisLabels
          {
            Formatter =
              "function(val, index) { return (val ?? 0).toFixed(0); }"
          }
        }
      ];

      options.Annotations = new Annotations
      {
        Yaxis = [
          new AnnotationsYAxis
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
          }
        ]
      };
    }
    else
    {
      options.Annotations = new Annotations();

      options.Yaxis =
      [
        new YAxis
        {
          Labels = new YAxisLabels
          {
            Formatter =
              "function(val, index) { return (val ?? 0).toFixed(0); }"
          }
        }
      ];
    }

    return options;
  }
}

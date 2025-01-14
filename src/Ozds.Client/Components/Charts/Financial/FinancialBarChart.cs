using ApexCharts;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Client.Components.Base;
using Ozds.Client.Extensions;
using Ozds.Client.State;

namespace Ozds.Client.Components.Charts;

public partial class FinancialBarChart : OzdsComponentBase
{
  [Parameter]
  public FinancialChartParameters Parameters { get; set; } = default!;

  [Parameter]
  public bool Area { get; set; }

  [CascadingParameter]
  private Breakpoint Breakpoint { get; set; } = default!;

  [CascadingParameter]
  private ThemeState ThemeState { get; set; } = default!;

  private readonly string _id = Guid.NewGuid().ToString();

  private ApexChart<IFinancial>? _chart = default!;

  private ApexChartOptions<IFinancial> _options =
    new ApexChartOptions<IFinancial>()
      .WithFixedScriptPath();

  protected override void OnInitialized()
  {
    _options = CreateGraphOptions();
  }

  protected override async Task OnParametersSetAsync()
  {
    _options = CreateGraphOptions();
    if (_chart is { } chart)
    {
      await chart.UpdateSeriesAsync(animate: true);
      await chart.UpdateOptionsAsync(false, true, false);
    }
  }

  private ApexChartOptions<IFinancial> CreateGraphOptions()
  {
    var options = _options;
    options.Chart.Id = _id;

    options = Breakpoint <= Breakpoint.Sm
      ? options.WithSmAndDown(Translate("finance"))
      : options.WithMdAndUp(Translate("finance"));

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

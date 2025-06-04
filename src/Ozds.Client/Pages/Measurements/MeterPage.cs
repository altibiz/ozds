using System.Globalization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ozds.Business.Analysis;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Business.Queries;
using Ozds.Client.Components.Models.Base;
using Ozds.Client.State;

namespace Ozds.Client.Pages;

public partial class MeterPage
  : OzdsIdentifiableModelPageComponentBase<IMeter>
{
  private DateTime selectedMonth =
    // NOTE: just so something is there
    DateTimeOffset.Parse(
      "2000-01-01T00:00:00Z",
      CultureInfo.InvariantCulture).DateTime;

  private ObisModel selectedObis = ObisModel.ActiveEnergyTotalImportT1_kWh;

  [CascadingParameter]
  private AnalysisState AnalysisState { get; set; } = default!;

  [Parameter]
  public string Id { get; set; } = default!;

  [CascadingParameter]
  private RepresentativeState RepresentativeState { get; set; } = default!;

  [CascadingParameter]
  private Breakpoint Breakpoint { get; set; }

  [Inject]
  private Analyzer Analyzer { get; set; } = default!;

  [Inject]
  private ClockQueries ClockQueries { get; set; } = default!;

  [Inject]
  private TimeQueries TimeQueries { get; set; } = default!;

  protected override void OnInitialized()
  {
    var now = ClockQueries.Now();
    selectedMonth = TimeQueries.GetStartOfLastMonth(now).DateTime;
  }
}

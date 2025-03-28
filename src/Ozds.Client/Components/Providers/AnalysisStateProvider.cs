using Microsoft.AspNetCore.Components;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Composite;
using Ozds.Business.Models.Enums;
using Ozds.Business.Queries;
using Ozds.Business.Time;
using Ozds.Client.Components.Base;
using Ozds.Client.State;

namespace Ozds.Client.Components.Providers;

public partial class AnalysisStateProvider : OzdsComponentBase
{
  private string? _previousLocationId;

  private string? _previousRepresentativeId;

  private AnalysisState? _state;

  [Parameter]
  public RenderFragment ChildContent { get; set; } = default!;

  [CascadingParameter]
  private RepresentativeState RepresentativeState { get; set; } = default!;

  [CascadingParameter]
  private LocationState LocationState { get; set; } = default!;

  [Inject]
  private ILogger<AnalysisStateProvider> Logger { get; set; } = default!;

  protected override void OnParametersSet()
  {
    if (_previousRepresentativeId == RepresentativeState.Representative.Id
      && _previousLocationId == LocationState.Location?.Id)
    {
      return;
    }

    _previousRepresentativeId = RepresentativeState.Representative.Id;
    _previousLocationId = LocationState.Location?.Id;

    _state = new AnalysisState(
      new Lazy<List<AnalysisBasisModel>>(
        () =>
        {
          Task.Run(
            async () =>
            {
              try
              {
                await FetchAnalysisBasesAsync();
              }
              catch (Exception ex)
              {
                Logger.LogError(ex, "Error setting analysis bases");
              }
            });

          return new List<AnalysisBasisModel>();
        })
    );
  }

  private async Task FetchAnalysisBasesAsync()
  {
    var measurementLocationQueries = ScopedServices
      .GetRequiredService<MeasurementLocationQueries>();
    var measurementQueries = ScopedServices
      .GetRequiredService<MeasurementQueries>();
    var financialQueries = ScopedServices
      .GetRequiredService<FinancialQueries>();

    var now = DateTimeOffset.UtcNow;
    var startOfMonthLastYear = now.GetStartOfMonthLastYear();

    var analysisBases = await measurementLocationQueries
      .ReadAnalysisBasisByLocationAndRepresentative(
        LocationState.Location?.Id,
        RepresentativeState.Representative,
        startOfMonthLastYear,
        now,
        CancellationToken
      );

    _state = new AnalysisState(
      new Lazy<List<AnalysisBasisModel>>(() => analysisBases));
    await InvokeAsync(StateHasChanged);

    var monthlyAggregates = await measurementQueries
      .ReadByMeasurementLocationIds(
        analysisBases.Select(x => x.MeasurementLocation.Id),
        IntervalModel.Month,
        startOfMonthLastYear,
        now,
        0,
        CancellationToken,
        analysisBases.Count * 12
      );
    foreach (var analysisBasis in analysisBases)
    {
      analysisBasis.MonthlyAggregates = monthlyAggregates
        .Items
        .Where(
          x => x.MeasurementLocationId
            == analysisBasis.MeasurementLocation.Id)
        .OfType<AggregateModel>()
        .ToList();
    }

    _state = new AnalysisState(
      new Lazy<List<AnalysisBasisModel>>(() => analysisBases));
    await InvokeAsync(StateHasChanged);

    var lastMeasurements = await measurementQueries
      .ReadByMeasurementLocationIdsLast(
        analysisBases.Select(x => x.MeasurementLocation.Id),
        CancellationToken
      );
    foreach (var analysisBasis in analysisBases)
    {
      analysisBasis.LastMeasurement = lastMeasurements
          .FirstOrDefault(
            x => x.MeasurementLocationId
              == analysisBasis.MeasurementLocation.Id)
        as MeasurementModel;
    }

    _state = new AnalysisState(
      new Lazy<List<AnalysisBasisModel>>(() => analysisBases));
    await InvokeAsync(StateHasChanged);

    var financials = await financialQueries
      .ReadByMeasurementLocationIds(
        analysisBases.Select(x => x.MeasurementLocation.Id),
        startOfMonthLastYear,
        now,
        0,
        CancellationToken,
        analysisBases.Count * 12
      );
    foreach (var analysisBasis in analysisBases)
    {
      analysisBasis.Calculations = financials
        .Items
        .OfType<INetworkUserCalculation>()
        .Where(
          x => x.NetworkUserMeasurementLocationId
            == analysisBasis.MeasurementLocation.Id)
        .OfType<CalculationModel>()
        .ToList();
      analysisBasis.Invoices = financials
        .Items
        .OfType<INetworkUserInvoice>()
        .Where(
          x => x.NetworkUserId
            == analysisBasis.NetworkUser?.Id)
        .OfType<InvoiceModel>()
        .ToList();
    }

    _state = new AnalysisState(
      new Lazy<List<AnalysisBasisModel>>(() => analysisBases));
    await InvokeAsync(StateHasChanged);
  }
}

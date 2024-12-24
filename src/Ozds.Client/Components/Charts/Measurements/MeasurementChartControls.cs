using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Ozds.Business.Aggregation.Agnostic;
using Ozds.Business.Aggregation.Base;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
using Ozds.Business.Queries.Abstractions;
using Ozds.Business.Queries.Agnostic;
using Ozds.Client.Components.Base;

namespace Ozds.Client.Components.Charts;

#pragma warning disable S3881 // "IDisposable" should be implemented correctly
public partial class MeasurementChartControls : OzdsOwningComponentBase
#pragma warning restore S3881 // "IDisposable" should be implemented correctly
{
  [Parameter]
  public List<IMeter> Meters { get; set; } = new();

  [Parameter]
  public List<IMeasurementLocation> MeasurementLocations { get; set; } = new();

  [Parameter]
  public HashSet<MeasurementChartProfile> Profiles { get; set; } = new();

  [Parameter]
  public RenderFragment<MeasurementChartParameters> ChildContent { get; set; } = default!;

  [Inject]
  private IMeasurementFinalizeSubscriber MeasurementSubscriber { get; set; } = default!;

  [Inject]
  private AgnosticAggregateUpserter AggregateUpserter { get; set; } = default!;

  private readonly MeasurementChartParameters _parameters = new();

  protected override void OnInitialized()
  {
    MeasurementSubscriber.SubscribeFinalize(OnUpsert);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      MeasurementSubscriber.UnsubscribeFinalize(OnUpsert);
    }
  }

  protected override async Task OnParametersSetAsync()
  {
    var queries = ScopedServices.GetRequiredService<MeasurementQueries>();

    var fromDate = _parameters.FromDate;
    var toDate = fromDate.Subtract(_parameters.Resolution
      .ToTimeSpan(_parameters.Multiplier, fromDate));
    _parameters.Measurements = await queries.ReadByMeterIdsDynamic(
      Meters,
      _parameters.Resolution,
      _parameters.Multiplier,
      pageNumber: 1,
      cancellationToken: CancellationToken.None,
      fromDate: fromDate,
      toDate: toDate
    );
  }

  private void OnUpsert(
    object? _sender,
    MeasurementFinalizeEventArgs args)
  {
    if (!_parameters.Refresh)
    {
      return;
    }

    InvokeAsync(() =>
    {
      var now = DateTimeOffset.UtcNow;
      var timestamp = _parameters.Measurements.Items.LastOrDefault()?.Timestamp ?? now;
      var timeSpan = _parameters.Resolution
        .ToTimeSpan(_parameters.Multiplier, timestamp);
      var appropriateInterval = QueryConstants
        .AppropriateInterval(timeSpan, now);

      if (appropriateInterval is null)
      {
        var newMeasurements = args.Measurements
          .Where(x => x.Timestamp >= timestamp)
          .Where(x => Meters.Exists(meter =>
            meter.Id == x.MeterId))
          .OrderBy(x => x.Timestamp)
          .ToList();
        var concatenated = _parameters.Measurements.Items
          .Concat(newMeasurements)
          .ToList();
        _parameters.Measurements = new PaginatedList<IMeasurement>(
          concatenated,
          _parameters.Measurements.TotalCount + newMeasurements.Count
        );
      }
      else
      {
        var newAggregates = args.Aggregates
          .Where(x => x.Timestamp >= timestamp)
          .Where(x => x.Interval == appropriateInterval)
          .Where(x => Meters.Exists(meter =>
            meter.Id == x.MeterId))
          .OrderBy(x => x.Timestamp)
          .OfType<IAggregate>()
          .ToList();
        var aggregated = _parameters.Measurements.Items.OfType<IAggregate>()
          .Concat(newAggregates)
          .GroupBy(x => x.Timestamp)
          .Select(x => x
            .Aggregate(AggregateUpserter.UpsertModelAgnostic))
          .OfType<IMeasurement>()
          .ToList();
        _parameters.Measurements = new PaginatedList<IMeasurement>(
          aggregated.ToList(),
          _parameters.Measurements.TotalCount - _parameters.Measurements.Items.Count + aggregated.Count
        );
      }

      StateHasChanged();
    });
  }
}

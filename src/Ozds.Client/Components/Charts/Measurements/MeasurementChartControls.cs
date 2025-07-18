using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ozds.Business.Aggregation;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Business.Naming;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
using Ozds.Business.Queries;
using Ozds.Business.Queries.Abstractions;
using Ozds.Client.Components.Base;

namespace Ozds.Client.Components.Charts;

public partial class MeasurementChartControls : OzdsComponentBase
{
  private MeasurementChartParameters _parameters = new();

  private MudSelect<string> _select = default!;

  [Parameter]
  public List<IMeter> Meters { get; set; } = new();

  [Parameter]
  public List<IMeasurementLocation> MeasurementLocations { get; set; } = new();

  [Parameter]
  public HashSet<MeasurementChartProfile> Profiles { get; set; } = new();

  [Parameter]
  public RenderFragment<MeasurementChartParameters> ChildContent { get; set; } =
    default!;

  [Inject]
  private IDataModelsChangedSubscriber
    DataModelsChangedSubscriber { get; set; } = default!;

  [Inject]
  private IMeasurementsBufferedSubscriber MeasurementsBufferedSubscriber
  {
    get;
    set;
  } = default!;

  [Inject]
  private AggregateUpserter AggregateUpserter { get; set; } = default!;

  [Inject]
  private MeterNamingConvention MeterNamingConvention { get; set; } = default!;

  [Inject]
  private ClockQueries ClockQueries { get; set; } = default!;

  [Inject]
  private TimeQueries TimeQueries { get; set; } = default!;

  protected override void OnInitialized()
  {
    var now = ClockQueries.Now();
    var fromDate = now.Subtract(
      TimeQueries
        .ResolutionTimeSpan(
          _parameters.Resolution, now, _parameters.Multiplier));
    _parameters.FromDate = fromDate;

    DataModelsChangedSubscriber.Subscribe(OnDataModelsChanged);
    MeasurementsBufferedSubscriber.Subscribe(OnMeasurementsBuffered);
  }

  protected override void Dispose(bool disposing)
  {
    if (IsDisposed)
    {
      return;
    }

    if (disposing)
    {
      DataModelsChangedSubscriber.Unsubscribe(OnDataModelsChanged);
      MeasurementsBufferedSubscriber.Unsubscribe(OnMeasurementsBuffered);
    }

    base.Dispose(disposing);
  }

  protected override void OnParametersSet()
  {
    if (MeasurementLocations.Count == 1)
    {
      _parameters.MeasurementLocations = MeasurementLocations.ToHashSet();
      return;
    }

    if (Meters.Count == 1)
    {
      _parameters.Meters = Meters.ToHashSet();
      return;
    }

    if (MeasurementLocations.Count > 0)
    {
      _parameters.MeasurementLocations = MeasurementLocations
        .Take(1)
        .ToHashSet();
      return;
    }

    if (Meters.Count > 0)
    {
      _parameters.Meters = Meters
        .Take(1)
        .ToHashSet();
    }
  }

  private IEnumerable<MeasureModel> Measures()
  {
    return _parameters.MeasurementLocations
      .SelectMany(
        x => MeterNamingConvention
          .CapabilitiesForMeterId(x.MeterId).Measures)
      .Concat(Meters.SelectMany(x => x.Capabilities.Measures))
      .Distinct();
  }

  protected override async Task OnParametersSetAsync()
  {
    await Fetch();
  }

  private async Task OnMeasurementLocationsChanged(
    IEnumerable<string> measurementLocationIds)
  {
    _parameters.MeasurementLocations = MeasurementLocations
      .Where(
        measurementLocation =>
          measurementLocationIds.Contains(measurementLocation.Id))
      .ToHashSet();
    var now = ClockQueries.Now();
    var fromDate = now.Subtract(
      TimeQueries
        .ResolutionTimeSpan(
          _parameters.Resolution, now, _parameters.Multiplier));
    _parameters.FromDate = fromDate;
    await Fetch();
  }

  private async Task OnMetersChanged(IEnumerable<string> meterIds)
  {
    _parameters.Meters = Meters
      .Where(
        meter =>
          meterIds.Contains(meter.Id))
      .ToHashSet();
    var now = ClockQueries.Now();
    _parameters.FromDate = now.Subtract(
      TimeQueries.ResolutionTimeSpan(
        _parameters.Resolution,
        now,
        _parameters.Multiplier));
    await Fetch();
  }

  private async Task OnRefreshChanged(bool refresh)
  {
    _parameters.Refresh = refresh;
    if (_parameters.Refresh)
    {
      var now = ClockQueries.Now();
      var fromDate = now.Subtract(
        TimeQueries
          .ResolutionTimeSpan(
            _parameters.Resolution, now, _parameters.Multiplier));
      _parameters.FromDate = fromDate;
      await Fetch();
    }
  }

  private async Task OnResolutionChanged(ResolutionModel resolution)
  {
    _parameters.Resolution = resolution;
    if (_parameters.Refresh)
    {
      var now = ClockQueries.Now();
      var fromDate = now.Subtract(
        TimeQueries
          .ResolutionTimeSpan(
            _parameters.Resolution, now, _parameters.Multiplier));
      _parameters.FromDate = fromDate;
    }

    await Fetch();
  }

  private async Task OnMultiplierChanged(int multiplier)
  {
    _parameters.Multiplier = multiplier;
    if (_parameters.Refresh)
    {
      var now = ClockQueries.Now();
      var fromDate = now.Subtract(
        TimeQueries
          .ResolutionTimeSpan(
            _parameters.Resolution, now, _parameters.Multiplier));
      _parameters.FromDate = fromDate;
    }

    await Fetch();
  }

  private void OnDataModelsChanged(
    object? _sender,
    DataModelsChangedEventArgs args)
  {
    var measurements = args.Models
      .Where(x => x.State is DataModelChangedState.Added)
      .Select(x => x.Model)
      .OfType<IMeasurement>()
      .Where(x => x is not IAggregate)
      .ToList();
    var aggregates = args.Models
      .Where(x => x.State is DataModelChangedState.Added)
      .Select(x => x.Model)
      .OfType<IAggregate>()
      .ToList();

    Refresh(measurements, aggregates);
  }

  private void OnMeasurementsBuffered(
    object? _sender,
    MeasurementsBufferedEventArgs args)
  {
    Refresh(args.Measurements.ToList(), new List<IAggregate>());
  }

  private async Task Fetch()
  {
    var queries = ScopedServices.GetRequiredService<MeasurementQueries>();
    var fromDate = _parameters.FromDate;
    var toDate = fromDate.Add(
      TimeQueries.ResolutionTimeSpan(
        _parameters.Resolution,
        fromDate,
        _parameters.Multiplier));
    var fromMeters = await queries.ReadByMeterIds(
      _parameters.Meters.Select(x => x.Id).ToList(),
      _parameters.Resolution,
      _parameters.Multiplier,
      0,
      CancellationToken,
      fromDate: fromDate,
      toDate: toDate
    );
    var fromMeasurementLocations = await queries
      .ReadByMeasurementLocationIds(
        _parameters.MeasurementLocations.Select(x => x.Id).ToList(),
        _parameters.Resolution,
        _parameters.Multiplier,
        0,
        CancellationToken,
        fromDate,
        toDate
      );
    _parameters.Measurements = new PaginatedList<IMeasurement>(
      fromMeters.Items
        .Concat(fromMeasurementLocations.Items)
        .DistinctBy(
          x =>
            (x.MeterId,
              x.MeasurementLocationId,
              x.Timestamp,
              x.GetType()))
        .OrderBy(x => x.Timestamp)
        .ToList(),
      fromMeters.TotalCount + fromMeasurementLocations.TotalCount
    );
  }

  private void Refresh(
    List<IMeasurement> measurements,
    List<IAggregate> aggregates)
  {
    if (!_parameters.Refresh
      || (measurements.Count == 0
        && aggregates.Count == 0))
    {
      return;
    }

    var now = ClockQueries.Now();
    var timeSpan = TimeQueries.ResolutionTimeSpan(
      _parameters.Resolution,
      now,
      _parameters.Multiplier);
    var min = now.Subtract(timeSpan);
    var minNew =
      _parameters.Measurements.Items.LastOrDefault()?.Timestamp ?? min;
    var appropriateInterval = TimeQueries.AppropriateInterval(timeSpan, now);

    if (appropriateInterval is null)
    {
      var newMeasurements = measurements
        .Where(
          x =>
            Meters.Exists(meter => meter.Id == x.MeterId)
            || MeasurementLocations.Exists(
              location => location.Id == x.MeasurementLocationId))
        .ToList();
      var concatenated = _parameters.Measurements.Items
        .Concat(newMeasurements)
        .Where(x => x.Timestamp >= min)
        .DistinctBy(
          x =>
            (x.MeterId,
              x.MeasurementLocationId,
              x.Timestamp,
              x.GetType()))
        .OrderBy(x => x.Timestamp)
        .ToList();
      _parameters.Measurements = new PaginatedList<IMeasurement>(
        concatenated,
        _parameters.Measurements.TotalCount + newMeasurements.Count
      );
    }
    else
    {
      var newAggregates = aggregates
        .Where(x => x.Timestamp >= minNew)
        .Where(x => x.Interval == appropriateInterval)
        .Where(
          x =>
            Meters.Exists(meter => meter.Id == x.MeterId)
            || MeasurementLocations.Exists(
              location => location.Id == x.MeasurementLocationId))
        .OfType<IAggregate>();
      var aggregated = _parameters.Measurements.Items.OfType<IAggregate>()
        .Concat(newAggregates)
        .Where(x => x.Timestamp >= min)
        .GroupBy(
          x =>
            (x.Timestamp, x.MeasurementLocationId, x.MeterId, x.GetType()))
        .Select(
          x => x
            .Aggregate(AggregateUpserter.UpsertAggregate))
        .OfType<IMeasurement>()
        .DistinctBy(
          x =>
            (x.MeterId,
              x.MeasurementLocationId,
              x.Timestamp,
              x.GetType()))
        .OrderBy(x => x.Timestamp)
        .ToList();
      _parameters.Measurements = new PaginatedList<IMeasurement>(
        aggregated.ToList(),
        _parameters.Measurements.TotalCount
        - _parameters.Measurements.Items.Count + aggregated.Count
      );
    }

    InvokeAsync(StateHasChanged);
  }

  private string UpdateSelectDisplay()
  {
    var ids = _parameters.MeasurementLocations.Select(x => x.Id).ToList();
    return ids.Count == 1
      ? MeasurementLocations.First(m => m.Id == ids[0]).Title
      : string.Join(", ", ids);
  }
}

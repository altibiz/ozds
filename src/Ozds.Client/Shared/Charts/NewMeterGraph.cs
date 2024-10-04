using ApexCharts;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using Ozds.Business.Aggregation.Agnostic;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
using Ozds.Business.Queries;
using Ozds.Business.Queries.Abstractions;
using Ozds.Client.Base;
using Ozds.Client.Extensions;

// FIXME: chart updates are always behind by one render

namespace Ozds.Client.Shared.Charts;

#pragma warning disable S3881 // "IDisposable" should be implemented correctly
public partial class NewMeterGraph : OzdsOwningComponentBase
#pragma warning restore S3881 // "IDisposable" should be implemented correctly
{
  [Parameter]
  public List<IMeter> Meters { get; set; } = default!;

  [Parameter]
  public DateTimeOffset Timestamp { get; set; } = default!;

  [Parameter]
  public MeasureModel Measure { get; set; } = MeasureModel.ActivePower;

  [Parameter]
  public ResolutionModel Resolution { get; set; } = ResolutionModel.Minute;

  [Parameter]
  public int Multiplier { get; set; } = 5;

  [Parameter]
  public bool Sum { get; set; } = false;

  [Parameter]
  public bool Static { get; set; } = false;

  [Parameter]
  public HashSet<PhaseModel> Phases { get; set; } =
    Enum.GetValues<PhaseModel>().ToHashSet();

  [Parameter]
  public bool Refresh { get; set; } = true;

  [CascadingParameter]
  public Breakpoint Breakpoint { get; set; } = default!;

  [Inject]
  public IMeasurementUpsertSubscriber MeasurementSubscriber { get; set; } = default!;

  [Inject]
  public AgnosticAggregateUpserter AggregateUpserter { get; set; } = default!;

  private ApexChart<IMeasurement>? _chart = default!;

  private PaginatedList<IMeasurement> _measurements = new(
    new List<IMeasurement>(), 0);

  private ApexChartOptions<IMeasurement> _options =
    new ApexChartOptions<IMeasurement>().WithFixedScriptPath();

  private HashSet<IMeter> _selectedMeters = new();

  protected override void OnInitialized()
  {
    MeasurementSubscriber.SubscribeUpsert(OnUpsert);

    _options = CreateGraphOptions();

    if (Static)
    {
      _selectedMeters = Meters.ToHashSet();
    }
    else
    {
      _selectedMeters = Meters.Take(1).ToHashSet();
    }
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      MeasurementSubscriber.UnsubscribeUpsert(OnUpsert);
    }
  }

  protected override async Task OnInitializedAsync()
  {
    await OnParametersSetAsync();
  }

  protected override async Task OnParametersSetAsync()
  {
    var queries = ScopedServices.GetRequiredService<MeterGraphQueries>();

    _measurements = await queries.Read(
      Meters,
      Resolution,
      Multiplier,
      fromDate: Timestamp
    );
    _options = CreateGraphOptions();

    if (_chart is { } chart)
    {
      await chart.UpdateSeriesAsync(animate: true);
      await chart.UpdateOptionsAsync(false, true, false);
    }
  }

  private void OnUpsert(
    object? _sender,
    MeasurementUpsertEventArgs args)
  {
    if (!Refresh)
    {
      return;
    }

    Task.Run(async () =>
    {
      var now = DateTimeOffset.UtcNow;
      var timestamp = _measurements.Items.LastOrDefault()?.Timestamp ?? now;
      var timeSpan = Resolution.ToTimeSpan(Multiplier, timestamp);
      var appropriateInterval = QueryConstants.AppropriateInterval(timeSpan, now);

      if (appropriateInterval is null)
      {
        var newMeasurements = args.Measurements
          .Where(x => x.Timestamp >= timestamp)
          .Where(x => Meters.Exists(meter =>
            meter.Id == x.MeterId))
          .OrderBy(x => x.Timestamp)
          .ToList();
        var concatenated = _measurements.Items.Concat(newMeasurements).ToList();
        _measurements = new PaginatedList<IMeasurement>(
          concatenated,
          _measurements.TotalCount + newMeasurements.Count
        );

        if (_chart is { } chart)
        {
          await chart.AppendDataAsync(newMeasurements);
        }
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
        var aggregated = _measurements.Items.OfType<IAggregate>()
          .Concat(newAggregates)
          .GroupBy(x => x.Timestamp)
          .Select(x => x
            .Aggregate(AggregateUpserter.UpsertModelAgnostic))
          .OfType<IMeasurement>()
          .ToList();
        _measurements = new PaginatedList<IMeasurement>(
          aggregated.ToList(),
          _measurements.TotalCount - _measurements.Items.Count + aggregated.Count
        );

        if (_chart is { } chart)
        {
          await chart.UpdateSeriesAsync();
        }
      }
    });
  }

  private ApexChartOptions<IMeasurement> CreateGraphOptions()
  {
    var maxPower = _measurements.Items
      .Select(x => x.ActivePower_W.TariffUnary().DuplexImport().PhaseSum())
      .OrderByDescending(x => x)
      .Cast<decimal?>()
      .FirstOrDefault();

    var options = _options;
    foreach (var meter in _selectedMeters)
    {
      options = _options.WithActivePower(
        $"{meter.Id} {T["CONNECTION POWER"]}",
        meter.ConnectionPower_W,
        maxPower
      );
    }

    var measure = $"{T[Measure.ToTitle()]} ({Measure.ToUnit()})";
    options = Breakpoint <= Breakpoint.Sm
      ? options.WithSmAndDown(measure)
      : options.WithMdAndUp(measure);

    return options;
  }
}

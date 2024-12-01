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
using Ozds.Business.Queries.Abstractions;
using Ozds.Business.Queries.Agnostic;
using Ozds.Client.Components.Base;
using Ozds.Client.Extensions;

// FIXME: chart updates are always behind by one render

namespace Ozds.Client.Components.Charts;

#pragma warning disable S3881 // "IDisposable" should be implemented correctly
public partial class MeasurementLineChart : OzdsOwningComponentBase
#pragma warning restore S3881 // "IDisposable" should be implemented correctly
{
  [CascadingParameter]
  public Breakpoint Breakpoint { get; set; } = default!;

  [Parameter]
  public List<IMeter> Meters { get; set; } = default!;

  [Parameter]
  public MeasurementChartParameters Parameters { get; set; } = default!;

  [Parameter]
  public bool Area { get; set; } = false;

  [Parameter]
  public bool Brush { get; set; } = false;

  [Inject]
  public IMeasurementUpsertSubscriber MeasurementSubscriber { get; set; } = default!;

  [Inject]
  public AgnosticAggregateUpserter AggregateUpserter { get; set; } = default!;

  private readonly string _id = Guid.NewGuid().ToString();

#pragma warning disable S2933 // Fields that are only assigned in the constructor should be "readonly"
  private ApexChart<IMeasurement>? _chart = default!;
#pragma warning restore S2933 // Fields that are only assigned in the constructor should be "readonly"

#pragma warning disable S2933 // Fields that are only assigned in the constructor should be "readonly"
  private ApexChart<IMeasurement>? _brushChart = default!;
#pragma warning restore S2933 // Fields that are only assigned in the constructor should be "readonly"

  private PaginatedList<IMeasurement> _measurements = new(
    new List<IMeasurement>(), 0);

  private ApexChartOptions<IMeasurement> _options =
    new ApexChartOptions<IMeasurement>()
      .WithFixedScriptPath();

  private ApexChartOptions<IMeasurement> _brushOptions =
    new ApexChartOptions<IMeasurement>()
      .WithFixedScriptPath();

  protected override void OnInitialized()
  {
    MeasurementSubscriber.SubscribeUpsert(OnUpsert);

    _options = CreateGraphOptions();

    _brushOptions = CreateBrushOptions();
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
    var queries = ScopedServices.GetRequiredService<MeasurementQueries>();

    var fromDate = Parameters.FromDate;
    var toDate = fromDate.Subtract(Parameters.Resolution
      .ToTimeSpan(Parameters.Multiplier, fromDate));
    _measurements = await queries.ReadByMeterIdsDynamic(
      Meters,
      Parameters.Resolution,
      Parameters.Multiplier,
      pageNumber: 1,
      cancellationToken: CancellationToken.None,
      fromDate: fromDate,
      toDate: toDate
    );

    _options = CreateGraphOptions();
    if (_chart is { } chart)
    {
      await chart.UpdateSeriesAsync(animate: true);
      await InvokeAsync(() => StateHasChanged());
      await chart.UpdateOptionsAsync(false, true, false);
    }

    _brushOptions = CreateBrushOptions();
    if (_brushChart is { } brushChart)
    {
      await brushChart.UpdateOptionsAsync(false, true, false);
    }
  }

  private void OnUpsert(
    object? _sender,
    MeasurementUpsertEventArgs args)
  {
    if (!Parameters.Refresh)
    {
      return;
    }

    Task.Run(async () =>
    {
      var now = DateTimeOffset.UtcNow;
      var timestamp = _measurements.Items.LastOrDefault()?.Timestamp ?? now;
      var timeSpan = Parameters.Resolution
        .ToTimeSpan(Parameters.Multiplier, timestamp);
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
        var concatenated = _measurements.Items
          .Concat(newMeasurements)
          .ToList();
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
    var options = _options;
    options.Chart.Id = _id;

    var maxPower = _measurements.Items
      .Select(x => x.ActivePower_W.TariffUnary().DuplexImport().PhaseSum())
      .OrderByDescending(x => x)
      .Cast<decimal?>()
      .FirstOrDefault();
    if (Parameters.Measure == MeasureModel.ActivePower
      && Parameters.Selection.Count == 1)
    {
      options = _options.WithActivePower(
        $"{Parameters.Selection.First().Id} {Translate("CONNECTION POWER")}",
        Parameters.Selection.First().ConnectionPower_W,
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

using Microsoft.AspNetCore.Components;
using Ozds.Business.Analysis;
using Ozds.Business.Math;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Business.Queries;
using Ozds.Client.Components.Base;

namespace Ozds.Client.Components.Dashboards;

public partial class NetworkUserRepresentativeDashboard : OzdsComponentBase
{
  [Parameter]
  public Analysis Model { get; set; } = default!;

  [Parameter]
  public List<IMeasurementLocation> MeasurementLocations { get; set; } =
    default!;

  [Inject]
  private ClockQueries ClockQueries { get; set; } = default!;

  [Inject]
  private TimeQueries TimeQueries { get; set; } = default!;

  // TODO: better way to do this
  private sealed class MonthlyMeasurement(List<IMeasurement> Aggregates)
    : Model,
      IMeasurement
  {
    public string MeterId
    {
      get { return Aggregates.First().MeterId; }
    }

    public string MeasurementLocationId
    {
      get { return Aggregates.First().MeasurementLocationId; }
    }

    public DateTimeOffset Timestamp
    {
      get { return Aggregates.First().Timestamp; }
    }

    public TariffMeasure<decimal> Current_A
    {
      get { return TariffMeasure<decimal>.Null; }
    }

    public TariffMeasure<decimal> Voltage_V
    {
      get { return TariffMeasure<decimal>.Null; }
    }

    public TariffMeasure<decimal> ActivePower_W
    {
      get { return TariffMeasure<decimal>.Null; }
    }

    public TariffMeasure<decimal> ReactivePower_VAR
    {
      get { return TariffMeasure<decimal>.Null; }
    }

    public TariffMeasure<decimal> ApparentPower_VA
    {
      get { return TariffMeasure<decimal>.Null; }
    }

    public TariffMeasure<decimal> ActiveEnergy_Wh
    {
      get
      {
        return Aggregates.Count == 0
          ? TariffMeasure<decimal>.Null
          : Aggregates
            .Skip(1)
            .Aggregate(
              Aggregates.First().ActiveEnergy_Wh,
              (x, y) => x.Add(y.ActiveEnergy_Wh)
            );
      }
    }

    public TariffMeasure<decimal> ReactiveEnergy_VARh
    {
      get
      {
        return Aggregates.Count == 0
          ? TariffMeasure<decimal>.Null
          : Aggregates
            .Skip(1)
            .Aggregate(
              Aggregates.First().ReactiveEnergy_VARh,
              (x, y) => x.Add(y.ReactiveEnergy_VARh)
            );
      }
    }

    public TariffMeasure<decimal> ApparentEnergy_VAh
    {
      get
      {
        return Aggregates.Count == 0
          ? TariffMeasure<decimal>.Null
          : Aggregates
            .Skip(1)
            .Aggregate(
              Aggregates.First().ApparentEnergy_VAh,
              (x, y) => x.Add(y.ApparentEnergy_VAh)
            );
      }
    }
  }
}

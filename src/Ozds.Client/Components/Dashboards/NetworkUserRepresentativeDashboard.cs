using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Ozds.Business.Analysis;
using Ozds.Business.Math;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries;
using Ozds.Client.Components.Base;

namespace Ozds.Client.Components.Dashboards;

public partial class NetworkUserRepresentativeDashboard : OzdsComponentBase
{
  [Parameter]
  public Analysis Model { get; set; } = default!;

  [Parameter]
  public List<IMeasurementLocation> MeasurementLocations { get; set; } = default!;

  [Inject]
  private ClockQueries ClockQueries { get; set; } = default!;

  [Inject]
  private TimeQueries TimeQueries { get; set; } = default!;

  // TODO: better way to do this
  private sealed class MonthlyMeasurement(
    List<IMeasurement> Aggregates
  ) : IMeasurement
  {
    public string MeterId => Aggregates.First().MeterId;

    public string MeasurementLocationId => Aggregates.First().MeasurementLocationId;

    public DateTimeOffset Timestamp => Aggregates.First().Timestamp;

    public TariffMeasure<decimal> Current_A => TariffMeasure<decimal>.Null;

    public TariffMeasure<decimal> Voltage_V => TariffMeasure<decimal>.Null;

    public TariffMeasure<decimal> ActivePower_W => TariffMeasure<decimal>.Null;

    public TariffMeasure<decimal> ReactivePower_VAR => TariffMeasure<decimal>.Null;

    public TariffMeasure<decimal> ApparentPower_VA => TariffMeasure<decimal>.Null;

    public TariffMeasure<decimal> ActiveEnergy_Wh =>
      Aggregates.Count == 0 ? TariffMeasure<decimal>.Null :
      Aggregates.Skip(1).Aggregate(
        Aggregates.First().ActiveEnergy_Wh,
        (x, y) => x.Add(y.ActiveEnergy_Wh));

    public TariffMeasure<decimal> ReactiveEnergy_VARh =>
      Aggregates.Count == 0 ? TariffMeasure<decimal>.Null :
      Aggregates.Skip(1).Aggregate(
        Aggregates.First().ReactiveEnergy_VARh,
        (x, y) => x.Add(y.ReactiveEnergy_VARh));

    public TariffMeasure<decimal> ApparentEnergy_VAh =>
      Aggregates.Count == 0 ? TariffMeasure<decimal>.Null :
      Aggregates.Skip(1).Aggregate(
        Aggregates.First().ApparentEnergy_VAh,
        (x, y) => x.Add(y.ApparentEnergy_VAh));

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      yield break;
    }
  }
}

using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Models.Abstractions;

public interface IAggregate : IValidatableObject, IReadonly
{
  public string MeterId { get; }

  public DateTimeOffset Timestamp { get; }

  public IntervalModel Interval { get; }

  public long Count { get; }

  public TariffMeasure<float> Current_A { get; }

  public TariffMeasure<float> Voltage_V { get; }

  public TariffMeasure<float> ActivePower_W { get; }

  public TariffMeasure<float> ReactivePower_VAR { get; }

  public TariffMeasure<float> ApparentPower_VA { get; }

  public TariffMeasure<float> ActiveEnergy_Wh { get; }

  public TariffMeasure<float> ReactiveEnergy_VARh { get; }

  public TariffMeasure<float> ApparentEnergy_VAh { get; }

  public SpanningMeasure<float> ActiveEnergySpan_Wh { get; }

  public SpanningMeasure<float> ReactiveEnergySpan_VARh { get; }

  public SpanningMeasure<float> ApparentEnergySpan_VAh { get; }
}

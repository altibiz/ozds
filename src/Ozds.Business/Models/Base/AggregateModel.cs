using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Base;

public abstract record AggregateModel(
  string MeterId,
  DateTimeOffset Timestamp,
  TimeSpan Interval,
  long Count
) : IAggregate
{
  public abstract TariffMeasure Current_A { get; }

  public abstract TariffMeasure Voltage_V { get; }

  public abstract SpanningMeasure ActiveEnergySpan_Wh { get; }

  public abstract SpanningMeasure ReactiveEnergySpan_VARh { get; }

  public abstract SpanningMeasure ApparentEnergySpan_VAh { get; }

  public virtual TariffMeasure ActivePower_W
  {
    get
    {
      return ActiveEnergySpan_Wh.SpanDifferential((float)Interval.TotalHours);
    }
  }

  public virtual TariffMeasure ReactivePower_VAR
  {
    get
    {
      return ReactiveEnergySpan_VARh.SpanDifferential(
        (float)Interval.TotalHours);
    }
  }

  public virtual TariffMeasure ApparentPower_VA
  {
    get
    {
      return ApparentEnergySpan_VAh.SpanDifferential(
        (float)Interval.TotalHours);
    }
  }

  public virtual TariffMeasure ActiveEnergyCumulative_Wh
  {
    get { return ActiveEnergySpan_Wh.SpanMax; }
  }

  public virtual TariffMeasure ReactiveEnergyCumulative_VARh
  {
    get { return ReactiveEnergySpan_VARh.SpanMax; }
  }

  public virtual TariffMeasure ApparentEnergyCumulative_VAh
  {
    get { return ApparentEnergySpan_VAh.SpanMax; }
  }

  public abstract IEnumerable<ValidationResult> Validate(ValidationContext validationContext);
}

using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Models.Base;

public abstract class AggregateModel : IAggregate
{
  [Required] public required string MeterId { get; set; }

  [Required]
  public required DateTimeOffset Timestamp { get; init; } =
    DateTimeOffset.UtcNow;

  [Required] public required IntervalModel Interval { get; init; }

  [Required] public required long Count { get; init; } = 0;

  public abstract TariffMeasure Current_A { get; }

  public abstract TariffMeasure Voltage_V { get; }

  public abstract SpanningMeasure ActiveEnergySpan_Wh { get; }

  public abstract SpanningMeasure ReactiveEnergySpan_VARh { get; }

  public abstract SpanningMeasure ApparentEnergySpan_VAh { get; }

  public virtual TariffMeasure ActivePower_W
  {
    get
    {
      return ActiveEnergySpan_Wh.SpanDifferential(
        (float)Interval.ToTimeSpan(Timestamp).TotalHours);
    }
  }

  public virtual TariffMeasure ReactivePower_VAR
  {
    get
    {
      return ReactiveEnergySpan_VARh.SpanDifferential(
        (float)Interval.ToTimeSpan(Timestamp).TotalHours);
    }
  }

  public virtual TariffMeasure ApparentPower_VA
  {
    get
    {
      return ApparentEnergySpan_VAh.SpanDifferential(
        (float)Interval.ToTimeSpan(Timestamp).TotalHours);
    }
  }

  public virtual TariffMeasure ActiveEnergy_Wh
  {
    get { return ActiveEnergySpan_Wh.SpanMax; }
  }

  public virtual TariffMeasure ReactiveEnergy_VARh
  {
    get { return ReactiveEnergySpan_VARh.SpanMax; }
  }

  public virtual TariffMeasure ApparentEnergy_VAh
  {
    get { return ApparentEnergySpan_VAh.SpanMax; }
  }

  public virtual IEnumerable<ValidationResult> Validate(
    ValidationContext validationContext)
  {
    if (validationContext.ObjectInstance != this)
    {
      yield break;
    }

    if (
      validationContext.MemberName is null or nameof(Count) &&
      Count < 0
    )
    {
      yield return new ValidationResult(
        "Count must be greater than or equal to zero",
        new[] { nameof(Count) });
    }

    if (
      validationContext.MemberName is null or nameof(Timestamp) &&
      Timestamp > DateTimeOffset.UtcNow
    )
    {
      yield return new ValidationResult(
        "Timestamp must be in the past",
        new[] { nameof(Timestamp) });
    }
  }
}

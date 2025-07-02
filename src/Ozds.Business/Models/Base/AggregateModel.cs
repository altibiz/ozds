using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Business.Queries;

namespace Ozds.Business.Models.Base;

public abstract class AggregateModel : IAggregate
{
  [Required]
  public required string MeterId { get; set; }

  [Required]
  public required string MeasurementLocationId { get; set; }

  [Required]
  public required DateTimeOffset Timestamp { get; set; }

  [Required]
  public required IntervalModel Interval { get; set; }

  [Required]
  public required long Count { get; set; } = 0;

  [Required]
  public required long QuarterHourCount { get; set; } = 0;

  public abstract TariffMeasure<decimal> Current_A { get; }

  public abstract TariffMeasure<decimal> Voltage_V { get; }

  public abstract TariffMeasure<decimal> ActivePower_W { get; }

  public abstract TariffMeasure<decimal> ReactivePower_VAR { get; }

  public abstract TariffMeasure<decimal> ApparentPower_VA { get; }

  public abstract TariffMeasure<decimal> ActiveEnergy_Wh { get; }

  public abstract TariffMeasure<decimal> ReactiveEnergy_VARh { get; }

  public abstract TariffMeasure<decimal> ApparentEnergy_VAh { get; }

  public abstract TariffMeasure<decimal> DerivedActivePower_W { get; }

  public abstract TariffMeasure<decimal> DerivedReactivePower_VAR { get; }

  public abstract TariffMeasure<decimal> DerivedApparentPower_VA { get; }

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

    var clock = validationContext.GetRequiredService<ClockQueries>();
    var now = clock.Timestamp();
    if (
      validationContext.MemberName is null or nameof(Timestamp) &&
      Timestamp > now
    )
    {
      yield return new ValidationResult(
        "Timestamp must be in the past",
        new[] { nameof(Timestamp) });
    }
  }
}

using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Ozds.Business.Math;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Business.Time;

namespace Ozds.Business.Models.Base;

public abstract class AggregateModel : IAggregate
{
  private IntervalModel _interval = IntervalModel.QuarterHour;

  // NOTE: just so it doesn't break if interval is set before timestamp
  private DateTimeOffset _timestamp =
    DateTimeOffset.Parse("2000-01-01T00:00:00Z", CultureInfo.InvariantCulture);

  [Required]
  public required string MeterId { get; set; }

  [Required]
  public required string MeasurementLocationId { get; set; }

  [Required]
  public required DateTimeOffset Timestamp
  {
    get { return _timestamp.ToUniversalTime(); }
    set
    {
      _ = Interval switch
      {
#pragma warning disable S1121
        IntervalModel.QuarterHour => _timestamp = value.GetStartOfQuarterHour(),
        IntervalModel.Day => _timestamp = value.GetStartOfDay(),
        IntervalModel.Month => _timestamp = value.GetStartOfMonth(),
#pragma warning restore S1121
        _ => throw new InvalidOperationException(
          $"Unsupported interval {Interval}"
        )
      };
    }
  }

  [Required]
  public required IntervalModel Interval
  {
    get { return _interval; }
    set
    {
      _interval = value;
      Timestamp = _timestamp;
    }
  }

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

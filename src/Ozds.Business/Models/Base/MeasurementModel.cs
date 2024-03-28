using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Base;

public abstract class MeasurementModel<T> : IMeasurement
  where T : class, IMeasurementValidator
{
  [Required]
  public required string MeterId { get; init; }
  [Required]
  public required DateTimeOffset Timestamp { get; init; } = DateTimeOffset.UtcNow;

  public abstract TariffMeasure Current_A { get; }

  public abstract TariffMeasure Voltage_V { get; }

  public abstract TariffMeasure ActivePower_W { get; }

  public abstract TariffMeasure ReactivePower_VAR { get; }

  public abstract TariffMeasure ApparentPower_VA { get; }

  public abstract TariffMeasure ActiveEnergy_Wh { get; }

  public abstract TariffMeasure ReactiveEnergy_VARh { get; }

  public abstract TariffMeasure ApparentEnergy_VAh { get; }

  public virtual IEnumerable<ValidationResult> Validate(
    ValidationContext validationContext)
  {
    if (validationContext.ObjectInstance != this)
    {
      yield break;
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

    if (validationContext.Items["MeasurementValidator"] is T validator)
    {
      foreach (var result in validator.Validate(validationContext))
      {
        yield return result;
      }
    }
  }
}

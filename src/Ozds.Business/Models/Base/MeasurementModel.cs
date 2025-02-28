using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Base;

public abstract class MeasurementModel : IMeasurement
{
  public const string ValidatorKey = "MeasurementValidator";

  [Required]
  public required string MeterId { get; set; }

  [Required]
  public required string MeasurementLocationId { get; set; }

  [Required]
  public required DateTimeOffset Timestamp { get; set; } =
    DateTimeOffset.UtcNow;

  public abstract TariffMeasure<decimal> Current_A { get; }

  public abstract TariffMeasure<decimal> Voltage_V { get; }

  public abstract TariffMeasure<decimal> ActivePower_W { get; }

  public abstract TariffMeasure<decimal> ReactivePower_VAR { get; }

  public abstract TariffMeasure<decimal> ApparentPower_VA { get; }

  public abstract TariffMeasure<decimal> ActiveEnergy_Wh { get; }

  public abstract TariffMeasure<decimal> ReactiveEnergy_VARh { get; }

  public abstract TariffMeasure<decimal> ApparentEnergy_VAh { get; }

  public abstract IEnumerable<ValidationResult> Validate(
    ValidationContext validationContext);
}

public abstract class MeasurementModel<T> : MeasurementModel
  where T : class, IMeasurementValidator
{
  public override IEnumerable<ValidationResult> Validate(
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

    if (validationContext.Items[ValidatorKey] is T validator)
    {
      foreach (var result in validator.Validate(validationContext))
      {
        yield return result;
      }
    }
  }
}

using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Ozds.Business.Math;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries;

namespace Ozds.Business.Models.Base;

public abstract class MeasurementModel : Model, IMeasurement
{
  public const string ValidatorKey = "MeasurementValidator";

  [Required]
  public required string MeterId { get; set; }

  [Required]
  public required string MeasurementLocationId { get; set; }

  [Required]
  public required DateTimeOffset Timestamp { get; set; } =
    // NOTE: just so something is there
    DateTimeOffset.Parse("2000-01-01T00:00:00Z", CultureInfo.InvariantCulture);

  public abstract TariffMeasure<decimal> Current_A { get; }

  public abstract TariffMeasure<decimal> Voltage_V { get; }

  public abstract TariffMeasure<decimal> ActivePower_W { get; }

  public abstract TariffMeasure<decimal> ReactivePower_VAR { get; }

  public abstract TariffMeasure<decimal> ApparentPower_VA { get; }

  public abstract TariffMeasure<decimal> ActiveEnergy_Wh { get; }

  public abstract TariffMeasure<decimal> ReactiveEnergy_VARh { get; }

  public abstract TariffMeasure<decimal> ApparentEnergy_VAh { get; }
}

#pragma warning disable S2326 // Unused type parameters should be removed
public abstract class MeasurementModel<T> : MeasurementModel
#pragma warning restore S2326 // Unused type parameters should be removed
  where T : class, IMeasurementValidator
{
  public override IEnumerable<ValidationResult> Validate(
    ValidationContext validationContext
  )
  {
    foreach (var validationResult in base.Validate(validationContext))
    {
      yield return validationResult;
    }

    if (validationContext.ObjectInstance != this)
    {
      yield break;
    }

    var clock = validationContext.GetRequiredService<ClockQueries>();

    var now = clock.Timestamp();

    if (
      validationContext.MemberName is null or nameof(Timestamp)
      && Timestamp > now
    )
    {
      yield return new ValidationResult(
        "Timestamp must be in the past",
        new[] { nameof(Timestamp) }
      );
    }
  }
}

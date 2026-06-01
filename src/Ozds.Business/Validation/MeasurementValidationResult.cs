using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Validation;

public sealed class MeasurementValidationResult : ValidationResult
{
  public MeasurementValidationResult(
    ValidationResult source,
    IMeter meter,
    IMeasurementLocation? measurementLocation
  )
    : base(source.ErrorMessage, source.MemberNames)
  {
    Meter = meter;
    MeasurementLocation = measurementLocation;
  }

  public IMeter Meter { get; }

  public IMeasurementLocation? MeasurementLocation { get; }
}

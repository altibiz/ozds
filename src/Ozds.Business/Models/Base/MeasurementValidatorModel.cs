using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Base;

public abstract class MeasurementValidatorModel : AuditableModel
{
}

public abstract class MeasurementValidatorModel<T> : MeasurementValidatorModel,
  IMeasurementValidator, IModel
  where T : IMeasurement
{
  public override IEnumerable<ValidationResult> Validate(
    ValidationContext validationContext)
  {
    foreach (var result in base.Validate(validationContext))
    {
      yield return result;
    }

    if (validationContext.ObjectInstance is T measurement)
    {
      foreach (var result in ValidateMeasurement(
        measurement,
        validationContext.MemberName))
      {
        yield return result;
      }
    }
    else
    {
      foreach (var result in ValidateSelf(validationContext.MemberName))
      {
        yield return result;
      }
    }
  }

  public virtual IEnumerable<ValidationResult> ValidateMeasurement(
    T measurement,
    string? memberName
  )
  {
    return Enumerable.Empty<ValidationResult>();
  }

  public virtual IEnumerable<ValidationResult> ValidateSelf(string? memberName)
  {
    return Enumerable.Empty<ValidationResult>();
  }
}

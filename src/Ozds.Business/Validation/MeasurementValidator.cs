using System.ComponentModel.DataAnnotations;
using Ozds.Business.Caching;
using Ozds.Business.Models.Abstractions;
using Ozds.Data.Queries;

namespace Ozds.Business.Validation.Base;

public class MeasurementValidator(
  ValidationCache cache
) : ModelValidator<IMeasurement>
{
  public override async Task<List<ValidationResult>> ValidateAsync(
    IMeasurement model,
    CancellationToken cancellationToken
  )
  {
    var validator = await cache.GetAsync(model.MeterId, cancellationToken);
    if (validator is null)
    {
      throw new InvalidOperationException(
        $"MeasurementValidator not found for meter {model.MeterId}");
    }

    var validationContext = new ValidationContext(this);
    validationContext.Items["MeasurementValidator"] = validator;
    return model.Validate(validationContext).ToList();
  }
}

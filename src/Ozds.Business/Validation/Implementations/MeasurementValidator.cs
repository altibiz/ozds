using System.ComponentModel.DataAnnotations;
using Ozds.Business.Caching;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Validation.Base;

namespace Ozds.Business.Validation.Implementations;

public class MeasurementValidator(
  IServiceProvider serviceProvider,
  MeasurementValidatorByMeterCache cache
) : ConcreteModelValidator<IMeasurement>(serviceProvider)
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

    var validationContext =
      new ValidationContext(model, serviceProvider, null);
    return validator.Validate(validationContext).ToList();
  }
}

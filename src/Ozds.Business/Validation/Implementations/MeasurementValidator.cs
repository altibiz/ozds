using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries;
using Ozds.Business.Validation.Base;

namespace Ozds.Business.Validation.Implementations;

public class MeasurementValidator(IServiceProvider serviceProvider)
  : ConcreteModelValidator<IMeasurement>(serviceProvider)
{
  public override async Task<List<ValidationResult>> ValidateAsync(
    IMeasurement model,
    CancellationToken cancellationToken
  )
  {
    await using var scope = serviceProvider.CreateAsyncScope();
    var trackableQueries =
      scope.ServiceProvider.GetRequiredService<TrackableQueries>();

    var meter = await trackableQueries.ReadById<IMeter>(
      model.MeterId,
      cancellationToken
    );
    if (meter is null)
    {
      throw new InvalidOperationException(
        $"Meter not found for meter {model.MeterId}"
      );
    }

    var validator = await trackableQueries.ReadById<IMeasurementValidator>(
      meter.MeasurementValidatorId,
      cancellationToken
    );
    if (validator is null)
    {
      throw new InvalidOperationException(
        $"MeasurementValidator not found for meter {model.MeterId}"
      );
    }

    var validationContext = new ValidationContext(model, serviceProvider, null);
    return validator.Validate(validationContext).ToList();
  }
}

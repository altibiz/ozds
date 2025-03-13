using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models;
using Ozds.Business.Validation.Base;

namespace Ozds.Business.Validation.Implementations;

public class NetworkUserMeasurementLocationValidator(
  HtmlSanitizer sanitizer
) : ConcreteModelValidator<NetworkUserMeasurementLocationModel>
{
  public override Task<List<ValidationResult>> ValidateAsync(
    NetworkUserMeasurementLocationModel model,
    CancellationToken cancellationToken
  )
  {
    var validationResults = new List<ValidationResult>();

    if (sanitizer.Validate(model.CalculationRemark) is { } validationResult)
    {
      validationResults.Add(validationResult);
    }

    return Task.FromResult(validationResults);
  }
}

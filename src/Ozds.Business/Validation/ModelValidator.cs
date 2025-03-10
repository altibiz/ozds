using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Validation.Abstractions;

namespace Ozds.Business.Validation;

public class ModelValidator(
  IServiceProvider serviceProvider
)
{
  public async Task<List<ValidationResult>> ValidateAsync(
    IModel model,
    CancellationToken cancellationToken
  )
  {
    var validationResults = new List<ValidationResult>();
    var validationContext = new ValidationContext(this);

    validationResults.AddRange(model.Validate(validationContext));

    var validator = serviceProvider
      .GetServices<IValidator>()
      .FirstOrDefault(service => service.CanValidate(model.GetType()));
    if (validator is not null)
    {
      validationResults.AddRange(
        await validator.ValidateAsync(model, cancellationToken));
    }

    return validationResults;
  }
}

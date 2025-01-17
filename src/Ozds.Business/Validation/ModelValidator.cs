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
    var validator = serviceProvider
      .GetServices<IValidator>()
      .FirstOrDefault(service => service.CanValidate(model.GetType()));
    if (validator is null)
    {
      return model.Validate(new ValidationContext(this)).ToList();
    }

    return await validator.ValidateAsync(model, cancellationToken);
  }
}

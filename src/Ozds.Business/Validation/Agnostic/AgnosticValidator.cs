using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Validation.Abstractions;

// TODO: default validator

namespace Ozds.Business.Validation.Agnostic;

public class AgnosticValidator(
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
      .FirstOrDefault(service => service.CanValidate(model.GetType()))
      ?? throw new InvalidOperationException(
          $"No validator found for {model.GetType()}"
        );

    return await validator.ValidateAsync(model, cancellationToken);
  }
}

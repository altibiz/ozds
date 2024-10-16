using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Validation.Abstractions;

public interface IValidator
{
  public bool CanValidate(Type modelType);

  public Task<List<ValidationResult>> ValidateAsync(
    IModel model,
    CancellationToken cancellationToken
  );
}

using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Validation.Abstractions;

namespace Ozds.Business.Validation.Base;

public abstract class ConcreteModelValidator<T> : IValidator
  where T : IModel
{
  public virtual bool CanValidate(Type modelType)
  {
    return typeof(T).IsAssignableFrom(modelType);
  }

  public Task<List<ValidationResult>> ValidateAsync(
    IModel model,
    CancellationToken cancellationToken
  )
  {
    return ValidateAsync((T)model, cancellationToken);
  }

  public virtual Task<List<ValidationResult>> ValidateAsync(
    T model,
    CancellationToken cancellationToken
  )
  {
    var validationContext = new ValidationContext(this);
    var validationResults = model.Validate(validationContext);
    return Task.FromResult(validationResults.ToList());
  }
}

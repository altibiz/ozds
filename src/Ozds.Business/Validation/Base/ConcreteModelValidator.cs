using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Validation.Abstractions;

namespace Ozds.Business.Validation.Base;

public abstract class ConcreteModelValidator<T>(
  IServiceProvider serviceProvider
) : IValidator
  where T : IModel
{
#pragma warning disable SA1401 // Fields should be private
  protected IServiceProvider serviceProvider = serviceProvider;
#pragma warning restore SA1401 // Fields should be private

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
    var validationContext = new ValidationContext(model, serviceProvider, null);
    var validationResults = model.Validate(validationContext);
    return Task.FromResult(validationResults.ToList());
  }
}

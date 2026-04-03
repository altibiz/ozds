using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Validation.Abstractions;

namespace Ozds.Business.Validation;

public class ModelValidator(IServiceProvider serviceProvider)
{
  private readonly ConcurrentDictionary<Type, IValidator?> validatorCache =
    new();

  public async Task<List<ValidationResult>> Validate(
    IModel model,
    CancellationToken cancellationToken
  )
  {
    var validationResults = new List<ValidationResult>();
    var validationContext = new ValidationContext(model, serviceProvider, null);

    validationResults.AddRange(model.Validate(validationContext));

    var validator = GetValidator(model.GetType());
    if (validator is not null)
    {
      validationResults.AddRange(
        await validator.ValidateAsync(model, cancellationToken)
      );
    }

    return validationResults;
  }

  public async Task<List<ValidationResult>> Validate(
    IEnumerable<IModel> models,
    CancellationToken cancellationToken
  )
  {
    var validationResults = new List<ValidationResult>();

    var enumerator = models.GetEnumerator();
    if (!enumerator.MoveNext())
    {
      return validationResults;
    }

    var current = enumerator.Current;
    var validator = GetValidator(current.GetType());

    var validationContext = new ValidationContext(current, serviceProvider, null);
    validationResults.AddRange(current.Validate(validationContext));

    if (validator is not null)
    {
      validationResults.AddRange(
        await validator.ValidateAsync(current, cancellationToken)
      );
    }

    while (enumerator.MoveNext())
    {
      var next = enumerator.Current;
      if (next.GetType() != current.GetType())
      {
        validator = GetValidator(next.GetType());
        current = next;
      }

      validationContext = new ValidationContext(next, serviceProvider, null);
      validationResults.AddRange(next.Validate(validationContext));

      if (validator is not null)
      {
        validationResults.AddRange(
          await validator.ValidateAsync(next, cancellationToken)
        );
      }
    }

    return validationResults;
  }

  private IValidator? GetValidator(Type type)
  {
    if (validatorCache.TryGetValue(type, out var validator))
    {
      return validator;
    }

    validator = serviceProvider
      .GetServices<IValidator>()
      .FirstOrDefault(service => service.CanValidate(type));

    validatorCache.TryAdd(type, validator);

    return validator;
  }
}

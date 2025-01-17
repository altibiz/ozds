using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Business.Validation;
using Ozds.Data.Entities.Abstractions;
using DataReadonlyMutations = Ozds.Data.Mutations.ReadonlyMutations;

namespace Ozds.Business.Mutations;

public class ReadonlyMutations(
  DataReadonlyMutations mutations,
  AgnosticModelEntityConverter modelEntityConverter,
  ModelValidator validator
) : IMutations
{
  public async Task<string> Create<T>(
    T model,
    CancellationToken cancellationToken
  )
    where T : IReadonly, IIdentifiable
  {
    var validationResults = await validator
      .ValidateAsync(model, cancellationToken);
    if (validationResults.Count > 0)
    {
      var validationResult = string.Join("\n", validationResults);
      throw new InvalidOperationException(
        $"Model {model.GetType()} failed validation {validationResult}"
      );
    }

    var entity = modelEntityConverter.ToEntity<IReadonlyEntity>(model);
    await mutations.Create(entity, cancellationToken);
    return (entity as IIdentifiableEntity)!.Id;
  }

  public async Task Create(
    IReadonly model,
    CancellationToken cancellationToken
  )
  {
    var validationResults = await validator
      .ValidateAsync(model, cancellationToken);
    if (validationResults.Count > 0)
    {
      var validationResult = string.Join("\n", validationResults);
      throw new InvalidOperationException(
        $"Model {model.GetType()} failed validation {validationResult}"
      );
    }

    var entity = modelEntityConverter.ToEntity<IReadonlyEntity>(model);
    await mutations.Create(entity, cancellationToken);
  }
}

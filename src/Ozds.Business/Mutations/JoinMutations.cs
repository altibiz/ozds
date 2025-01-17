using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Validation;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Mutations.Abstractions;
using DataJoinMutations = Ozds.Data.Mutations.Agnostic.JoinMutations;

namespace Ozds.Business.Mutations;

public class JoinMutations(
  DataJoinMutations mutations,
  AgnosticModelEntityConverter modelEntityConverter,
  ModelValidator validator
) : IMutations
{
  public async Task Create(
    IJoin model,
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

    var entity = modelEntityConverter.ToEntity<IJoinEntity>(model);

    await mutations.Create(entity, cancellationToken);
  }

  public async Task Update(
    IJoin model,
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

    var entity = modelEntityConverter.ToEntity<IJoinEntity>(model);

    await mutations.Update(entity, cancellationToken);
  }

  public async Task Delete(
    IJoin model,
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

    var entity = modelEntityConverter.ToEntity<IJoinEntity>(model);

    await mutations.Delete(entity, cancellationToken);
  }
}

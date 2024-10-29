using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Business.Validation.Agnostic;
using Ozds.Data.Entities.Abstractions;
using DataAuditableMutations = Ozds.Data.Mutations.Agnostic.AuditableMutations;

namespace Ozds.Business.Mutations.Agnostic;

public class AuditableMutations(
  DataAuditableMutations mutations,
  AgnosticModelEntityConverter modelEntityConverter,
  AgnosticValidator validator
)
: IMutations
{
  public async Task<string> Create(
    IAuditable model,
    CancellationToken cancellationToken
  )
  {
    var validationResults = await validator
      .ValidateAsync(model, cancellationToken);
    if (validationResults.Count > 0)
    {
      var result = string.Join("\n", validationResults);
      throw new InvalidOperationException(
        $"Model {model.GetType()} {model.Id} failed validation {result}"
      );
    }

    var entity = modelEntityConverter.ToEntity<IAuditableEntity>(model);
    await mutations.Create(entity, cancellationToken);
    return entity.Id;
  }

  public async Task Update(
    IAuditable model,
    CancellationToken cancellationToken
  )
  {
    var validationResults = await validator
      .ValidateAsync(model, cancellationToken);
    if (validationResults.Count > 0)
    {
      var result = string.Join("\n", validationResults);
      throw new InvalidOperationException(
        $"Model {model.GetType()} {model.Id} failed validation {result}"
      );
    }

    var entity = modelEntityConverter.ToEntity<IAuditableEntity>(model);

    await mutations.Update(entity, cancellationToken);
  }

  public async Task Delete(
    IAuditable model,
    CancellationToken cancellationToken
  )
  {
    var validationResults = await validator
      .ValidateAsync(model, cancellationToken);
    if (validationResults.Count > 0)
    {
      var result = string.Join("\n", validationResults);
      throw new InvalidOperationException(
        $"Model {model.GetType()} {model.Id} failed validation {result}"
      );
    }

    var entity = modelEntityConverter.ToEntity<IAuditableEntity>(model);

    await mutations.Delete(entity, cancellationToken);
  }
}

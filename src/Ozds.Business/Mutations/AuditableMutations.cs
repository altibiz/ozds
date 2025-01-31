using Ozds.Business.Conversion;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Business.Queries;
using Ozds.Business.Validation;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Base;
using DataAuditableMutations = Ozds.Data.Mutations.AuditableMutations;

namespace Ozds.Business.Mutations;

public class AuditableMutations(
  DataAuditableMutations mutations,
  ModelEntityConverter modelEntityConverter,
  ModelValidator validator,
  RepresentativeQueries representativeQueries
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

    var representativeId = await representativeQueries
      .ReadAuthenticatedRepresentativeId(cancellationToken);

    var entity = modelEntityConverter.ToEntity<IAuditableEntity>(model);
    entity.RepresentativeId = representativeId;

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

    var representativeId = await representativeQueries
      .ReadAuthenticatedRepresentativeId(cancellationToken);

    var entity = modelEntityConverter.ToEntity<IAuditableEntity>(model);
    entity.RepresentativeId = representativeId;

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

    var representativeId = await representativeQueries
      .ReadAuthenticatedRepresentativeId(cancellationToken);

    var entity = modelEntityConverter.ToEntity<IAuditableEntity>(model);
    entity.RepresentativeId = representativeId;

    await mutations.Delete(entity, cancellationToken);
  }

  public async Task Restore(
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

    var representativeId = await representativeQueries
      .ReadAuthenticatedRepresentativeId(cancellationToken);

    var entity = modelEntityConverter.ToEntity<AuditableEntity>(model);
    entity.RepresentativeId = representativeId;
    entity.Restore = true;

    await mutations.Create(entity, cancellationToken);
  }

  public async Task Forget(
    IAuditable model,
    CancellationToken cancellationToken
  )
  {
    var representativeId = await representativeQueries
      .ReadAuthenticatedRepresentativeId(cancellationToken);

    var entity = modelEntityConverter.ToEntity<AuditableEntity>(model);
    entity.Forget = true;
    entity.RepresentativeId = representativeId;

    await mutations.Delete(entity, cancellationToken);
  }
}

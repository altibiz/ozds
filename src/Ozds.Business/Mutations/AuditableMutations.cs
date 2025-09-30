using Ozds.Business.Conversion;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Business.Queries;
using Ozds.Business.Validation;
using Ozds.Data.Entities.Abstractions;
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
  public async Task Create(
    IAuditable model,
    CancellationToken cancellationToken
  )
  {
    var validationResults = await validator
      .Validate(model, cancellationToken);
    if (validationResults.Count > 0)
    {
      var result = string.Join(Environment.NewLine, validationResults);
      throw new InvalidOperationException(
        $"Model {model.GetType()} {model.AuditingId} failed validation {result}"
      );
    }

    var representativeId = await representativeQueries
      .ReadAuthenticatedRepresentativeId(cancellationToken);

    var entity = modelEntityConverter.ToEntity<IAuditableEntity>(model);
    entity.AuditingRepresentativeId = representativeId;

    await mutations.Create(entity, cancellationToken);

    if (model is IdentifiableModel identifiableModel)
    {
      identifiableModel.Id = entity.AuditingId;
    }
  }

  public async Task Delete(
    IAuditable model,
    CancellationToken cancellationToken
  )
  {
    var validationResults = await validator
      .Validate(model, cancellationToken);
    if (validationResults.Count > 0)
    {
      var result = string.Join(Environment.NewLine, validationResults);
      throw new InvalidOperationException(
        $"Model {model.GetType()} {model.AuditingId} failed validation {result}"
      );
    }

    var representativeId = await representativeQueries
      .ReadAuthenticatedRepresentativeId(cancellationToken);

    var entity = modelEntityConverter.ToEntity<IAuditableEntity>(model);
    entity.AuditingRepresentativeId = representativeId;

    await mutations.Delete(entity, cancellationToken);
  }
}

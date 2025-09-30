using Ozds.Business.Conversion;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Business.Queries;
using Ozds.Business.Validation;
using Ozds.Data.Entities.Abstractions;
using DataTrackableMutations = Ozds.Data.Mutations.TrackableMutations;

namespace Ozds.Business.Mutations;

public class TrackableMutations(
  DataTrackableMutations mutations,
  ModelEntityConverter modelEntityConverter,
  ModelValidator validator,
  RepresentativeQueries representativeQueries
)
  : IMutations
{
  public async Task Create(
    ITrackable model,
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

    var entity = modelEntityConverter.ToEntity<ITrackableEntity>(model);
    entity.AuditingRepresentativeId = representativeId;

    await mutations.Create(entity, cancellationToken);

    if (model is IdentifiableModel identifiableModel)
    {
      identifiableModel.Id = entity.AuditingId;
    }
  }

  public async Task Update(
    ITrackable model,
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

    var entity = modelEntityConverter.ToEntity<ITrackableEntity>(model);
    entity.AuditingRepresentativeId = representativeId;

    await mutations.Update(entity, cancellationToken);
  }

  public async Task Delete(
    ITrackable model,
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

    var entity = modelEntityConverter.ToEntity<ITrackableEntity>(model);
    entity.AuditingRepresentativeId = representativeId;

    await mutations.Delete(entity, cancellationToken);
  }

  public async Task Restore(
    ITrackable model,
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

    var entity = modelEntityConverter.ToEntity<ITrackableEntity>(model);
    entity.AuditingRepresentativeId = representativeId;
    entity.Restore = true;

    await mutations.Create(entity, cancellationToken);
  }

  public async Task Forget(
    ITrackable model,
    CancellationToken cancellationToken
  )
  {
    var representativeId = await representativeQueries
      .ReadAuthenticatedRepresentativeId(cancellationToken);

    var entity = modelEntityConverter.ToEntity<ITrackableEntity>(model);
    entity.Forget = true;
    entity.AuditingRepresentativeId = representativeId;

    await mutations.Delete(entity, cancellationToken);
  }
}

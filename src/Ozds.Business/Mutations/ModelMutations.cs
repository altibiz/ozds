using Ozds.Business.Conversion;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Business.Validation;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Base;
using DataEntityMutations = Ozds.Data.Mutations.EntityMutations;

namespace Ozds.Business.Mutations;

public class ModelMutations(
  DataEntityMutations entityMutations,
  ModelEntityConverter modelEntityConverter,
  ModelValidator modelValidator
) : IMutations
{
  public async Task Create(
    IModel model,
    CancellationToken cancellationToken
  )
  {
    var validationResult = await modelValidator.Validate(
      model,
      cancellationToken
    );
    if (validationResult.Count > 0)
    {
      throw new InvalidOperationException(
        $"Model {model.GetType()} is invalid: {string.Join(
          Environment.NewLine,
          validationResult.Select(x => x.ErrorMessage)
        )}"
      );
    }

    var entity = modelEntityConverter.ToEntity<IEntity>(model);

    await entityMutations.Create(entity, cancellationToken);

    if (model is IdentifiableModel identifiableModel &&
      entity is IdentifiableEntity identifiableEntity)
    {
      identifiableModel.Id = identifiableEntity.Id;
    }
  }

  public async Task Create(
    IEnumerable<IModel> models,
    CancellationToken cancellationToken
  )
  {
    var validationResult = await modelValidator.Validate(
      models,
      cancellationToken
    );
    if (validationResult.Count > 0)
    {
      throw new InvalidOperationException(
        $"Model {models.First().GetType()} is invalid: {string.Join(
          Environment.NewLine,
          validationResult.Select(x => x.ErrorMessage)
        )}"
      );
    }

    var entities = models
      .Select(modelEntityConverter.ToEntity<IEntity>)
      .ToList();

    await entityMutations.Create(entities, cancellationToken);

    foreach (var (model, entity) in models.Zip(entities))
    {
      if (model is IdentifiableModel identifiableModel &&
        entity is IdentifiableEntity identifiableEntity)
      {
        identifiableModel.Id = identifiableEntity.Id;
      }
    }
  }
}

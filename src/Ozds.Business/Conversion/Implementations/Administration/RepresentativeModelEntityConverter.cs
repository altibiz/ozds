using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Complex;
using Ozds.Data.Entities.Enums;

namespace Ozds.Business.Conversion.Implementations.Administration;

public class RepresentativeModelEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
      RepresentativeModel,
      AuditableModel,
      RepresentativeEntity,
      AuditableEntity>(serviceProvider)
{
  private readonly ModelEntityConverter modelEntityConverter =
    serviceProvider.GetRequiredService<ModelEntityConverter>();

  public override void InitializeEntity(
    RepresentativeModel model,
    RepresentativeEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.Role = modelEntityConverter.ToEntity<RoleEntity>(model.Role);
    entity.PhysicalPerson = model.PhysicalPerson is null ? null!
      : modelEntityConverter.ToEntity<PhysicalPersonEntity>(
          model.PhysicalPerson);
    entity.Topics = model.Topics
      .Select(topic => modelEntityConverter.ToEntity<TopicEntity>(topic))
      .ToList();
  }

  public override void InitializeModel(
    RepresentativeEntity entity,
    RepresentativeModel model
  )
  {
    base.InitializeModel(entity, model);
    model.Role = entity.Role.ToModel();
    model.PhysicalPerson = entity.PhysicalPerson is null ? null!
      : modelEntityConverter.ToModel<PhysicalPersonModel>(
          entity.PhysicalPerson);
    model.Topics = entity.Topics
      .Select(topic => modelEntityConverter.ToModel<TopicModel>(topic))
      .ToList();
  }
}

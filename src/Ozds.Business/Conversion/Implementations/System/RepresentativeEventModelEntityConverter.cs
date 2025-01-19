using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.System;

public class RepresentativeEventEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
      RepresentativeEventModel,
      EventModel,
      RepresentativeEventEntity,
      EventEntity>(serviceProvider)
{
  public override void InitializeEntity(
    RepresentativeEventModel model,
    RepresentativeEventEntity entity)
  {
    base.InitializeEntity(model, entity);
    entity.RepresentativeId = model.RepresentativeId;
  }

  public override void InitializeModel(
    RepresentativeEventEntity entity,
    RepresentativeEventModel model)
  {
    base.InitializeModel(entity, model);
    model.RepresentativeId = entity.RepresentativeId;
  }
}

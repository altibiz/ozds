using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Enums;

namespace Ozds.Business.Conversion.Implementations.System;

public class NotificationModelEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
  NotificationModel,
  IdentifiableModel,
  NotificationEntity,
  IdentifiableEntity>(serviceProvider)
{
  private readonly ModelEntityConverter modelEntityConverter =
    serviceProvider.GetRequiredService<ModelEntityConverter>();

  public override void InitializeEntity(
    NotificationModel model,
    NotificationEntity entity)
  {
    base.InitializeEntity(model, entity);
    entity.Summary = model.Summary;
    entity.Content = model.Content;
    entity.Timestamp = model.Timestamp;
    entity.EventId = model.EventId;
    entity.Topics = model.Topics
      .Select(x => modelEntityConverter.ToEntity<TopicEntity>(x))
      .ToList();
  }

  public override void InitializeModel(
    NotificationEntity entity,
    NotificationModel model)
  {
    base.InitializeModel(entity, model);
    model.Summary = entity.Summary;
    model.Content = entity.Content;
    model.Timestamp = entity.Timestamp;
    model.EventId = entity.EventId;
    model.Topics = entity.Topics
      .Select(x => modelEntityConverter.ToModel<TopicModel>(x))
      .ToHashSet();
  }
}

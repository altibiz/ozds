using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Enums;

namespace Ozds.Business.Conversion.Implementations.System;

public class EventModelEntityConverter(IServiceProvider serviceProvider)
  : InheritingModelEntityConverter<
      EventModel,
      IdentifiableModel,
      EventEntity,
      IdentifiableEntity>(serviceProvider)
{
  private readonly ModelEntityConverter modelEntityConverter =
    serviceProvider.GetRequiredService<ModelEntityConverter>();

  public override void InitializeEntity(
    EventModel model,
    EventEntity entity)
  {
    base.InitializeEntity(model, entity);
    entity.Categories = model.Categories
      .Select(category => modelEntityConverter
        .ToEntity<CategoryEntity>(category))
      .ToList();
    entity.Timestamp = model.Timestamp;
    entity.Level = modelEntityConverter.ToEntity<LevelEntity>(model.Level);
    entity.Content = model.Content;
    entity.Kind = model.Kind;
  }

  public override void InitializeModel(
    EventEntity entity,
    EventModel model)
  {
    base.InitializeModel(entity, model);
    model.Categories = entity.Categories
      .Select(category => modelEntityConverter
        .ToModel<CategoryModel>(category))
      .ToList();
    model.Timestamp = entity.Timestamp;
    model.Level = modelEntityConverter.ToModel<LevelModel>(entity.Level);
    model.Content = entity.Content;
    model.Kind = entity.Kind;
  }
}

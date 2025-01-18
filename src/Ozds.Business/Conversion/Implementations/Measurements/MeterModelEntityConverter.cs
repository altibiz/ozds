using Ozds.Business.Conversion.Base;
using Ozds.Business.Conversion.Primitive;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Enums;

namespace Ozds.Business.Conversion.Implementations.Measurements;

public class MeterModelEntityConverter(IServiceProvider serviceProvider)
  : InheritingModelEntityConverter<
      MeterModel,
      AuditableModel,
      MeterEntity,
      AuditableEntity>(serviceProvider)
{
  private readonly ModelEntityConverter modelEntityConverter =
    serviceProvider.GetRequiredService<ModelEntityConverter>();

  public override void InitializeEntity(
    MeterModel model,
    MeterEntity entity)
  {
    base.InitializeEntity(model, entity);
    entity.ConnectionPower_W = model.ConnectionPower_W.ToFloat();
    entity.MessengerId = model.MessengerId;
    entity.Phases = model.Phases
      .Select(phase => modelEntityConverter.ToEntity<PhaseEntity>(phase))
      .ToList();
    entity.Kind = model.Kind;
  }

  public override void InitializeModel(
    MeterEntity entity,
    MeterModel model)
  {
    base.InitializeModel(entity, model);
    model.ConnectionPower_W = entity.ConnectionPower_W.ToDecimal();
    model.MessengerId = entity.MessengerId;
    model.Phases = entity.Phases
      .Select(phase => modelEntityConverter.ToModel<PhaseModel>(phase))
      .ToHashSet();
    model.Kind = entity.Kind;
  }
}

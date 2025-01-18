using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.Finances;

public class CalculationModelEntityConverter(IServiceProvider serviceProvider)
  : InheritingModelEntityConverter<
      CalculationModel,
      IdentifiableModel,
      CalculationEntity,
      IdentifiableEntity>(serviceProvider)
{
  private readonly ModelEntityConverter modelEntityConverter =
    serviceProvider.GetRequiredService<ModelEntityConverter>();

  public override void InitializeEntity(
    CalculationModel model,
    CalculationEntity entity)
  {
    base.InitializeEntity(model, entity);
    entity.MeterId = model.MeterId;
    entity.ArchivedMeter = model.ArchivedMeter is null ? null! :
      modelEntityConverter.ToEntity<MeterEntity>(model.ArchivedMeter);
  }

  public override void InitializeModel(
    CalculationEntity entity,
    CalculationModel model)
  {
    base.InitializeModel(entity, model);
    model.MeterId = entity.MeterId;
    model.ArchivedMeter = entity.ArchivedMeter is null ? null! :
      modelEntityConverter.ToModel<MeterModel>(entity.ArchivedMeter);
  }
}

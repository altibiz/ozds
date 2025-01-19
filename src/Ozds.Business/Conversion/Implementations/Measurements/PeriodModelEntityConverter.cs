using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities.Complex;
using Ozds.Data.Entities.Enums;

namespace Ozds.Business.Conversion.Implementations.Measurements;

public class PeriodModelEntityConverter(IServiceProvider serviceProvider)
  : ConcreteModelEntityConverter<PeriodModel, PeriodEntity>
{
  private readonly ModelEntityConverter modelEntityConverter =
    serviceProvider.GetRequiredService<ModelEntityConverter>();

  public override void InitializeEntity(PeriodModel model, PeriodEntity entity)
  {
    base.InitializeEntity(model, entity);
    entity.Duration = modelEntityConverter
      .ToEntity<DurationEntity>(model.Duration);
    entity.Multiplier = model.Multiplier;
  }

  public override void InitializeModel(PeriodEntity entity, PeriodModel model)
  {
    base.InitializeModel(entity, model);
    model.Duration = modelEntityConverter
      .ToModel<DurationModel>(entity.Duration);
    model.Multiplier = entity.Multiplier;
  }
}

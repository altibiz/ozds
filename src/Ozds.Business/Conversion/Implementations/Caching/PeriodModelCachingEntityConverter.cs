using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Enums;
using Ozds.Caching.Entities.Complex;
using Ozds.Caching.Entities.Enums;

namespace Ozds.Business.Conversion.Implementations.Caching;

public class PeriodModelCachingEntityConverter(IServiceProvider serviceProvider)
  : ConcreteModelCachingEntityConverter<PeriodModel, PeriodEntity>
{
  private readonly ModelCachingEntityConverter modelEntityConverter =
    serviceProvider.GetRequiredService<ModelCachingEntityConverter>();

  public override void InitializeEntity(PeriodModel model, PeriodEntity entity)
  {
    base.InitializeEntity(model, entity);
    entity.Duration = modelEntityConverter.ToEntity<DurationEntity>(
      model.Duration
    );
    entity.Multiplier = model.Multiplier;
  }

  public override void InitializeModel(PeriodEntity entity, PeriodModel model)
  {
    base.InitializeModel(entity, model);
    model.Duration = modelEntityConverter.ToModel<DurationModel>(
      entity.Duration
    );
    model.Multiplier = entity.Multiplier;
  }
}

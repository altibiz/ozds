using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Enums;

namespace Ozds.Business.Conversion.Implementations.Administration;

public class MeasurementScopeModelEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
  MeasurementScopeModel,
  ScopeModel,
  MeasurementScopeEntity,
  ScopeEntity>(serviceProvider)
{
  private readonly ModelEntityConverter modelEntityConverter =
    serviceProvider.GetRequiredService<ModelEntityConverter>();

  public override void InitializeEntity(
    MeasurementScopeModel model,
    MeasurementScopeEntity entity
  )
  {
    base.InitializeEntity(model, entity);

    entity.Interval =
      modelEntityConverter.ToEntity<IntervalEntity>(model.Interval);
  }

  public override void InitializeModel(
    MeasurementScopeEntity entity,
    MeasurementScopeModel model
  )
  {
    base.InitializeModel(entity, model);

    model.Interval =
      modelEntityConverter.ToModel<IntervalModel>(entity.Interval);
  }
}

using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;
using Ozds.Caching.Entities;
using Ozds.Caching.Entities.Base;
using Ozds.Caching.Entities.Enums;

namespace Ozds.Business.Conversion.Implementations.Caching;

public class RegisterModelCachingEntityConverter(
  IServiceProvider serviceProvider
)
  : InheritingModelCachingEntityConverter<
    RegisterModel,
    TrackableModel,
    RegisterEntity,
    TrackableEntity
  >(serviceProvider)
{
  private readonly ModelCachingEntityConverter modelEntityConverter =
    serviceProvider.GetRequiredService<ModelCachingEntityConverter>();

  public override void InitializeEntity(
    RegisterModel model,
    RegisterEntity entity
  )
  {
    base.InitializeEntity(model, entity);

    entity.ScopeId = model.ScopeId;
    entity.Name = model.Name;
    entity.Measure = modelEntityConverter.ToEntity<MeasureEntity>(
      model.Measure
    );
    entity.OrderOfMagnitude = model.OrderOfMagnitude is null
      ? null
      : modelEntityConverter.ToEntity<OrderOfMagnitudeEntity>(
        model.OrderOfMagnitude
      );
    entity.Tariff = model.Tariff is null
      ? null
      : modelEntityConverter.ToEntity<TariffEntity>(model.Tariff);
    entity.Duplex = model.Duplex is null
      ? null
      : modelEntityConverter.ToEntity<DuplexEntity>(model.Duplex);
    entity.Phase = model.Phase is null
      ? null
      : modelEntityConverter.ToEntity<PhaseEntity>(model.Phase);
    entity.Aggregation = model.Aggregation is null
      ? null
      : modelEntityConverter.ToEntity<AggregationEntity>(model.Aggregation);
  }

  public override void InitializeModel(
    RegisterEntity entity,
    RegisterModel model
  )
  {
    base.InitializeModel(entity, model);

    model.ScopeId = entity.ScopeId;
    model.Name = entity.Name;
    model.Measure = modelEntityConverter.ToModel<MeasureModel>(entity.Measure);
    model.OrderOfMagnitude = entity.OrderOfMagnitude is null
      ? null
      : modelEntityConverter.ToModel<OrderOfMagnitudeModel>(
        entity.OrderOfMagnitude
      );
    model.Tariff = entity.Tariff is null
      ? null
      : modelEntityConverter.ToModel<TariffModel>(entity.Tariff);
    model.Duplex = entity.Duplex is null
      ? null
      : modelEntityConverter.ToModel<DuplexModel>(entity.Duplex);
    model.Phase = entity.Phase is null
      ? null
      : modelEntityConverter.ToModel<PhaseModel>(entity.Phase);
    model.Aggregation = entity.Aggregation is null
      ? null
      : modelEntityConverter.ToModel<AggregationModel>(entity.Aggregation);
  }
}

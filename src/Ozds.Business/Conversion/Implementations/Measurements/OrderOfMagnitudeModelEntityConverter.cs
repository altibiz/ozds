using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities.Enums;

namespace Ozds.Business.Conversion.Implementations.Measurements;

public class OrderOfMagnitudeModelEntityConverter
  : ConcreteModelEntityConverter<OrderOfMagnitudeModel, OrderOfMagnitudeEntity>
{
  public override OrderOfMagnitudeEntity ToEntity(OrderOfMagnitudeModel model)
  {
    return model switch
    {
      OrderOfMagnitudeModel.Giga => OrderOfMagnitudeEntity.Giga,
      OrderOfMagnitudeModel.Mega => OrderOfMagnitudeEntity.Mega,
      OrderOfMagnitudeModel.Kilo => OrderOfMagnitudeEntity.Kilo,
      OrderOfMagnitudeModel.Hecto => OrderOfMagnitudeEntity.Hecto,
      OrderOfMagnitudeModel.Deca => OrderOfMagnitudeEntity.Deca,
      OrderOfMagnitudeModel.Deci => OrderOfMagnitudeEntity.Deci,
      OrderOfMagnitudeModel.Centi => OrderOfMagnitudeEntity.Centi,
      OrderOfMagnitudeModel.Milli => OrderOfMagnitudeEntity.Milli,
      OrderOfMagnitudeModel.Micro => OrderOfMagnitudeEntity.Micro,
      OrderOfMagnitudeModel.Nano => OrderOfMagnitudeEntity.Nano,
      _ => throw new ArgumentOutOfRangeException(nameof(model), model, null)
    };
  }

  public override OrderOfMagnitudeModel ToModel(OrderOfMagnitudeEntity entity)
  {
    return entity switch
    {
      OrderOfMagnitudeEntity.Giga => OrderOfMagnitudeModel.Giga,
      OrderOfMagnitudeEntity.Mega => OrderOfMagnitudeModel.Mega,
      OrderOfMagnitudeEntity.Kilo => OrderOfMagnitudeModel.Kilo,
      OrderOfMagnitudeEntity.Hecto => OrderOfMagnitudeModel.Hecto,
      OrderOfMagnitudeEntity.Deca => OrderOfMagnitudeModel.Deca,
      OrderOfMagnitudeEntity.Deci => OrderOfMagnitudeModel.Deci,
      OrderOfMagnitudeEntity.Centi => OrderOfMagnitudeModel.Centi,
      OrderOfMagnitudeEntity.Milli => OrderOfMagnitudeModel.Milli,
      OrderOfMagnitudeEntity.Micro => OrderOfMagnitudeModel.Micro,
      OrderOfMagnitudeEntity.Nano => OrderOfMagnitudeModel.Nano,
      _ => throw new ArgumentOutOfRangeException(nameof(entity), entity, null)
    };
  }
}

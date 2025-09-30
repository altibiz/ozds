using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities.Enums;

namespace Ozds.Business.Conversion.Implementations.Measurements;

public class AggregationModelEntityConverter
  : ConcreteModelEntityConverter<AggregationModel, AggregationEntity>
{
  public override AggregationEntity ToEntity(AggregationModel model)
  {
    return model switch
    {
      AggregationModel.Min => AggregationEntity.Min,
      AggregationModel.Max => AggregationEntity.Max,
      AggregationModel.Avg => AggregationEntity.Avg,
      _ => throw new ArgumentOutOfRangeException(nameof(model), model, null)
    };
  }

  public override AggregationModel ToModel(AggregationEntity entity)
  {
    return entity switch
    {
      AggregationEntity.Min => AggregationModel.Min,
      AggregationEntity.Max => AggregationModel.Max,
      AggregationEntity.Avg => AggregationModel.Avg,
      _ => throw new ArgumentOutOfRangeException(nameof(entity), entity, null)
    };
  }
}

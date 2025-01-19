using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities.Enums;

namespace Ozds.Business.Conversion.Implementations.Measurements;

public class DurationModelEntityConverter
  : ConcreteModelEntityConverter<DurationModel, DurationEntity>
{
  public override DurationEntity ToEntity(DurationModel model)
  {
    return model switch
    {
      DurationModel.Second => DurationEntity.Second,
      DurationModel.Minute => DurationEntity.Minute,
      DurationModel.Hour => DurationEntity.Hour,
      DurationModel.Day => DurationEntity.Day,
      DurationModel.Week => DurationEntity.Week,
      DurationModel.Month => DurationEntity.Month,
      DurationModel.Year => DurationEntity.Year,
      _ => throw new NotImplementedException()
    };
  }

  public override DurationModel ToModel(DurationEntity entity)
  {
    return entity switch
    {
      DurationEntity.Second => DurationModel.Second,
      DurationEntity.Minute => DurationModel.Minute,
      DurationEntity.Hour => DurationModel.Hour,
      DurationEntity.Day => DurationModel.Day,
      DurationEntity.Week => DurationModel.Week,
      DurationEntity.Month => DurationModel.Month,
      DurationEntity.Year => DurationModel.Year,
      _ => throw new NotImplementedException()
    };
  }
}

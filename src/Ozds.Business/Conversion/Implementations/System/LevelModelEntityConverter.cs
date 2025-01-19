using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities.Enums;

namespace Ozds.Business.Conversion.Implementations.System;

public class LevelModelEntityConverter
  : ConcreteModelEntityConverter<LevelModel, LevelEntity>
{
  public override LevelEntity ToEntity(LevelModel model)
  {
    return model switch
    {
      LevelModel.Trace => LevelEntity.Trace,
      LevelModel.Debug => LevelEntity.Debug,
      LevelModel.Information => LevelEntity.Info,
      LevelModel.Warning => LevelEntity.Warning,
      LevelModel.Error => LevelEntity.Error,
      LevelModel.Critical => LevelEntity.Critical,
      _ => throw new NotImplementedException()
    };
  }

  public override LevelModel ToModel(LevelEntity entity)
  {
    return entity switch
    {
      LevelEntity.Trace => LevelModel.Trace,
      LevelEntity.Debug => LevelModel.Debug,
      LevelEntity.Info => LevelModel.Information,
      LevelEntity.Warning => LevelModel.Warning,
      LevelEntity.Error => LevelModel.Error,
      LevelEntity.Critical => LevelModel.Critical,
      _ => throw new NotImplementedException()
    };
  }
}

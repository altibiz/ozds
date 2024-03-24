using Ozds.Data.Entities.Enums;

namespace Ozds.Business.Models.Enums;

public enum LevelModel
{
  Trace,
  Debug,
  Information,
  Warning,
  Error,
  Critical
}

public static class LeveLModelExtensions
{
  public static LevelModel ToModel(this LevelEntity levelEntity)
  {
    return levelEntity switch
    {
      LevelEntity.Trace => LevelModel.Trace,
      LevelEntity.Debug => LevelModel.Debug,
      LevelEntity.Info => LevelModel.Information,
      LevelEntity.Warning => LevelModel.Warning,
      LevelEntity.Error => LevelModel.Error,
      LevelEntity.Critical => LevelModel.Critical,
      _ => throw new ArgumentOutOfRangeException(nameof(levelEntity), levelEntity, null)
    };
  }

  public static LevelEntity ToEntity(this LevelModel levelModel)
  {
    return levelModel switch
    {
      LevelModel.Trace => LevelEntity.Trace,
      LevelModel.Debug => LevelEntity.Debug,
      LevelModel.Information => LevelEntity.Info,
      LevelModel.Warning => LevelEntity.Warning,
      LevelModel.Error => LevelEntity.Error,
      LevelModel.Critical => LevelEntity.Critical,
      _ => throw new ArgumentOutOfRangeException(nameof(levelModel), levelModel, null)
    };
  }
}

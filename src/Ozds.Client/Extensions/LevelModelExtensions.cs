using MudBlazor;
using Ozds.Business.Models.Enums;

namespace Ozds.Client.Extensions;

public static class LevelModelExtensions
{
  public static Color ToColor(this LevelModel model)
  {
    return model switch
    {
      LevelModel.Critical => Color.Error,
      LevelModel.Error => Color.Error,
      LevelModel.Warning => Color.Warning,
      LevelModel.Information => Color.Info,
      LevelModel.Debug => Color.Secondary,
      LevelModel.Trace => Color.Tertiary,
      _ => throw new ArgumentOutOfRangeException(nameof(model), model, null)
    };
  }
}

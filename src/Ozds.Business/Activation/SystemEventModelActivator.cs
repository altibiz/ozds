using System.Text.Json;
using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities;

namespace Ozds.Business.Activation;

public class SystemEventModelActivator : ModelActivator<SystemEventModel>
{
  public override SystemEventModel ActivateConcrete()
  {
    return New();
  }

  public static SystemEventModel New()
  {
    return new SystemEventModel
    {
      Id = default!,
      Title = string.Empty,
      Timestamp = DateTimeOffset.UtcNow,
      Content = JsonSerializer.SerializeToDocument(string.Empty),
      Level = LevelModel.Information,
      Categories = new List<CategoryModel>
      {
        CategoryModel.All,
      }
    };
  }
}

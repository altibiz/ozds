using System.Text.Json;
using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Activation;

public class MessengerEventModelActivator :
  ModelActivator<MessengerEventModel>
{
  public override MessengerEventModel ActivateConcrete()
  {
    return New();
  }

  public static MessengerEventModel New()
  {
    return new MessengerEventModel
    {
      Id = default!,
      Title = string.Empty,
      Timestamp = DateTimeOffset.UtcNow,
      Content = JsonSerializer.SerializeToDocument(string.Empty),
      Level = LevelModel.Information,
      Categories = new List<CategoryModel>
      {
        CategoryModel.All
      },
      MessengerId = default!
    };
  }
}

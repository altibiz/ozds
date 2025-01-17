using System.Text.Json;
using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Activation.Implementations;

public class EventModelActivator(IServiceProvider serviceProvider)
  : InheritingModelActivator<EventModel, IdentifiableModel>(serviceProvider)
{
  public override void Initialize(EventModel model)
  {
    base.Initialize(model);
    model.Title = string.Empty;
    model.Timestamp = DateTimeOffset.UtcNow;
    model.Content = JsonSerializer.SerializeToDocument(string.Empty);
    model.Level = LevelModel.Information;
    model.Categories = new List<CategoryModel>
    {
      CategoryModel.All
    };
  }
}

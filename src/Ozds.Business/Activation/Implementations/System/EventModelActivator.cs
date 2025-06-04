using System.Text.Json;
using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;
using Ozds.Business.Queries;

namespace Ozds.Business.Activation.Implementations.System;

public class EventModelActivator(
  IServiceProvider serviceProvider,
  ClockQueries clock
)
  : InheritingModelActivator<EventModel, IdentifiableModel>(serviceProvider)
{
  public override void Initialize(EventModel model)
  {
    base.Initialize(model);
    model.Title = string.Empty;
    model.Timestamp = clock.Timestamp();
    model.Content = JsonSerializer.SerializeToDocument(string.Empty);
    model.Level = LevelModel.Information;
    model.Categories = new List<CategoryModel>
    {
      CategoryModel.All
    };
  }
}

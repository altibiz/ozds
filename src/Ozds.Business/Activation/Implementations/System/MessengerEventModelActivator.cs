using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation.Implementations.System;

public class MessengerEventModelActivator(IServiceProvider serviceProvider) :
  InheritingModelActivator<MessengerEventModel, EventModel>(serviceProvider)
{
  public override void Initialize(MessengerEventModel model)
  {
    base.Initialize(model);
    model.MessengerId = string.Empty;
  }
}

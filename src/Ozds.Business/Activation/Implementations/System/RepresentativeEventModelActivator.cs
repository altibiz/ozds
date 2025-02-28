using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation.Implementations.System;

public class RepresentativeEventModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<RepresentativeEventModel, EventModel>(
  serviceProvider
)
{
  public override void Initialize(RepresentativeEventModel model)
  {
    base.Initialize(model);

    model.RepresentativeId = string.Empty;
  }
}

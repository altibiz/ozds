using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation.Implementations.System;

public class SystemEventModelActivator(IServiceProvider serviceProvider)
  : InheritingModelActivator<SystemEventModel, EventModel>(serviceProvider)
{
}

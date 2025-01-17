using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation.Implementations.Measurements;

public class PidgeonMessengerModelActivator(IServiceProvider serviceProvider)
  : InheritingModelActivator<PidgeonMessengerModel, MessengerModel>(
    serviceProvider
  )
{
}

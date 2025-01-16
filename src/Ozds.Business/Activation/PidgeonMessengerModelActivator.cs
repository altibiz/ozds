using Ozds.Business.Activation.Base;
using Ozds.Business.Activation.Complex;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation;

public class PidgeonMessengerModelActivator(IServiceProvider serviceProvider)
  : InheritingModelActivator<PidgeonMessengerModel, MessengerModel>(
    serviceProvider
  )
{
}

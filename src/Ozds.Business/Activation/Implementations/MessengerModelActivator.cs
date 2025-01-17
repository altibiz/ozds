using Ozds.Business.Activation.Agnostic;
using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;

namespace Ozds.Business.Activation.Implementations;

public class MessengerModelActivator(IServiceProvider serviceProvider)
  : InheritingModelActivator<MessengerModel, AuditableModel>(serviceProvider)
{
  private readonly AgnosticModelActivator _agnosticModelActivator =
    serviceProvider.GetRequiredService<AgnosticModelActivator>();

  public override void Initialize(MessengerModel model)
  {
    base.Initialize(model);
    model.LocationId = "0";
    model.MaxInactivityPeriod = _agnosticModelActivator.Activate<PeriodModel>();
    model.PushDelayPeriod = _agnosticModelActivator.Activate<PeriodModel>();
  }
}

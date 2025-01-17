using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;

namespace Ozds.Business.Activation.Implementations.Measurements;

public class MessengerModelActivator(IServiceProvider serviceProvider)
  : InheritingModelActivator<MessengerModel, AuditableModel>(serviceProvider)
{
  private readonly ModelActivator modelActivator =
    serviceProvider.GetRequiredService<ModelActivator>();

  public override void Initialize(MessengerModel model)
  {
    base.Initialize(model);
    model.LocationId = "0";
    model.MaxInactivityPeriod = modelActivator.Activate<PeriodModel>();
    model.PushDelayPeriod = modelActivator.Activate<PeriodModel>();
  }
}

using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;

namespace Ozds.Business.Activation.Implementations;

public class NetworkUserModelActivator(IServiceProvider serviceProvider)
  : InheritingModelActivator<NetworkUserModel, AuditableModel>(serviceProvider)
{
  private readonly ModelActivator _agnosticModelActivator =
    serviceProvider.GetRequiredService<ModelActivator>();

  public override void Initialize(NetworkUserModel model)
  {
    base.Initialize(model);

    model.LocationId = "0";
    model.LegalPerson = _agnosticModelActivator.Activate<LegalPersonModel>();
    model.AltiBizSubProjectCode = string.Empty;
  }
}

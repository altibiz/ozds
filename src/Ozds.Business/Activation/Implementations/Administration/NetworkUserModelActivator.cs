using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;

namespace Ozds.Business.Activation.Implementations.Administration;

public class NetworkUserModelActivator(IServiceProvider serviceProvider)
  : InheritingModelActivator<NetworkUserModel, AuditableModel>(serviceProvider)
{
  private readonly ModelActivator modelActivator =
    serviceProvider.GetRequiredService<ModelActivator>();

  public override void Initialize(NetworkUserModel model)
  {
    base.Initialize(model);

    model.LocationId = "0";
    model.LegalPerson = modelActivator.Activate<LegalPersonModel>();
    model.AltiBizSubProjectCode = string.Empty;
  }
}

using Ozds.Business.Activation.Agnostic;
using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;

namespace Ozds.Business.Activation;

public class LocationModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<LocationModel, AuditableModel>(
      serviceProvider
    )
{
  private readonly AgnosticModelActivator _agnosticModelActivator =
    serviceProvider.GetRequiredService<AgnosticModelActivator>();

  public override void Initialize(LocationModel model)
  {
    base.Initialize(model);

    model.WhiteMediumNetworkUserCatalogueId = "0";
    model.BlueLowNetworkUserCatalogueId = "0";
    model.WhiteLowNetworkUserCatalogueId = "0";
    model.RedLowNetworkUserCatalogueId = "0";
    model.RegulatoryCatalogueId = "0";
    model.LegalPerson = _agnosticModelActivator
      .Activate<LegalPersonModel>();
    model.AltiBizSubProjectCode = string.Empty;
  }
}

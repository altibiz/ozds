using Ozds.Business.Activation.Agnostic;
using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Complex;

namespace Ozds.Business.Activation;

public class LocationModelActivator(
  IServiceProvider serviceProvider
) : ConcreteModelActivator<LocationModel>
{
  private readonly AgnosticModelActivator _agnosticModelActivator =
    serviceProvider.GetRequiredService<AgnosticModelActivator>();

  public override void Initialize(LocationModel model)
  {
    base.Initialize(model);

    model.WhiteMediumNetworkUserCatalogueId = default!;
    model.BlueLowNetworkUserCatalogueId = default!;
    model.WhiteLowNetworkUserCatalogueId = default!;
    model.RedLowNetworkUserCatalogueId = default!;
    model.RegulatoryCatalogueId = default!;
    model.LegalPerson = _agnosticModelActivator
      .Activate<LegalPersonModel>();
    model.AltiBizSubProjectCode = default!;
  }
}

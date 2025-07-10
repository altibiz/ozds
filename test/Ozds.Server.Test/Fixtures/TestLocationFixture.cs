using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Server.Test.Containers;

namespace Ozds.Server.Test.Fixtures;

public record LocationWithCataloguesAndMessenger(
  RegulatoryCatalogueModel RegulatoryCatalogue,
  RedLowNetworkUserCatalogueModel RedLowNetworkUserCatalogue,
  BlueLowNetworkUserCatalogueModel BlueLowNetworkUserCatalogue,
  WhiteLowNetworkUserCatalogueModel WhiteLowNetworkUserCatalogue,
  WhiteMediumNetworkUserCatalogueModel WhiteMediumNetworkUserCatalogue,
  MessengerModel Messenger,
  LocationModel Location
);

public class TestLocationFixture(
  ServiceComposition composition
)
{
  public async Task<LocationWithCataloguesAndMessenger> Create(
    CancellationToken cancellationToken
  )
  {
    var auditableFixture = new TestAuditableFixture(composition);

    var redLowNetworkUserCatalogue = await auditableFixture
      .Create<RedLowNetworkUserCatalogueModel>(cancellationToken);

    var blueLowNetworkUserCatalogue = await auditableFixture
      .Create<BlueLowNetworkUserCatalogueModel>(cancellationToken);

    var whiteLowNetworkUserCatalogue = await auditableFixture
      .Create<WhiteLowNetworkUserCatalogueModel>(cancellationToken);

    var whiteMediumNetworkUserCatalogue = await auditableFixture
      .Create<WhiteMediumNetworkUserCatalogueModel>(cancellationToken);

    var regulatoryCatalogue = await auditableFixture
      .Create<RegulatoryCatalogueModel>(cancellationToken);

    var location = await auditableFixture
      .Create<LocationModel>(
        cancellationToken, l =>
        {
          l.RedLowNetworkUserCatalogueId = redLowNetworkUserCatalogue.Id;
          l.BlueLowNetworkUserCatalogueId = blueLowNetworkUserCatalogue.Id;
          l.WhiteLowNetworkUserCatalogueId = whiteLowNetworkUserCatalogue.Id;
          l.WhiteMediumNetworkUserCatalogueId =
            whiteMediumNetworkUserCatalogue.Id;
          l.RegulatoryCatalogueId = regulatoryCatalogue.Id;
        });

    var messenger = await auditableFixture
      .Create<MessengerModel>(
        cancellationToken, m => { m.LocationId = location.Id; });

    return new LocationWithCataloguesAndMessenger(
      regulatoryCatalogue,
      redLowNetworkUserCatalogue,
      blueLowNetworkUserCatalogue,
      whiteLowNetworkUserCatalogue,
      whiteMediumNetworkUserCatalogue,
      messenger,
      location
    );
  }

  public async Task PickFirstLocationOnLocationPicker(
    CancellationToken _
  )
  {
    await composition.Playwright.Page.ClickAsync("button[type=button]");
  }
}

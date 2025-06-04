using Ozds.Business.Models;
using Ozds.Client.Test.Containers;

namespace Ozds.Client.Test.Fixtures;

public record LocationCatalogueSet(
  LocationModel Location,
  RedLowNetworkUserCatalogueModel RedLowNetworkUserCatalogue,
  BlueLowNetworkUserCatalogueModel BlueLowNetworkUserCatalogue,
  WhiteLowNetworkUserCatalogueModel WhiteLowNetworkUserCatalogue,
  WhiteMediumNetworkUserCatalogueModel WhiteMediumNetworkUserCatalogue,
  RegulatoryCatalogueModel RegulatoryCatalogue
);

public class TestLocationFixture(
  ServiceComposition composition
)
{
  public async Task<LocationCatalogueSet> CreateWithCatalogues(
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

    return new LocationCatalogueSet(
      location,
      redLowNetworkUserCatalogue,
      blueLowNetworkUserCatalogue,
      whiteLowNetworkUserCatalogue,
      whiteMediumNetworkUserCatalogue,
      regulatoryCatalogue
    );
  }

  public async Task PickFirstLocationOnLocationPicker(
    CancellationToken _
  )
  {
    await composition.Playwright.Page.ClickAsync("button[type=button]");
  }
}

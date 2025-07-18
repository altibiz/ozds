using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Server.Test.Containers;

namespace Ozds.Server.Test.Fixtures;

public record NetworkUserWithLocation(
  RegulatoryCatalogueModel RegulatoryCatalogue,
  RedLowNetworkUserCatalogueModel RedLowNetworkUserCatalogue,
  BlueLowNetworkUserCatalogueModel BlueLowNetworkUserCatalogue,
  WhiteLowNetworkUserCatalogueModel WhiteLowNetworkUserCatalogue,
  WhiteMediumNetworkUserCatalogueModel WhiteMediumNetworkUserCatalogue,
  MessengerModel Messenger,
  LocationModel Location,
  NetworkUserModel NetworkUser
);

public class TestNetworkUserFixture(
  ServiceComposition composition
)
{
  public async Task<NetworkUserWithLocation> Create(
    CancellationToken cancellationToken
  )
  {
    var locationFixture = new TestLocationFixture(composition);

    var auditableFixture = new TestAuditableFixture(composition);

    var location = await locationFixture.Create(cancellationToken);

    var networkUser = await auditableFixture
      .Create<NetworkUserModel>(
        cancellationToken, n => { n.LocationId = location.Location.Id; });

    return new NetworkUserWithLocation(
      location.RegulatoryCatalogue,
      location.RedLowNetworkUserCatalogue,
      location.BlueLowNetworkUserCatalogue,
      location.WhiteLowNetworkUserCatalogue,
      location.WhiteMediumNetworkUserCatalogue,
      location.Messenger,
      location.Location,
      networkUser
    );
  }
}

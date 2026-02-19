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

public class TestNetworkUserFixture(ServiceComposition composition)
{
  public async Task<NetworkUserWithLocation> Create(
    CancellationToken cancellationToken,
    Action<Configurator>? configure = null
  )
  {
    var configurator = new Configurator();
    if (configure is not null)
    {
      configure(configurator);
    }

    var locationFixture = new TestLocationFixture(composition);

    var trackableFixture = new TestTrackableFixture(composition);

    var location = await locationFixture.Create(
      cancellationToken,
      configurator.ConfigureLocation
    );

    var networkUser = await trackableFixture.Create<NetworkUserModel>(
      cancellationToken,
      n =>
      {
        n.LocationId = location.Location.Id;
        configurator.ConfigureNetworkUser(n);
      }
    );

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

  public class Configurator
  {
    public Action<NetworkUserModel> ConfigureNetworkUser { get; private set; } =
      _ => { };

    public Action<TestLocationFixture.Configurator> ConfigureLocation
    {
      get;
      private set;
    } = _ => { };

    public Configurator WithNetworkUser(Action<NetworkUserModel> configure)
    {
      var prior = ConfigureNetworkUser;
      ConfigureNetworkUser = x =>
      {
        prior(x);
        configure(x);
      };
      return this;
    }

    public Configurator WithLocation(
      Action<TestLocationFixture.Configurator> configure
    )
    {
      var prior = ConfigureLocation;
      ConfigureLocation = x =>
      {
        prior(x);
        configure(x);
      };
      return this;
    }
  }
}

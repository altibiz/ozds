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
    CancellationToken cancellationToken,
    Action<Configurator>? configure = null
  )
  {
    var configurator = new Configurator();
    if (configure is not null)
    {
      configure(configurator);
    }

    var trackableFixture = new TestTrackableFixture(composition);

    var redLowNetworkUserCatalogue = await trackableFixture
      .Create(
        cancellationToken,
        configurator.ConfigureRedLowNetworkUserCatalogue);

    var blueLowNetworkUserCatalogue = await trackableFixture
      .Create(
        cancellationToken,
        configurator.ConfigureBlueLowNetworkUserCatalogue);

    var whiteLowNetworkUserCatalogue = await trackableFixture
      .Create(
        cancellationToken,
        configurator.ConfigureWhiteLowNetworkUserCatalogue);

    var whiteMediumNetworkUserCatalogue = await trackableFixture
      .Create(
        cancellationToken,
        configurator.ConfigureWhiteMediumNetworkUserCatalogue);

    var regulatoryCatalogue = await trackableFixture
      .Create(
        cancellationToken,
        configurator.ConfigureRegulatoryCatalogue);

    var location = await trackableFixture
      .Create<LocationModel>(
        cancellationToken, l =>
        {
          l.RedLowNetworkUserCatalogueId = redLowNetworkUserCatalogue.Id;
          l.BlueLowNetworkUserCatalogueId = blueLowNetworkUserCatalogue.Id;
          l.WhiteLowNetworkUserCatalogueId = whiteLowNetworkUserCatalogue.Id;
          l.WhiteMediumNetworkUserCatalogueId =
            whiteMediumNetworkUserCatalogue.Id;
          l.RegulatoryCatalogueId = regulatoryCatalogue.Id;
          configurator.ConfigureLocation(l);
        });

    var messenger = await trackableFixture
      .Create<MessengerModel>(
        cancellationToken, m =>
        {
          m.LocationId = location.Id;
          configurator.ConfigureMessenger(m);
        });

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

  public class Configurator
  {
    public Action<RedLowNetworkUserCatalogueModel>
      ConfigureRedLowNetworkUserCatalogue { get; private set; } = _ => { };

    public Action<BlueLowNetworkUserCatalogueModel>
      ConfigureBlueLowNetworkUserCatalogue { get; private set; } = _ => { };

    public Action<WhiteLowNetworkUserCatalogueModel>
      ConfigureWhiteLowNetworkUserCatalogue { get; private set; } = _ => { };

    public Action<WhiteMediumNetworkUserCatalogueModel>
      ConfigureWhiteMediumNetworkUserCatalogue { get; private set; } = _ => { };

    public Action<RegulatoryCatalogueModel> ConfigureRegulatoryCatalogue
    {
      get;
      private set;
    } = _ => { };

    public Type MessengerType { get; private set; } =
      typeof(PidgeonMessengerModel);

    public Action<MessengerModel> ConfigureMessenger { get; private set; } =
      _ => { };

    public Action<LocationModel> ConfigureLocation { get; private set; } =
      _ => { };

    public Configurator WithRedLowNetworkUserCatalogue(
      Action<RedLowNetworkUserCatalogueModel> configure)
    {
      var prior = ConfigureRedLowNetworkUserCatalogue;
      ConfigureRedLowNetworkUserCatalogue = x =>
      {
        prior(x);
        configure(x);
      };
      return this;
    }

    public Configurator WithBlueLowNetworkUserCatalogue(
      Action<BlueLowNetworkUserCatalogueModel> configure)
    {
      var prior = ConfigureBlueLowNetworkUserCatalogue;
      ConfigureBlueLowNetworkUserCatalogue = x =>
      {
        prior(x);
        configure(x);
      };
      return this;
    }

    public Configurator WithWhiteLowNetworkUserCatalogue(
      Action<WhiteLowNetworkUserCatalogueModel> configure)
    {
      var prior = ConfigureWhiteLowNetworkUserCatalogue;
      ConfigureWhiteLowNetworkUserCatalogue = x =>
      {
        prior(x);
        configure(x);
      };
      return this;
    }

    public Configurator WithWhiteMediumNetworkUserCatalogue(
      Action<WhiteMediumNetworkUserCatalogueModel> configure)
    {
      var prior = ConfigureWhiteMediumNetworkUserCatalogue;
      ConfigureWhiteMediumNetworkUserCatalogue = x =>
      {
        prior(x);
        configure(x);
      };
      return this;
    }

    public Configurator WithRegulatoryCatalogue(
      Action<RegulatoryCatalogueModel> configure)
    {
      var prior = ConfigureRegulatoryCatalogue;
      ConfigureRegulatoryCatalogue = x =>
      {
        prior(x);
        configure(x);
      };
      return this;
    }

    public Configurator WithMessengerType(
      Type messengerType)
    {
      MessengerType = messengerType;
      return this;
    }

    public Configurator WithMessenger(
      Action<MessengerModel> configure)
    {
      var prior = ConfigureMessenger;
      ConfigureMessenger = x =>
      {
        prior(x);
        configure(x);
      };
      return this;
    }

    public Configurator WithLocation(
      Action<LocationModel> configure)
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

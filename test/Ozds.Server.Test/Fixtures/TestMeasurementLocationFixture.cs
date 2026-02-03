using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Server.Test.Containers;

namespace Ozds.Server.Test.Fixtures;

public record MeasurementLocationWithNetworkUserAndMeter(
  RegulatoryCatalogueModel RegulatoryCatalogue,
  RedLowNetworkUserCatalogueModel RedLowNetworkUserCatalogue,
  BlueLowNetworkUserCatalogueModel BlueLowNetworkUserCatalogue,
  WhiteLowNetworkUserCatalogueModel WhiteLowNetworkUserCatalogue,
  WhiteMediumNetworkUserCatalogueModel WhiteMediumNetworkUserCatalogue,
  MessengerModel Messenger,
  LocationModel Location,
  NetworkUserModel NetworkUser,
  MeasurementValidatorModel MeasurementValidator,
  MeterModel Meter,
  NetworkUserMeasurementLocationModel MeasurementLocation
);

public class TestMeasurementLocationFixture(
  ServiceComposition composition
)
{
  public async Task<MeasurementLocationWithNetworkUserAndMeter>
    Create(
      MeasurementLocationWithNetworkUserAndMeter basis,
      CancellationToken cancellationToken,
      Action<Configurator>? configure = null
    )
  {
    var configurator = new Configurator();
    if (configure is not null)
    {
      configure(configurator);
    }

    var meterFixture = new TestMeterFixture(composition);

    var meter = await meterFixture.Create(
      cancellationToken,
      x =>
      {
        x.WithMeter(y => y.MessengerId = basis.Messenger.Id);
        configurator.ConfigureMeter(x);
      });

    var trackableFixture = new TestTrackableFixture(composition);

    var measurementLocation = await trackableFixture.Create<NetworkUserMeasurementLocationModel>(
        cancellationToken,
        m =>
        {
          m.NetworkUserId = basis.NetworkUser.Id;
          m.MeterId = meter.Meter.Id;
          m.NetworkUserCatalogueId = configurator
            .GetNetworkUserCatalogueId(basis.Location);
          configurator.ConfigureMeasurementLocation(m);
        }
      );

    return new MeasurementLocationWithNetworkUserAndMeter(
        basis.RegulatoryCatalogue,
        basis.RedLowNetworkUserCatalogue,
        basis.BlueLowNetworkUserCatalogue,
        basis.WhiteLowNetworkUserCatalogue,
        basis.WhiteMediumNetworkUserCatalogue,
        basis.Messenger,
        basis.Location,
        basis.NetworkUser,
        meter.MeasurementValidator,
        meter.Meter,
        measurementLocation
      );
  }

  public async Task<MeasurementLocationWithNetworkUserAndMeter>
    Create(
      CancellationToken cancellationToken,
      Action<Configurator>? configure = null
    )
  {
    var configurator = new Configurator();
    if (configure is not null)
    {
      configure(configurator);
    }

    var networkUserFixture = new TestNetworkUserFixture(composition);

    var networkUser = await networkUserFixture
      .Create(cancellationToken, configurator.ConfigureNetworkUser);

    var meterFixture = new TestMeterFixture(composition);

    var meter = await meterFixture
      .Create(
        cancellationToken,
        x =>
        {
          x.WithMeter(y => y.MessengerId = networkUser.Messenger.Id);
          configurator.ConfigureMeter(x);
        });

    var trackableFixture = new TestTrackableFixture(composition);

    var measurementLocation = await trackableFixture
      .Create<NetworkUserMeasurementLocationModel>(
        cancellationToken, m =>
        {
          m.NetworkUserId = networkUser.NetworkUser.Id;
          m.MeterId = meter.Meter.Id;
          m.NetworkUserCatalogueId = configurator
            .GetNetworkUserCatalogueId(networkUser.Location);
          configurator.ConfigureMeasurementLocation(m);
        });

    return new MeasurementLocationWithNetworkUserAndMeter(
      networkUser.RegulatoryCatalogue,
      networkUser.RedLowNetworkUserCatalogue,
      networkUser.BlueLowNetworkUserCatalogue,
      networkUser.WhiteLowNetworkUserCatalogue,
      networkUser.WhiteMediumNetworkUserCatalogue,
      networkUser.Messenger,
      networkUser.Location,
      networkUser.NetworkUser,
      meter.MeasurementValidator,
      meter.Meter,
      measurementLocation
    );
  }

  public class Configurator
  {
    public Action<TestMeterFixture.Configurator> ConfigureMeter
    {
      get;
      private set;
    } =
      _ => { };

    public Action<TestNetworkUserFixture.Configurator> ConfigureNetworkUser
    {
      get;
      private set;
    } =
      _ => { };

    public Action<NetworkUserMeasurementLocationModel>
      ConfigureMeasurementLocation { get; private set; } =
      _ => { };

    public Func<LocationModel, string> GetNetworkUserCatalogueId
    {
      get;
      private set;
    } =
      location => location.RedLowNetworkUserCatalogueId;

    public Configurator WithMeter(
      Action<TestMeterFixture.Configurator> configure)
    {
      var prior = ConfigureMeter;
      ConfigureMeter = x =>
      {
        prior(x);
        configure(x);
      };
      return this;
    }

    public Configurator WithNetworkUser(
      Action<TestNetworkUserFixture.Configurator> configure)
    {
      var prior = ConfigureNetworkUser;
      ConfigureNetworkUser = x =>
      {
        prior(x);
        configure(x);
      };
      return this;
    }

    public Configurator WithNetworkUserCatalogueId(
      Func<LocationModel, string> getNetworkUserCatalogueId
    )
    {
      GetNetworkUserCatalogueId = getNetworkUserCatalogueId;
      return this;
    }

    public Configurator WithMeasurementLocation(
      Action<NetworkUserMeasurementLocationModel> configure)
    {
      var prior = ConfigureMeasurementLocation;
      ConfigureMeasurementLocation = x =>
      {
        prior(x);
        configure(x);
      };
      return this;
    }
  }
}

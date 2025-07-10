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
      .Create(cancellationToken);

    var meterFixture = new TestMeterFixture(composition);

    var meter = await meterFixture
      .Create(cancellationToken, configurator.Meter);

    var auditableFixture = new TestAuditableFixture(composition);

    var measurementLocation = await auditableFixture
      .Create<NetworkUserMeasurementLocationModel>(
        cancellationToken, m =>
        {
          m.NetworkUserId = networkUser.NetworkUser.Id;
          m.MeterId = meter.Meter.Id;
          m.NetworkUserCatalogueId = configurator
            .GetNetworkUserCatalogueId(networkUser.Location);
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
    public Action<TestMeterFixture.Configurator> Meter { get; private set; } =
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
      Meter = configure;
      return this;
    }

    public Configurator WithNetworkUserCatalogueId(
      Func<LocationModel, string> getNetworkUserCatalogueId
    )
    {
      GetNetworkUserCatalogueId = getNetworkUserCatalogueId;
      return this;
    }
  }
}

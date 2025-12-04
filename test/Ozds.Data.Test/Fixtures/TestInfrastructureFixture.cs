using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;
using Ozds.Data.Reflection;
using Ozds.Data.Test.Extensions;
using Ozds.Data.Test.Specimens;

namespace Ozds.Data.Test.Fixtures;

public record InfrastructureEntities(
  RegulatoryCatalogueEntity RegulatoryCatalogue,
  RedLowNetworkUserCatalogueEntity RedLowNetworkUserCatalogue,
  BlueLowNetworkUserCatalogueEntity BlueLowNetworkUserCatalogue,
  WhiteLowNetworkUserCatalogueEntity WhiteLowNetworkUserCatalogue,
  WhiteMediumNetworkUserCatalogueEntity WhiteMediumNetworkUserCatalogue,
  MessengerEntity Messenger,
  LocationEntity Location,
  NetworkUserEntity NetworkUser,
  MeasurementValidatorEntity MeasurementValidator,
  MeterEntity Meter,
  NetworkUserMeasurementLocationEntity MeasurementLocation
);

public class TestInfrastructureFixture(
  IDbContextFactory<DataDbContext> factory,
  EntityReflector reflector
)
{
  private static int messengerIndex;

  private static int meterIndex;

  public async Task<InfrastructureEntities> Create(
    CancellationToken cancellationToken,
    Action<Configurator>? configure = null
  )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);
    var configurator = new Configurator(reflector);
    configure?.Invoke(configurator);

    var fixture = context.ContextualFixture();
    fixture.Customizations.Add(
      new DateTimeOffsetInRangeSpecimenBuilder(
        Constants.Now.AddMonths(-1), Constants.Now));

    var redLowCatalogue = await fixture.CreateInDb(
      context,
      cancellationToken,
      configurator.ConfigureRedLowNetworkUserCatalogue);

    var blueLowCatalogue = await fixture.CreateInDb(
      context,
      cancellationToken,
      configurator.ConfigureBlueLowNetworkUserCatalogue);

    var whiteLowCatalogue = await fixture.CreateInDb(
      context,
      cancellationToken,
      configurator.ConfigureWhiteLowNetworkUserCatalogue);

    var whiteMediumCatalogue = await fixture.CreateInDb(
      context,
      cancellationToken,
      configurator.ConfigureWhiteMediumNetworkUserCatalogue);

    var regulatoryCatalogue = await fixture.CreateInDb(
      context,
      cancellationToken,
      configurator.ConfigureRegulatoryCatalogue);

    var location = await fixture.CreateInDb<LocationEntity>(
      context,
      cancellationToken,
      location =>
      {
        location.RedLowNetworkUserCatalogueId = redLowCatalogue.Id;
        location.BlueLowNetworkUserCatalogueId = blueLowCatalogue.Id;
        location.WhiteLowNetworkUserCatalogueId = whiteLowCatalogue.Id;
        location.WhiteMediumNetworkUserCatalogueId = whiteMediumCatalogue.Id;
        location.RegulatoryCatalogueId = regulatoryCatalogue.Id;
        configurator.ConfigureLocation(location);
      });

    var messenger = await fixture.CreateInDb<MessengerEntity>(
      context,
      configurator.MessengerType,
      cancellationToken,
      messenger =>
      {
        messenger.Id = $"pidgeon-{Interlocked.Increment(ref messengerIndex)}";
        messenger.LocationId = location.Id;
        configurator.ConfigureMessenger(messenger);
      });

    var networkUser = await fixture.CreateInDb<NetworkUserEntity>(
      context,
      cancellationToken,
      networkUser =>
      {
        networkUser.LocationId = location.Id;
        configurator.ConfigureNetworkUser(networkUser);
      });

    var validator = await fixture.CreateInDb(
      context,
      configurator.MeasurementValidatorType,
      cancellationToken,
      configurator.ConfigureMeasurementValidator);

    var meter = await fixture.CreateInDb<MeterEntity>(
      context,
      configurator.MeterType,
      cancellationToken,
      meter =>
      {
        meter.Id = $"meter-{Interlocked.Increment(ref meterIndex)}";
        meter.MeasurementValidatorId = validator.Id;
        if (configurator.AttachMessengerToMeter)
        {
          meter.MessengerId = messenger.Id;
        }

        configurator.ConfigureMeter(meter);
      });

    var measurementLocation = await fixture
      .CreateInDb<NetworkUserMeasurementLocationEntity>(
        context,
        cancellationToken,
        measurementLocation =>
        {
          measurementLocation.NetworkUserId = networkUser.Id;
          measurementLocation.MeterId = meter.Id;
          measurementLocation.NetworkUserCatalogueId =
            configurator.GetNetworkUserCatalogueId(location);
          configurator.ConfigureMeasurementLocation(measurementLocation);
        });

    return new InfrastructureEntities(
      regulatoryCatalogue,
      redLowCatalogue,
      blueLowCatalogue,
      whiteLowCatalogue,
      whiteMediumCatalogue,
      messenger,
      location,
      networkUser,
      validator,
      meter,
      measurementLocation
    );
  }

  public class Configurator(EntityReflector reflector)
  {
    public Action<RedLowNetworkUserCatalogueEntity>
      ConfigureRedLowNetworkUserCatalogue { get; private set; } = _ => { };

    public Action<BlueLowNetworkUserCatalogueEntity>
      ConfigureBlueLowNetworkUserCatalogue { get; private set; } = _ => { };

    public Action<WhiteLowNetworkUserCatalogueEntity>
      ConfigureWhiteLowNetworkUserCatalogue { get; private set; } = _ => { };

    public Action<WhiteMediumNetworkUserCatalogueEntity>
      ConfigureWhiteMediumNetworkUserCatalogue { get; private set; } = _ => { };

    public Action<RegulatoryCatalogueEntity> ConfigureRegulatoryCatalogue
    {
      get;
      private set;
    } = _ => { };

    public Action<LocationEntity> ConfigureLocation { get; private set; } =
      _ => { };

    public Action<NetworkUserEntity>
      ConfigureNetworkUser { get; private set; } = _ => { };

    public Action<MessengerEntity> ConfigureMessenger { get; private set; } =
      _ => { };

    public Action<MeasurementValidatorEntity> ConfigureMeasurementValidator
    {
      get;
      private set;
    } = _ => { };

    public Action<MeterEntity> ConfigureMeter { get; private set; } = _ => { };

    public Action<NetworkUserMeasurementLocationEntity>
      ConfigureMeasurementLocation { get; private set; } = _ => { };

    public Type MessengerType { get; private set; } =
      typeof(PidgeonMessengerEntity);

    public Type MeasurementValidatorType { get; private set; } =
      typeof(AbbB2xMeasurementValidatorEntity);

    public Type MeterType { get; private set; } = typeof(AbbB2xMeterEntity);
    public bool AttachMessengerToMeter { get; private set; } = true;

    public Func<LocationEntity, string> GetNetworkUserCatalogueId
    {
      get;
      private set;
    } =
      location => location.RedLowNetworkUserCatalogueId;

    public Configurator WithRedLowNetworkUserCatalogue(
      Action<RedLowNetworkUserCatalogueEntity> configure)
    {
      ConfigureRedLowNetworkUserCatalogue = Chain(
        ConfigureRedLowNetworkUserCatalogue, configure);
      return this;
    }

    public Configurator WithBlueLowNetworkUserCatalogue(
      Action<BlueLowNetworkUserCatalogueEntity> configure)
    {
      ConfigureBlueLowNetworkUserCatalogue = Chain(
        ConfigureBlueLowNetworkUserCatalogue, configure);
      return this;
    }

    public Configurator WithWhiteLowNetworkUserCatalogue(
      Action<WhiteLowNetworkUserCatalogueEntity> configure)
    {
      ConfigureWhiteLowNetworkUserCatalogue = Chain(
        ConfigureWhiteLowNetworkUserCatalogue, configure);
      return this;
    }

    public Configurator WithWhiteMediumNetworkUserCatalogue(
      Action<WhiteMediumNetworkUserCatalogueEntity> configure)
    {
      ConfigureWhiteMediumNetworkUserCatalogue = Chain(
        ConfigureWhiteMediumNetworkUserCatalogue, configure);
      return this;
    }

    public Configurator WithRegulatoryCatalogue(
      Action<RegulatoryCatalogueEntity> configure)
    {
      ConfigureRegulatoryCatalogue = Chain(
        ConfigureRegulatoryCatalogue, configure);
      return this;
    }

    public Configurator WithMessengerType(Type t)
    {
      MessengerType = t;
      return this;
    }

    public Configurator WithMeterType(Type t)
    {
      MeterType = t;
      MeasurementValidatorType =
        reflector.ResolveMeterMeasurementValidatorType(t);
      return this;
    }

    public Configurator WithAttachMessengerToMeter(bool attach = true)
    {
      AttachMessengerToMeter = attach;
      return this;
    }

    public Configurator WithLocation(Action<LocationEntity> configure)
    {
      ConfigureLocation = Chain(ConfigureLocation, configure);
      return this;
    }

    public Configurator WithNetworkUser(Action<NetworkUserEntity> configure)
    {
      ConfigureNetworkUser = Chain(ConfigureNetworkUser, configure);
      return this;
    }

    public Configurator WithMessenger(Action<MessengerEntity> configure)
    {
      ConfigureMessenger = Chain(ConfigureMessenger, configure);
      return this;
    }

    public Configurator WithMeasurementValidator(
      Action<MeasurementValidatorEntity> configure)
    {
      ConfigureMeasurementValidator = Chain(
        ConfigureMeasurementValidator, configure);
      return this;
    }

    public Configurator WithMeter(Action<MeterEntity> configure)
    {
      ConfigureMeter = Chain(ConfigureMeter, configure);
      return this;
    }

    public Configurator WithMeasurementLocation(
      Action<NetworkUserMeasurementLocationEntity> configure)
    {
      ConfigureMeasurementLocation = Chain(
        ConfigureMeasurementLocation, configure);
      return this;
    }

    public Configurator WithNetworkUserCatalogueId(
      Func<LocationEntity, string> pickId)
    {
      GetNetworkUserCatalogueId = pickId;
      return this;
    }

    private static Action<T> Chain<T>(Action<T> first, Action<T> second)
    {
      return item =>
      {
        first(item);
        second(item);
      };
    }
  }
}

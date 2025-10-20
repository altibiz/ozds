using Ozds.Business.Activation;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;
using Ozds.Business.Models.Joins;
using Ozds.Business.Reflection;
using Ozds.Server.Test.Containers;

namespace Ozds.Server.Test.Fixtures;

public class TestScopeFixture(
  ServiceComposition composition
)
{
  public async
    Task<MeasurementScopeWithRegisters>
    CreateMeasurementForLocationWithRegisters(
      LocationModel location,
      IEnumerable<TestRegister> testRegisters,
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

    var modelReflector = composition.Ozds.Services
      .GetRequiredService<ModelReflector>();

    var scope = await trackableFixture.Create<MeasurementScopeModel>(
      cancellationToken,
      x =>
      {
        x.ScopeModelType = modelReflector
          .ResolveModelName(typeof(LocationModel));
        x.ScopeModelId = location.Id;

        configurator.ConfigureMeasurementScope(x);
      });

    var registers = await Task.WhenAll(
      testRegisters
        .Select(
          async testRegister =>
          {
            return await trackableFixture
              .Create<RegisterModel>(
                cancellationToken,
                register =>
                {
                  register.ScopeId = scope.Id;
                  TestRegisterToRegisterModel(testRegister, register);
                });
          }));

    return new MeasurementScopeWithRegisters(
      registers.ToList(),
      scope
    );
  }

  public RegisterModel TestRegisterToRegisterModel(
    TestRegister testRegister,
    RegisterModel? register = null)
  {
    if (register is null)
    {
      var activator = composition.Ozds.Services
        .GetRequiredService<ModelActivator>();
      register = activator.Activate<RegisterModel>();
    }

    register.Name = testRegister.Name;
    register.Measure = testRegister.Measure;
    register.OrderOfMagnitude = testRegister.OrderOfMagnitude;
    register.Tariff = testRegister.Tariff;
    register.Duplex = testRegister.Duplex;
    register.Phase = testRegister.Phase;
    register.Aggregation = testRegister.Aggregation;
    return register;
  }

  public class Configurator
  {
    public Action<TestMeasurementLocationFixture.Configurator>
      ConfigureMeasurementLocation { get; private set; } = _ => { };

    public Action<ApiKeyModel> ConfigureApiKey { get; private set; } =
      _ => { };

    public Action<MeasurementScopeModel> ConfigureMeasurementScope
    {
      get;
      private set;
    } =
      _ => { };

    public Action<ApiKeyScopeModel> ConfigureApiKeyScope { get; private set; } =
      _ => { };

    public Configurator WithMeasurementLocation(
      Action<TestMeasurementLocationFixture.Configurator> configure)
    {
      var prior = ConfigureMeasurementLocation;
      ConfigureMeasurementLocation = x =>
      {
        prior(x);
        configure(x);
      };
      return this;
    }

    public Configurator WithApiKey(
      Action<ApiKeyModel> configure)
    {
      var prior = ConfigureApiKey;
      ConfigureApiKey = x =>
      {
        prior(x);
        configure(x);
      };
      return this;
    }

    public Configurator WithMeasurementScope(
      Action<MeasurementScopeModel> configure)
    {
      var prior = ConfigureMeasurementScope;
      ConfigureMeasurementScope = x =>
      {
        prior(x);
        configure(x);
      };
      return this;
    }

    public Configurator WithApiKeyScope(
      Action<ApiKeyScopeModel> configure)
    {
      var prior = ConfigureApiKeyScope;
      ConfigureApiKeyScope = x =>
      {
        prior(x);
        configure(x);
      };
      return this;
    }
  }
}

public record MeasurementScopeWithRegisters(
  List<RegisterModel> Registers,
  MeasurementScopeModel MeasurementScope
);

#pragma warning disable SA1310 // Field names should not contain underscore
public record TestRegister(
  string Name,
  MeasureModel Measure,
  OrderOfMagnitudeModel? OrderOfMagnitude,
  TariffModel? Tariff,
  DuplexModel? Duplex,
  PhaseModel? Phase,
  AggregationModel? Aggregation
)
{
  public static readonly TestRegister CurrentL1AnyT0_A =
    new(
      "current_l1_any_t0_a",
      MeasureModel.Current,
      default,
      TariffModel.T0,
      DuplexModel.Any,
      PhaseModel.L1,
      AggregationModel.Avg
    );

  public static readonly TestRegister CurrentL2AnyT0_A =
    new(
      "current_l2_any_t0_a",
      MeasureModel.Current,
      default,
      TariffModel.T0,
      DuplexModel.Any,
      PhaseModel.L2,
      AggregationModel.Avg
    );

  public static readonly TestRegister CurrentL3AnyT0_A =
    new(
      "current_l3_any_t0_a",
      MeasureModel.Current,
      default,
      TariffModel.T0,
      DuplexModel.Any,
      PhaseModel.L3,
      AggregationModel.Avg
    );

  public static readonly TestRegister VoltageL1AnyT0_V =
    new(
      "voltage_l1_any_t0_v",
      MeasureModel.Voltage,
      default,
      TariffModel.T0,
      DuplexModel.Any,
      PhaseModel.L1,
      AggregationModel.Avg
    );

  public static readonly TestRegister VoltageL2AnyT0_V =
    new(
      "voltage_l2_any_t0_v",
      MeasureModel.Voltage,
      default,
      TariffModel.T0,
      DuplexModel.Any,
      PhaseModel.L2,
      AggregationModel.Avg
    );

  public static readonly TestRegister VoltageL3AnyT0_V =
    new(
      "voltage_l3_any_t0_v",
      MeasureModel.Voltage,
      default,
      TariffModel.T0,
      DuplexModel.Any,
      PhaseModel.L3,
      AggregationModel.Avg
    );

  public static readonly TestRegister ActivePowerL1NetT0_W =
    new(
      "active_power_l1_net_t0_w",
      MeasureModel.ActivePower,
      default,
      TariffModel.T0,
      DuplexModel.Net,
      PhaseModel.L1,
      AggregationModel.Avg
    );

  public static readonly TestRegister ActivePowerL2NetT0_W =
    new(
      "active_power_l2_net_t0_w",
      MeasureModel.ActivePower,
      default,
      TariffModel.T0,
      DuplexModel.Net,
      PhaseModel.L2,
      AggregationModel.Avg
    );

  public static readonly TestRegister ActivePowerL3NetT0_W =
    new(
      "active_power_l3_net_t0_w",
      MeasureModel.ActivePower,
      default,
      TariffModel.T0,
      DuplexModel.Net,
      PhaseModel.L3,
      AggregationModel.Avg
    );

  public static readonly TestRegister ReactivePowerTotalNetT0_VAR =
    new(
      "reactive_power_total_net_t0_var",
      MeasureModel.ReactivePower,
      default,
      TariffModel.T0,
      DuplexModel.Net,
      default,
      AggregationModel.Avg
    );

  public static readonly TestRegister ApparentPowerTotalNetT0_VA =
    new(
      "apparent_power_total_net_t0_va",
      MeasureModel.ApparentPower,
      default,
      TariffModel.T0,
      DuplexModel.Net,
      default,
      AggregationModel.Avg
    );

  public static readonly TestRegister ActiveEnergyL1ImportT0_Wh =
    new(
      "active_energy_l1_import_t0_wh",
      MeasureModel.ActiveEnergy,
      default,
      TariffModel.T0,
      DuplexModel.Import,
      PhaseModel.L1,
      AggregationModel.Min
    );

  public static readonly TestRegister ActiveEnergyL2ImportT0_Wh =
    new(
      "active_energy_l2_import_t0_wh",
      MeasureModel.ActiveEnergy,
      default,
      TariffModel.T0,
      DuplexModel.Import,
      PhaseModel.L2,
      AggregationModel.Min
    );

  public static readonly TestRegister ActiveEnergyL3ImportT0_Wh =
    new(
      "active_energy_l3_import_t0_wh",
      MeasureModel.ActiveEnergy,
      default,
      TariffModel.T0,
      DuplexModel.Import,
      PhaseModel.L3,
      AggregationModel.Min
    );

  public static readonly TestRegister ActiveEnergyTotalImportT0_Wh =
    new(
      "active_energy_total_import_t0_wh",
      MeasureModel.ActiveEnergy,
      default,
      TariffModel.T0,
      DuplexModel.Import,
      default,
      AggregationModel.Min
    );

  public static readonly TestRegister ActiveEnergyTotalExportT0_Wh =
    new(
      "active_energy_total_export_t0_wh",
      MeasureModel.ActiveEnergy,
      default,
      TariffModel.T0,
      DuplexModel.Export,
      default,
      AggregationModel.Min
    );

  public static readonly TestRegister ReactiveEnergyTotalImportT0_VARh =
    new(
      "reactive_energy_total_import_t0_varh",
      MeasureModel.ReactiveEnergy,
      default,
      TariffModel.T0,
      DuplexModel.Import,
      default,
      AggregationModel.Min
    );

  public static readonly TestRegister ReactiveEnergyTotalExportT0_VARh =
    new(
      "reactive_energy_total_export_t0_varh",
      MeasureModel.ReactiveEnergy,
      default,
      TariffModel.T0,
      DuplexModel.Export,
      default,
      AggregationModel.Min
    );

  public static readonly TestRegister ActiveEnergyTotalImportT1_Wh =
    new(
      "active_energy_total_import_t1_wh",
      MeasureModel.ActiveEnergy,
      default,
      TariffModel.T1,
      DuplexModel.Import,
      default,
      AggregationModel.Min
    );

  public static readonly TestRegister ActiveEnergyTotalImportT2_Wh =
    new(
      "active_energy_total_import_t2_wh",
      MeasureModel.ActiveEnergy,
      default,
      TariffModel.T2,
      DuplexModel.Import,
      default,
      AggregationModel.Min
    );

  public static readonly IReadOnlyCollection<TestRegister>
    SchneideriEM3xxxSet =
      new List<TestRegister>
      {
        CurrentL1AnyT0_A,
        CurrentL2AnyT0_A,
        CurrentL3AnyT0_A,
        VoltageL1AnyT0_V,
        VoltageL2AnyT0_V,
        VoltageL3AnyT0_V,
        ActivePowerL1NetT0_W,
        ActivePowerL2NetT0_W,
        ActivePowerL3NetT0_W,
        ReactivePowerTotalNetT0_VAR,
        ApparentPowerTotalNetT0_VA,
        ActiveEnergyL1ImportT0_Wh,
        ActiveEnergyL2ImportT0_Wh,
        ActiveEnergyL3ImportT0_Wh,
        ActiveEnergyTotalImportT0_Wh,
        ActiveEnergyTotalExportT0_Wh,
        ReactiveEnergyTotalImportT0_VARh,
        ReactiveEnergyTotalExportT0_VARh,
        ActiveEnergyTotalImportT1_Wh,
        ActiveEnergyTotalImportT2_Wh
      };
}
#pragma warning restore SA1310 // Field names should not contain underscore

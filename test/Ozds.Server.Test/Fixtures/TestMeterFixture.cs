using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Server.Test.Containers;

namespace Ozds.Server.Test.Fixtures;

public record MeterWithMeasurementValidator(
  MeasurementValidatorModel MeasurementValidator,
  MeterModel Meter
);

public class TestMeterFixture(ServiceComposition composition)
{
  public async Task<MeterWithMeasurementValidator> Create(
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

    var measurementValidator =
      await trackableFixture.Create(
        configurator.MeasurementValidatorType,
        cancellationToken,
        measurementValidator =>
        {
          configurator.ConfigureMeasurementValidator(
            (measurementValidator as MeasurementValidatorModel)!
          );
        }
      ) as MeasurementValidatorModel;
    if (measurementValidator is null)
    {
      throw new InvalidOperationException(
        "Cannot create measurement validator of type"
          + configurator.MeasurementValidatorType
      );
    }

    var meter =
      await trackableFixture.Create(
        configurator.MeterType,
        cancellationToken,
        m =>
        {
          (m as MeterModel)!.MeasurementValidatorId = measurementValidator.Id;
          configurator.ConfigureMeter((m as MeterModel)!);
        }
      ) as MeterModel;
    if (meter is null)
    {
      throw new InvalidOperationException(
        "Cannot create meter of type" + configurator.MeterType
      );
    }

    return new MeterWithMeasurementValidator(measurementValidator, meter);
  }

  public class Configurator
  {
    public Type MeasurementValidatorType { get; private set; } =
      typeof(SchneideriEM3xxxMeasurementValidatorModel);

    public Action<MeasurementValidatorModel> ConfigureMeasurementValidator
    {
      get;
      private set;
    } = _ => { };

    public Type MeterType { get; private set; } =
      typeof(SchneideriEM3xxxMeterModel);

    public Action<MeterModel> ConfigureMeter { get; private set; } = _ => { };

    public Configurator WithMeterType(Type meterType)
    {
      MeterType = meterType;
      return this;
    }

    public Configurator WithMeter(Action<MeterModel> configure)
    {
      var prior = ConfigureMeter;
      ConfigureMeter = x =>
      {
        prior(x);
        configure(x);
      };
      return this;
    }

    public Configurator WithMeasurementValidatorType(
      Type measurementValidatorType
    )
    {
      MeasurementValidatorType = measurementValidatorType;
      return this;
    }

    public Configurator WithMeasurementValidator(
      Action<MeasurementValidatorModel> configure
    )
    {
      var prior = ConfigureMeasurementValidator;
      ConfigureMeasurementValidator = x =>
      {
        prior(x);
        configure(x);
      };
      return this;
    }
  }
}

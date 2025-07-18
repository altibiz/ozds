using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Server.Test.Containers;

namespace Ozds.Server.Test.Fixtures;

public record MeterWithMeasurementValidator(
  MeasurementValidatorModel MeasurementValidator,
  MeterModel Meter
);

public class TestMeterFixture(
  ServiceComposition composition
)
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

    var auditableFixture = new TestAuditableFixture(composition);

    var measurementValidator = await auditableFixture
        .Create(configurator.MeasurementValidatorType, cancellationToken)
      as MeasurementValidatorModel;
    if (measurementValidator is null)
    {
      throw new InvalidOperationException(
        "Cannot create measurement validator of type"
        + configurator.MeasurementValidatorType
      );
    }

    var meter = await auditableFixture
      .Create(
        configurator.MeterType, cancellationToken,
        m =>
        {
          (m as MeterModel)!.MeasurementValidatorId = measurementValidator.Id;
        }) as MeterModel;
    if (meter is null)
    {
      throw new InvalidOperationException(
        "Cannot create meter of type"
        + configurator.MeterType
      );
    }

    return new MeterWithMeasurementValidator(
      measurementValidator,
      meter
    );
  }

  public class Configurator
  {
    public Type MeasurementValidatorType { get; private set; } =
      typeof(SchneideriEM3xxxMeasurementValidatorModel);

    public Type MeterType { get; private set; } =
      typeof(SchneideriEM3xxxMeterModel);

    public Configurator WithMeterType(Type meterType)
    {
      MeterType = meterType;
      return this;
    }

    public Configurator WithMeasurementValidatorType(
      Type measurementValidatorType
    )
    {
      MeasurementValidatorType = measurementValidatorType;
      return this;
    }
  }
}

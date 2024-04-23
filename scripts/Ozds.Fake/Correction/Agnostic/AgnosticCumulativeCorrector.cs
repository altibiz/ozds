using Ozds.Fake.Correction.Abstractions;
using Ozds.Fake.Records.Abstractions;

namespace Ozds.Fake.Correction.Agnostic;

public class AgnosticCumulativeCorrector
{
  private readonly IServiceProvider _serviceProvider;

  public AgnosticCumulativeCorrector(IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }

  public IMeasurementRecord CorrectCumulatives(
    DateTimeOffset timestamp,
    IMeasurementRecord measurementRecord,
    IMeasurementRecord firstMeasurementRecord,
    IMeasurementRecord lastMeasurementRecord
  )
  {
    var corrector = _serviceProvider.GetServices<ICumulativeCorrector>()
                      .FirstOrDefault(c =>
                        c.CanCorrectCumulativesFor(measurementRecord.GetType()))
                    ?? throw new InvalidOperationException(
                      $"No corrector found for {measurementRecord.GetType().Name}");

    return corrector.CorrectCumulatives(
      timestamp,
      measurementRecord,
      firstMeasurementRecord,
      lastMeasurementRecord
    );
  }
}

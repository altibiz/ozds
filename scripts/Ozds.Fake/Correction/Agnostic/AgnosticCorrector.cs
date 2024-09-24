using Ozds.Fake.Correction.Abstractions;
using Ozds.Fake.Records.Abstractions;

namespace Ozds.Fake.Correction.Agnostic;

public class AgnosticCorrector(IServiceProvider serviceProvider)
{
  private readonly IServiceProvider _serviceProvider = serviceProvider;

  public IMeasurementRecord CorrectMeterId(
    IMeasurementRecord measurementRecord,
    string meterId
  )
  {
    var corrector = _serviceProvider.GetServices<ICorrector>()
        .FirstOrDefault(
          c =>
            c.CanCorrectFor(
              measurementRecord.GetType()))
      ?? throw new InvalidOperationException(
        $"No corrector found for {measurementRecord.GetType().Name}");

    return corrector.CorrectMeterId(measurementRecord, meterId);
  }

  public IMeasurementRecord CorrectTimestamp(
    IMeasurementRecord measurementRecord,
    DateTimeOffset timestamp
  )
  {
    var corrector = _serviceProvider.GetServices<ICorrector>()
        .FirstOrDefault(
          c =>
            c.CanCorrectFor(
              measurementRecord.GetType()))
      ?? throw new InvalidOperationException(
        $"No corrector found for {measurementRecord.GetType().Name}");

    return corrector.CorrectTimestamp(measurementRecord, timestamp);
  }

  public IMeasurementRecord CorrectCumulatives(
    DateTimeOffset timestamp,
    IMeasurementRecord measurementRecord,
    IMeasurementRecord firstMeasurementRecord,
    IMeasurementRecord lastMeasurementRecord
  )
  {
    var corrector = _serviceProvider.GetServices<ICorrector>()
        .FirstOrDefault(
          c =>
            c.CanCorrectFor(
              measurementRecord.GetType()))
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

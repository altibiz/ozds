using Ozds.Business.Iot;
using Ozds.Fake.Conversion.Abstractions;
using Ozds.Fake.Generators.Abstractions;
using Ozds.Fake.Loaders;
using Ozds.Fake.Records.Abstractions;

// TODO: check time logic here
// TODO: fixup cumulatives

namespace Ozds.Fake.Generators.Base;

public abstract class
  RepeatingCsvResourceMeasurementGenerator<TMeasurement> : IMeasurementGenerator
  where TMeasurement : IMeasurementRecord
{
  private readonly ResourceCache _resources;

  private readonly IServiceProvider _serviceProvider;

  public RepeatingCsvResourceMeasurementGenerator(
    IServiceProvider serviceProvider)
  {
    _resources = serviceProvider.GetRequiredService<ResourceCache>();
    _serviceProvider = serviceProvider;
  }

  protected abstract string CsvResourceName { get; }

  protected abstract string MeterIdPrefix { get; }

  public bool CanGenerateMeasurementsFor(string meterId)
  {
    return meterId.StartsWith(MeterIdPrefix);
  }

  public async Task<List<MessengerPushRequestMeasurement>> GenerateMeasurements(
    DateTimeOffset dateFrom,
    DateTimeOffset dateTo,
    string meterId
  )
  {
    var records = await _resources
      .GetAsync<CsvLoader<TMeasurement>, List<TMeasurement>>(CsvResourceName);
    var csvRecordsMinTimestamp = records.Min(record => record.Timestamp);
    var csvRecordsMaxTimestamp = records.Max(record => record.Timestamp);
    var csvRecordsTimeSpan = csvRecordsMaxTimestamp - csvRecordsMinTimestamp;
    dateFrom = csvRecordsMinTimestamp.AddTicks(
      (dateFrom - csvRecordsMinTimestamp).Ticks % csvRecordsTimeSpan.Ticks
    );
    dateTo = csvRecordsMinTimestamp.AddTicks(
      (dateTo - csvRecordsMinTimestamp).Ticks % csvRecordsTimeSpan.Ticks
    );
    return records
      .Where(record =>
        record.Timestamp >= dateFrom
        && record.Timestamp <= dateTo)
      .Select(measurement =>
      {
        var converter = _serviceProvider
          .GetServices<IMeasurementRecordPushRequestConverter>()
          .FirstOrDefault(c => c.CanConvertToPushRequest(measurement));
        if (converter is null)
        {
          return null;
        }

        var timestamp = csvRecordsMinTimestamp.AddTicks(
          (measurement.Timestamp - csvRecordsMinTimestamp).Ticks %
          csvRecordsTimeSpan.Ticks
        );

        return new MessengerPushRequestMeasurement(
          measurement.MeterId,
          timestamp,
          converter.ConvertToPushRequest(measurement)
        );
      })
      .OfType<MessengerPushRequestMeasurement>()
      .ToList();
  }
}

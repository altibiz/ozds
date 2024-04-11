using Ozds.Business.Iot;
using Ozds.Fake.Conversion.Agnostic;
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
  private readonly AgnosticMeasurementRecordPushRequestConverter _converter;
  private readonly ResourceCache _resources;

  public RepeatingCsvResourceMeasurementGenerator(
    IServiceProvider serviceProvider)
  {
    _resources = serviceProvider.GetRequiredService<ResourceCache>();
    _converter = serviceProvider
      .GetRequiredService<AgnosticMeasurementRecordPushRequestConverter>();
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
    var now = DateTimeOffset.UtcNow;
    var records = await _resources
      .GetAsync<CsvLoader<TMeasurement>, List<TMeasurement>>(CsvResourceName);
    var csvRecordsMinTimestamp = records.Min(record => record.Timestamp);
    var csvRecordsMaxTimestamp = records.Max(record => record.Timestamp);
    var csvRecordsTimeSpan = csvRecordsMaxTimestamp - csvRecordsMinTimestamp;
    var dateFromCsv = csvRecordsMinTimestamp.AddTicks(
      (dateFrom - csvRecordsMinTimestamp).Ticks % csvRecordsTimeSpan.Ticks
    );
    var dateToCsv = csvRecordsMinTimestamp.AddTicks(
      (dateTo - csvRecordsMinTimestamp).Ticks % csvRecordsTimeSpan.Ticks
    );
    return records
      .Where(record =>
        record.Timestamp >= dateFromCsv
        && record.Timestamp <= dateToCsv)
      .Select(record =>
      {
        var pushRequest = _converter.ConvertToPushRequest(record);
        var timestamp = dateFrom.AddTicks(
          (record.Timestamp - dateFromCsv).Ticks
        );
        pushRequest["Timestamp"] = timestamp;

        return new MessengerPushRequestMeasurement(
          record.MeterId,
          now,
          pushRequest
        );
      })
      .OfType<MessengerPushRequestMeasurement>()
      .ToList();
  }
}

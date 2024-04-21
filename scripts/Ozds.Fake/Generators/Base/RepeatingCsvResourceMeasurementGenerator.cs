using Ozds.Business.Iot;
using Ozds.Fake.Conversion.Agnostic;
using Ozds.Fake.Generators.Abstractions;
using Ozds.Fake.Loaders;
using Ozds.Fake.Records.Abstractions;

// TODO: check time logic here
// TODO: add energy logic
// TODO: fixup cumulatives
// TODO: repeating enumerable

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
    var records = await _resources
      .GetAsync<CsvLoader<TMeasurement>, List<TMeasurement>>(CsvResourceName);
    return ExpandRecords(records, dateFrom, dateTo).ToList();
  }

  private IEnumerable<MessengerPushRequestMeasurement> ExpandRecords(
    List<TMeasurement> records,
    DateTimeOffset dateFrom,
    DateTimeOffset dateTo
  )
  {
    var csvRecordsMinTimestamp = records.Min(record => record.Timestamp);
    var csvRecordsMaxTimestamp = records.Max(record => record.Timestamp);
    var csvRecordsTimeSpan = csvRecordsMaxTimestamp - csvRecordsMinTimestamp;

    var timeSpan = dateTo - dateFrom;
    var dateFromCsv = csvRecordsMinTimestamp.AddTicks(
      (dateFrom - csvRecordsMinTimestamp).Ticks % csvRecordsTimeSpan.Ticks
    );
    var dateToCsv = dateFromCsv + timeSpan > csvRecordsMaxTimestamp
      ? csvRecordsMaxTimestamp
      : dateFromCsv + timeSpan;
    var currentDateFrom = dateFrom;
    var currentDateTo = dateFrom + (dateToCsv - dateFromCsv);
    while (timeSpan > TimeSpan.Zero)
    {
      foreach (var record in records
        .OrderBy(record => record.Timestamp)
        .Where(record =>
          record.Timestamp >= dateFromCsv
          && record.Timestamp <= dateToCsv))
      {
        var timestamp = currentDateFrom.AddTicks(
          (record.Timestamp - dateFromCsv).Ticks
        );
        yield return new MessengerPushRequestMeasurement(
          record.MeterId,
          timestamp,
          _converter.ConvertToPushRequest(record)
        );
      }

      timeSpan -= dateToCsv - dateFromCsv;

      dateFromCsv = dateToCsv == csvRecordsMaxTimestamp
        ? csvRecordsMinTimestamp
        : dateToCsv;
      dateToCsv = dateFromCsv + timeSpan > csvRecordsMaxTimestamp
        ? csvRecordsMaxTimestamp
        : dateFromCsv + timeSpan;

      currentDateFrom = currentDateTo;
      currentDateTo = currentDateFrom + (dateToCsv - dateFromCsv);
    }
  }
}

using Ozds.Fake.Conversion.Agnostic;
using Ozds.Fake.Correction.Agnostic;
using Ozds.Fake.Generators.Abstractions;
using Ozds.Fake.Loaders;
using Ozds.Fake.Records.Abstractions;
using Ozds.Iot.Entities.Abstractions;

// TODO: check time logic here
// TODO: add energy logic
// TODO: fixup cumulatives
// TODO: repeating enumerable

namespace Ozds.Fake.Generators.Base;

public abstract class
  RepeatingCsvResourceMeasurementGenerator<TMeasurement>(
    IServiceProvider serviceProvider) : IMeasurementGenerator
  where TMeasurement : class, IMeasurementRecord
{
  private readonly AgnosticMeasurementRecordPushRequestConverter _converter =
    serviceProvider
      .GetRequiredService<AgnosticMeasurementRecordPushRequestConverter>();

  private readonly AgnosticCumulativeCorrector _corrector =
    serviceProvider.GetRequiredService<AgnosticCumulativeCorrector>();

  private readonly ResourceCache _resources =
    serviceProvider.GetRequiredService<ResourceCache>();

  protected abstract string CsvResourceName { get; }

  protected abstract string MeterIdPrefix { get; }

  public bool CanGenerateMeasurementsFor(string meterId)
  {
    return meterId.StartsWith(MeterIdPrefix);
  }

  public async Task<List<IMeterPushRequestEntity>> GenerateMeasurements(
    DateTimeOffset dateFrom,
    DateTimeOffset dateTo,
    string messengerId,
    string meterId,
    CancellationToken cancellationToken = default
  )
  {
    var records = await _resources
      .GetAsync<CsvLoader<TMeasurement>, List<TMeasurement>>(
        CsvResourceName,
        cancellationToken);
    var pushRequestMeasurements =
      ExpandRecords(records, messengerId, meterId, dateFrom, dateTo).ToList();
    return pushRequestMeasurements;
  }

  private IEnumerable<IMeterPushRequestEntity> ExpandRecords(
    List<TMeasurement> records,
    string messengerId,
    string meterId,
    DateTimeOffset dateFrom,
    DateTimeOffset dateTo
  )
  {
    var ordered = records.OrderBy(record => record.Timestamp).ToList();
    var firstRecord = ordered.FirstOrDefault();
    var lastRecord = ordered.LastOrDefault();
    if (firstRecord == null || lastRecord == null)
    {
      yield break;
    }

    var csvRecordsMinTimestamp = firstRecord.Timestamp;
    var csvRecordsMaxTimestamp = lastRecord.Timestamp;
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
      foreach (var record in ordered
        .Where(
          record =>
            record.Timestamp >= dateFromCsv
            && record.Timestamp < dateToCsv))
      {
        var timestamp = currentDateFrom.AddTicks(
          (record.Timestamp - dateFromCsv).Ticks
        );
        var withCorrectedMeterId = _corrector.CorrectMeterId(
          record,
          meterId
        );
        var withCorrectedCumulatives = _corrector.CorrectCumulatives(
          timestamp,
          withCorrectedMeterId,
          firstRecord,
          lastRecord
        );
        var pushRequest = _converter.ConvertToPushRequest(
          withCorrectedCumulatives, messengerId);
        yield return pushRequest;
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

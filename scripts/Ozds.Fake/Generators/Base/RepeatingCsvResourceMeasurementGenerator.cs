using System.Runtime.CompilerServices;
using Ozds.Fake.Correction;
using Ozds.Fake.Generators.Abstractions;
using Ozds.Fake.Identification;
using Ozds.Fake.Loaders;
using Ozds.Fake.Records.Abstractions;

namespace Ozds.Fake.Generators.Base;

public abstract class
  RepeatingCsvResourceMeasurementGenerator<TMeasurement>(
    IServiceProvider serviceProvider) : IMeasurementRecordGenerator
  where TMeasurement : class, IMeasurementRecord
{
  private readonly RecordCorrector _corrector =
    serviceProvider.GetRequiredService<RecordCorrector>();

  private readonly ResourceCache _resources =
    serviceProvider.GetRequiredService<ResourceCache>();

  protected abstract string CsvResourceName { get; }

  protected abstract string MeterIdPrefix { get; }

  public bool CanGenerateFor(string meterId)
  {
    return meterId.StartsWith(MeterIdPrefix);
  }

  public async IAsyncEnumerable<IMeasurementRecord> GenerateMeasurementRecords(
    DateTimeOffset dateFrom,
    DateTimeOffset dateTo,
    MeasurementLocationMeterId id,
    [EnumeratorCancellation] CancellationToken cancellationToken
  )
  {
    var records = await _resources
      .GetAsync<CsvLoader<TMeasurement>, List<TMeasurement>>(
        CsvResourceName,
        cancellationToken);
    var expanded = ExpandRecords(
      records,
      dateFrom,
      dateTo
    );
    foreach (var record in expanded)
    {
      if (cancellationToken.IsCancellationRequested)
      {
        break;
      }

      var withCorrectedMeterId = _corrector.CorrectMeterId(
        record,
        id.MeterId
      );
      var withCorrectedMeasurementLocationId =
        _corrector.CorrectMeasurementLocationId(
          withCorrectedMeterId,
          id.MeasurementLocationId
        );
      yield return withCorrectedMeasurementLocationId;
    }
  }

  public async IAsyncEnumerable<IMeasurementRecord> BatchMeasurementRecords(
    DateTimeOffset dateFrom,
    DateTimeOffset dateTo,
    IEnumerable<MeasurementLocationMeterId> ids,
    [EnumeratorCancellation] CancellationToken cancellationToken
  )
  {
    var records = await _resources
      .GetAsync<CsvLoader<TMeasurement>, List<TMeasurement>>(
        CsvResourceName,
        (initial, cancellationToken) =>
          Task.FromResult(
            initial
              .OrderBy(record => record.Timestamp)
              .ToList()),
        cancellationToken);
    var expanded = ExpandRecords(
      records,
      dateFrom,
      dateTo
    );
    foreach (var record in expanded)
    {
      if (cancellationToken.IsCancellationRequested)
      {
        break;
      }

      foreach (var id in ids)
      {
        var withCorrectedMeterId = _corrector.CorrectMeterId(
          record,
          id.MeterId
        );
        var withCorrectedMeasurementLocationId =
          _corrector.CorrectMeasurementLocationId(
            withCorrectedMeterId,
            id.MeasurementLocationId
          );
        yield return withCorrectedMeasurementLocationId;
      }
    }
  }

  private IEnumerable<IMeasurementRecord> ExpandRecords(
    List<TMeasurement> records,
    DateTimeOffset dateFrom,
    DateTimeOffset dateTo
  )
  {
    var firstRecord = records.FirstOrDefault();
    var lastRecord = records.LastOrDefault();
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
      foreach (var record in records
        .Where(
          record =>
            record.Timestamp >= dateFromCsv
            && record.Timestamp < dateToCsv))
      {
        var timestamp = currentDateFrom + (record.Timestamp - dateFromCsv);
        var withCorrectedTimestamp = _corrector.CorrectTimestamp(
          record,
          timestamp
        );
        var withCorrectedCumulatives = _corrector.CorrectCumulatives(
          timestamp,
          withCorrectedTimestamp,
          firstRecord,
          lastRecord
        );
        yield return withCorrectedCumulatives;
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

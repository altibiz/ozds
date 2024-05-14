# Ozds.Fake

<div style="display: none;">
  \page structure-fake Fake
</div>

This is a project intended only for development purposes. It is treated as a
script that pushes measurements to a development `Ozds.Server` instance. It has
two main modes:

- `push`: pushes measurements to the server continuously from a CSV file.
  Depending on the requested meter id, it will pick a CSV file corresponding to
  that type of meter and push measurements to the server. The CSV files are
  loaded as embedded resources from the `Assets` but are tracked via `DVC`. The
  CSV files have an arbitrary amount of measurements in an arbitrary time
  interval and `Ozds.Fake` corrects that for the current point in time.

- `seed`: seeds the database by pushing measurements as it usually would with
  `push` but for a requested interval of time immediately in big batches. This
  mode takes some time depending on the interval but it is well worth the wait
  depending on the testing scenario.

The project has a similar namespace structure to `Ozds.Business` and references
it in order to mimic the API data structure needed to push measurements.

## Ozds.Fake.Client

Contains the client that pushes measurements to the server. It contains a single
class with a single method that makes HTTP requests to the server.

## Ozds.Fake.Correction

Correction logic when converting CSV records to measurements. So far, only
cumulative measure correction is needed and implemented via the
`ICumulativeCorrector` interface and classes that implement it.

```plantuml
interface ICumulativeCorrector
{
  bool CanCorrectCumulativeFor(Type measurementRecordType);

  IMeasurementRecord CorrectCumulatives( \
    DateTimeOffset timestamp, \
    IMeasurementRecord record, \
    IMeasurementRecord firstMeasurementRecord, \
    IMeasurementRecord lastMeasurementRecord \
  );
}

class AgnosticCumulativeCorrector
{
  + IMeasurementRecord CorrectCumulatives( \
      DateTimeOffset timestamp, \
      IMeasurementRecord record, \
      IMeasurementRecord firstMeasurementRecord, \
      IMeasurementRecord lastMeasurementRecord \
    )
}

AgnosticCumulativeCorrector o-- "0..N" ICumulativeCorrector
```

## Ozds.Fake.Generators

Generation logic for push request measurements. Generators deserialize CSV
records, correct cumulatives and generate push request measurements within a
specified time interval. The reason why generators do all this is because to
correct cumulatives and generate push request measurements, knowledge about the
whole dataset is needed. So far, only repeating CSV file generators are needed
and implemented.

```plantuml
interface IMeasurementGenerator
{
  + bool CanGenerateMeasurementsFor(string meterId);

  + Task<List<MessengerPushRequestMeasurement>> GenerateMeasurements( \
      DateTimeOffset dateFrom, \
      DateTimeOffset dateTo, \
      string meterId \
    )
}

abstract class RepeatingCsvResourceMeasurementGenerator<TMeasurement>
{
  - AgnosticMeasurementRecordPushRequestConverter _converter
  - AgnosticCumulativeCorrector _corrector
  - ResourceCache _resourceCache

  # <<abstract>> string CsvResourceName
  # <<abstract>> string MeterIdPrefix

  + Task<List<MessengerPushRequestMeasurement>> GenerateMeasurements( \
      DateTimeOffset dateFrom, \
      DateTimeOffset dateTo, \
      string meterId \
    )

  - IEnumerable<MessengerPushRequestMeasurement> ExpandRecords( \
      IEnumerable<TMeasurement> records, \
      DateTimeOffset dateFrom, \
      DateTimeOffset dateTo \
    )
}

IMeasurementGenerator <|-- RepeatingCsvResourceMeasurementGenerator
```

## Ozds.Fake.Loaders

Loaders for different types for embedded resources. Currently only a CSV file
loader is needed and implemented. These loaders are then used by the
`ResourceCache` that caches the loaded resources. In the case of CSV files, the
loaded resource is a list of records.

```plantuml
interface ILoader<T>
{
  + T Load(Stream stream)
}

class CsvLoader<TRecord>
{
  + List<TRecord> Load(Stream stream)
}

class ResourceCache
{
  - ConcurrentDictionary<(string, Type), Lazy<Task<object>>> _cache

  + TOut Get<TLoader, TOut>(string name)
  + Task<TOut> GetAsync<TLoader, TOut>(string name)

  - <<static>> Stream Load(string name)
}

ILoader <|-- CsvLoader
ResourceCache o-- "0..N" ILoader
```

## Ozds.Fake.Records

Contains CSV record classes that are used to deserialize the CSV files and
correct cumulative measures. All concrete classes implement the
`IMeasurementRecord` interface.

```plantuml
interface IMeasurementRecord
{
  + string MeterId
  + DateTimeOffset Timestamp
  + TariffMeasure<float> ActiveEnergy_Wh
  + TariffMeasure<float> ReactiveEnergy_VARh
  + TariffMeasure<float> ApparentEnergy_VAh
}
```

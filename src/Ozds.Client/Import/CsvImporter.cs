using System.Globalization;
using CsvHelper;
using Ozds.Client.Import.Abstractions;

namespace Ozds.Client.Import;

public class CsvImporter : IImporter
{
  public IImportStreamer<T> Import<T>(
    Stream csvStream,
    CancellationToken cancellationToken
  )
  {
    var streamReader = new StreamReader(csvStream);
    var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
    return new CsvImportStreamer<T>(
      streamReader,
      csvReader,
      cancellationToken
    );
  }

  public IImportStreamer Import(
    Type type,
    Stream csvStream,
    CancellationToken cancellationToken
  )
  {
    var streamReader = new StreamReader(csvStream);
    var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
    return new CsvImportStreamer(
      type,
      streamReader,
      csvReader,
      cancellationToken
    );
  }
}

public sealed class CsvImportStreamer(
  Type type,
  StreamReader streamReader,
  CsvReader reader,
  CancellationToken cancellationToken
) : IImportStreamer
{
  public IAsyncEnumerable<object> Stream()
  {
    return reader.GetRecordsAsync(type, cancellationToken);
  }

  public void Dispose()
  {
    streamReader.Dispose();
    reader.Dispose();
  }
}

public sealed class CsvImportStreamer<T>(
  StreamReader streamReader,
  CsvReader reader,
  CancellationToken cancellationToken
) : IImportStreamer<T>
{
  public IAsyncEnumerable<T> Stream()
  {
    return reader.GetRecordsAsync<T>(cancellationToken);
  }

  public void Dispose()
  {
    streamReader.Dispose();
    reader.Dispose();
  }
}

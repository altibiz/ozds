using System.Globalization;
using CsvHelper;
using Ozds.Client.Import.Abstractions;

namespace Ozds.Client.Import;

public class CsvImporter : IImporter
{
  public IAsyncEnumerable<T> Import<T>(
    Stream csvStream,
    CancellationToken cancellationToken
  )
  {
    using var streamReader = new StreamReader(csvStream);
    using var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
    return csvReader.GetRecordsAsync<T>(cancellationToken);
  }
}

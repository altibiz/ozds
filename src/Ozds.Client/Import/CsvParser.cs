using System.Globalization;
using CsvHelper;
using Ozds.Client.Import.Abstractions;
using static Ozds.Client.Import.ModelsImport.NetworkUserImporter;

namespace Ozds.Client.Import;

public class CsvParser : ICsvParser
{
  public async Task<IEnumerable<T>> ParseCsvAsync<T>(Stream csvStream)
    where T : new()
  {
    using var streamReader = new StreamReader(csvStream);
    using var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
    var records = new List<T>();
    await foreach (var record in csvReader.GetRecordsAsync<T>())
    {
      records.Add(record);
    }
    return records;
  }

  public async Task<IEnumerable<TModel>> ParseAndMapCsvAsync<TCsv, TModel>(Stream csvStream, Func<IEnumerable<TCsv>, Task<IEnumerable<TModel>>> mappingFunc)
      where TCsv : new()
  {
    var csvRecords = await ParseCsvAsync<TCsv>(csvStream);
    return await mappingFunc(csvRecords);
  }
}

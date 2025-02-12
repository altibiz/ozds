namespace Ozds.Client.Import.Abstractions;

public interface ICsvParser
{
  Task<IEnumerable<T>> ParseCsvAsync<T>(Stream csvStream)
    where T : new();
  Task<IEnumerable<TModel>> ParseAndMapCsvAsync<TCsv, TModel>(Stream csvStream, Func<IEnumerable<TCsv>, Task<IEnumerable<TModel>>> mappingFunc)
      where TCsv : new();
}

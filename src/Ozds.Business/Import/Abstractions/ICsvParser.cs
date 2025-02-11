namespace Ozds.Business.Import.Abstractions;

public interface ICsvParser
{
  IEnumerable<T> ParseCsv<T>(Stream csvStream)
    where T : new();
}

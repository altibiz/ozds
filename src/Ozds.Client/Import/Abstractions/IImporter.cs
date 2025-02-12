namespace Ozds.Client.Import.Abstractions;

public interface IImporter
{
  public IAsyncEnumerable<T> Import<T>(
    Stream csvStream,
    CancellationToken cancellationToken);
}

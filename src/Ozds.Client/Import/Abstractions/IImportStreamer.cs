namespace Ozds.Client.Import.Abstractions;

public interface IImportStreamer<T> : IDisposable
{
  public IAsyncEnumerable<T> Stream();
}

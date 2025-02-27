namespace Ozds.Client.Import.Abstractions;

public interface IImportStreamer : IDisposable
{
  public IAsyncEnumerable<object> Stream();
}

public interface IImportStreamer<T> : IDisposable
{
  public IAsyncEnumerable<T> Stream();
}

namespace Ozds.Client.Import.Abstractions;

public interface IImporter
{
  public IImportStreamer<T> Import<T>(
    Stream csvStream,
    CancellationToken cancellationToken);
}

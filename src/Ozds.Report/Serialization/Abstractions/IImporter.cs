using System.Globalization;

namespace Ozds.Report.Serialization.Abstractions;

public interface IImporter
{
  public string Extension { get; }

  public IImportStreamer<T> Import<T>(CultureInfo culture, Stream csvStream);

  public IImportStreamer Import(
    CultureInfo culture,
    Type type,
    Stream csvStream
  );

  public IAsyncImportStreamer<T> Import<T>(
    CultureInfo culture,
    Stream csvStream,
    CancellationToken cancellationToken
  );

  public IAsyncImportStreamer Import(
    CultureInfo culture,
    Type type,
    Stream csvStream,
    CancellationToken cancellationToken
  );
}

public interface IImportStreamer : IDisposable
{
  public IEnumerable<object> Stream();
}

public interface IImportStreamer<T> : IDisposable
{
  public IEnumerable<T> Stream();
}

public interface IAsyncImportStreamer : IDisposable
{
  public IAsyncEnumerable<object> Stream();
}

public interface IAsyncImportStreamer<T> : IDisposable
{
  public IAsyncEnumerable<T> Stream();
}

using System.Globalization;

namespace Ozds.Report.Serialization.Abstractions;

public interface IExporter
{
  public string Extension { get; }

  public Task<string> Export<T>(
    CultureInfo culture,
    IEnumerable<T> models,
    CancellationToken cancellationToken
  );

  public Task<string> Export<T>(
    CultureInfo culture,
    IAsyncEnumerable<T> models,
    CancellationToken cancellationToken
  );
}

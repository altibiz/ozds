using System.Globalization;
using Ozds.Report.Mutations.Abstractions;
using Ozds.Report.Serialization.Abstractions;

namespace Ozds.Report.Mutations;

public class ReportMutations(IServiceProvider services) : IMutations
{
  public async Task<string> Create<T>(
    string fileName,
    CultureInfo culture,
    IEnumerable<T> entities,
    CancellationToken cancellationToken
  )
  {
    var exporter = GetExporter(fileName);

    return await exporter.Export(culture, entities, cancellationToken);
  }

  public async Task<string> Create<T>(
    string fileName,
    CultureInfo culture,
    IAsyncEnumerable<T> entities,
    CancellationToken cancellationToken
  )
  {
    var exporter = GetExporter(fileName);

    return await exporter.Export(culture, entities, cancellationToken);
  }

  private IExporter GetExporter(string fileName)
  {
    var exporter =
      services
        .GetServices<IExporter>()
        .FirstOrDefault(x => fileName.EndsWith(x.Extension))
      ?? throw new InvalidOperationException(
        $"Exporter for {fileName} not found."
      );

    return exporter;
  }
}

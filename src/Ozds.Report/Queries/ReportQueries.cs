using System.Globalization;
using Ozds.Report.Queries.Abstractions;
using Ozds.Report.Serialization.Abstractions;

namespace Ozds.Report.Queries;

public class ReportQueries(
  IServiceProvider services
) : IQueries
{
  public IImportStreamer<T> Read<T>(
    string fileName,
    CultureInfo culture,
    Stream stream
  )
  {
    var importer = GetImporter(fileName);

    return importer.Import<T>(culture, stream);
  }

  public IImportStreamer Read(
    string fileName,
    CultureInfo culture,
    Type type,
    Stream stream
  )
  {
    var importer = GetImporter(fileName);

    return importer.Import(culture, type, stream);
  }

  public IAsyncImportStreamer<T> Read<T>(
    string fileName,
    CultureInfo culture,
    Stream stream,
    CancellationToken cancellationToken)
  {
    var importer = GetImporter(fileName);

    return importer.Import<T>(culture, stream, cancellationToken);
  }

  public IAsyncImportStreamer Read(
    string fileName,
    CultureInfo culture,
    Type type,
    Stream stream,
    CancellationToken cancellationToken)
  {
    var importer = GetImporter(fileName);

    return importer.Import(culture, type, stream, cancellationToken);
  }

  private IImporter GetImporter(
    string fileName
  )
  {
    var importer = services
        .GetServices<IImporter>()
        .FirstOrDefault(x => fileName.EndsWith(x.Extension))
      ?? throw new InvalidOperationException(
        $"Exporter for {fileName} not found.");

    return importer;
  }
}

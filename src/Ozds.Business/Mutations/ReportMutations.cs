using System.Globalization;
using Ozds.Business.Conversion;
using Ozds.Business.Extensions;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Mutations.Abstractions;
using ReportReportMutations = Ozds.Report.Mutations.ReportMutations;
using ReportReportQueries = Ozds.Report.Queries.ReportQueries;

namespace Ozds.Business.Mutations;

public class ReportMutations(
  ReportReportQueries queries,
  ReportReportMutations mutations,
  AuditableMutations auditableMutations,
  ReadonlyMutations readonlyMutations,
  MeasurementMutations measurementMutations,
  ModelReportEntityConverter converter
) : IMutations
{
  public async Task<string> Export<T>(
    string fileName,
    CultureInfo culture,
    IAsyncEnumerable<T> models,
    CancellationToken cancellationToken
  )
  {
    var entities = models
      .Where(x => x is not null)
      .Select(x => (object)x!)
      .Select(converter.ToEntity);

    return await mutations.Create(
      fileName, culture, entities, cancellationToken);
  }

  public async Task<string> Export<T>(
    string fileName,
    CultureInfo culture,
    IEnumerable<T> models,
    CancellationToken cancellationToken
  )
  {
    var entities = models
      .Where(x => x is not null)
      .Select(x => (object)x!)
      .Select(converter.ToEntity);

    return await mutations.Create(
      fileName, culture, entities, cancellationToken);
  }

  public async Task Import<T>(
    string fileName,
    CultureInfo culture,
    Stream stream,
    CancellationToken cancellationToken
  )
  {
    var entityType = converter.EntityType(typeof(T));

    using var streamer = queries
      .Read(fileName, culture, entityType, stream, cancellationToken);
    await foreach (var entities in streamer
      .Stream()
      .Chunk(cancellationToken))
    {
      var models = entities
        .OfType<object>()
        .Select(converter.ToModel<T>);

      foreach (var model in models)
      {
        if (model is IAuditable auditableModel)
        {
          await auditableMutations.Create(auditableModel, cancellationToken);
        }
        else if (model is IMeasurement measurementModel)
        {
          await measurementMutations.CreateMeasurements(
            [measurementModel],
            cancellationToken
          );
        }
        else if (model is IReadonly readonlyModel)
        {
          await readonlyMutations.Create(readonlyModel, cancellationToken);
        }
      }
    }
  }

  public async Task Import(
    string fileName,
    CultureInfo culture,
    Type type,
    Stream stream,
    CancellationToken cancellationToken
  )
  {
    var entityType = converter.EntityType(type);

    using var streamer = queries
      .Read(fileName, culture, entityType, stream, cancellationToken);
    await foreach (var entities in streamer
      .Stream()
      .Chunk(cancellationToken))
    {
      var models = entities.Select(converter.ToModel);

      foreach (var model in models)
      {
        if (model is IAuditable auditableModel)
        {
          await auditableMutations.Create(auditableModel, cancellationToken);
        }
        else if (model is IMeasurement measurementModel)
        {
          await measurementMutations.CreateMeasurements(
            [measurementModel],
            cancellationToken
          );
        }
        else if (model is IReadonly readonlyModel)
        {
          await readonlyMutations.Create(readonlyModel, cancellationToken);
        }
      }
    }
  }
}

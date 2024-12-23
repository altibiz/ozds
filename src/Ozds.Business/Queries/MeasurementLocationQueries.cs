using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries.Abstractions;
using DataMeasurementLocationQueries = Ozds.Data.Queries.MeasurementLocationQueries;

namespace Ozds.Business.Queries;

public class MeasurementLocationQueries(
  DataMeasurementLocationQueries queries,
  AgnosticModelEntityConverter modelEntityConverter
) : IQueries
{
  public async Task<List<IMeasurementLocation>> ReadByMeters(
    IEnumerable<string> meterIds,
    CancellationToken cancellationToken
  )
  {
    var entities = await queries.ReadByMeters(meterIds, cancellationToken);
    var models = entities
      .Select(modelEntityConverter.ToModel<IMeasurementLocation>)
      .ToList();
    return models;
  }
}

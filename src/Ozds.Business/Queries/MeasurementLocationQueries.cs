using Ozds.Business.Conversion;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries.Abstractions;
using DataMeasurementLocationQueries =
  Ozds.Data.Queries.MeasurementLocationQueries;

namespace Ozds.Business.Queries;

public class MeasurementLocationQueries(
  DataMeasurementLocationQueries queries,
  ModelEntityConverter modelEntityConverter
) : IQueries
{
  public async Task<IMeasurementLocation?> ReadByMeterId(
    string meterId,
    CancellationToken cancellationToken
  )
  {
    var entity = await queries.ReadByMeterId(
      meterId,
      cancellationToken
    );
    return entity is null
      ? null
      : modelEntityConverter.ToModel<IMeasurementLocation>(entity);
  }

  public async Task<
    List<IMeasurementLocation>
  > ReadNetworkUserId(
    string networkUserId,
    CancellationToken cancellationToken
  )
  {
    var entities = await queries.ReadByNetworkUserId(
      networkUserId,
      cancellationToken
    );
    return entities
      .Select(modelEntityConverter.ToModel<IMeasurementLocation>)
      .ToList();
  }

  public async Task<
    List<IMeasurementLocation>
  > ReadByLocationId(
    string locationId,
    CancellationToken cancellationToken
  )
  {
    var entities = await queries.ReadByLocationId(
      locationId,
      cancellationToken
    );
    return entities
      .Select(modelEntityConverter.ToModel<IMeasurementLocation>)
      .ToList();
  }
}

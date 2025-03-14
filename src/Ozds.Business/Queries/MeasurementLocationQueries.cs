using Ozds.Business.Conversion;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data.Entities.Abstractions;
using DataMeasurementLocationQueries = Ozds.Data.Queries.MeasurementLocationQueries;

namespace Ozds.Business.Queries;

public class MeasurementLocationQueries(
  DataMeasurementLocationQueries queries,
  ModelEntityConverter modelEntityConverter
) : IQueries
{
  public async Task<IMeasurementLocation?> ReadMeasurementLocationByMeter(
    string meterId,
    CancellationToken cancellationToken
  )
  {
    var entity = await queries.ReadMeasurementLocationByMeter(
      meterId,
      cancellationToken
    );
    return entity is null
      ? null
      : modelEntityConverter.ToModel<IMeasurementLocation>(entity);
  }

  public async Task<IMeter?> ReadMeterByMeasurementLocation(
    string measurementLocationId,
    CancellationToken cancellationToken
  )
  {
    var entity = await queries.ReadMeterByMeasurementLocation(
      measurementLocationId,
      cancellationToken
    );
    return entity is null ? null : modelEntityConverter.ToModel<IMeter>(entity);
  }

  public async Task<List<IMeasurementLocation>?> ReadMeasurementLocationByNetworkUser(
  string networkUserId,
  CancellationToken cancellationToken
)
  {
    var entities = await queries.ReadMeasurementLocationByNetworkUser(
      networkUserId,
      cancellationToken
    );
    return entities?.Select(modelEntityConverter.ToModel<IMeasurementLocation>)
        .ToList();
  }

  public async Task<List<IMeasurementLocation>?> ReadMeasurementLocationByLocation(
  string locationId,
  CancellationToken cancellationToken
)
  {
    var entities = await queries.ReadMeasurementLocationByLocation(
      locationId,
      cancellationToken
    );
    return entities?.Select(modelEntityConverter.ToModel<IMeasurementLocation>)
        .ToList();
  }
}

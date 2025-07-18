using Ozds.Business.Conversion;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries.Abstractions;
using DataMeterQueries = Ozds.Data.Queries.MeterQueries;

namespace Ozds.Business.Queries;

public class MeterQueries(
  DataMeterQueries queries,
  ModelEntityConverter modelEntityConverter
) : IQueries
{
  public async Task<IMeter?> ReadByMeasurementLocationId(
    string measurementLocationId,
    CancellationToken cancellationToken
  )
  {
    var entity = await queries.ReadByMeasurementLocationId(
      measurementLocationId,
      cancellationToken
    );
    return entity is null ? null : modelEntityConverter.ToModel<IMeter>(entity);
  }

  public async Task<IMeter?> ReadByMessengerId(
    string messengerId,
    CancellationToken cancellationToken
  )
  {
    var entity = await queries.ReadByMessengerId(
      messengerId,
      cancellationToken
    );
    return entity is null ? null : modelEntityConverter.ToModel<IMeter>(entity);
  }
}

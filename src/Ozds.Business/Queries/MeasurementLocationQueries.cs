using Ozds.Business.Conversion;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries.Abstractions;
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
    var entity = await queries.ReadMeasurementLocationByMeter(meterId, cancellationToken);
    return entity is null
      ? null
      : modelEntityConverter.ToModel<IMeasurementLocation>(entity);
  }

  public async Task<IMeter?> ReadMeterByMeasurementLocation(
    string measurementLocationId,
    CancellationToken cancellationToken
  )
  {
    var entity = await queries.ReadMeterByMeasurementLocation(measurementLocationId, cancellationToken);
    return entity is null
      ? null
      : modelEntityConverter.ToModel<IMeter>(entity);
  }
}

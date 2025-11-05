using Ozds.Business.Conversion;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries.Abstractions;
using Ozds.Caching.Entities.Base;
using Ozds.Caching.Entities.Composite;
using CachingCompositeMutations =
  Ozds.Caching.Mutations.CompositeEntityMutations;
using CachingCompositeQueries = Ozds.Caching.Queries.CompositeEntityQueries;
using CachingIdentifiableQueries = Ozds.Caching.Queries.IdentifiableEntityQueries;
using DataMeasurementLocationQueries =
  Ozds.Data.Queries.MeasurementLocationQueries;

namespace Ozds.Business.Queries;

public class MeasurementLocationQueries(
  DataMeasurementLocationQueries queries,
  ModelEntityConverter modelEntityConverter,
  ModelCachingEntityConverter modelCachingEntityConverter,
  CachingCompositeMutations cachingCompositeMutations,
  CachingCompositeQueries cachingCompositeQueries,
  CachingIdentifiableQueries cachingIdentifiableQueries
) : IQueries
{
  public async Task<IMeasurementLocation?> ReadByMeterId(
    string meterId,
    CancellationToken cancellationToken
  )
  {
    var cachedEntity = await cachingCompositeQueries
      .Read<MeterMeasurementLocationEntity>(meterId, cancellationToken);

    if (cachedEntity is not null)
    {
      var cachedModel = modelCachingEntityConverter
        .ToModel<IMeasurementLocation>(cachedEntity.MeasurementLocation);

      return cachedModel;
    }

    var entity = await queries.ReadByMeterId(
      meterId,
      cancellationToken
    );
    if (entity is null)
    {
      return default;
    }

    var model = modelEntityConverter.ToModel<IMeasurementLocation>(entity);
    if (model is not null)
    {
      var cachingMeter = await cachingIdentifiableQueries
        .Read<MeterEntity>(meterId, cancellationToken);
      if (cachingMeter is null)
      {
        return model;
      }

      var cachingMeasurementLocation = modelCachingEntityConverter
        .ToEntity<MeasurementLocationEntity>(model);

      var cachingEntity = new MeterMeasurementLocationEntity
      {
        Meter = cachingMeter,
        MeasurementLocation = cachingMeasurementLocation
      };

      await cachingCompositeMutations.Create(cachingEntity, cancellationToken);
    }

    return model;
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

  public async Task<
    List<IMeasurementLocation>
  > ReadByNetworkUserId(
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
}

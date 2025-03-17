using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Composite;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data.Entities;
using DataMeasurementLocationQueries =
  Ozds.Data.Queries.MeasurementLocationQueries;

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

  public async Task<
    List<IMeasurementLocation>
  > ReadMeasurementLocationByNetworkUser(
    string networkUserId,
    CancellationToken cancellationToken
  )
  {
    var entities = await queries.ReadMeasurementLocationByNetworkUser(
      networkUserId,
      cancellationToken
    );
    return entities
      .Select(modelEntityConverter.ToModel<IMeasurementLocation>)
      .ToList();
  }

  public async Task<
    List<IMeasurementLocation>
  > ReadMeasurementLocationByLocation(
    string locationId,
    CancellationToken cancellationToken
  )
  {
    var entities = await queries.ReadMeasurementLocationByLocation(
      locationId,
      cancellationToken
    );
    return entities
      .Select(modelEntityConverter.ToModel<IMeasurementLocation>)
      .ToList();
  }

  public async Task<List<AnalysisBasisModel>>
    ReadAnalysisBasisByLocationAndRepresentative(
      string? locationId,
      RepresentativeModel? representative,
      DateTimeOffset fromDate,
      DateTimeOffset toDate,
      CancellationToken cancellationToken
    )
  {
    var representativeEntity = representative is null
      ? null
      : modelEntityConverter
        .ToEntity<RepresentativeEntity>(representative);

    var entities = await queries
      .ReadAnalysisBasesByLocationAndRepresentative(
        locationId,
        representativeEntity,
        fromDate,
        toDate,
        cancellationToken
      );

    return entities
      .Select(
        entity => new AnalysisBasisModel
        {
          Representative = representative,
          FromDate = fromDate,
          ToDate = toDate,
          Location = modelEntityConverter
            .ToModel<LocationModel>(entity.Location),
          NetworkUser = modelEntityConverter
            .ToModel<NetworkUserModel>(entity.NetworkUser),
          MeasurementLocation = modelEntityConverter
            .ToModel<MeasurementLocationModel>(entity.MeasurementLocation),
          Meter = modelEntityConverter.ToModel<MeterModel>(entity.Meter),
          Calculations = [],
          Invoices = [],
          LastMeasurement = null,
          MonthlyAggregates = []
        })
      .ToList();
  }
}

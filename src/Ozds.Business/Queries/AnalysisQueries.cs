using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Composite;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data.Entities;
using DataAnalysisQueries = Ozds.Data.Queries.AnalysisQueries;

namespace Ozds.Business.Queries;

public class AnalysisQueries(
  DataAnalysisQueries queries,
  ModelEntityConverter modelEntityConverter
) : IQueries
{
  public async Task<List<AnalysisBasisModel>>
    ReadByLocationIdAndRepresentative(
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
      .ReadByLocationIdAndRepresentative(
        locationId,
        representativeEntity,
        fromDate,
        toDate,
        cancellationToken
      );

    return entities
      .Select(entity => new AnalysisBasisModel
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

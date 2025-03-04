using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Composite;
using Ozds.Business.Models.Enums;
using Ozds.Data.Queries.Abstractions;
using DataMeasurementLocationQueries = Ozds.Data.Queries.AnalysisQueries;

namespace Ozds.Business.Queries;

public class AnalysisQueries(
  DataMeasurementLocationQueries queries,
  ModelEntityConverter modelEntityConverter
) : IQueries
{
  public async Task<List<AnalysisBasisModel>>
    ReadAnalysisBasesByRepresentativeAndLocation(
      string representativeId,
      RoleModel role,
      DateTimeOffset fromDate,
      DateTimeOffset toDate,
      string? locationId,
      CancellationToken cancellationToken
    )
  {
    var entities = await queries
      .ReadAnalysisBasesByRepresentativeAndLocation(
        representativeId,
        role.ToEntity(),
        fromDate,
        toDate,
        locationId,
        cancellationToken
      );

    var models = entities
      .Select(
        entity => new AnalysisBasisModel
        {
          Representative = entity.Representative is null
            ? null
            : modelEntityConverter
              .ToModel<RepresentativeModel>(entity.Representative),
          FromDate = entity.FromDate,
          ToDate = entity.ToDate,
          Location = modelEntityConverter
            .ToModel<LocationModel>(entity.Location),
          NetworkUser = modelEntityConverter
            .ToModel<NetworkUserModel>(entity.NetworkUser),
          MeasurementLocation = modelEntityConverter
            .ToModel<MeasurementLocationModel>(entity.MeasurementLocation),
          Meter = modelEntityConverter.ToModel<MeterModel>(entity.Meter),
          Calculations = entity.Calculations
            .Select(x => modelEntityConverter.ToModel<CalculationModel>(x))
            .ToList(),
          Invoices = entity.Invoices
            .Select(x => modelEntityConverter.ToModel<InvoiceModel>(x))
            .ToList(),
          LastMeasurement = modelEntityConverter
            .ToModel<MeasurementModel>(entity.LastMeasurement),
          MonthlyAggregates = entity.MonthlyAggregates
            .Select(x => modelEntityConverter.ToModel<AggregateModel>(x))
            .ToList()
        })
      .ToList();

    return models;
  }
}

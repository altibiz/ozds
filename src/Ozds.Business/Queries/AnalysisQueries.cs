using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Composite;
using Ozds.Business.Models.Enums;
using Ozds.Data.Queries.Abstractions;
using DataMeasurementLocationQueries = Ozds.Data.Queries.AnalysisQueries;

// TODO: location measurement locations

namespace Ozds.Business.Queries;

public class AnalysisQueries(
  DataMeasurementLocationQueries queries,
  AgnosticModelEntityConverter modelEntityConverter
) : IQueries
{
  public async Task<PaginatedList<AnalysisBasisModel>>
    ReadAnalysisBasesByRepresentative(
      string representativeId,
      RoleModel role,
      DateTimeOffset fromDate,
      DateTimeOffset toDate,
      int pageNumber,
      CancellationToken cancellationToken,
      int pageCount = QueryConstants.DefaultPageCount
    )
  {
    var entities = await queries
      .ReadAnalysisBasesByRepresentative(
        representativeId,
        role.ToEntity(),
        fromDate,
        toDate,
        pageNumber,
        cancellationToken,
        pageCount
      );

    return entities.Items
      .Select(entity => new AnalysisBasisModel(
        modelEntityConverter.ToModel<LocationModel>(entity.Location),
        entity.NetworkUser is { }
          ? modelEntityConverter.ToModel<NetworkUserModel>(entity.NetworkUser)
          : null,
        modelEntityConverter.ToModel<MeasurementLocationModel>(
          entity.MeasurementLocation),
        modelEntityConverter.ToModel<MeterModel>(entity.Meter),
        entity.Calculations
          .Select(modelEntityConverter.ToModel<CalculationModel>)
          .ToList(),
        entity.Invoices
          .Select(modelEntityConverter.ToModel<InvoiceModel>)
          .ToList(),
        modelEntityConverter.ToModel<MeasurementModel>(entity.LastMeasurement),
        entity.MonthlyAggregates
          .Select(modelEntityConverter.ToModel<AggregateModel>)
          .ToList(),
        entity.MonthlyMaxPowerAggregates
          .Select(modelEntityConverter.ToModel<AggregateModel>)
          .ToList()
      ))
      .ToPaginatedList(entities.TotalCount);
  }

  public async Task<List<AnalysisBasisModel>>
    ReadAnalysisBasesByRepresentative(
      string representativeId,
      RoleModel role,
      DateTimeOffset fromDate,
      DateTimeOffset toDate,
      CancellationToken cancellationToken
    )
  {
    var entities = await queries
      .ReadAnalysisBasesByRepresentative(
        representativeId,
        role.ToEntity(),
        fromDate,
        toDate,
        cancellationToken
      );

    return entities
      .Select(entity => new AnalysisBasisModel(
        modelEntityConverter.ToModel<LocationModel>(entity.Location),
        entity.NetworkUser is { }
          ? modelEntityConverter.ToModel<NetworkUserModel>(entity.NetworkUser)
          : null,
        modelEntityConverter.ToModel<MeasurementLocationModel>(
          entity.MeasurementLocation),
        modelEntityConverter.ToModel<MeterModel>(entity.Meter),
        entity.Calculations
          .Select(modelEntityConverter.ToModel<CalculationModel>)
          .ToList(),
        entity.Invoices
          .Select(modelEntityConverter.ToModel<InvoiceModel>)
          .ToList(),
        modelEntityConverter.ToModel<MeasurementModel>(entity.LastMeasurement),
        entity.MonthlyAggregates
          .Select(modelEntityConverter.ToModel<AggregateModel>)
          .ToList(),
        entity.MonthlyMaxPowerAggregates
          .Select(modelEntityConverter.ToModel<AggregateModel>)
          .ToList()
      ))
      .ToList();
  }
}

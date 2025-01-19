using Ozds.Business.Conversion;
using Ozds.Business.Models;
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

    return entities
      .Select(modelEntityConverter.ToModel<AnalysisBasisModel>)
      .ToList();
  }
}

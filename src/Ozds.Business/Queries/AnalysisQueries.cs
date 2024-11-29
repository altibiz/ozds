using Ozds.Business.Conversion;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models;
using Ozds.Business.Models.Composite;
using Ozds.Business.Models.Enums;
using Ozds.Data.Queries.Abstractions;
using DataMeasurementLocationQueries = Ozds.Data.Queries.AnalysisQueries;

// TODO: location measurement locations
// TODO: paging
// TODO: ModelEntityConverter stuff

namespace Ozds.Business.Queries;

public class AnalysisQueries(
  DataMeasurementLocationQueries queries
) : IQueries
{
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

    return entities.Select(entity => entity.ToModel()).ToList();
  }
}

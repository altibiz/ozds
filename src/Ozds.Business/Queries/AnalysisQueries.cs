using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Models.Composite;
using Ozds.Business.Models.Enums;
using Ozds.Data.Queries.Abstractions;
using DataMeasurementLocationQueries = Ozds.Data.Queries.AnalysisQueries;

// TODO: location measurement locations
// TODO: paging
// TODO: ModelEntityConverter stuff
// TODO: locationId

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
      CancellationToken cancellationToken,
#pragma warning disable IDE0060 // Remove unused parameter
      string? locationId = null
#pragma warning restore IDE0060 // Remove unused parameter
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

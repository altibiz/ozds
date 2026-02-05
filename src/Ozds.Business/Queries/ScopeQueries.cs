using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Queries.Abstractions;

namespace Ozds.Business.Queries;

using DataScopeQueries = Data.Queries.ScopeQueries;

public class ScopeQueries(
  DataScopeQueries dataScopeQueries,
  ModelEntityConverter modelEntityConverter
) : IQueries
{
  public async Task<PaginatedList<ScopeModel>> ReadByApiKeyId(
    string apiKeyId,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageSize = QueryConstants.DefaultPageCount,
    bool deleted = false,
    string? title = null
  )
  {
    var entities = await dataScopeQueries.ReadByApiKeyId(
      apiKeyId,
      pageNumber,
      cancellationToken,
      pageSize,
      deleted,
      title
    );

    var models = entities
      .Items.Select(modelEntityConverter.ToModel<ScopeModel>)
      .ToPaginatedList(entities.TotalCount);

    return models;
  }
}

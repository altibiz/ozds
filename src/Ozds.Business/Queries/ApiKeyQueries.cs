using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Queries.Abstractions;
using DataApiKeyQueries = Ozds.Data.Queries.ApiKeyQueries;

namespace Ozds.Business.Queries;

public class ApiKeyQueries(
  DataApiKeyQueries dataApiKeyQueries,
  ModelEntityConverter modelEntityConverter
) : IQueries
{
  public async Task<PaginatedList<ApiKeyModel>> ReadByRepresentativeId(
    string representativeId,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageSize = QueryConstants.DefaultPageCount,
    bool deleted = false,
    string? title = null
  )
  {
    var entities = await dataApiKeyQueries.ReadByRepresentativeId(
      representativeId,
      pageNumber,
      cancellationToken,
      pageSize,
      deleted,
      title
    );

    var models = entities
      .Items.Select(modelEntityConverter.ToModel<ApiKeyModel>)
      .ToPaginatedList(entities.TotalCount);

    return models;
  }

  public async Task<PaginatedList<ApiKeyModel>> ReadByMessengerId(
    string messengerId,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageSize = QueryConstants.DefaultPageCount,
    bool deleted = false,
    string? title = null
  )
  {
    var entities = await dataApiKeyQueries.ReadByMessengerId(
      messengerId,
      pageNumber,
      cancellationToken,
      pageSize,
      deleted,
      title
    );

    var models = entities
      .Items.Select(modelEntityConverter.ToModel<ApiKeyModel>)
      .ToPaginatedList(entities.TotalCount);

    return models;
  }
}

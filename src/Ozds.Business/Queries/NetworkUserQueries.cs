using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;
using Ozds.Business.Queries.Abstractions;
using DataNetworkUserQueries = Ozds.Data.Queries.NetworkUserQueries;

namespace Ozds.Business.Queries;

public class NetworkUserQueries(
  DataNetworkUserQueries dataNetworkUserQueries,
  AgnosticModelEntityConverter modelEntityConverter
) : IQueries
{
  public async Task<NetworkUserModel?> ReadNetworkUserByRepresentativeId(
    string representativeId,
    RoleModel role,
    string locationId,
    CancellationToken cancellationToken,
    bool deleted = false
  )
  {
    var entity = await dataNetworkUserQueries.ReadNetworkUserByRepresentativeId(
      representativeId,
      role.ToEntity(),
      locationId,
      cancellationToken,
      deleted
    );

    var model = entity is null
      ? null
      : modelEntityConverter.ToModel<NetworkUserModel>(entity);

    return model;
  }

  public async Task<PaginatedList<NetworkUserModel>>
    ReadNetworkUsersByRepresentativeId(
      string representativeId,
      RoleModel role,
      int pageNumber,
      CancellationToken cancellationToken,
      int pageSize = QueryConstants.DefaultPageCount,
      bool deleted = false
    )
  {
    var entities = await dataNetworkUserQueries.ReadNetworkUsersByRepresentativeId(
      representativeId,
      role.ToEntity(),
      pageNumber,
      cancellationToken,
      pageSize,
      deleted
    );

    var models = entities
      .Items
      .Select(modelEntityConverter.ToModel<NetworkUserModel>)
      .ToPaginatedList(entities.TotalCount);

    return models;
  }
}

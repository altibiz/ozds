using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;
using Ozds.Business.Queries.Abstractions;
using DataNetworkUserQueries = Ozds.Data.Queries.NetworkUserQueries;

namespace Ozds.Business.Queries;

public class NetworkUserQueries(
  DataNetworkUserQueries dataNetworkUserQueries,
  ModelEntityConverter modelEntityConverter
) : IQueries
{
  public async Task<PaginatedList<NetworkUserModel>> ReadByRepresentativeId(
    string representativeId,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageSize = QueryConstants.DefaultPageCount,
    bool deleted = false,
    string? title = null
  )
  {
    var entities = await dataNetworkUserQueries.ReadByRepresentativeId(
      representativeId,
      pageNumber,
      cancellationToken,
      pageSize,
      deleted,
      title
    );

    var models = entities
      .Items.Select(modelEntityConverter.ToModel<NetworkUserModel>)
      .ToPaginatedList(entities.TotalCount);

    return models;
  }

  public async Task<NetworkUserModel?> ReadIndirectByRepresentativeIdAndId(
    string representativeId,
    RoleModel role,
    string locationId,
    CancellationToken cancellationToken,
    bool deleted = false
  )
  {
    var entity =
      await dataNetworkUserQueries.ReadIndirectByRepresentativeIdAndId(
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

  public async Task<
    PaginatedList<NetworkUserModel>
  > ReadIndirectByRepresentativeId(
    string representativeId,
    RoleModel role,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageSize = QueryConstants.DefaultPageCount,
    bool deleted = false,
    string? title = null
  )
  {
    var entities = await dataNetworkUserQueries.ReadIndirectByRepresentativeId(
      representativeId,
      role.ToEntity(),
      pageNumber,
      cancellationToken,
      pageSize,
      deleted,
      title
    );

    var models = entities
      .Items.Select(modelEntityConverter.ToModel<NetworkUserModel>)
      .ToPaginatedList(entities.TotalCount);

    return models;
  }
}

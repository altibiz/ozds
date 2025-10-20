using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;
using Ozds.Business.Queries.Abstractions;
using DataLocationQueries = Ozds.Data.Queries.LocationQueries;

namespace Ozds.Business.Queries;

public class LocationQueries(
  DataLocationQueries dataLocationQueries,
  ModelEntityConverter modelEntityConverter
) : IQueries
{
  public async Task<PaginatedList<LocationModel>>
    ReadByRepresentativeId(
      string representativeId,
      int pageNumber,
      CancellationToken cancellationToken,
      int pageSize = QueryConstants.DefaultPageCount,
      bool deleted = false,
      string? title = null
    )
  {
    var entities = await dataLocationQueries.ReadByRepresentativeId(
      representativeId,
      pageNumber,
      cancellationToken,
      pageSize,
      deleted,
      title
    );

    var models = entities
      .Items
      .Select(modelEntityConverter.ToModel<LocationModel>)
      .ToPaginatedList(entities.TotalCount);

    return models;
  }

  public async Task<LocationModel?> ReadIndirectByRepresentativeIdAndId(
    string representativeId,
    RoleModel role,
    string locationId,
    CancellationToken cancellationToken,
    bool deleted = false
  )
  {
    var entity = await dataLocationQueries.ReadIndirectByRepresentativeIdAndId(
      representativeId,
      role.ToEntity(),
      locationId,
      cancellationToken,
      deleted
    );

    var model = entity is null
      ? null
      : modelEntityConverter.ToModel<LocationModel>(entity);

    return model;
  }

  public async Task<PaginatedList<LocationModel>>
    ReadIndirectByRepresentativeId(
      string representativeId,
      RoleModel role,
      int pageNumber,
      CancellationToken cancellationToken,
      int pageSize = QueryConstants.DefaultPageCount,
      bool deleted = false,
      string? title = null
    )
  {
    var entities = await dataLocationQueries.ReadIndirectByRepresentativeId(
      representativeId,
      role.ToEntity(),
      pageNumber,
      cancellationToken,
      pageSize,
      deleted,
      title
    );

    var models = entities
      .Items
      .Select(modelEntityConverter.ToModel<LocationModel>)
      .ToPaginatedList(entities.TotalCount);

    return models;
  }

  public async Task<List<LocationModel>> ReadAllIndirectByRepresentativeId(
    string representativeId,
    RoleModel role,
    CancellationToken cancellationToken
  )
  {
    var entities = await dataLocationQueries.ReadAllIndirectByRepresentativeId(
      representativeId,
      role.ToEntity(),
      cancellationToken
    );

    var models = entities
      .Select(modelEntityConverter.ToModel<LocationModel>)
      .ToList();

    return models;
  }
}

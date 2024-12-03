using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;
using Ozds.Business.Queries.Abstractions;
using DataLocationQueries = Ozds.Data.Queries.LocationQueries;

namespace Ozds.Business.Queries;

public class LocationQueries(
  DataLocationQueries dataLocationQueries,
  AgnosticModelEntityConverter modelEntityConverter
)
{
  public async Task<LocationModel?> ReadLocationByRepresentativeId(
    string representativeId,
    RoleModel role,
    string locationId,
    CancellationToken cancellationToken,
    bool deleted = false
  )
  {
    var entity = await dataLocationQueries.ReadLocationByRepresentativeId(
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

  public async Task<PaginatedList<LocationModel>> ReadLocationsByRepresentativeId(
    string representativeId,
    RoleModel role,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageSize = QueryConstants.DefaultPageCount,
    bool deleted = false
  )
  {
    var entities = await dataLocationQueries.ReadLocationsByRepresentativeId(
      representativeId,
      role.ToEntity(),
      pageNumber,
      cancellationToken,
      pageSize,
      deleted
    );

    var models = entities
      .Items
      .Select(modelEntityConverter.ToModel<LocationModel>)
      .ToPaginatedList(entities.TotalCount);

    return models;
  }
}

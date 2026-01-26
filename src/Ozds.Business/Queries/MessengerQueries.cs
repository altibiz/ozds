using Ozds.Business.Conversion;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries.Abstractions;
using DataMessengerQueries = Ozds.Data.Queries.MessengerQueries;

namespace Ozds.Business.Queries;

public class MessengerQueries(
  DataMessengerQueries queries,
  ModelEntityConverter modelEntityConverter
) : IQueries
{
  public async Task<PaginatedList<IMessenger>> ReadByLocationId(
    string locationId,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount,
    bool deleted = false,
    string? title = null
  )
  {
    var entities = await queries.ReadByLocationId(
      locationId,
      pageNumber,
      cancellationToken,
      pageCount,
      deleted,
      title
    );

    var models = entities.Items
      .Select(modelEntityConverter.ToModel<IMessenger>)
      .ToPaginatedList(entities.TotalCount);

    return models;
  }

  public async Task<IMessenger?> ReadByMeterId(
    string meterId,
    CancellationToken cancellationToken
  )
  {
    var entity = await queries.ReadByMeterId(
      meterId,
      cancellationToken
    );

    var model = entity is null
      ? null
      : modelEntityConverter.ToModel<IMessenger>(entity);

    return model;
  }

  public async Task<List<IMessenger?>> ReadByMeterIdsOrdered(
    IEnumerable<string> meterIds,
    CancellationToken cancellationToken
  )
  {
    var entities = await queries.ReadByMeterIdsOrdered(
      meterIds,
      cancellationToken
    );

    var models = entities
      .Select(entity => entity is null
        ? null
        : modelEntityConverter.ToModel<IMessenger>(entity))
      .ToList();

    return models;
  }
}

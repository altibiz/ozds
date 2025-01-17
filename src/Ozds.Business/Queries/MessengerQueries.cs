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
    int pageCount = QueryConstants.DefaultPageCount
  )
  {
    var entities = await queries.ReadByLocationId(
      locationId,
      pageNumber,
      cancellationToken,
      pageCount
    );

    var models = entities.Items
      .Select(modelEntityConverter.ToModel<IMessenger>)
      .ToPaginatedList(entities.TotalCount);

    return models;
  }
}

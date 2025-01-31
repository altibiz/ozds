using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Queries.Abstractions;
using MessagingNetworkUserInvoiceStateQueries =
  Ozds.Messaging.Queries.NetworkUserInvoiceStateQueries;

namespace Ozds.Business.Queries;

public class NetworkUserInvoiceStateQueries(
  MessagingNetworkUserInvoiceStateQueries queries,
  ModelEntityConverter modelEntityConverter
) : IQueries
{
  public async Task<NetworkUserInvoiceStateModel?> ReadAsync(
    string networkUserInvoiceId,
    CancellationToken cancellationToken
  )
  {
    var entity = await queries.ReadAsync(
      networkUserInvoiceId,
      cancellationToken
    );

    return entity is null
      ? default
      : modelEntityConverter.ToModel<NetworkUserInvoiceStateModel>(entity);
  }
}

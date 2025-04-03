using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Composite;
using Ozds.Business.Queries;
using Ozds.Client.Components.Models.Base;

namespace Ozds.Client.Pages;

public partial class NetworkUserInvoicePage
  : OzdsIdentifiableModelPageComponentBase<INetworkUserInvoice>
{
  public async Task<StatefulNetworkUserInvoiceModel?> OnLoadAsync()
  {
    var readonlyQueries = ScopedServices
      .GetRequiredService<ReadonlyQueries>();

    var stateQueries = ScopedServices
      .GetRequiredService<NetworkUserInvoiceStateQueries>();

    var invoice = await readonlyQueries.ReadSingle<NetworkUserInvoiceModel>(
      Id,
      CancellationToken
    );
    if (invoice is null)
    {
      return default;
    }

    var state = await stateQueries.ReadAsync(
      Id,
      CancellationToken
    );

    return new StatefulNetworkUserInvoiceModel
    {
      Invoice = invoice,
      State = state
    };
  }
}

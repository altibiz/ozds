using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models.Composite;
using Ozds.Business.Queries.Abstractions;
using DataBillingQueries = Ozds.Data.Queries.BillingQueries;

// TODO: location invoices

namespace Ozds.Business.Queries;

public class BillingQueries(
  DataBillingQueries queries,
  AgnosticModelEntityConverter modelEntityConverter
) : IQueries
{
  public async Task<NetworkUserInvoiceIssuingBasisModel>
    IssuingBasisForNetworkUser(
      string networkUserId,
      DateTimeOffset fromDate,
      DateTimeOffset toDate,
      CancellationToken cancellationToken
    )
  {
    var entity = await queries.ReadIssuingBasisForNetworkUser(
      networkUserId,
      fromDate,
      toDate,
      cancellationToken
    );

    return modelEntityConverter
      .ToModel<NetworkUserInvoiceIssuingBasisModel>(entity);
  }
}

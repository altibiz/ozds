using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Composite;
using Ozds.Data.Queries.Abstractions;
using DataCalculatedInvoiceQueries = Ozds.Data.Queries.CalculatedInvoiceQueries;

namespace Ozds.Business.Queries;

public class CalculatedInvoiceQueries(
  DataCalculatedInvoiceQueries queries,
  ModelEntityConverter modelEntityConverter
) : IQueries
{
  public async Task<CalculatedNetworkUserInvoiceModel?>
    ReadCalculatedNetworkUserInvoice(
      string id,
      CancellationToken cancellationToken
    )
  {
    var entity = await queries.ReadCalculatedNetworkUserInvoice(
      id,
      cancellationToken
    );

    return entity is null ? default :
      new CalculatedNetworkUserInvoiceModel
      {
        Calculations = entity.Calculations
          .Select(modelEntityConverter.ToModel<NetworkUserCalculationModel>)
          .ToList(),
        Invoice = modelEntityConverter.ToModel<NetworkUserInvoiceModel>(
          entity.Invoice
        )
      };
  }
}

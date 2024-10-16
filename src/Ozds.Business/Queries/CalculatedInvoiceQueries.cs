using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Composite;
using Ozds.Data.Queries.Abstractions;
using DataCalculatedInvoiceQueries = Ozds.Data.Queries.CalculatedInvoiceQueries;

// TODO: location invoices

namespace Ozds.Business.Queries;

public class CalculatedInvoiceQueries(
  DataCalculatedInvoiceQueries queries,
  AgnosticModelEntityConverter modelEntityConverter
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
      new CalculatedNetworkUserInvoiceModel(
        entity.Calculations
          .Select(modelEntityConverter.ToModel<NetworkUserCalculationModel>)
          .ToList(),
        modelEntityConverter.ToModel<NetworkUserInvoiceModel>(entity.Invoice)
      );
  }
}

using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Composite;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Composite;
using DataCalculatedInvoiceMutations = Ozds.Data.Mutations.CalculatedInvoiceMutations;

namespace Ozds.Business.Mutations;

public class CalculatedInvoiceMutations(
  DataCalculatedInvoiceMutations mutations,
  ModelEntityConverter modelEntityConverter
) : IMutations
{
  public async Task<CalculatedNetworkUserInvoiceModel> CreateCalculatedInvoice(
    CalculatedNetworkUserInvoiceModel invoice,
    CancellationToken cancellationToken)
  {
    var entity = modelEntityConverter
      .ToEntity<CalculatedNetworkUserInvoiceEntity>(invoice);
    await mutations.CreateCalculatedInvoice(
      entity,
      cancellationToken
    );
    var model = modelEntityConverter
      .ToModel<CalculatedNetworkUserInvoiceModel>(entity);

    return model;
  }
}

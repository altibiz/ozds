using Ozds.Business.Conversion.Agnostic;
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
  AgnosticModelEntityConverter modelEntityConverter
) : IMutations
{
  public async Task<CalculatedNetworkUserInvoiceModel> CreateCalculatedInvoice(
    CalculatedNetworkUserInvoiceModel invoice,
    CancellationToken cancellationToken)
  {
    var entity = new CalculatedNetworkUserInvoiceEntity(
      invoice.Calculations
        .Select(modelEntityConverter.ToEntity<NetworkUserCalculationEntity>)
        .ToList(),
      modelEntityConverter.ToEntity<NetworkUserInvoiceEntity>(invoice.Invoice)
    );
    await mutations.CreateCalculatedInvoice(
      entity,
      cancellationToken
    );
    var model = new CalculatedNetworkUserInvoiceModel(
      entity.Calculations
        .Select(modelEntityConverter.ToModel<NetworkUserCalculationModel>)
        .ToList(),
      modelEntityConverter.ToModel<NetworkUserInvoiceModel>(entity.Invoice)
    );

    return model;
  }
}

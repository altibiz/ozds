using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Composite;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Business.Queries;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Composite;
using DataCalculatedInvoiceMutations =
  Ozds.Data.Mutations.CalculatedInvoiceMutations;

namespace Ozds.Business.Mutations;

public class CalculatedInvoiceMutations(
  DataCalculatedInvoiceMutations mutations,
  ModelEntityConverter modelEntityConverter,
  RepresentativeQueries representativeQueries
) : IMutations
{
  public async Task<CalculatedNetworkUserInvoiceModel> CreateCalculatedInvoice(
    CalculatedNetworkUserInvoiceModel invoice,
    CancellationToken cancellationToken)
  {
    var representativeId = await representativeQueries
      .ReadAuthenticatedRepresentativeId(cancellationToken);

    var entity = new CalculatedNetworkUserInvoiceEntity
    {
      Calculations = invoice.Calculations
        .Select(modelEntityConverter.ToEntity<NetworkUserCalculationEntity>)
        .ToList(),
      Invoice = modelEntityConverter
        .ToEntity<NetworkUserInvoiceEntity>(invoice.Invoice)
    };

    foreach (var calculation in entity.Calculations)
    {
      calculation.RepresentativeId = representativeId;
    }

    entity.Invoice.RepresentativeId = representativeId;

    await mutations.CreateCalculatedInvoice(
      entity,
      cancellationToken
    );

    var model = new CalculatedNetworkUserInvoiceModel
    {
      Invoice = modelEntityConverter.ToModel<NetworkUserInvoiceModel>(
        entity.Invoice
      ),
      Calculations = entity.Calculations
        .Select(modelEntityConverter.ToModel<NetworkUserCalculationModel>)
        .ToList()
    };

    return model;
  }
}

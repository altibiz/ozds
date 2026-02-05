using Ozds.Business.Conversion;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Composite;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Document.Entities;
using DocumentDocumentQueries = Ozds.Document.Queries.DocumentQueries;

namespace Ozds.Business.Mutations;

public class DocumentMutations(
  DocumentDocumentQueries documentQueries,
  ModelDocumentEntityConverter converter
) : IMutations
{
  public async Task<string?> CreateHtmlForNetworkUserInvoice(
    CalculatedNetworkUserInvoiceModel model,
    CancellationToken cancellationToken
  )
  {
    var entity = new CalculatedNetworkUserInvoiceEntity
    {
      Calculations = model
        .Calculations.Select(converter.ToEntity<NetworkUserCalculationEntity>)
        .ToList(),
      Invoice = converter.ToEntity<NetworkUserInvoiceEntity>(model.Invoice),
    };

    var html = await documentQueries.ReadHtmlForNetworkUserInvoice(
      entity,
      cancellationToken
    );
    return html;
  }

#pragma warning disable SA1011 // Closing square brackets should be spaced correctly
  public async Task<byte[]?> CreatePdfForNetworkUserInvoice(
    CalculatedNetworkUserInvoiceModel model,
    CancellationToken cancellationToken
  )
#pragma warning restore SA1011 // Closing square brackets should be spaced correctly
  {
    var entity = new CalculatedNetworkUserInvoiceEntity
    {
      Calculations = model
        .Calculations.Select(converter.ToEntity<NetworkUserCalculationEntity>)
        .ToList(),
      Invoice = converter.ToEntity<NetworkUserInvoiceEntity>(model.Invoice),
    };

    var pdf = await documentQueries.ReadPdfForNetworkUserInvoice(
      entity,
      cancellationToken
    );
    return pdf;
  }

  public async Task<string?> CreateHtmlForNetworkUserCalculation(
    NetworkUserCalculationModel model,
    CancellationToken cancellationToken
  )
  {
    var entity = converter.ToEntity<NetworkUserCalculationEntity>(model);

    var html = await documentQueries.ReadHtmlForNetworkUserCalculation(
      entity,
      cancellationToken
    );
    return html;
  }

#pragma warning disable SA1011 // Closing square brackets should be spaced correctly
  public async Task<byte[]?> CreatePdfForNetworkUserCalculation(
    NetworkUserCalculationModel model,
    CancellationToken cancellationToken
  )
#pragma warning restore SA1011 // Closing square brackets should be spaced correctly
  {
    var entity = converter.ToEntity<NetworkUserCalculationEntity>(model);

    var pdf = await documentQueries.ReadPdfForNetworkUserCalculation(
      entity,
      cancellationToken
    );
    return pdf;
  }
}
